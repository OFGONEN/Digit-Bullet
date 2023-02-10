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
    [ LabelText( "Custom Size" ), SerializeField ] Vector3 size;

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
		if( contactPoint.y >= pivot_world.y + size.y / 2f )
		{
			normal = Vector3.up;
		}
		else if( contactPoint.y <= pivot_world.y - size.y / 2f )
		{
			normal = Vector3.down;
		}
		else
		{
			if( contactPoint.x >= pivot_world.x + size.x / 2f )
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
		var pivot = transform.TransformPoint( this.pivot );
		Handles.Label( pivot, "Custom Square: " + pivot );
		Handles.DrawWireCube( pivot, size );
	}
#endif
#endregion
}