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
    [ SerializeField ] GameEvent event_level_failed;

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
	
	public void OnLevelFinished()
	{
		EmptyDelegates();
		_aimTrajectory.StopAim();
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
			CurrentLevelData.Instance.levelData.number_array[ 0 ] );

		number_spawn_index = 1;

		for( var i = 0; 
			i < CurrentLevelData.Instance.levelData.number_array_size && 
			number_spawn_index < CurrentLevelData.Instance.levelData.number_array.Length; 
			i++ )
		{
			var number = pool_number_actor.GetEntity();

			number.Spawn( position + CurrentLevelData.Instance.levelData.number_array_spawn_offset + CurrentLevelData.Instance.levelData.number_array_offset * i,
			CurrentLevelData.Instance.levelData.number_array_scale,
			CurrentLevelData.Instance.levelData.number_array[ number_spawn_index ] );

			number_spawn_index++;

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
		_aimTrajectory.StopAim();
		EmptyDelegates();

		ShootCurrentNumber();
	}

	void ShootCurrentNumber()
	{
		if( number_list.Count > 0 )
		{
			number_current.StartMovement( _aimTrajectory.AimDirection );

			for( var i = number_list.Count - 1; i > 0; i-- )
				number_list[ i ].JumpSmall( number_list[ i - 1 ].transform.position );

			number_list[ 0 ].JumpBig( position, OnJumpBigComplete );
		}
		else
			number_current.StartMovement( _aimTrajectory.AimDirection, event_level_failed.Raise );
	}

	void OnJumpBigComplete()
	{
		onFingerDown = StartAim;

		number_current = number_list[ 0 ];
		number_list.RemoveAt( 0 );

		if( number_spawn_index < CurrentLevelData.Instance.levelData.number_array.Length )
		{
			var number = pool_number_actor.GetEntity();
			number_list.Add( number );

			number.Spawn( position + CurrentLevelData.Instance.levelData.number_array_spawn_offset + CurrentLevelData.Instance.levelData.number_array_offset * ( number_list.Count - 1 ),
				CurrentLevelData.Instance.levelData.number_array_scale,
				CurrentLevelData.Instance.levelData.number_array[ number_spawn_index ] );

			number_spawn_index++;
		}
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