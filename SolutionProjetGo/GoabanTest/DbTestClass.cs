using System.Diagnostics;
using DbGobansContext;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GoabanTest
{
    public class DbTestClass
    {
        public DbGobansDataContext Context { get; set; }

        [TestInitialize]
        public void init()
        {
            Debug.WriteLine("Initializing test");
            this.Context = new DbGobansDataContext();
        }

        [TestCleanup]
        public void cleanup()
        {
            Debug.WriteLine("Cleaning test");
            Context.RejectChanges();
            Context.Dispose();
        }

    }
}
