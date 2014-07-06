using UnityEngine;
using System.Collections;

public class BloomExplosion : MonoBehaviour
{
	private static BloomExplosion _instance;

	public static BloomExplosion Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = GameObject.FindObjectOfType<BloomExplosion>();
			}
			return _instance;
		}
		
		private set { }
	}

	Bloom _bloom;
	bool _exploding;

	void Awake ()
	{
		_bloom = GetComponent<Bloom>();
	}

	public void Explode ()
	{
		_exploding = true;
		_bloom.bloomIntensity = 7;
	}

	void Update ()
	{
		if (_exploding)
			_bloom.bloomIntensity = Mathf.Lerp(_bloom.bloomIntensity, 1, Time.deltaTime * 2);
	}
}
