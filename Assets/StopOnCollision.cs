using UnityEngine;
using System.Collections;

public class StopOnCollision : MonoBehaviour
{
	public Color baseCouleur;
	// Use this for initialization
	void Start()
	{
	
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}

	public void Select()
	{
		this.renderer.material.color = Color.green;
	}

	public void Deselect()
	{
		this.renderer.material.color = this.baseCouleur;
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
