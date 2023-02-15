/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;

public class ParticleSpawnOnContactPoint : MonoBehaviour
{
#region Fields
    [ SerializeField ] BoxCollider _boxCollider;
    [ SerializeField ] CustomNormalSquare customNormal;
    [ SerializeField ] ParticleDataForward _particleData;
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    public void OnTrigger( Collider collider )
    {
		var targetPosition = collider.transform.position;

		var normal = customNormal.GetNormal( targetPosition );
		var point  = _boxCollider.ClosestPoint( targetPosition );

		Transform parent = _particleData.parent ? transform : null;

		_particleData.particle_event.Raise( _particleData.alias, point, normal, parent, _particleData.size );
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
