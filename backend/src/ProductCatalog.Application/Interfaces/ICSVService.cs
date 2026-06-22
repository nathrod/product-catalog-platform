namespace ProductCatalog.Application.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICSVService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="file"></param>
        /// <returns></returns>
        public IEnumerable<T> ReadCSV<T>(Stream file);
    }
}