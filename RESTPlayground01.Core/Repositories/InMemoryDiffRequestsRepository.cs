using System.Collections.Concurrent;
using RESTPlayground01.Core.Models;

namespace RESTPlayground01.Core.Repositories
{
    public class InMemoryDiffRequestsRepository : IDiffRequestsRepository
    {
        private ConcurrentDictionary<int, DiffRequest> _dictionary 
            = new ConcurrentDictionary<int, DiffRequest>();

        public DiffRequest Single(int id)
        {
            return _dictionary.ContainsKey(id) ? _dictionary[id].Clone() : null;
        }

        public DiffRequest SingleOrDefault(int id, DiffRequest defaultEntity)
        {
            var entity = Single(id);
            return entity != null ? entity : defaultEntity;
        }

        public void Update(int id, DiffRequest entity)
        {
            entity.Id = id;
            _dictionary[id] = entity.Clone();
        }
    }
}
