using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using Assets.ObjetsDeJeu;

namespace Assets.GameUtils.Sgf
{
	public class SgfHeader
	{
		public IList<Coordonnees> BlackTerritory;

		public IList<Coordonnees> WhiteTerritory;
		public int Size { get; set; }

		public SgfConst.GameType GameType { get; set; }

		public Player BlackPlayer { get; set; }

		public Player WhitePlayer { get; set; }

		public string GameName { get; set; }

		public string Commentary { get; set; }

		public string Place { get; set; }

		public DateTime EnventDate { get; set; }

		public string Event { get; set; }

		public int HadicapStoneNumber { get; set; }

		public int Komi { get; set; }


		public void BuildFromText(string headerText)
		{
			IDictionary<string, string> propDict = GetHeaderPropertiesDictionnaryFromString(headerText);
			foreach(KeyValuePair<string, string> keyValuePair in propDict)
			{
				BuildPropertyFromString(keyValuePair.Key, keyValuePair.Value);
			}
		}

		private static IDictionary<string, string> GetHeaderPropertiesDictionnaryFromString(string strHeader)
		{
			IDictionary<string, string> dict = new Dictionary<string, string>();
			foreach(Match match in Regex.Matches(strHeader, SgfConst.PropertiesValuePattern))
			{
				if(match.Success)
				{
					dict.Add(match.Groups[1].Value, match.Groups[2].Value);
				}
			}

			return dict;
		}

		protected virtual void BuildPropertyFromString(string prop, string value)
		{
			switch(prop)
			{
			case (SgfConst.SizeCode):
				Size = int.Parse(value);
				break;
			case (SgfConst.GameTypeCode):
				GameType = (SgfConst.GameType)int.Parse(value);
				break;
			case (SgfConst.EventTitleCode):
				Event = value;
				break;
			case (SgfConst.DateCode):
				EnventDate = DateTime.ParseExact(value, SgfConst.DateFormat, CultureInfo.InvariantCulture);
				break;
			case (SgfConst.PlaceCode):
				Place = value;
				break;
			case (SgfConst.BlackPlayerCode):
				BlackPlayer = new Player(value, PlayerColor.Black);
				break;
			case (SgfConst.WhitePlayerCode):
				WhitePlayer = new Player(value, PlayerColor.White);
				break;
			case (SgfConst.CommentaryCode):
				Commentary = value;
				break;
			case (SgfConst.GameNameCode):
				GameName = value;
				break;
			case (SgfConst.HandicapCode):
				HadicapStoneNumber = int.Parse(value);
				break;
			case (SgfConst.KomiCode):
				Komi = int.Parse(value);
				break;
			}
		}
	}
}