using System;

namespace Assets.Db
{
    public class DbCoup
    {
        public virtual int IdCoup { get; set; }
        public virtual DbPartie Partie { get; set; }
        public virtual DateTime HeureCoup { get; set; }
        public virtual int IdJoueur { get; set; }
        public virtual int X { get; set; }
        public virtual int Y { get; set; }

  //`idCoup` int(10) NOT NULL AUTO_INCREMENT,
  //`idPartie` int(10) NOT NULL,
  //`heureCoup` datetime NOT NULL,
  //`idJoueur` int(10) unsigned NOT NULL,
  //`x` int(10) DEFAULT NULL,
  //`y` int(10) DEFAULT NULL,

    }
}
