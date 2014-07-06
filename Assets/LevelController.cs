using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour {

	private static LevelController _instance;
	
	public static LevelController Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = GameObject.FindObjectOfType<LevelController>();
			}
			return _instance;
		}
		
		private set { }
	}

	float _currentLevel = 0;
	float _currentTime = 0;

	void Awake ()
	{
		DontDestroyOnLoad(gameObject);
	}

	void Start ()
	{
		Timer.Instance.StartTimer(0);
	}

	public void NextLevel ()
	{
		if (_currentLevel == 7)
		{
			StartCoroutine(EndGame());
			return;
		}

		StartCoroutine(NextLevelAsync());
	}

	IEnumerator NextLevelAsync ()
	{
		BloomExplosion.Instance.Explode();
		Timer.Instance.StopTimer();
		_currentTime = Timer.Instance.CurrentTime;
		_currentLevel ++;
		InputManager.Instance.enabled = false;
		yield return new WaitForSeconds(0.5f);
		Fader.Instance.FadeInBlack(LoadLevel);
	}

	IEnumerator EndGame ()
	{
		BloomExplosion.Instance.Explode();
		Timer.Instance.StopTimer();
		_currentTime = Timer.Instance.CurrentTime;
		_currentLevel ++;
		InputManager.Instance.enabled = false;
		PlayerPrefs.SetFloat("bestTime", _currentTime);
		yield return new WaitForSeconds(0.5f);
		Fader.Instance.FadeInBlack(LoadMainScreen);
	}

	void OnLevelWasLoaded (int level)
	{
		Timer.Instance.StartTimer(_currentTime);
	}

	void LoadMainScreen ()
	{
		Application.LoadLevel("levelselect");
		Destroy(gameObject);
	}

	void LoadLevel ()
	{
		Application.LoadLevel("level"+_currentLevel.ToString());
	}
}
