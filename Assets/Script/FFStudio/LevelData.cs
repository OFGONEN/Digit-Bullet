﻿/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;
using System.IO;
using System.Collections;

namespace FFStudio
{
	[ CreateAssetMenu( fileName = "LevelData", menuName = "FF/Data/LevelData" ) ]
	public class LevelData : ScriptableObject
    {
	  [ Title( "Setup" ) ]
		[ ValueDropdown( "SceneList" ), LabelText( "Scene Index" ) ] public int scene_index;
        [ LabelText( "Override As Active Scene" ) ] public bool scene_overrideAsActiveScene;

	  [ Title( "Level Releated" ) ]
	  	[ LabelText( "Aim Trajectory Contact Point Count" ) ] public int trajectory_point_count;
	  	[ LabelText( "Number Shoot Array" ) ] public int[] number_array;
	  	[ LabelText( "Number Shoot Array Spawn Offset" ) ] public Vector3 number_array_spawn_offset;
	  	[ LabelText( "Number Shoot Array Offset" ) ] public Vector3 number_array_offset;
	  	[ LabelText( "Number Shoot Array Size" ) ] public int number_array_size;
	  	[ LabelText( "Number Shoot Scale" ) ] public float number_scale; // Current Number to shoot
	  	[ LabelText( "Number Shoot Array Scale" ) ] public float number_array_scale; // Number waiting to be shot

#if UNITY_EDITOR
		static IEnumerable SceneList()
        {
			var list = new ValueDropdownList< int >();

			var scene_count = SceneManager.sceneCountInBuildSettings;

			for( var i = 0; i < scene_count; i++ )
				list.Add( Path.GetFileNameWithoutExtension( SceneUtility.GetScenePathByBuildIndex( i ) ) + $" ({i})", i );

			return list;
		}
#endif
    }
}
