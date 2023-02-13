/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using FFStudio;
using Sirenix.OdinInspector;

public class TargetNumber : MonoBehaviour
{
#region Fields
  [ Title( "Setup" ) ]
    [ SerializeField ] int target_number;

  [ Title( "Shared" ) ]
    [ SerializeField ] GameEvent event_target_number_appear;
    [ SerializeField ] GameEvent event_target_number_disappear;

  [ Title( "Components" ) ]
	[ SerializeField ] NumberDisplayer _numberDisplayer;
#endregion

#region Properties
#endregion

#region Unity API
    void OnEnable()
    {
		event_target_number_appear.Raise();
	}

    void OnDisable()
    {
		event_target_number_disappear.Raise();
    }

	private void Start()
	{
		_numberDisplayer.UpdateVisual( target_number, GameSettings.Instance.number_target_material );
	}
#endregion

#region API
    public void OnTrigger( Collider collider )
    {
        var actorNumber = collider.GetComponent< ComponentHost >().HostComponent as ActorNumber;

		target_number -= actorNumber.NumberValue;

		actorNumber.OnTargetNumberTrigger();

		if( target_number <= 0 )
			Disappear();
		else
			_numberDisplayer.UpdateVisual( target_number, GameSettings.Instance.number_target_material );
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
	private void OnDrawGizmos()
	{
		Handles.Label( transform.position, "Target Number: " + target_number );
	}
#endif
#endregion
}