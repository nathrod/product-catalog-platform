namespace ProductCatalog.Application.DTOs.Queries
{
    //Dto for sorting  rules used to generate sqlsugar ORDER BY clauses
    public class SortDto
    {
        //Raw sort direction input "desc", "asc" or emtpy used to derive the normalizes OrderDirection property
        public string? Direction { get; set; }

        //Database field name to sort by (required, non-nullable)
        public string FieldName { get; set; } = string.Empty;

        public string OrderDirection
        {
            get
            {
                //Null safe check for sort direction
                return !string.IsNullOrWhiteSpace(Direction)
                    && Direction.IndexOf("desc", StringComparison.CurrentCultureIgnoreCase) > -1
                    ? "desc" : "asc";
            }
        }
    }
}