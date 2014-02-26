using System.Diagnostics;
using DbGobansContext;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GoabanTest
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
