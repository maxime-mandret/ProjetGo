using UnityEngine;
using System.Collections;

public class Case : MonoBehaviour
{
	// Use this for initialization
	public Color ScoreColor;
	private bool dalleCreated = false;
	public GameObject dalleScore;
	void Start()
	{
		ScoreColor = Color.green;
	}
	
	// Update is called once per frame
	void Update()
	{
		if (ScoreColor != Color.green && !dalleCreated) {
			GameObject dalle = (GameObject)GameObject.Instantiate(dalleScore,transform.position,Quaternion.identity);
			dalle.renderer.material.color = ScoreColor;
			dalleCreated = true;
		}
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
