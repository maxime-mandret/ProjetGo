using System;

namespace Assets.ObjetsDeJeu
{
	public class Move : IEquatable<Move>
	{
		public Move(Player player, Coordonnees coord)
		{
			Player = player;
			Coord = coord;
		}

		public Move(Player player, int x, int y)
            : this(player, new Coordonnees(x, y))
		{
		}

		public Player Player { get; set; }

		public Coordonnees Coord { get; set; }

        #region IEquatable<Move> Membres

		public bool Equals(Move other)
		{
			if(ReferenceEquals(null, other))
			{
				return false;
			}
			if(ReferenceEquals(this, other))
			{
				return true;
			}
			return Equals(Player, other.Player) && Equals(Coord, other.Coord);
		}

		public override bool Equals(Object other)
		{
			if(ReferenceEquals(null, other))
			{
				return false;
			}
			if(ReferenceEquals(this, other))
			{
				return true;
			}
			if(other.GetType() != GetType())
			{
				return false;
			}
			return Equals((Move)other);
		}

        #endregion

		public override int GetHashCode()
		{
			unchecked
			{
				return ((Player != null ? Player.GetHashCode() : 0) * 397) ^ (Coord != null ? Coord.GetHashCode() : 0);
			}
		}
	}
}