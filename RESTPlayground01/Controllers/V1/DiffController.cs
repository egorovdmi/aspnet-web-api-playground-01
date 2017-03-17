using System;
using System.Web.Http;
using RESTPlayground01.Core.Models;
using RESTPlayground01.Core.Repositories;
using RESTPlayground01.Core.Services;
using RESTPlayground01.Models;

namespace RESTPlayground01.Controllers.V1
{
    public class DiffController : ApiController
    {
        private readonly IDiffRequestsRepository _diffRequestsRepository;
        private readonly IBinaryDataDiffAnalyzer _binaryDataDiffAnalyzer;

        public DiffController(IDiffRequestsRepository diffRequestsRepository,
            IBinaryDataDiffAnalyzer binaryDataDiffAnalyzer)
        {
            _diffRequestsRepository = diffRequestsRepository;
            _binaryDataDiffAnalyzer = binaryDataDiffAnalyzer;
        }

        /// <summary>
        /// Get a diff of compared binary conent uploaded with PUT method.
        /// </summary>
        /// <param name="id">Id.</param>
        /// <remarks>GET: /v1/diff/id</remarks>
        public IHttpActionResult Get(int id)
        {
            var model = _diffRequestsRepository.Single(id);

            if (model == null || model.Left == null || model.Right == null)
            {
                return NotFound();
            }

            var result = _binaryDataDiffAnalyzer.Diff(model.Left, model.Right);
            return Ok(result);
        }

        /// <summary>
        /// Creates or updates diff request.
        /// </summary>
        /// <param name="id">Id.</param>
        /// <param name="side">Side of content.</param>
        /// <param name="content">Base64 encoded binary data.</param>
        /// <remarks>PUT: /v1/diff/5/left|right</remarks>
        public IHttpActionResult Put(int id, ContentSide side, [FromBody]ContentModel content)
        {
            if (side == ContentSide.NotDefined)
                return NotFound();

            if (content == null || content.Data == null)
                return BadRequest();

            var model = _diffRequestsRepository.SingleOrDefault(id, new DiffRequest());
            switch(side)
            {
                case ContentSide.Left:
                    model.Left = Convert.FromBase64String(content.Data);
                    break;
                case ContentSide.Right:
                    model.Right = Convert.FromBase64String(content.Data);
                    break;
                default:
                    return NotFound();
            }

            _diffRequestsRepository.Update(id, model);
            return Created(ControllerContext.Request.RequestUri, model);
        }
    }
}
