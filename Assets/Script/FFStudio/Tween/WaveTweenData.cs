/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;

namespace FFStudio
{
	public class WaveTweenData : TweenData
	{
#region Fields
	[ Title( "Scale Tween" ) ]
    	[ BoxGroup( "Tween" ), PropertyOrder( int.MinValue ) ] public float wave_radius;
		[ BoxGroup( "Tween" ), PropertyOrder( int.MinValue ) ] public float wave_speed;

		float cofactor = 1f;
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
		public override Tween CreateTween( bool isReversed = false )
		{
			var targetPosition = Random.insideUnitCircle.ConvertToVector3() * wave_radius + Vector3.right * wave_radius * cofactor;

			recycledTween.Recycle( transform.DOLocalMove(
				targetPosition,
				wave_speed )
				.SetSpeedBased(), () => FFLogger.Log( "Complete" ) );

			cofactor *= -1f;

#if UNITY_EDITOR
			recycledTween.Tween.SetId( "_ff_wave_tween___" + description );
#endif
			return base.CreateTween();
		}
#endregion

#region Implementation
#endregion

#region EditorOnly
#if UNITY_EDITOR
#endif
#endregion
	}
}