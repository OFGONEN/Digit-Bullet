/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;

public class ParticleSpawnOnTrigger : MonoBehaviour
{
#region Fields
    [ SerializeField ] Collider _collider;
    [ SerializeField ] ParticleData _particleData;
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    public void OnTrigger( Collider collider )
    {
		var point = _collider.ClosestPoint( collider.transform.position );

		Transform parent = _particleData.parent ? transform : null;
		var       offset = _particleData.keepParentRotation ? transform.TransformVector( _particleData.offset ) : _particleData.offset;

		_particleData.particle_event.Raise( _particleData.alias, point + offset, parent, _particleData.size, _particleData.keepParentRotation );
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
