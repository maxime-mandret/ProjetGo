using UnityEngine;
using System.Collections;

public class stopOnCollision : MonoBehaviour
{

	// Use this for initialization
	void Start()
	{
	
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}


	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject == GameObject.Find("TheGoban"))
		{
			gameObject.rigidbody.Sleep();
			gameObject.rigidbody.isKinematic = true;
		}
	}
}
