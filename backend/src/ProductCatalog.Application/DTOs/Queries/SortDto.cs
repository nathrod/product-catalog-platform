namespace ProductCatalog.Application.DTOs.Queries
{
    public class SortDto
    {
        public string? Direction { get; set; }
        public string FieldName { get; set; } = string.Empty;
        public string OrderDirection
        {
            get
            {
                return !string.IsNullOrWhiteSpace(Direction)
                    && Direction.IndexOf("desc", StringComparison.CurrentCultureIgnoreCase) > -1
                    ? "desc" : "asc";
            }
        }
    }
}