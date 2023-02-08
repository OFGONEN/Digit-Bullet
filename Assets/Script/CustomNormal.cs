/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;
using UnityEditor;

#if UNITY_EDITOR
using Shapes;
#endif

public class CustomNormal : MonoBehaviour, ICustomNormal
{
#region Fields
    [ SerializeField ] Vector3 normal;
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    public Vector3 GetNormal()
    {
		return normal;
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
    [ Button() ]
    public void LogTransformForward()
    {
        FFLogger.Log( "Forward: " + transform.forward );
    }

    private void OnDrawGizmos() 
    {
		Draw.UseDashes = true;
		Draw.DashStyle = DashStyle.RelativeDashes( DashType.Basic, 1, 1 );

		Draw.Line( transform.position, transform.position + normal, 0.1f, LineEndCap.None );
		Draw.Cone( transform.position + normal, normal, 0.1f, 0.2f );
	}
#endif
#endregion
}