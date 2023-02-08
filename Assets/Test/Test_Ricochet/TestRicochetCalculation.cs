/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using FFStudio;

public class TestRicochetCalculation : MonoBehaviour
{
#region Fields
    public Rigidbody _rigidbody;
    public Vector3 movement_direction;
    public float movement_speed;

    float speed = 0;
#endregion

#region Properties
	private void Start()
	{
		transform.forward = movement_direction;
	}
#endregion

#region Unity API
    private void FixedUpdate()
    {
		_rigidbody.MovePosition( transform.position + movement_direction * Time.fixedDeltaTime * speed );
	}
#endregion

#region API
    [ Button() ]
    public void StartToMove()
    {
		speed = movement_speed;
	}

	private void OnCollisionEnter( Collision collision )
	{
		var point  = collision.contacts[ 0 ].point;
		var target = collision.gameObject.transform;

		movement_direction = Vector3.Reflect( movement_direction, target.forward );
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}