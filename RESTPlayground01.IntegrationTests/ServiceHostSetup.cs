using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Owin.Hosting;
using System;

namespace RESTPlayground01.IntegrationTests
{
    [TestClass]
    public class ServiceHostSetup
    {
        public const int WebapiServicePort = 9443;
        public const string WebapiServiceBaseurl = "http://localhost";

        private static IDisposable _webApp;

        [AssemblyInitialize]
        public static void Start(TestContext tc)
        {
            _webApp = WebApp.Start<Startup>(string.Format("http://*:{0}/", WebapiServicePort));
        }

        [AssemblyCleanup]
        public static void Stop()
        {
            _webApp.Dispose();
        }
    }
}
