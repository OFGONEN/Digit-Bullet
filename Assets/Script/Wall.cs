/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

public class Wall : MonoBehaviour
{
#region Fields
  [ Title( "Setup" ) ]
    [ SerializeField ] CustomNormal _customNormal;
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    public void OnCollision( Collision collision )
    {
        var actorNumber = collision.collider.GetComponent< ComponentHost >().HostComponent as ActorNumber;
		actorNumber.DoRicochet( _customNormal.GetNormal() );
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}