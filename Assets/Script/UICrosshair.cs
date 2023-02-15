/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

public class UICrosshair : MonoBehaviour
{
#region Fields
  [ Title( "Components" ) ]
    [ SerializeField ] RectTransform crosshair_transform;

  [ Title( "Shared" ) ]
    [ SerializeField ] SharedVector2 shared_finger_position;

    UnityMessage onUpdate;
#endregion

#region Properties
#endregion

#region Unity API
    private void Awake()
    {
		onUpdate = Extensions.EmptyMethod;
	}
    private void Update()
    {
		onUpdate();
	}
#endregion

#region API
    public void OnFingerDown()
    {
		onUpdate = OnFingerUpdate;
		crosshair_transform.gameObject.SetActive( true );
	}

    public void OnFingerUp()
    {
		onUpdate = Extensions.EmptyMethod;
		crosshair_transform.gameObject.SetActive( false );
	}
#endregion

#region Implementation
    void OnFingerUpdate()
    {
		crosshair_transform.position = shared_finger_position.sharedValue;
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
