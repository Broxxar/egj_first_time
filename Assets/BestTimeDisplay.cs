using UnityEngine;
using System.Collections;
using System;

public class BestTimeDisplay : MonoBehaviour
{
	
	void Start ()
	{
		TextMesh textMesh = GetComponent<TextMesh>();
		float bestTime = PlayerPrefs.GetFloat("bestTime", 300);
		TimeSpan span = TimeSpan.FromSeconds(bestTime);
		textMesh.text = string.Format("{0}:{1}:{2}", span.Minutes.ToString("D2"), span.Seconds.ToString("D2"), span.Milliseconds.ToString("D3"));
	}
}
