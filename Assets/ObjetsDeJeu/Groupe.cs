using System.Collections.Generic;
using System.Linq;

namespace Assets.ObjetsDeJeu
{
	public class Groupe : List<Intersection>
	{
        public Groupe getPrisoners(Goban goban)
		{
			Groupe prs = new Groupe ();
			//On parcours chaque groupe pour regarder si il se trouve dans notre groupe actuel
			//SI le groupe à plus de 3 pions
			if (this.Count > 3) {
				foreach (Groupe g in goban.Groupes) {
						if (this.IsPointInPolygon (g.getCentroid ())) {
								//Le centroid du coupe est dans le notre
								prs.AddRange (g);
						}
					}
				}
			return prs;
		}

		public int getFreeInterCount(Goban goban)
		{
			int nb = 0;
			foreach(Intersection i in goban.Intersections)
			{
				if(IsPointInPolygon(i.Coord))
				nb++;
			}
			return nb;
		}

		private Coordonnees getCentroid()
		{
			if (this.Count < 2) {
				return this[0].Coord;
			} else {
				Coordonnees centroid = new Coordonnees(0,0);
				
				for (int i = 0; i < this.Count; i++) {
					centroid.X += this[i].Coord.X;
					centroid.Y += this[i].Coord.Y;
				}
				
				int totalPoints = this.Count/2;
				centroid.X = centroid.X / totalPoints;
				centroid.Y = centroid.Y / totalPoints;
				
				return centroid;
			}

		}

		public List<Intersection> ConvexHull()
		{

			if (this.Count < 3)
			{
				return new List<Intersection>();
			}
			
			List<Intersection> hull = new List<Intersection>();
			
			// get leftmost point
			Intersection vPointOnHull = this.Where(p => p.Coord.X == this.Min(min => min.Coord.X)).First();
			
			Intersection vEndpoint;
			do
			{
				hull.Add(vPointOnHull);
				vEndpoint = this[0];
				
				for (int i = 1; i < this.Count; i++)
				{
					if ((vPointOnHull == vEndpoint)
					    || (Orientation(vPointOnHull, vEndpoint, this[i]) == -1))
					{
						vEndpoint = this[i];
					}
				}
				
				vPointOnHull = vEndpoint;
				
			}
			while (vEndpoint != hull[0]);
			
			return hull;
		}
		
		private static int Orientation(Intersection p1, Intersection p2, Intersection p)
		{
			// Determinant
			int Orin = (p2.Coord.X - p1.Coord.X) * (p.Coord.Y - p1.Coord.Y) - (p.Coord.X - p1.Coord.X) * (p2.Coord.Y - p1.Coord.Y);
			
			if (Orin > 0)
				return -1; //          (* Orientaion is to the left-hand side  *)
			if (Orin < 0)
				return 1; // (* Orientaion is to the right-hand side *)
			
			return 0; //  (* Orientaion is neutral aka collinear  *)
		}

		private bool IsPointInPolygon(Coordonnees testPoint)
		{
			bool result = false;
			List<Intersection> hull = this.ConvexHull ();
			int j = hull.Count - 1;
			for (int i = 0; i < hull.Count(); i++)
			{
				if (hull[i].Coord.Y < testPoint.Y && hull[j].Coord.Y >= testPoint.Y || hull[j].Coord.Y < testPoint.Y && hull[i].Coord.Y >= testPoint.Y)
				{
					if (hull[i].Coord.X + (testPoint.Y - hull[i].Coord.Y) / (hull[j].Coord.Y - hull[i].Coord.Y) * (hull[j].Coord.X - hull[i].Coord.X) < testPoint.X)
					{
						result = !result;
					}
				}
				j = i; 
			}
			return result;
		}

	}

}
