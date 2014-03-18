using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;

namespace GobanTest
{
    internal class DbTest
    {
        private ISessionFactory factory;
        protected ISession session;

        [TestInitialize]
        public void init()
        {
            factory = new NHibernate.Cfg.Configuration().Configure().BuildSessionFactory();
            session = factory.OpenSession();
        }

        [TestCleanup]
        public void cleanUp()
        {
            session.Flush();
            session.Dispose();
            factory.Dispose();
        }
    }
}
