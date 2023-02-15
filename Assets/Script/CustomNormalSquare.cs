/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using FFStudio;
using Sirenix.OdinInspector;

public class CustomNormalSquare : CustomNormal
{
#region Fields
    [ LabelText( "Box Collider" ), SerializeField ] BoxCollider _boxCollider;
    [ ShowInInspector, ReadOnly ] Vector3 size;
#endregion

#region Properties
#endregion

#region Unity API
    private void Awake()
    {
		// size = Vector3.Scale( _boxCollider.size, _boxCollider.transform.localScale );
		size = _boxCollider.size;
	}
#endregion

#region API
	public override Vector3 GetNormal( Vector3 contactPoint )
    {
		Vector3 normal = Vector3.up;

		var localContactPoint = _boxCollider.transform.InverseTransformPoint( contactPoint ) * 1.05f;

		if( localContactPoint.y >= size.y / 2f )
			normal = _boxCollider.transform.up;
		else if( localContactPoint.y <= -size.y / 2f )
			normal = _boxCollider.transform.up * -1f;
		else
		{
			if( localContactPoint.x >= size.x / 2f )
				normal = _boxCollider.transform.right;
			else
				normal = _boxCollider.transform.right * -1f;
		}

		// Debug.DrawRay( contactPoint, normal, Color.red, 1f );
		return normal;
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}