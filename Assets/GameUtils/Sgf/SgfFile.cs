using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Assets.GameLogic;
using Assets.ObjetsDeJeu;

namespace Assets.GameUtils.Sgf
{
	public class SgfFile
	{
		public SgfHeader Header { get; set; }

		public IList<Move> Moves { get; set; }

		public SgfFile(string path)
		{
			string fileContent = File.ReadAllText(path);
			//Suppression de la premiere et derniere parenthese
			string content = String.Concat(fileContent.Skip(1).Reverse().Skip(1).Reverse());
			string[] splitedContent = content.Split(';');
			string header = splitedContent[1];
			Header = new SgfHeader();
			Header.BuildFromText(header);
			if(Header.BlackPlayer == null)
			{
				Header.BlackPlayer = new Player("Black Player", PlayerColor.Black);
			}
			if(Header.WhitePlayer == null)
			{
				Header.WhitePlayer = new Player("White Player", PlayerColor.White);
			}
			IEnumerable<string> moves = splitedContent.Skip(2);
			Moves = BuildMovesFromString(moves);
		}

		public SgfFile(Game game)
		{
			this.Header.GameType = SgfConst.GameType.Go;
			this.Header.BlackPlayer = game.BlackPlayer;
			this.Header.WhitePlayer = game.WhitePlayer;
			this.Header.Size = game.Goban.Size;
			this.Header.GameName = string.Format("{0} vs {1}", this.Header.BlackPlayer.Name, this.Header.WhitePlayer.Name);
			foreach(var move in Moves)
			{
				this.Moves.Add(move);
			}
		}

		private IList<Move> BuildMovesFromString(IEnumerable<string> strListMove)
		{
			IList<Move> msList = new List<Move>();
			Regex movesRegex = new Regex(SgfConst.MovePattern);
			foreach(string strMove in strListMove)
			{
				Match m = movesRegex.Match(strMove);
				if(m.Success)
				{
					string player = m.Groups[1].Value;
					string colonneStr = m.Groups[2].Value;
					string ligneStr = m.Groups[3].Value;

					int colonneIndex = SgfConst.GetIndexFromColumnName(colonneStr[0]);
					int ligneIndex = SgfConst.GetIndexFromColumnName(ligneStr[0]);

					if(player.Equals(SgfConst.WhiteMovePlayerCode))
					{
						msList.Add(new Move(Header.WhitePlayer, colonneIndex, ligneIndex));
					} else if(player.Equals(SgfConst.BlackMovePlayerCode))
					{
						msList.Add(new Move(Header.BlackPlayer, colonneIndex, ligneIndex));
					}
				}
			}
			return msList;
		}
	}
}