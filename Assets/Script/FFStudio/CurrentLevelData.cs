﻿/* Created by and for usage of FF Studios (2021). */

using UnityEngine;

namespace FFStudio
{
    public class CurrentLevelData : ScriptableObject
    {
#region Fields
		public int currentLevel_Real;
		public int currentLevel_Shown;
		public LevelData levelData;

        static CurrentLevelData instance;

        delegate CurrentLevelData ReturnCurrentLevel();
        static ReturnCurrentLevel returnInstance = LoadInstance;

        public static CurrentLevelData Instance => returnInstance();
#endregion

#region API
		public void LoadCurrentLevelData()
		{
			if( currentLevel_Real > GameSettings.Instance.maxLevelCount )
				currentLevel_Real = Random.Range( GameSettings.Instance.minLevelCount, GameSettings.Instance.maxLevelCount );

			levelData = Resources.Load< LevelData >( "level_data_" + currentLevel_Real );
		}
#endregion

#region Implementation
        static CurrentLevelData LoadInstance()
		{
			if( instance == null )
				instance = Resources.Load< CurrentLevelData >( "level_current" );

			returnInstance = ReturnInstance;

            return instance;
        }

        static CurrentLevelData ReturnInstance()
        {
            return instance;
        }
#endregion
    }
}