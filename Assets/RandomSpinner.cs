using UnityEngine;
using System.Collections;

public class RandomSpinner : MonoBehaviour
{
	public float MinSpeed;
	public float MaxSpeed;

	float _rotationSpeed;

	void Start ()
	{
		transform.Rotate(Vector3.back * Random.Range(-180, 180));
		_rotationSpeed = Random.Range (MinSpeed, MaxSpeed);
	}

	void Update ()
	{
		transform.Rotate(Vector3.back * _rotationSpeed * Time.deltaTime);
	}
}
