using UnityEngine;
using System.Collections;

public class LineHelper : MonoBehaviour {

	Transform _start;
	Transform _end;
	LineRenderer _lineRenderer;

	void Awake ()
	{
		_start = transform.FindChild("start");
		_end = transform.FindChild("end");
		_lineRenderer = GetComponent<LineRenderer>();
	}

	void Update ()
	{
		_lineRenderer.SetPosition(0, _start.position);
		_lineRenderer.SetPosition(1, _end.position);
	}
}
