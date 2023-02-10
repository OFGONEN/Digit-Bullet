/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using FFStudio;
using Sirenix.OdinInspector;

public class CustomNormalSquare : MonoBehaviour, ICustomNormal
{
#region Fields
    [ LabelText( "Custom Pivot Point" ), SerializeField ] Vector3 pivot;

    Vector3 pivot_world;
#endregion

#region Properties
#endregion

#region Unity API
    private void Awake()
    {
		pivot_world = transform.TransformPoint( pivot );
	}
#endregion

#region API
	public Vector3 GetNormal( Vector3 contactPoint )
    {
		Vector3 normal = Vector3.up;
		if( contactPoint.y >= pivot_world.y + 0.5f )
		{
			normal = Vector3.up;
		}
		else if( contactPoint.y <= pivot_world.y - 0.5f )
		{
			normal = Vector3.down;
		}
		else
		{
			if( contactPoint.x >= pivot_world.x + 0.5f )
				normal = Vector3.right;
			else
				normal = Vector3.left;
		}

		return normal;
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
		Handles.DrawWireCube( transform.TransformPoint( pivot ), 0.1f * Vector3.one );
	}
#endif
#endregion
}