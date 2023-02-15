/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

public class AimTrajectory : MonoBehaviour
{
#region Fields
  [ Title( "Shared" ) ]
    [ SerializeField ] SharedVector2 shared_finger_position;
    [ SerializeField ] SharedReferenceNotifier notif_camera_reference;

  [ Title( "Components" ) ]
    [ SerializeField ] LineRenderer _lineRenderer;

    Camera _camera;
	int layerMask;
	Transform parent;
	[ ShowInInspector, ReadOnly ] List< Vector3 > point_list;
	[ ShowInInspector, ReadOnly ] int point_count;

	Vector3 finger_position;
	UnityMessage onUpdate;
#endregion

#region Properties
	public Vector3 AimDirection => ( finger_position - parent.position ).normalized ;
#endregion

#region Unity API
    private void Awake()
    {
		EmptyDelegates();

		parent                      = transform.parent;
		point_count                 = CurrentLevelData.Instance.levelData.trajectory_point_count + 1;  // Start and End points
		layerMask                   = 1 << GameSettings.Instance.trajectory_layer;
		point_list                  = new List< Vector3 >( point_count );
		_lineRenderer.positionCount = point_count;
	}

    private void Start()
    {
        _camera = ( notif_camera_reference.sharedValue as Transform ).GetComponent< Camera >();
	}

    private void Update()
    {
		onUpdate();
	}
#endregion

#region API
    public void StartAim()
    {
		point_list.Clear();
		_lineRenderer.enabled = true;

		onUpdate = OnAim;
	}

    public void StopAim()
    {
		onUpdate = Extensions.EmptyMethod;
		_lineRenderer.enabled = false;
	}
#endregion

#region Implementation
    void OnAim()
    {
		finger_position = _camera.ScreenToWorldPoint( shared_finger_position.sharedValue
			.ConvertToVector3_Z( Mathf.Abs( _camera.transform.position.z ) ) );

		parent.right = AimDirection;

		point_list.Clear();
		point_list.Add( transform.position );

		var castOrigin    = parent.position;
		var castDirection = ( finger_position - castOrigin ).normalized;

		RaycastHit hit;
		bool isHit;
		int hitCount = 0;

		isHit = Physics.Raycast( castOrigin, castDirection, out hit,
            GameSettings.Instance.trajectory_line_length,
			layerMask );

		while( isHit && hitCount < CurrentLevelData.Instance.levelData.trajectory_point_count )
		{
			var hitPoint = hit.point;
			point_list.Add( hitPoint );

			hitCount++;
			castOrigin = hitPoint;

			var normal       = hit.collider.GetComponent< ICustomNormal >().GetNormal( hitPoint );
			    castDirection = Vector3.Reflect( castDirection, normal );

			isHit = Physics.Raycast( castOrigin, castDirection, out hit,
	            GameSettings.Instance.trajectory_line_length,
	            layerMask );
        }

        if( !isHit )
		    point_list.Add( point_list.GetLastItem() + castDirection * GameSettings.Instance.trajectory_line_length );

		SetLineRendererPoints();
	}

    void SetLineRendererPoints()
    {
		var lastIndex = point_list.Count - 1;
		for( var i = 0; i < point_count; i++ )
			_lineRenderer.SetPosition( i, point_list[ Mathf.Min( i, lastIndex ) ] );
	}

    void EmptyDelegates()
    {
		onUpdate = Extensions.EmptyMethod;
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}