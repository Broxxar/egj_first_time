using UnityEngine;
using System.Collections;

public class Moveable : MonoBehaviour
{
	public Vector3 StartPostion;
	public Vector3 EndPosition;

	void Update ()
	{
		float cosTime = Mathf.Cos(Time.time);

		transform.position = Vector3.Lerp(StartPostion, EndPosition, (1 + cosTime)/2);

	}

	public void SetStartPosition ()
	{
		StartPostion = transform.position;
	}
	
	public void SetEndPosition ()
	{
		EndPosition = transform.position;
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
