using UnityEngine;
using System.Collections;
using System;

public class Timer : MonoBehaviour
{
	private static Timer _instance;

	public static Timer Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = GameObject.FindObjectOfType<Timer>();
			}
			return _instance;
		}
		
		private set { }
	}

	float _currentTime;

	public float CurrentTime
	{
		get
		{
			return _currentTime;
		}
		private set
		{
			_currentTime = value;
			TimeSpan span = TimeSpan.FromSeconds(value);
			_textMesh.text = string.Format("{0}:{1}:{2}", span.Minutes.ToString("D2"), span.Seconds.ToString("D2"), span.Milliseconds.ToString("D3"));
		}
	}

	TextMesh _textMesh;
	bool _started;

	void Awake ()
	{
		_textMesh = GetComponent<TextMesh>();
	}

	public void StartTimer (float startTime)
	{
		CurrentTime = startTime;
		_started = true;
	}

	public void StopTimer ()
	{
		_started = false;
	}

	void Update ()
	{
		if (_started)
		{
			CurrentTime += Time.deltaTime;
		}
	}
}
