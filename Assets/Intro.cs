using UnityEngine;
using System.Collections;

public class Intro : MonoBehaviour
{
	// Use this for initialization
	public float endCameraSecond;
	void Start()
	{

	}
	
	// Update is called once per frame
	void Update()
	{
		if(endCameraSecond > 0)
		{
			endCameraSecond -= Time.deltaTime;
		} else
		{
		}
	}


}
