using UnityEngine;
using System.Collections;

public class MenuClick : MonoBehaviour
{
	public Font thefont;
	// Use this for initialization
	void Start()
	{
		displayMainMenu();
	}	
	
	// Update is called once per frame
	void Update()
	{
		
	}

	public void displayNewGameMenu()
	{
		clean();
		//createButton ("jvj", "Joueur vs Joueur", 0);
		//createButton ("jvia", "Joueur vs IA", 1);
		createButton("iavia", "IA vs IA", 2);
		createButton("retour", "Retour", 3);
	}

	private void clean()
	{
		GameObject[] gs = GameObject.FindGameObjectsWithTag("button");
		foreach(GameObject g in gs)
		{
			Destroy(g);
		}
	}

	public void displayMainMenu()
	{
		clean();
		createButton("newgame", "Nouvelle partie", 0);
		createButton("loadgame", "Charger une partie", 1);
		createButton("exit", "Quitter", 2);
	}

	public void jvjGame()
	{
		PlayerPrefs.SetInt("gameMode", 0);
		PlayerPrefs.Save();
		Application.LoadLevel("Goban");
	}

	public void iaviaGame()
	{
		PlayerPrefs.SetInt("gameMode", 2);
		PlayerPrefs.Save();
		Application.LoadLevel("Goban");
	}

	void createButton(string name, string message, int position)
	{
		GameObject menu1 = new GameObject(name);
		menu1.AddComponent(typeof(Button));
		GUIText gui = (GUIText)menu1.AddComponent(typeof(GUIText));
		gui.font = thefont;
		gui.color = Color.grey;
		gui.text = message;
		gui.anchor = TextAnchor.MiddleCenter;
		gui.alignment = TextAlignment.Center;
		gui.fontSize = 38;
		menu1.tag = "button";
		menu1.transform.position = new Vector3(0.5f, 0.7f - (0.2f * position), 1f);
	}
}
