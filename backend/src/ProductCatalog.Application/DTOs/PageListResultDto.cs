using System.Text.Json.Serialization;

namespace ProductCatalog.Application.DTOs
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageListResultDto<T> where T :  class
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("total")]
        public int Total { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("pageIndex")]
        public int PageIndex { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("items")]
        public List<T> Items { get; set; } = [];
    }
}