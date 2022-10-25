using System;
using Castle.Windsor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Holding.Base.CommandBus;
using Holding.Base.CommandBus.Installer;

namespace TestCommandBus
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            WindsorContainer container = new WindsorContainer();
            var busInstaller = new SyncableCommandBusInstaller();
            container.Install(busInstaller);

            var commandBus = container.Resolve<ICommandBus>();

//            var syncBus = busInstaller.SyncBus;
        }
    }
}
