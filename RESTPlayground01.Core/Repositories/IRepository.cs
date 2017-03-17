using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTPlayground01.Core.Repositories
{
    public interface IRepository<TKey, T>
    {
        /// <summary>
        /// Get object by key.
        /// </summary>
        /// <param name="id">Unique key.</param>
        /// <returns></returns>
        T Single(TKey id);

        /// <summary>
        /// Get object by key. If doesn't exist then use defaut object as returen value.
        /// </summary>
        /// <param name="id">Unique key.</param>
        /// <returns></returns>
        T SingleOrDefault(TKey id, T defaultEntity);

        /// <summary>
        /// Update or create object.
        /// </summary>
        /// <param name="id">Unique key.</param>
        /// <param name="entity">Object.</param>
        void Update(TKey id, T entity);
    }
}
