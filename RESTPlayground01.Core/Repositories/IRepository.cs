namespace RESTPlayground01.Core.Repositories
{
    public interface IRepository<in TKey, T>
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
        /// <param name="defaultEntity">Object that will be used if target object doesn't exist in repository.</param>
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
