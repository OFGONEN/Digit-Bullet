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
    [ ShowInInspector, ReadOnly ] List< DigitDisplayer > digit_displayer_list = new List< DigitDisplayer >( 6 );
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    public void UpdateVisual( int value, Material material )
    {
        for( var i = 0; i < digit_displayer_list.Count; i++ )
			digit_displayer_list[ i ].ReturnToPool();

		digit_displayer_list.Clear();

		value.ExtractDigits( digit_list );
		float offset = 0;

		DigitDisplayer number = null;

		for( var i = 0; i < digit_list.Count; i++ )
		{
			    number     = pool_digit_displayer.GetEntity();
			var numberData = library_number_display.GetNumberDisplayData( digit_list[ i ] );

			number.transform.parent        = display_child;
			number.transform.localPosition = Vector3.right * offset + Vector3.up * GameSettings.Instance.number_spawn_height;
			number.transform.localScale    = Vector3.one;

			offset += numberData.size + numberData.offset;

			number.UpdateVisual( numberData.mesh, material );
			digit_displayer_list.Add( number );
		}

		display_child.localPosition = Vector3.left * number.transform.localPosition.x / 2f;	
    }


	public void ChangeMaterial( Material material )
	{
		var rendererArray = display_child.GetComponentsInChildren< MeshRenderer >();

		for( var i = 0; i < rendererArray.Length; i++ )
			rendererArray[ i ].sharedMaterial = material;
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
	[ Button() ]
	public void BakeVisualActor( int value )
	{
		UnityEditor.SceneManagement.EditorSceneManager.MarkAllScenesDirty();

		display_child.DestroyAllChildren();

		value.ExtractDigits( digit_list );
		float offset = 0;

		DigitDisplayer number = null;

		for( var i = 0; i < digit_list.Count; i++ )
		{
            number = ( PrefabUtility.InstantiatePrefab( 
                AssetDatabase.LoadAssetAtPath< GameObject >( "Assets/Prefab/character.prefab" ) ) as GameObject ).GetComponent< DigitDisplayer >();
			var numberData = library_number_display.GetNumberDisplayData( digit_list[ i ] );

			number.transform.parent        = display_child;
			number.transform.localPosition = Vector3.right * offset + Vector3.up * GameSettings.Instance.number_spawn_height;

			offset += numberData.size + numberData.offset;

			number.UpdateVisual( numberData.mesh, GameSettings.Instance.number_material_positive );
		}

		display_child.localPosition = Vector3.left * number.transform.localPosition.x / 2f;
	}

	[ Button() ]
	public void BakeVisualSymbol( int value )
	{
		UnityEditor.SceneManagement.EditorSceneManager.MarkAllScenesDirty();

		display_child.DestroyAllChildren();

		value.ExtractDigits( digit_list );
		float offset = 0;

		DigitDisplayer number = null;

		for( var i = 0; i < digit_list.Count; i++ )
		{
			number = ( PrefabUtility.InstantiatePrefab(
				AssetDatabase.LoadAssetAtPath< GameObject >( "Assets/Prefab/character.prefab" ) ) as GameObject ).GetComponent<DigitDisplayer>();
			var numberData = library_number_display.GetNumberDisplayData( digit_list[ i ] );

			number.transform.parent = display_child;
			number.transform.localPosition = Vector3.right * offset + Vector3.up * GameSettings.Instance.number_spawn_height;

			offset += numberData.size + numberData.offset;

			number.UpdateVisual( numberData.mesh, GameSettings.Instance.number_material_positive );
		}

		display_child.localPosition = Vector3.left * number.transform.localPosition.x / 2f;

		ChangeMaterial( GameSettings.Instance.number_operator_material_positive );
	}

	[ Button() ]
	public void BakeVisual( OperatorSymbol symbol, int value )
	{
		UnityEditor.SceneManagement.EditorSceneManager.MarkAllScenesDirty();

		display_child.DestroyAllChildren();

		value.ExtractDigits( digit_list );
		float offset = 0;


		var symbolDisplay = ( PrefabUtility.InstantiatePrefab(
			AssetDatabase.LoadAssetAtPath< GameObject >( "Assets/Prefab/character.prefab" ) ) as GameObject ).GetComponent< DigitDisplayer >();
		var symbolData = library_number_display.GetNumberOperatorDisplayData( symbol );

		symbolDisplay.transform.parent = display_child;
		symbolDisplay.transform.localPosition = Vector3.right * offset + Vector3.up * GameSettings.Instance.number_spawn_height;

		offset += symbolData.size + symbolData.offset;

		symbolDisplay.UpdateVisual( symbolData.mesh, GameSettings.Instance.number_material_positive );

		DigitDisplayer number = null;

		for( var i = 0; i < digit_list.Count; i++ )
		{
			number = ( PrefabUtility.InstantiatePrefab(
				AssetDatabase.LoadAssetAtPath< GameObject >( "Assets/Prefab/character.prefab" ) ) as GameObject ).GetComponent< DigitDisplayer >();
			var numberData = library_number_display.GetNumberDisplayData( digit_list[ i ] );

			number.transform.parent        = display_child;
			number.transform.localPosition = Vector3.right * offset + Vector3.up * GameSettings.Instance.number_spawn_height;

			offset += numberData.size + numberData.offset;

			number.UpdateVisual( numberData.mesh, GameSettings.Instance.number_material_positive );
		}

		display_child.localPosition = Vector3.left * number.transform.localPosition.x / 2f;
	}

	[ Button() ]
	public void ChangeMaterialEditor( Material material )
	{
		UnityEditor.SceneManagement.EditorSceneManager.MarkAllScenesDirty();

		var rendererArray = display_child.GetComponentsInChildren< MeshRenderer >();

		for( var i = 0; i < rendererArray.Length; i++ )
			rendererArray[ i ].sharedMaterial = material;
	}
#endif
#endregion
}