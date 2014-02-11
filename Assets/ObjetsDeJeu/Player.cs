using System;

namespace Assets.ObjetsDeJeu
{
	public enum PlayerColor
	{
		White,
		Black
    }
	;

	public class Player : IEquatable<Player>
	{
        public PlayerColor Color { get; set; }
		public Player(string name, PlayerColor color)
		{
			Name = name;
			this.Color = color;
		}

		public string Name { get; set; }
		public int Score { get; set; }
		public int NbAbandonSuccessifs { get; set; }

        #region IEquatable<Player> Members

		public bool Equals(Player other)
		{
			if(ReferenceEquals(null, other))
			{
				return false;
			}
			if(ReferenceEquals(this, other))
			{
				return true;
			}
			if(GetHashCode() != other.GetHashCode())
			{
				return false;
			}
			return string.Equals(Name, other.Name); //&& Color == other.Color;
		}

        #endregion

		public override bool Equals(object obj)
		{
			if(ReferenceEquals(null, obj))
			{
				return false;
			}
			if(ReferenceEquals(this, obj))
			{
				return true;
			}
			if(obj.GetType() != GetType())
			{
				return false;
			}
			return Equals((Player)obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return ((Name != null ? Name.GetHashCode() : 0) * 397);// ^ (int) Color;
			}
		}
	}
}