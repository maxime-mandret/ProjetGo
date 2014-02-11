using Assets.GameLogic;
using Assets.ObjetsDeJeu;
using UnityEngine;
using System.Collections;

enum GameTypes
{
	JcJ,
	JcIA,
	IAcIA
}

public class GameLogic : MonoBehaviour
{
	// Use this for initialization
	private float casesEcart = 0.17f;
    private Vector3 initPos;
    private GameObject uneCase;
    public Game Game { get; set; }
    private float coolTime = 1f;
	private float downTime;
	private bool updateLock;

	void Start()
	{

		GameObject initialPet = GameObject.Find("TheGoban");
		//INIT THE GRID
		//TODO remplacer 9 avec Goban.length
		for(int i = 0; i<9; i++)
		{
			for(int j = 0; j<9; j++)
			{
				Vector3 pos = new Vector3((initPos.x - (casesEcart * i)), (initPos.y - (casesEcart * j)), initPos.z);
				GameObject unecase = (GameObject)GameObject.Instantiate(uneCase, initialPet.transform.position, Quaternion.identity);
				unecase.name = "inter_" + i + "_" + j;
				unecase.AddComponent(typeof(Case));
				unecase.transform.parent = initialPet.transform;
				unecase.transform.localPosition = pos;
			}
		}
		if(PlayerPrefs.GetInt("gameMode") == 2)
		{
			RandomIaPlayer p1 = new RandomIaPlayer("Bob", PlayerColor.White);
			RandomIaPlayer p2 = new RandomIaPlayer("Johnny", PlayerColor.Black);
			GameObject.Find("NomBlanc").transform.FindChild("nom").guiText.text = p1.Name;
			GameObject.Find("NomNoir").transform.FindChild("nom").guiText.text = p2.Name;
			Game = new Game(9, p1, p2);

		}
		updateLock = false;
		downTime = coolTime;
	}
	
	// Update is called once per frame
	void Update()
	{
		if(updateLock)
		{
			downTime -= Time.deltaTime;
			if(downTime <= 0)
			{
				Game.Update();
				GameObject g = GameObject.Find("toursuivant");
				g.guiText.enabled = true;
				g.guiText.text = string.Format("Tour {0}", Game.NbTour);

				GameObject.Find("NomBlanc").transform.FindChild("abandons").guiText.text = "Abandons : " + Game.WhitePlayer.NbAbandonSuccessifs;
				GameObject.Find("NomNoir").transform.FindChild("abandons").guiText.text = "Abandons : " + Game.BlackPlayer.NbAbandonSuccessifs;
				updateLock = false;
				downTime = coolTime;

			}
		}

		if(Game.Status == "over")
		{
			GameObject.Find("GameOver").guiText.enabled = true;
		}

	}
	public void updateReal()
	{
		updateLock = true;
	}
}
