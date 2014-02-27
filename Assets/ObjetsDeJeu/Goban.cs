using System.Collections.Generic;
using System.Linq;

namespace Assets.ObjetsDeJeu
{
	public class Goban
	{
        private Intersection[,] _cases;

        public Intersection[,] Intersections
        {
            get { return _cases; }
            set { _cases = value; }
        }
		private Stack<Move> _moveList;
		public int Size{ get; set;}
		private IList<Groupe> _groupes;

	    public Goban() : this(9)
		{
		}

		public Goban(int size)
		{
			Size = size;
			_moveList = new Stack<Move>();
			this._groupes = new List<Groupe>();
			_cases = new Intersection[size, size];

			for(int x = 0; x < Size; x++)
			{
				for(int y = 0; y < Size; y++)
				{
					_cases[x, y] = new Intersection {Owner = null, Coord = new Coordonnees(x, y)};
				}
			}
		}

        private Intersection GetNorth (Intersection inter)
		{
			return IsInRange(inter.Coord.X, inter.Coord.Y - 1) ? this[inter.Coord.X, inter.Coord.Y - 1] : null;
		}

        private Intersection GetSouth (Intersection inter)
		{
			return IsInRange(inter.Coord.X, inter.Coord.Y + 1) ? this[inter.Coord.X, inter.Coord.Y + 1] : null;
		}

        private Intersection GetWest (Intersection inter)
		{
			return IsInRange(inter.Coord.X - 1, inter.Coord.Y) ? this[inter.Coord.X - 1, inter.Coord.Y] : null;
		}
		
		private Intersection GetEast(Intersection inter)
		{
			return IsInRange(inter.Coord.X + 1, inter.Coord.Y) ? this[inter.Coord.X + 1, inter.Coord.Y] : null;
		}

		public List<Intersection> GetAround(Intersection inter)
		{
			var l = new List<Intersection> {GetNorth(inter), GetWest(inter), GetSouth(inter), GetEast(inter)};
			return l.FindAll(i => i != null);
		}

		public List<Intersection> GetLibertes(Intersection inter)
		{
			return GetAround(inter).FindAll(i => i.Owner != null);
		}
		
		public bool IsAtari(Intersection inter)
		{
			return GetAround(inter).TrueForAll(i => i.Owner != inter.Owner);
		}

		private bool Owned(Intersection i)
		{
			if(i != null)
			{
				return i.Owner != null;
			}
			return false;
				
		}

        private bool IsInRange (Intersection intersection)
		{
			return IsInRange(intersection.Coord.X, intersection.Coord.Y);
		}

		private bool IsInRange(int x, int y)
		{
			return (x < this.Size && x >= 0)
				&& (y < this.Size && y >= 0);
		}
		
		private bool IsFree(Intersection intersection)
		{
			return intersection.Owner == null;
		}
		
		public bool CanPlay(int x, int y)
		{
			if(IsInRange(x, y))
			{
				return IsFree(this[x, y]) && !IsAtari(this[x, y]);
			}
			return false;
		}

		public Intersection this[int x, int y] {
			get { return Intersections[x, y]; }
		}

		public Stack<Move> MoveList {
			get { return _moveList; }
			private set { _moveList = value; }
		}

		public IList<Groupe> Groupes {
			get { return _groupes; }
			private set { _groupes = value; }
		}

		public void PutRock(Player p, int x, int y)
		{
			PutRock(new Move(p, x, y));
		}

		public void PutRock(Move m)
		{
			Intersection target = this[m.Coord.X, m.Coord.Y];
            if (CanPlay(target.Coord.X, target.Coord.Y))
		    {
                target.Owner = m.Player;
                this.MoveList.Push(m);
                // Groupes
                var friends = GetAround(target).FindAll(i => i.Owner == target.Owner);

                Groupe g = new Groupe { target };
				foreach (Intersection fr in friends)
				{
					var group = GetGroupe(fr);
					if(group != null)
						g.AddRange(group);
				}
				foreach (Intersection fr in friends)
				{
					var group = GetGroupe(fr);
					if(group != null)
						this.Groupes.Remove(group);
				}
                this.Groupes.Add(g);
		    }
			
		}

		Groupe GetGroupe(Intersection i)
		{
			return this.Groupes.FirstOrDefault(g => g.Contains(i));;
		}


        
    }
}