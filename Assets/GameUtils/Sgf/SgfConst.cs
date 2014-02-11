using System.Globalization;

namespace Assets.GameUtils.Sgf
{
	public class SgfConst
	{
        #region GameType enum

		public enum GameType
		{
			Unknow = 0,
			Go = 1,
			Othello = 2,
			Chess = 3,
			GomokuRenju = 4,
			NineMensMorris = 5,
			Backgammon = 6,
			ChineseChess = 7,
			Shogi = 8,
			LinesOfAction = 9,
			Ataxx = 10,
			Hex = 11,
			Jungle = 12,
			Neutron = 13,
			PhilosophersFootball = 14,
			Quadrature = 15,
			Trax = 16,
			Tantrix = 17,
			Amazons = 18,
			Octi = 19,
			Gess = 20,
			Twixt = 21,
			Zertz = 22,
			Plateau = 23,
			Yinsh = 24,
			Punct = 25,
			Gobblet = 26,
			Hive = 27,
			Exxit = 28,
			Hnefatal = 29,
			Kuba = 30,
			Tripples = 31,
			Chase = 32,
			TumblingDown = 33,
			Sahara = 34,
			Byte = 35,
			Focus = 36,
			Dvonn = 37,
			Tamsk = 38,
			Gipf = 39,
			Kropki = 40
		}

        #endregion

		public const string SizeCode = "SZ";
		public const string GameTypeCode = "GM";
		public const string WhiteMovePlayerCode = "W";
		public const string BlackMovePlayerCode = "B";
		public const string EventTitleCode = "EV";
		public const string DateCode = "DT";
		public const string DateFormat = "yyyy-MM-dd";
		public const string PlaceCode = "PC";
		public const string BlackPlayerCode = "PB";
		public const string WhitePlayerCode = "PW";
		public const string HandicapCode = "HA";
		public const string KomiCode = "KM";
		public const string CommentaryCode = "GC";
		public const string GameNameCode = "GN";

		/// <summary>
		/// Pattern pour une propriété de header (forme XX[value])
		/// 1er groupe  : Code de la propriété
		/// 2eme groupe : Valeur
		/// </summary>
		public const string PropertiesValuePattern = @"(\w{2})\[(.*?)\]";

		/// <summary>
		/// Pattern pour un movement
		/// 1er groupe  : B (joueur noir) ou W (joueur blanc)
		/// 2eme groupe : Colonne (a-Z)
		/// 3eme groupe : Ligne (a-Z)
		/// </summary>
		public const string MovePattern = @"^([B|W]{1})\[(\w)(\w)\]$";

		private const string Alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

		public static int GetIndexFromColumnName(char colName)
		{
			return Alphabet.IndexOf(colName);
		}

		public static string GetColumnNameFromIndex(int index)
		{
			return Alphabet[index].ToString(CultureInfo.InvariantCulture);
		}
	}
}