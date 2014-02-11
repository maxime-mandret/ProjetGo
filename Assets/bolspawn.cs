using UnityEngine;
using System.Collections;

public class bolspawn : MonoBehaviour
{
	public float waitTime;
	// Use this for initialization
	void Start()
	{
		gameObject.renderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update()
	{
		if(waitTime > 0)
		{
			waitTime -= Time.deltaTime;
		} else
		{
			gameObject.renderer.enabled = true;
			rigidbody.useGravity = true;
		}
	}
}
