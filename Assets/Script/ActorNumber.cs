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
  [ Title( "Setup" ) ]
    [ SerializeField ] float number_value;

  [ Title( "Components" ) ]
	[ SerializeField ] Rigidbody _rigidbody;

	Vector3 movement_target_direction;
	Vector3 movement_target_position;
	ICustomNormal movement_target;
	float number_value_current;
	int layerMask;

	UnityMessage onFixedUpdate;
#endregion

#region Properties
    public float NumberValue => number_value_current;
#endregion

#region Unity API
	private void OnDisable()
	{
		EmptyDelegates();
	}

    void Awake()
    {
		number_value_current = number_value;
		layerMask            = 1 << GameSettings.Instance.trajectory_layer;

		EmptyDelegates();
	}

	private void FixedUpdate()
	{
		onFixedUpdate();
	}
#endregion

#region API
	public void OnTargetNumberTrigger()
	{
		gameObject.SetActive( false );
	}

	[ Button() ]
	public void StartMovement( Vector3 direction )
	{
		movement_target_direction = direction;

		FindMovementTargetPosition();
	}

	public void OnSafetyNetTrigger() 
	{
		gameObject.SetActive( false );
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