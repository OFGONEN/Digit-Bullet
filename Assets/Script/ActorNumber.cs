/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using FFStudio;
using Sirenix.OdinInspector;

public class ActorNumber : MonoBehaviour, ISafetyCollectable
{
#region Fields
  [ Title( "Shared" ) ]
	[ SerializeField ] PoolActorNumber pool_number_actor;

  [ Title( "Components" ) ]
	[ SerializeField ] Rigidbody _rigidbody;

	Vector3 movement_target_direction;
	Vector3 movement_target_position;
	ICustomNormal movement_target;
	int number_value;
	int layerMask;

	UnityMessage onFixedUpdate;
#endregion

#region Properties
    public int NumberValue => number_value;
#endregion

#region Unity API
	private void OnDisable()
	{
		EmptyDelegates();
	}

    void Awake()
    {
		layerMask = 1 << GameSettings.Instance.trajectory_layer;

		EmptyDelegates();
	}

	private void FixedUpdate()
	{
		onFixedUpdate();
	}
#endregion

#region API
	public void Spawn( Vector3 position, float size, int value )
	{
		gameObject.SetActive( true );
		number_value = value;
	}

	public void StartMovement( Vector3 direction )
	{
		movement_target_direction = direction;
		FindMovementTargetPosition();
	}

	public void OnTargetNumberTrigger()
	{
		pool_number_actor.ReturnEntity( this );
	}

	public void OnSafetyNetTrigger() 
	{
		pool_number_actor.ReturnEntity( this );
	}
#endregion

#region Implementation
	void OnMovementFree()
	{
		var position     = _rigidbody.position;
		var nextPosition = Vector3.MoveTowards( position, movement_target_position, Time.deltaTime * GameSettings.Instance.actor_movement_speed );

		_rigidbody.MovePosition( nextPosition );
	}

	void OnMovementTarget()
	{
		var position = _rigidbody.position;
		var nextPosition = Vector3.MoveTowards( position, movement_target_position, Time.deltaTime * GameSettings.Instance.actor_movement_speed );

		_rigidbody.MovePosition( nextPosition );

		if( nextPosition == position )
		{
			movement_target_direction = Vector3.Reflect( 
				movement_target_direction, 
				movement_target.GetNormal( movement_target_position ) );

			FindMovementTargetPosition();
		}
	}

	void FindMovementTargetPosition()
	{
		RaycastHit raycastHit;
		var position = transform.position;

		var isHit = Physics.Raycast( position, movement_target_direction, out raycastHit,
			GameSettings.Instance.actor_movement_delta_max,
			layerMask );

		if( isHit )
		{
			movement_target_position = raycastHit.point;
			movement_target          = raycastHit.collider.GetComponent< ICustomNormal >();
			onFixedUpdate                 = OnMovementTarget;
		}
		else
		{
			movement_target_position = position + movement_target_direction * GameSettings.Instance.actor_movement_delta_max;
			onFixedUpdate                 = OnMovementFree;
		}
	}

	void EmptyDelegates()
	{
		onFixedUpdate = Extensions.EmptyMethod;
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
	private void OnDrawGizmos()
	{
		if( !Application.isPlaying ) return;

		Handles.DrawLine( transform.position, movement_target_position );
	}
#endif
#endregion
}