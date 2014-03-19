using Assets.Db;
using UnityEngine;

public class MenuClick : MonoBehaviour
{
	public Font thefont;
    private DbGobansDataContext dataContext;
	// Use this for initialization
	void Start()
	{
        this.dataContext = new DbGobansDataContext();
		displayMainMenu();
	}	
	
	// Update is called once per frame
	void Update()
	{
		
	}

	private void clean()
	{
		GameObject[] gs = GameObject.FindGameObjectsWithTag("button");
		foreach(GameObject g in gs)
		{
			Destroy(g);
		}
		this.dataContext.Dispose();
	}

	public void displayMainMenu()
	{
		clean();
		createButton ("multiplayer", "Multijoueur", 0f, 38);
        createButton("iavia", "IA vs IA", 1f, 38);
        createButton("exit", "Retour", 2f, 38);
	}
	public void createNewMultiplayerGameMenu()
	{
		PlayerPrefs.SetInt("gameMode", 1);
		PlayerPrefs.Save();
		Application.LoadLevel("Goban");
	}
	
	
	public void displayMultiplayerMenu()
	{
		clean();
        createButton("newMultigame", "Créer une partie", 0f, 38);
        createButton("joingame", "Rejoindre une partie", 1f, 38);
        createButton("retour", "Quitter", 2f, 38);
	}
	
	public void displayJoinMenu()
	{
		clean();
        var pendings = DbPartie.GetPendingGames(dataContext);
		int[] ids = new int[5];
		for(int  i = 0; i < 5 && pendings[i] != null; i++)
		{
            ids[i] = (int)pendings[i].IdPartie;
		}
		//La tu fais ta requete pour choper tes parties bla bla
		//à remplacer par les
		//TODO remplace ce tableau d'id par tes dbparties
		/*5 max max*/
		/*ids[0] = 8;
		ids[1] = 6;
		ids[2] = 7;
		ids[3] = 33;
		ids[4] = 77;*/
		createText("Liste des parties en attente d'adversaire : ",0.9f);
		int d = 0;
		foreach(int gameInfo in ids)
		{
			createButton("game","Partie "+gameInfo,(float)d*0.1f+0.4f,25,gameInfo);
			d++;	
		}
		
		createText("Cliquez sur une partie pour rejoindre une partie",0.1f);
        createButton("retour", "Quitter", 2, 38);
	}
	
	public void createText(string message,float heigth)
	{
		GameObject menu1 = new GameObject(name);
		GUIText gui = (GUIText)menu1.AddComponent(typeof(GUIText));
		menu1.transform.position = new Vector3(0.5f, heigth, 1f);
		gui.text = message;
		gui.anchor = TextAnchor.MiddleCenter;
		gui.alignment = TextAlignment.Center;
		gui.color = Color.white;
		gui.fontSize = 20;
	}

	public void iaviaGame()
	{
		PlayerPrefs.SetInt("gameMode", 2);
		PlayerPrefs.Save();
		Application.LoadLevel("Goban");
	}
	
	public void joinMultiGame(int idpartie)
	{
		PlayerPrefs.SetInt("gameMode", 0);
		PlayerPrefs.SetInt("idPartie", 2);
		PlayerPrefs.Save();
		Application.LoadLevel("Goban");
	}
	
	void createButton(string name, string message, float position,  int fontSize)
	{
		GameObject menu1 = new GameObject(name);
		Button b = (Button)menu1.AddComponent(typeof(Button));
		GUIText gui = (GUIText)menu1.AddComponent(typeof(GUIText));
		gui.font = thefont;
		gui.color = Color.grey;
		gui.text = message;
		gui.anchor = TextAnchor.MiddleCenter;
		gui.alignment = TextAlignment.Center;
		gui.fontSize = fontSize;
		menu1.tag = "button";
	}

	void createButton(string name, string message, float position,  int fontSize, int idpartie)
	{
		GameObject menu1 = new GameObject(name);
		Button b = (Button)menu1.AddComponent(typeof(Button));
		b.idPartie = idpartie;
		GUIText gui = (GUIText)menu1.AddComponent(typeof(GUIText));
		gui.font = thefont;
		gui.color = Color.grey;
		gui.text = message;
		gui.anchor = TextAnchor.MiddleCenter;
		gui.alignment = TextAlignment.Center;
		gui.fontSize = fontSize;
		menu1.tag = "button";
		if(idpartie != -1)
		{
			menu1.transform.position = new Vector3(0.5f, position, 1f);
		}else
		{
			menu1.transform.position = new Vector3(0.5f, 0.7f - (0.2f * position), 1f);
		}
	}
}
