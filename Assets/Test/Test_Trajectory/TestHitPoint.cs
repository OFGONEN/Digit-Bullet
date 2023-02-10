/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using FFStudio;
using Sirenix.OdinInspector;

public class TestHitPoint : MonoBehaviour
{
#region Fields
    [ SerializeField ] Transform target;

    ICustomNormal _customNormal;
#endregion

#region Properties
#endregion

#region Unity API
    private void Awake()
    {
        _customNormal = target.GetComponent< ICustomNormal >();
    }
#endregion

#region API
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if( !Application.isPlaying ) return;

		var normal = _customNormal.GetNormal( transform.position );
		Handles.DrawLine( transform.position, target.position );
		Handles.Label( transform.position, "Normal: " + normal );
	}
#endif
#endregion
}