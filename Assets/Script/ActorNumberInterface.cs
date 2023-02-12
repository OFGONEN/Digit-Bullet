/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;

[ CreateAssetMenu( fileName = "number_actor_interface", menuName = "FF/Game/Actor Number Interface" ) ]
public class ActorNumberInterface : ScriptableObject, IActorNumber
{
#region Fields
    ActorNumber number_current;
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    public void CacheNumber( Collider collider )
    {
        number_current = collider.GetComponent< ComponentHost >().HostComponent as ActorNumber;
    }

	public void Add( int value )
	{
		number_current.Add( value );
	}

	public void Substract( int value )
	{
		number_current.Substract( value );
	}

	public void Multiply( int value )
	{
		number_current.Multiply( value );
	}

	public void Divide( int value )
	{
		number_current.Divide( value );
	}

	public void OnSafetyNetTrigger()
	{
		number_current.OnSafetyNetTrigger();
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
