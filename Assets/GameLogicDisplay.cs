using Assets.GameLogic;
using Assets.ObjetsDeJeu;
using UnityEngine;
using System.Collections;
using Assets.GameUtils;

enum GameTypes
{
	JcJ,
	JcIA,
	IAcIA
}

public class GameLogicDisplay : MonoBehaviour
{
	// Use this for initialization
	public float casesEcart = 0.17f;
    public Vector3 initPos;
    public GameObject uneCase;
    public Game Game { get; set; }
    public float coolTime;
    private float downTime;
    public bool updateLock;
	private UnityUiMananger ui;
	private int _width = 9;
	private int _height = 9;

	void Start()
	{
		if (initPos == null) {
				initPos = new Vector3 (0.7040288f, 0.6923118f, 1.020664f);
		}
		this.ui = new UnityUiMananger ();
		GameObject initialPet = GameObject.Find("TheGoban");
		//INIT THE GRID
		//TODO remplacer 9 avec Goban.length
		for(int i = 0; i < _width; i++)
		{
			for(int j = 0; j < _height; j++)
			{
				var pos = new Vector3((initPos.x - (casesEcart * i)), (initPos.y - (casesEcart * j)), initPos.z);
				var unecase = (GameObject)Object.Instantiate(uneCase, initialPet.transform.position, Quaternion.identity);
				unecase.name = "inter_" + i + "_" + j;
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

//				DEBUG on liste le nombre d'intersections par groupe
//				foreach(Groupe gr in Game.Goban.Groupes)
//				{
//					Debug.Log(gr.Count);
//				}
//				Debug.Log("--------");
			}
		}

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit))
		{
			if(hit.collider.gameObject.tag == "case" && hit.collider.transform.childCount > 0)
			{
				ui.DisplayToolTip (hit.collider.name);
			}else
			{
				if(ui.IsToolTipDisplayed())
				{
					ui.HideToolTip();
				}
			}
		}

		if(Game.Status == "over" && !GameObject.Find("GameOver").guiText.enabled)
		{
			GameObject.Find("GameOver").guiText.enabled = true;
			GameObject.Find("lescore").guiText.text = string.Format("Score blanc : {0} - Score noir : {1}",this.Game.WhiteScore,this.Game.BlackScore);
			GameObject.Find("lescore").guiText.enabled = true;
		}

		if(Game.Status == "calculated")
		{
			//GameObject.Find("GameOver").guiText.enabled = true;
			Debug.Log(string.Format("score blanc : {0} score noir : {1}",Game.WhiteScore,Game.BlackScore));
		}


	}
	public void UpdateReal()
	{
		updateLock = true;
	}
}
