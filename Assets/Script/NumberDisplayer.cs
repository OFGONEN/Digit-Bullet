/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

public class NumberDisplayer : MonoBehaviour
{
#region Fields
    [ SerializeField ] PoolDigitDisplayer pool_digit_displayer;
    [ SerializeField ] NumberDisplayLibrary library_number_display;

    [ ShowInInspector, ReadOnly ] List< int > digit_list = new List< int >( 6 );
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    [ Button() ]
    public void UpdateVisual( int value )
    {
		value.ExtractDigits( digit_list );
	}

    public void UpdateVisual( OperatorSymbol symbol, int value )
    {
    }
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
