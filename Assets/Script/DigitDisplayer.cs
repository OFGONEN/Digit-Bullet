/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigitDisplayer : MonoBehaviour
{
#region Fields
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
		_meshFilter.mesh       = mesh;
		_meshRenderer.material = material;
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}