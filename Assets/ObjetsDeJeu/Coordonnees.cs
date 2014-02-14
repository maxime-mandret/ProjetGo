using System;
using System.Linq;

namespace Assets.ObjetsDeJeu
{
	public class Coordonnees : IEquatable<Coordonnees>
	{
		private int _x;

		private int _y;
        
		public Coordonnees(int x, int y)
		{
			_x = x;
			_y = y;
		}

        #region IEquatable<Coordonnees> Membres

		public bool Equals(Coordonnees other)
		{
			if(ReferenceEquals(null, other))
			{
				return false;
			}
			if(ReferenceEquals(this, other))
			{
				return true;
			}
			return _x == other._x && _y == other._y;
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
			return Equals((Coordonnees)other);
		}

        #endregion

		public int X {
			get { return _x; }
			set { _x = value; }
		}

		public int Y {
			get { return _y; }
			set { _y = value; }
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (_x * 397) ^ _y;
			}
		}
	}
}