using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbGobansContext
{
    public partial class DbGoban
    {
        public DbPion GetLastCoup(long idGoban)
        {
            long maxId = this.DbPions.Where(pion => pion.IdGoban == idGoban).Max(pion => pion.IdPion);
            return this.DbPions.FirstOrDefault(pion => pion.IdPion == maxId);
        }
    }
}
