using UnityEngine;
using System.Collections;

public class Case : MonoBehaviour
{

	// Use this for initialization
	void Start()
	{
	
	}
	
	// Update is called once per frame
	void Update()
	{
		//TODO faire la séléction des cases
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.name == "pion")
		{
			Destroy(gameObject);
			GameObject.Find("Game Logic").GetComponent<GameLogic>().UpdateReal();
		}
	}
}
