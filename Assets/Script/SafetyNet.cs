/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;

public class SafetyNet : MonoBehaviour
{
#region Fields
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    public void OnTrigger( Collider collider )
    {
		( collider.GetComponent< ComponentHost >().HostComponent as ISafetyCollectable ).OnSafetyNetTrigger();
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}