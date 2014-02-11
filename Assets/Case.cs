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

	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.name == "pion")
		{
			//Destroy(gameObject);
			other.transform.parent = this.transform;
			GameObject.Find("Game Logic").GetComponent<GameLogicDisplay>().UpdateReal();
		}
	}
}
