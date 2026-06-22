using SqlSugar;

namespace ProductCatalog.Application.DTOs.Queries
{
    public class FilterDto : IConditionalModel
    {
        public string FieldName { get; set; } = string.Empty;
        public string? FieldValue { get; set; }
        public ConditionalType ConditionalType { get; set; }
    }
}