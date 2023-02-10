/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using FFStudio;

public class CustomNormalSphere : MonoBehaviour, ICustomNormal
{
#region Fields
    [ SerializeField ] Vector3 pivot;

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
		return ( contactPoint - pivot_world ).normalized;
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