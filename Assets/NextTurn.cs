using UnityEngine;
using System.Collections;

public class NextTurn : MonoBehaviour
{

	// Use this for initialization
	void Start()
	{
	
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}

	void OnMouseDown()
	{
		GameObject.Find("Game Logic").GetComponent<GameLogic>().Game.Update();
	}
}
