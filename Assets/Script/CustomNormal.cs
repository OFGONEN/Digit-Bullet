/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CustomNormal : MonoBehaviour
{
#region Fields
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    public abstract Vector3 GetNormal( Vector3 point );
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
