using UnityEngine;
using System.Collections;

public class PlayerLogic : MonoBehaviour
{
	public GameObject lepion;
	// Use this for initialization
	void Start()
	{
	}
	
	// Update is called once per frame
	void Update()
	{
		//On vérifie si on peut jouer
//		if (Input.GetMouseButtonDown (0)) {
//			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//			RaycastHit hit;
//			if (Physics.Raycast(ray, out hit))
//			{
//				if(hit.collider.tag == "case")
//				{
//					//TODO Vérifier si on peut jouer
//					GameObject pion = GameObject.Instantiate(lepion,hit.collider.transform.position+Vector3.up,Quaternion.identity) as GameObject;
//					pion.name = "pion";
//				}
//			}
//		}
	}
}
