/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TestAngleSphere : MonoBehaviour
{
#region Fields
    public Transform target_contact;
    public Transform target_movement;
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
		var movementDelta = target_movement.position - transform.position;
		var normal        = target_movement.position - target_contact.position;
		var newDirection  = Vector3.Reflect( movementDelta.normalized, normal.normalized );

		Handles.DrawLine( transform.position, target_movement.position );
		Handles.DrawLine( target_movement.position, target_movement.position + newDirection * 2 );
		Handles.Label( target_contact.position, "Target Contact" );
		Handles.Label( target_movement.position, "Target Movement" );
	}

#endif
#endregion
}