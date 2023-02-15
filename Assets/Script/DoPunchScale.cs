/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

public class DoPunchScale : MonoBehaviour
{
#region Fields
    [ SerializeField ] PunchScaleTween punchScaleTween;
    [ SerializeField ] Transform target;

    Vector3 scale;
#endregion

#region Properties
#endregion

#region Unity API
    private void Awake()
    {
		scale = transform.localScale;
	}
#endregion

#region API
	[ Button() ]
    public void DoPunchScaleTween( Vector3 strength )
    {
		target.localScale = scale;
		punchScaleTween.CreateTween( target, strength );
	}

	[ Button() ]
	public void DoPunchScaleTween()
	{
		target.localScale = scale;
		punchScaleTween.CreateTween( target );
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}