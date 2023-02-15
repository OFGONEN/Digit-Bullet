/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;

namespace FFStudio
{
    public class LevelManager : MonoBehaviour
    {
#region Fields
        [ Header( "Fired Events" ) ]
        public GameEvent levelFailedEvent;
        public GameEvent levelCompleted;

        [ Header( "Level Releated" ) ]
        public SharedProgressNotifier notifier_progress;

// Private
        [ ShowInInspector, ReadOnly ] int target_number_count;
#endregion

#region UnityAPI
#endregion

#region API
        // Info: Called from Editor.
        public void LevelLoadedResponse()
        {
			var levelData = CurrentLevelData.Instance.levelData;
            // Set Active Scene.
			if( levelData.scene_overrideAsActiveScene )
				SceneManager.SetActiveScene( SceneManager.GetSceneAt( 1 ) );
            else
				SceneManager.SetActiveScene( SceneManager.GetSceneAt( 0 ) );
		}

        // Info: Called from Editor.
        public void LevelRevealedResponse()
        {
        }

        // Info: Called from Editor.
        public void LevelStartedResponse()
        {

        }

        public void LevelFailedResponse()
        {
			target_number_count = 0;
		}

        public void LevelResetResponse()
        {
			target_number_count = 0;
		}

        public void OnTargetNumberAppear()
        {
			target_number_count++;
		}

		public void OnTargetNumberDisappear()
		{
			target_number_count--;

            if( target_number_count == 0)
            {
				levelCompleted.Raise();
				target_number_count = 0;
			}
		}
#endregion

#region Implementation
#endregion
    }
}