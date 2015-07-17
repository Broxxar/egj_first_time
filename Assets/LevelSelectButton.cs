using UnityEngine;
using System.Collections;
using System;

public class LevelSelectButton : MonoBehaviour {

	public int LevelNumber;
	public float RollOverScale;
	public float RollOverSmoothingFactor;

	Vector3 _initialScale;

	void Awake ()
	{
		GetComponent<Clickable>().DownAction += OnDownAction;
		_initialScale = transform.localScale;
	}

	void OnDownAction (Vector3 position)
	{
		InputManager.Instance.enabled = false;
		Fader.Instance.FadeInBlack(LoadLevel);
	}

	void LoadLevel ()
	{
		Application.LoadLevel("level" + LevelNumber.ToString());
	}
	
	void Update ()
	{
		if (Physics2D.OverlapPoint(InputManager.Instance.MouseWorldPosition) == GetComponent<Collider2D>() && InputManager.Instance.enabled)
		{
			transform.localScale = Vector3.Lerp(transform.localScale, _initialScale * RollOverScale, Time.deltaTime * RollOverSmoothingFactor);
		}
		else
		{
			transform.localScale = Vector3.Lerp(transform.localScale, _initialScale, Time.deltaTime * RollOverSmoothingFactor);
		}
	}
}
