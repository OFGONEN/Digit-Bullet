/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using FFStudio;
using DG.Tweening;
using Sirenix.OdinInspector;

public class ActorNumber : MonoBehaviour, IActorNumber
{
#region Fields
  [ Title( "Shared" ) ]
	[ SerializeField ] PoolActorNumber pool_number_actor;

  [ Title( "Components" ) ]
	[ SerializeField ] Rigidbody _rigidbody;
	[ SerializeField ] NumberDisplayer _numberDisplayer;

	Vector3 movement_target_direction;
	Vector3 movement_target_position;
	CustomNormal movement_target;
	int movement_ricochet_count;

	[ ShowInInspector, ReadOnly ] int number_value;
	int layerMask;
	float size;

	RecycledTween recycledTween = new RecycledTween();

	UnityMessage onFixedUpdate;
	UnityMessage onDisappear;
#endregion

#region Properties
    public int NumberValue => number_value;
#endregion

#region Unity API
	private void OnDisable()
	{
		EmptyDelegates();
		recycledTween.Kill();
	}

    void Awake()
    {
		layerMask = 1 << GameSettings.Instance.trajectory_layer;

		EmptyDelegates();
	}

	private void FixedUpdate()
	{
		onFixedUpdate();
	}
#endregion

#region API
// IActorNumber start
	public void Add( int value )
	{
		number_value += value;
		_numberDisplayer.UpdateVisual( number_value, GameSettings.Instance.number_material_positive );
	}

	public void Substract( int value )
	{
		number_value -= value;
		_numberDisplayer.UpdateVisual( number_value, GameSettings.Instance.number_material_positive );
	}

	public void Multiply( int value )
	{
		number_value *= value;
		_numberDisplayer.UpdateVisual( number_value, GameSettings.Instance.number_material_positive );
	}

	public void Divide( int value )
	{
		number_value /= value;
		_numberDisplayer.UpdateVisual( number_value, GameSettings.Instance.number_material_positive );
	}

	public void OnTargetNumberTrigger()
	{
		pool_number_actor.ReturnEntity( this );
		onDisappear?.Invoke();
	}

	public void OnSafetyNetTrigger() 
	{
		pool_number_actor.ReturnEntity( this );
		onDisappear?.Invoke();
	}

	public void DoPunchScale()
	{
		_numberDisplayer.transform.localScale = Vector3.one ;
		recycledTween.Recycle( GameSettings.Instance.actor_scale_punch.CreateTween( _numberDisplayer.transform ) );
	}

	public void DoShakeScale()
	{
		_numberDisplayer.transform.localScale = Vector3.one ;
		recycledTween.Recycle( GameSettings.Instance.actor_scale_shake.CreateTween( _numberDisplayer.transform ) );
	}
// IActorNumber end
	public void Spawn( Vector3 position, float size, int value, Material material )
	{
		gameObject.SetActive( true );

		transform.position   = position;
		transform.localScale = Vector3.one * size;
		this.size            = size;

		number_value = value;

		_numberDisplayer.UpdateVisual( number_value, material );
	}

	public void StartMovement( Vector3 direction, UnityMessage onDisappear = null )
	{
		movement_target_direction = direction;
		movement_ricochet_count   = 1;

		this.onDisappear = onDisappear;

		FindMovementTargetPosition();
	}

	public void JumpBig( Vector3 position, UnityMessage onComplete )
	{
		recycledTween.Recycle( transform.DOJump(
			position,
			GameSettings.Instance.actor_jump_big_power,
			1,
			GameSettings.Instance.actor_jump_big_duration )
			.SetEase( GameSettings.Instance.actor_jump_big_ease ),
			() => { onComplete(); OnJumpBigComplete(); }
		);
	}

	public void JumpSmall( Vector3 position )
	{
		recycledTween.Recycle( transform.DOJump(
			position,
			GameSettings.Instance.actor_jump_small_power,
			1,
			GameSettings.Instance.actor_jump_small_duration )
			.SetEase( GameSettings.Instance.actor_jump_small_ease )
		);
	}
#endregion

#region Implementation
	void OnJumpBigComplete()
	{
		transform.localScale = Vector3.one;
		DoPunchScale();

		_numberDisplayer.ChangeMaterial( GameSettings.Instance.number_material_positive );
	}

	void OnMovementFree()
	{
		var position     = _rigidbody.position;
		var nextPosition = Vector3.MoveTowards( position, movement_target_position, Time.deltaTime * GameSettings.Instance.actor_movement_speed );

		_rigidbody.MovePosition( nextPosition );
	}

	void OnMovementTarget()
	{
		var position = _rigidbody.position;
		var nextPosition = Vector3.MoveTowards( position, movement_target_position, Time.deltaTime * GameSettings.Instance.actor_movement_speed );

		_rigidbody.MovePosition( nextPosition );

		if( nextPosition == position )
		{
			movement_target_direction = Vector3.Reflect( 
				movement_target_direction, 
				movement_target.GetNormal( movement_target_position ) );

			if( movement_ricochet_count < CurrentLevelData.Instance.levelData.number_ricochet_count )
			{
				FindMovementTargetPosition();
				movement_ricochet_count++;
			}
			else
			{
				EmptyDelegates();
				pool_number_actor.ReturnEntity( this );
				onDisappear?.Invoke();
			}
		}
	}

	void FindMovementTargetPosition()
	{
		RaycastHit raycastHit;
		var position = transform.position;

		var isHit = Physics.Raycast( position, movement_target_direction, out raycastHit,
			GameSettings.Instance.actor_movement_delta_max,
			layerMask );

		if( isHit )
		{
			movement_target_position = raycastHit.point;
			movement_target          = raycastHit.collider.GetComponent< CustomNormal >();
			onFixedUpdate            = OnMovementTarget;
		}
		else
		{
			movement_target_position = position + movement_target_direction * GameSettings.Instance.actor_movement_delta_max;
			onFixedUpdate                 = OnMovementFree;
		}
	}

	void EmptyDelegates()
	{
		onFixedUpdate = Extensions.EmptyMethod;
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
	private void OnDrawGizmos()
	{
		if( !Application.isPlaying ) return;

		Handles.DrawLine( transform.position, movement_target_position );
	}
#endif
#endregion
}