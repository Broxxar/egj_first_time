using UnityEngine;
using System.Collections;

public class CameraShaker : MonoBehaviour
{
	private static CameraShaker _instance; 

	public static CameraShaker Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = GameObject.FindObjectOfType<CameraShaker>();
			}
			return _instance;
		}
		
		private set { }
	}

	public float ShakeMagnitude;
	Vector3 originalPosition;

	public void Awake ()
	{
		originalPosition = transform.position;
	}

	public void Shake ()
	{
		StartCoroutine(ShakeAsync());
	}

	IEnumerator ShakeAsync ()
	{
		float t = 0;

		while (t < .25f)
		{
			t += Time.deltaTime;
			transform.position = originalPosition + (Vector3)Random.insideUnitCircle * ShakeMagnitude;
			yield return null;
		}

		transform.position = originalPosition;
	}
}
