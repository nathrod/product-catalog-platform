using System.Text.Json.Serialization;

namespace ProductCatalog.Application.DTOs
{
    public class PageListResultDto<T> where T :  class
    {
        [JsonPropertyName("total")]
        public int Total { get; set; }
        
        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; }

        [JsonPropertyName("pageIndex")]
        public int PageIndex { get; set; }

        [JsonPropertyName("items")]
        public List<T> Items { get; set; } = [];
    }
}