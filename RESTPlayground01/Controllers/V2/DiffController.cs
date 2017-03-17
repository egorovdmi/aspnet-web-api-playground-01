using System.Collections.Generic;
using System.Web.Http;

namespace RESTPlayground01.Controllers.V2
{
    public class DiffController : ApiController
    {
        // GET: api/Diff
        public IEnumerable<string> Get()
        {
            return new[] { "value1", "value2" };
        }

        // GET: api/Diff/5
        public string Get(int id)
        {
            return "Hello V2 controller!";
        }

        // POST: api/Diff
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Diff/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Diff/5
        public void Delete(int id)
        {
        }
    }
}
