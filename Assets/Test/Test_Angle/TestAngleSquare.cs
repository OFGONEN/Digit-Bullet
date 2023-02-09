/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TestAngleSquare : MonoBehaviour
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
		Vector3 normal = Vector3.up;
		if( target_movement.position.y >= target_contact.position.y + 0.5f )
        {
			normal = Vector3.up;
		}
		else if( target_movement.position.y <= target_contact.position.y - 0.5f )
        {
			normal = Vector3.down;
		}
        else
        {
            if( target_movement.position.x >= target_contact.position.x + 0.5f )
				normal = Vector3.right;
            else
				normal = Vector3.left;
		}

		var movementDelta = target_movement.position - transform.position;
		var newDirection  = Vector3.Reflect( movementDelta.normalized, normal );

		Handles.DrawLine( transform.position, target_movement.position );
		Handles.DrawLine( target_movement.position, target_movement.position + newDirection * 2 );
		Handles.Label( target_contact.position, "Target Contact" );
		Handles.Label( target_movement.position, "Target Movement" );
	}

#endif
#endregion
}