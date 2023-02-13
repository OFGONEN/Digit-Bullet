/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;
using UnityEditor;

public class NumberDisplayer : MonoBehaviour
{
#region Fields
  [ Title( "Shared" ) ]
    [ SerializeField ] PoolDigitDisplayer pool_digit_displayer;
    [ SerializeField ] NumberDisplayLibrary library_number_display;

  [ Title( "Setup" ) ]
    [ SerializeField ] Transform display_child;

    [ ShowInInspector, ReadOnly ] List< int > digit_list = new List< int >( 6 );
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    public void UpdateVisual( int value )
    {
		value.ExtractDigits( digit_list );

		float offset = 0;

		for( var i = 0; i < digit_list.Count; i++ )
        {
			var number     = pool_digit_displayer.GetEntity();
			var numberData = library_number_display.GetNumberDisplayData( digit_list[ i ] );

			number.transform.parent        = display_child;
			number.transform.localPosition = Vector3.right * offset + Vector3.up * GameSettings.Instance.number_spawn_height;

			offset = numberData.size / 2f + numberData.offset;

			number.UpdateVisual( numberData.mesh, GameSettings.Instance.number_material_positive );
		}

		display_child.localPosition = Vector3.right * offset / 2f;
	}

    public void UpdateVisual( OperatorSymbol symbol, int value )
    {
		value.ExtractDigits( digit_list );
    }
	#endregion

	#region Implementation
	#endregion

	#region Editor Only
#if UNITY_EDITOR
	[ Button() ]
	public void BakeVisual( int value )
	{
		UnityEditor.SceneManagement.EditorSceneManager.MarkAllScenesDirty();

		value.ExtractDigits( digit_list );
		float offset = 1;

		for( var i = 0; i < digit_list.Count; i++ )
		{
            var number =( PrefabUtility.InstantiatePrefab( 
                AssetDatabase.LoadAssetAtPath< GameObject >( "Assets/Prefab/character.prefab" ) ) as GameObject ).GetComponent< DigitDisplayer >();
			var numberData = library_number_display.GetNumberDisplayData( digit_list[ i ] );

			number.transform.parent        = display_child;
			number.transform.localPosition = Vector3.right * offset + Vector3.up * GameSettings.Instance.number_spawn_height;

			offset = numberData.size / 2f + numberData.offset;

			number.UpdateVisual( numberData.mesh, GameSettings.Instance.number_material_positive );
		}

		display_child.localPosition = Vector3.right * offset / 2f;
	}
#endif
#endregion
}