using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Owin.Hosting;
using System;

namespace RESTPlayground01.IntegrationTests
{
    [TestClass]
    public class ServiceHostSetup
    {
        public const int WEBAPI_SERVICE_PORT = 9443;
        public const string WEBAPI_SERVICE_BASEURL = "http://localhost";

        private static IDisposable _webApp;

        [AssemblyInitialize]
        public static void Start(TestContext tc)
        {
            _webApp = WebApp.Start<Startup>(string.Format("http://*:{0}/", WEBAPI_SERVICE_PORT));
        }

        [AssemblyCleanup]
        public static void Stop()
        {
            _webApp.Dispose();
        }
    }
}
