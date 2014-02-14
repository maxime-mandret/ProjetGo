using UnityEngine;
using System.Collections;

public class TitleIntro : MonoBehaviour
{
	// Use this for initialization
    public float _timeLeft;
    public float _fadeTime;
	public Font _dafont;

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
				var tepu = GameObject.Find("Game Logic").GetComponent<GameLogicDisplay>();
                tepu.Game.Update();
				Destroy(gameObject);
			}
		}
	}
}