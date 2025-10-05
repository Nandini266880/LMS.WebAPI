namespace LMS.Application.Services.IServices
{
    public interface IRedisCacheService
    {
        /// <summary>
        /// Get Data using key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<T> GetAsync<T>(string key);
        /// <summary>
        /// Set Data with Value and Expiration Time of Key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expirationTime"></param>
        /// <returns></returns>
        Task SetAsync<T>(string key, T value, TimeSpan expiration);
       
        /// <summary>
        /// Remove Data
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task RemoveAsync(string key);

    }
}
