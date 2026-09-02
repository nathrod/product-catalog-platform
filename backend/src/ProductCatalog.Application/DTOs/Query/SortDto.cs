using System.Text.Json.Serialization;

namespace ProductCatalog.Application.DTOs.Query
{
    public class SortDto
    {
        // [JsonPropertyName("direction")]
        // public string? Direction { get; set; }

        [JsonPropertyName("fieldName")]
        public string FieldName { get; set; } = string.Empty;
        [JsonPropertyName("descending")]
        public bool Descending { get; set; }

        //[JsonPropertyName("orderDirection")]
        // [JsonIgnore]
        // public string OrderDirection
        // {
        //     get
        //     {
        //         return !string.IsNullOrWhiteSpace(Direction)
        //             && Direction.IndexOf("desc", StringComparison.CurrentCultureIgnoreCase) > -1
        //             ? "desc" : "asc";
        //     }
        // }
    }
}