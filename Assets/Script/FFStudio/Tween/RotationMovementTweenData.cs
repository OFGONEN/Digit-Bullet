/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace FFStudio
{
	public class RotationMovementTweenData : TweenData
	{
#region Fields
        public float movement_radius; 
        public float movement_speed; 
        public float movement_angle;
        public float movement_angle_target;
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
		public override Tween CreateTween( bool isReversed = false )
		{
			recycledTween.Recycle( DOTween.To(
				GetAngle,
				SetAngle,
				movement_angle_target,
				movement_speed )
				.SetSpeedBased()
				.OnUpdate( OnUpdate )
			);

#if UNITY_EDITOR
			recycledTween.Tween.SetId( "_ff_rotationMovement_tween___" + description );
#endif
			return base.CreateTween();
		}
#endregion

#region Implementation
		void OnUpdate()
		{
			transform.localPosition = movement_angle.ReturnNormalizedVector() * movement_radius;
		}

    	float GetAngle()
    	{
			return movement_angle % movement_angle_target;
		}

		void SetAngle( float angle )
		{
			movement_angle = angle;
		}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
	}
}