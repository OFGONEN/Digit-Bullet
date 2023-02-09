/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

public class TargetNumber : MonoBehaviour
{
#region Fields
  [ Title( "Setup" ) ]
    [ SerializeField ] float target_number;

  [ Title( "Shared" ) ]
    // [ SerializeField ] GameEvent event_target_number_enable;
    // [ SerializeField ] GameEvent event_target_number_disable;

    float target_number_current;
#endregion

#region Properties
#endregion

#region Unity API
    void OnEnable()
    {
		// event_target_number_enable.Raise();
	}

    void OnDisable()
    {
		// event_target_number_disable.Raise();
    }
#endregion

#region API
    public void OnTrigger( Collider collider )
    {
        var actorNumber = collider.GetComponent< ComponentHost >().HostComponent as ActorNumber;

		target_number_current -= actorNumber.NumberValue;

		actorNumber.OnTargetNumberTrigger();

		if( target_number_current <= 0 )
			Disappear();
	}
#endregion

#region Implementation
    void Disappear()
    {
		gameObject.SetActive( false );
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}