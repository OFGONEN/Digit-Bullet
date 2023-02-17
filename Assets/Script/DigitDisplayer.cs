/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class DigitDisplayer : MonoBehaviour
{
#region Fields
  [ Title( "Shared" ) ]
    [ SerializeField ] PoolDigitDisplayer pool_digit_displayer;
    
  [ Title( "Components" ) ]
    [ SerializeField ] MeshFilter _meshFilter;
    [ SerializeField ] MeshRenderer _meshRenderer;
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    public void UpdateVisual( Mesh mesh, Material material )
    {
		gameObject.SetActive( true );
		_meshFilter.mesh       = mesh;
		_meshRenderer.material = material;
	}

	public void UpdateMaterial( Material material )
	{
		_meshRenderer.material = material;
	}

	public void ReturnToPool()
    {
		pool_digit_displayer.ReturnEntity( this );
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}