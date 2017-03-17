using System.IO;
using log4net.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RESTPlayground01.Tests
{
    [TestClass]
    public class LoggingSetup
    {

        [AssemblyInitialize]
        public static void Configure(TestContext tc)
        {
            XmlConfigurator.Configure(new FileInfo("log4net.config"));
        }
    }
}
