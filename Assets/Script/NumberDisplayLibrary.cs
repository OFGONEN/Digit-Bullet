/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ CreateAssetMenu( fileName = "library_number_display", menuName = "FF/Game/Library Number Display" ) ]
public class NumberDisplayLibrary : ScriptableObject
{
#region Fields
    [ SerializeField ] NumberDisplayData[] number_display_data_array;
    [ SerializeField ] NumberDisplayData[] number_operator_display_data_array;
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    public NumberDisplayData GetNumberDisplayData( int value )
    {
		return number_display_data_array[ value ];
	}

    public NumberDisplayData GetNumberOperatorDisplayData( OperatorSymbol symbol )
    {
		return number_operator_display_data_array[ ( int )symbol ];
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}

[ System.Serializable ]
public class NumberDisplayData
{
	public Mesh mesh;
	public float size;
	public float offset;
}

public enum OperatorSymbol
{
    Add,
    Divide,
    Multiply,
    Subtract
}