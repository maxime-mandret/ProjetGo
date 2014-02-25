using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace DbGobansContext
{
    public partial class DbGoban
    {
        public DbPion GetLastCoup()
        {
            int? maxCoup = this.DbPions.Max(pion => pion.NumeroCoup);
            if (maxCoup.HasValue)
            {
                DbPion firstOrDefault = this.DbPions.FirstOrDefault(pion => pion.NumeroCoup == maxCoup);
                Debug.WriteLine(string.Format("Last coup (n° {0}) on {{{1},{2}}}", maxCoup, firstOrDefault.PositionX, firstOrDefault.PositionY));
                return firstOrDefault;
            }
            else
            {
                return null;
            }
        }

        public DbPion PoserPion (int x, int y, string playerColor)
        {
            Debug.WriteLine(string.Format("Inserting pion at {{{0},{1}}} from {2}", x, y, playerColor));
            DbPion pion = new DbPion {DbGoban = this, PositionX = (byte) x, PositionY = (byte) y, Pioncol = playerColor};
            this.DbPions.Add(pion);
            return pion;
        }
    }
}
