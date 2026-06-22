using SqlSugar;

namespace ProductCatalog.Application.DTOs.Queries
{
    //Dto for custom fields condtions MAps to a single WHERE clause contiona field + operator + value
    public class FilterDto : IConditionalModel
    {
        //database field name to filter on required, non-nullable
        public string FieldName { get; set; } = string.Empty;

        //value to compare against the field nullable for optional values
        public string? FieldValue { get; set; }

        //Sqlsugar conditional operator type, Equal, Like, GreaterThan. Determines the filter logic FieldName = FieldValue
        public ConditionalType ConditionalType { get; set; }

    }
}