using UnityEngine;
using System.Collections;

public class Node : MonoBehaviour
{
	bool _dragging;
	LineHelper _lineHelper;

	void Awake ()
	{
		GetComponent<Clickable>().DownAction += OnDownAction;
		InputManager.Instance.GlobalUpAction += OnGlobalUpAction;
	}

	void OnGlobalUpAction (Vector3 position)
	{
	}

	void OnDownAction (Vector3 position)
	{
	}

	void Update ()
	{
	
	}
}
