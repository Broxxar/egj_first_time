using UnityEngine;
using System.Collections;

public class Moveable : MonoBehaviour
{
	public Vector3 StartPostion;
	public Vector3 EndPosition;
	public AnimationCurve XPositionCurve;
	public AnimationCurve YPositionCurve;
	public bool UseCurves;
	public float RoundTripTime;

	void Update ()
	{
		float cosTime = Mathf.Cos((Time.time * Mathf.PI * 2) / RoundTripTime + Mathf.PI);
		cosTime = (1 + cosTime)/2;

		if (UseCurves)
		{
			transform.position = new Vector3(XPositionCurve.Evaluate(cosTime), YPositionCurve.Evaluate(cosTime), 0);
		}
		else
		{
			transform.position = Vector3.Lerp(StartPostion, EndPosition, cosTime);
		}
	}

	public void SetStartPosition ()
	{
		StartPostion = transform.position;
		Keyframe keyx = new Keyframe(0, StartPostion.x);
		XPositionCurve.MoveKey(0, keyx);
		Keyframe keyy = new Keyframe(0, StartPostion.y);
		YPositionCurve.MoveKey(0, keyy);
	}

	public void SetEndPosition ()
	{
		EndPosition = transform.position;
		Keyframe keyx = new Keyframe(1, EndPosition.x);
		XPositionCurve.MoveKey(XPositionCurve.keys.Length - 1, keyx);
		Keyframe keyy = new Keyframe(1, EndPosition.y);
		YPositionCurve.MoveKey(YPositionCurve.keys.Length - 1, keyy);
	}
	
	public void ShowStartPosition ()
	{
		transform.position = StartPostion;
	}
	
	public void ShowEndPosition ()
	{
		transform.position = EndPosition;
	}
}
