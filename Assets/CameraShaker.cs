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

	public void Shake ()
	{
		StartCoroutine(ShakeAsync());
	}

	IEnumerator ShakeAsync ()
	{
		Vector3 oiriginalPosition = transform.position;
		float t = 0;

		while (t < .25f)
		{
			t += Time.deltaTime;
			transform.position = oiriginalPosition + (Vector3)Random.insideUnitCircle * ShakeMagnitude;
			yield return null;
		}

		transform.position = oiriginalPosition;
	}
}
