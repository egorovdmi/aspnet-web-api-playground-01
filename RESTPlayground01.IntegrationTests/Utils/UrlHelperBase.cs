using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTPlayground01.IntegrationTests.Utils
{
    public class UrlHelperBase
    {
        protected string _baseAddress;
        protected int _port;

        public UrlHelperBase(string baseAddress)
        {
            _baseAddress = baseAddress;
        }
    }
}
