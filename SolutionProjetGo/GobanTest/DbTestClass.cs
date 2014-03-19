using System.Diagnostics;
using Assets.Db;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GobanTest
{
    public class DbTestClass
    {
        public DbGobansDataContext Context { get; set; }

        [TestInitialize]
        public void Init()
        {
            Debug.WriteLine("Initializing test");
            this.Context = new DbGobansDataContext();
        }

        [TestCleanup]
        public void Cleanup()
        {
            Debug.WriteLine("Cleaning test");
            Context.RejectChanges();
            Context.Dispose();
        }

    }
}
