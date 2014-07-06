using UnityEngine;
using System.Collections;

public class GridAnimation : MonoBehaviour
{
	public float RotationChangeInterval;
	public float RotationVelocityFactor;
	public float VelocitySmoothingFactor;

	Vector3 _rotationVelocity;
	Vector3 _targetRotationVelcoity;

	void Start ()
	{
		StartCoroutine(ChangeRotation());
		transform.rotation = Random.rotation;
	}

	void Update ()
	{
		float cosTime = Mathf.Cos(Time.time / (Mathf.PI * 2));
		Vector3 scale = new Vector3(1, 1, Mathf.Lerp(1, 4, (1 + cosTime)/2));
		transform.localScale = scale;

		_rotationVelocity = Vector3.Lerp(_rotationVelocity, _targetRotationVelcoity, Time.deltaTime * VelocitySmoothingFactor);
		transform.Rotate(_rotationVelocity * RotationVelocityFactor * Time.deltaTime);
	}

	IEnumerator ChangeRotation()
	{
		while (true)
		{
			_targetRotationVelcoity = Random.insideUnitSphere;
			yield return new WaitForSeconds(RotationChangeInterval);
		}
	}
}
