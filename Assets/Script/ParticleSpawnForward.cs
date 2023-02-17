/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;

[ CreateAssetMenu( fileName = "event_pfx_forward_spawn", menuName = "FF/Game/Particle Forward GameEvent" ) ]
public class ParticleSpawnForward : GameEvent
{
	public string particle_alias;
	public Vector3 particle_spawn_point;
	public float particle_spawn_size;
	[ HideInInspector ] public Transform particle_spawn_parent;
	public Vector3 particle_spawn_forward;

	public void Raise( string alias, Vector3 position, Vector3 forward, Transform parent = null, float size = 1f )
	{
		particle_alias         = alias;
		particle_spawn_size    = size;
		particle_spawn_point   = position;
		particle_spawn_parent  = parent;
		particle_spawn_forward = forward;

		Raise();
	}
}