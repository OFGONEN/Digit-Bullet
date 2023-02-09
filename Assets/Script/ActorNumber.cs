/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

public class ActorNumber : MonoBehaviour
{
#region Fields
  [ Title( "Setup" ) ]
    [ SerializeField ] float number_value;

    float number_value_current;
#endregion

#region Properties
    public float NumberValue => number_value_current;
#endregion

#region Unity API
    void Awake()
    {
		number_value_current = number_value;
	}
#endregion

#region API
	public void OnTargetNumberTrigger()
	{
		gameObject.SetActive( false );
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
