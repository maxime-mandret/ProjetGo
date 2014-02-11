using UnityEngine;
using System.Collections;

public class titleIntro : MonoBehaviour
{
	// Use this for initialization
    private float _timeLeft;
    private float _fadeTime;
	private Font _dafont;

	void Start()
	{
	}
	
	// Update is called once per frame
	void Update()
	{
		_timeLeft -= Time.deltaTime;
		if(_timeLeft <= 0.0f)
		{
			_fadeTime -= Time.deltaTime;
			if(_fadeTime > 0.0f)
			{
				guiText.color = new Color(guiText.color.r, guiText.color.g, guiText.color.b, (guiText.color.a - (Time.deltaTime)));
			} else
			{
				GameObject.Find("Game Logic").GetComponent<GameLogic>().Game.Update();

				Destroy(gameObject);
			}
		}
	}
}