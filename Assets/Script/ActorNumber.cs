/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

public class ActorNumber : MonoBehaviour
{
#region Fields
  [ Title( "Setup" ) ]
    [ SerializeField ] float number_value;

  [ Title( "Components" ) ]
	[ SerializeField ] Rigidbody _rigidbody;

	Vector3 movement_direction;
	float number_value_current;

	UnityMessage onFixedUpdate;
#endregion

#region Properties
    public float NumberValue => number_value_current;
#endregion

#region Unity API
    void Awake()
    {
		number_value_current = number_value;

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
		movement_direction = direction;

		onFixedUpdate = OnMovement;
	}

	public void DoRicochet( Vector3 normal )
	{
		movement_direction = Vector3.Reflect( movement_direction, normal );
	}
#endregion

#region Implementation
	void OnMovement()
	{
		var position       = _rigidbody.position;
		var targetPosition = _rigidbody.position + movement_direction * Time.fixedDeltaTime * GameSettings.Instance.actor_movement_speed;

		_rigidbody.MovePosition( targetPosition );
	}

	void EmptyDelegates()
	{
		onFixedUpdate = Extensions.EmptyMethod;
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}