using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour
{
	private Color baseColor;
	// Use this for initialization
	void Start()
	{
		baseColor = gameObject.guiText.color;
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}

	void OnMouseDown()
	{
		if(gameObject.name == "newgame")
		{
			//Application.LoadLevel ("Goban");
			Camera.main.GetComponent<MenuClick>().displayNewGameMenu();
			
		}

		if(gameObject.name == "retour")
		{
			//Application.LoadLevel ("Goban");
			Camera.main.GetComponent<MenuClick>().displayMainMenu();
			
		}

		if(gameObject.name == "jvj")
		{
			//Application.LoadLevel ("Goban");
			Camera.main.GetComponent<MenuClick>().jvjGame();
			
		}
		
		if(gameObject.name == "iavia")
		{
			//Application.LoadLevel ("Goban");
			Camera.main.GetComponent<MenuClick>().iaviaGame();
			
		}

		if(gameObject.name == "exit")
		{
			Application.Quit();
		}
	}

	void OnMouseOver()
	{
		gameObject.guiText.color = Color.white;
	}
	
	void OnMouseExit()
	{
		gameObject.guiText.color = baseColor;
	}
}
