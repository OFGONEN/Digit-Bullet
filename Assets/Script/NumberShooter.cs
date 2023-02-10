/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

public class NumberShooter : MonoBehaviour
{
#region Fields
  [ Title( "Shared" ) ]
    [ SerializeField ] PoolActorNumber pool_number_actor;

  [ Title( "Components" ) ]
    [ SerializeField ] AimTrajectory _aimTrajectory;

    ActorNumber number_current;
	int number_spawn_index = 0;

	Vector3 position;
	List< ActorNumber > number_list;

    UnityMessage onFingerDown;
    UnityMessage onFingerUp;
#endregion

#region Properties
#endregion

#region Unity API
    private void Awake()
    {
		EmptyDelegates();
		position = transform.position;
	}

    private void Start()
    {
		number_list = new List< ActorNumber >( CurrentLevelData.Instance.levelData.number_array_size );
		SpawnNumbers();
	}
#endregion

#region API
    public void OnLevelStart()
    {
		onFingerDown = StartAim;
	}

    public void OnFingerDown()
    {
		onFingerDown();
	}

    public void OnFingerUp()
    {
		onFingerUp();
	}
#endregion

#region Implementation
	void SpawnNumbers()
	{
		number_current = pool_number_actor.GetEntity();
		number_current.Spawn( position,
			CurrentLevelData.Instance.levelData.number_scale,
			CurrentLevelData.Instance.levelData.number_array[ number_spawn_index ] );

		number_spawn_index++;

		for( var i = number_spawn_index; i <= CurrentLevelData.Instance.levelData.number_array_size; i++ )
		{
			var number = pool_number_actor.GetEntity();

			number.Spawn( position + CurrentLevelData.Instance.levelData.number_array_spawn_offset + CurrentLevelData.Instance.levelData.number_array_offset * ( i - 1 ),
			CurrentLevelData.Instance.levelData.number_array_scale,
			CurrentLevelData.Instance.levelData.number_array[ number_spawn_index ] );

			number_list.Add( number );
		}
	}

    void StartAim()
    {
		_aimTrajectory.StartAim();

		EmptyDelegates();
		onFingerUp = StopAim;
	}

    void StopAim()
    {
		number_current.StartMovement( _aimTrajectory.AimDirection );
		_aimTrajectory.StopAim();

		EmptyDelegates();
	}

    void EmptyDelegates()
    {
		onFingerDown = Extensions.EmptyMethod;
		onFingerUp   = Extensions.EmptyMethod;
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}