/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;

public class NumberShooter : MonoBehaviour
{
#region Fields
    [ SerializeField ] PoolActorNumber pool_number_actor;

    ActorNumber number_current;
	int number_spawn_index = 0;

	Vector3 position;
#endregion

#region Properties
#endregion

#region Unity API
    private void Awake()
    {
		position = transform.position;
	}

    private void Start()
    {
		number_current = pool_number_actor.GetEntity();
		number_current.Spawn( position, 1, CurrentLevelData.Instance.levelData.number_array[ number_spawn_index ] );
	}
#endregion

#region API
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}