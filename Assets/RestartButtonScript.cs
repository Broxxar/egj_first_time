using UnityEngine;
using System.Collections;

public class RestartButtonScript : MonoBehaviour {

	public float RollOverScale;
	public float RollOverSmoothingFactor;
	
	Vector3 _initialScale;
	
	void Awake ()
	{
		GetComponent<Clickable>().DownAction += OnDownAction;
		float diameter = Camera.main.orthographicSize/9;
		this.transform.localScale = new Vector3(diameter, diameter, 1);

		_initialScale = transform.localScale;
	}
	
	void OnDownAction (Vector3 position)
	{
		InputManager.Instance.enabled = false;
		Fader.Instance.FadeInBlack(LoadLevel);
	}
	
	void LoadLevel ()
	{
		Application.LoadLevel("LevelSelect");
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
