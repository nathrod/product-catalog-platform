using System.Globalization;
using CsvHelper;
using ProductCatalog.Application.Interfaces;

namespace ProductCatalog.Application.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class CSVService : ICSVService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="file"></param>
        /// <returns></returns>
        public IEnumerable<T> ReadCSV<T>(Stream file)
        {
            var reader = new StreamReader(file);
            var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            var records = csv.GetRecords<T>();
            return records;
        }
    }    
}