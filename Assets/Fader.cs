using UnityEngine;
using System.Collections;
using System;

public class Fader : MonoBehaviour {

	private static Fader _instance;
	
	public static Fader Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = GameObject.FindObjectOfType<Fader>();
			}
			return _instance;
		}
		
		private set { }
	}

	Material _material;
	bool _fading;

	void Awake ()
	{
		_material = transform.FindChild("quad").GetComponent<Renderer>().sharedMaterial;
		_material.color = Color.black;
	}

	void Start ()
	{
		FadeOutBlack();
	}

	public void FadeInBlack (Action callback)
	{
		if (_fading)
			return;

		StartCoroutine(FadeInAsync(callback));
	}
	
	public void FadeOutBlack ()
	{
		if (_fading)
			return;
		StartCoroutine(FadeOutAsync());
	}

	IEnumerator FadeInAsync (Action callback)
	{
		_fading = true;
		_material.color = Color.clear;
		float t = 0;

		while (t < 1)
		{
			t = Mathf.Clamp(t + Time.deltaTime * 4, 0, 1);
			_material.color = new Color(0, 0, 0, t);
			yield return null;
		}
		_material.color = Color.black;
		_fading = false;
		callback.Invoke();
	}

	IEnumerator FadeOutAsync ()
	{
		_fading = true;
		_material.color = Color.black;
		float t = 1;

		while (t > 0)
		{
			t = Mathf.Clamp(t - Time.deltaTime * 4, 0, 1);
			_material.color = new Color(0, 0, 0, t);
			yield return null;
		}
		_material.color = Color.clear;
		_fading = false;
	}
}
