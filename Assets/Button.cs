using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour
{
	private Color baseColor;
	public int idPartie;
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
	
		if(this.gameObject.name == "game")
		{
			//Nouvelle partie solo;
			Camera.main.GetComponent<MenuClick>().joinMultiGame(this.idPartie);
			
		}
		
		if(this.gameObject.name == "multiplayer")
		{
			//On affiche le menu multi
			Camera.main.GetComponent<MenuClick>().displayMultiplayerMenu();
			
		}
		
		if(this.gameObject.name == "newMultigame")
		{
			//Rejoindr eune artie
			Camera.main.GetComponent<MenuClick>().createNewMultiplayerGameMenu();
			
		}
		
		if(this.gameObject.name == "joingame")
		{
			//Rejoindre partie
			Camera.main.GetComponent<MenuClick>().displayJoinMenu();
			
		}

		if(this.gameObject.name == "retour")
		{
			//Application.LoadLevel ("Goban");
			Camera.main.GetComponent<MenuClick>().displayMainMenu();
			
		}
		
		if(this.gameObject.name == "iavia")
		{
			//Application.LoadLevel ("Goban");
			Camera.main.GetComponent<MenuClick>().iaviaGame();
			
		}

		if(this.gameObject.name == "exit")
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
