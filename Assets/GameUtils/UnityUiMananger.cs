using Assets.ObjetsDeJeu;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.GameUtils
{
    public class UnityUiMananger : IUiManager
    {
        public void PoserPion(Player p, int x, int y)
        {
            var pion = Object.Instantiate(Camera.main.GetComponent<PlayerLogic>().lepion, GameObject.Find("inter_" + y + "_" + x).transform.position + Vector3.up, Quaternion.identity) as GameObject;
            if (pion != null)
            {
                pion.name = "pion";
                pion.renderer.material.color = p.Color == PlayerColor.Black ? Color.black : Color.white;
            }
        }
    }
}
