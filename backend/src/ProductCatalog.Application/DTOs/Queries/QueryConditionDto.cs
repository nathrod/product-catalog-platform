using SqlSugar;

namespace ProductCatalog.Application.DTOs.Queries
{
    public class QueryConditionDto
    {
        private int pageSize = 10;

        public int PageSize
        {
            get => pageSize;
            set
            {
                pageSize = value < 1 ? 10 : value;
            }
        }

        private int pageIndex = 1;

        public int PageIndex
        {
            get => pageIndex;
            set
            {
                pageIndex = value < 1 ? 1 : value;
            }
        }

        //Condition to filter nullable, initialized to empty list to avoid null refrences map to SqlSugar for database filtering 

        public List<FilterDto>? Filters { get; set; } = [];

        //converted sqlsugar IcontionalModal list derived from Condtionals, used directly by SQLSugar for WHERE clause generation

        public List<IConditionalModel> Where
        {
            get
            {
                var data = new List<IConditionalModel>();
                if (Filters == null || !Filters.Any())
                {
                    return data;
                }

                foreach (var filter in Filters)
                {
                    //Skip invalid Filters missing FieldName to prevent SqlSugar errors
                    if (string.IsNullOrWhiteSpace(filter.FieldName))
                    {
                        continue;
                    }

                    data.Add(new ConditionalModel()
                    {
                        FieldName = filter.FieldName,
                        ConditionalType = filter.ConditionalType,
                        FieldValue = filter.FieldValue
                    });
                }
                return data;
            }
        }

        //Sorting rules initialized to empty list to avoid null references. Contains field name and direction for SqlSugar ORDER BY clause
        public List<SortDto>? Sorts { get; set; } = [];

        private string orderBy = string.Empty;

        //Formatted ORDER BY string, Id desc, Name asc will clear Orders if set value
        public string OrderBy
        {
            get
            {
                //null safe formatting, return empty string if no valid sort rules
                if (Sorts == null | !Sorts.Any())
                {
                    return orderBy;
                }

                //filter out invalid sort rules missing OrderField
                var validSorts = Sorts.Where(o => !string.IsNullOrWhiteSpace(o.FieldName)).ToList();
                if (!validSorts.Any())
                {
                    return string.Empty;
                }

                return string.Join(",", validSorts.Select(a => $"{a.FieldName} {a.OrderDirection}"));
            }

            set
            {
                if(!string.IsNullOrEmpty(value))
                {
                    orderBy = value;
                    Sorts = null;
                }
            }
        }
    }
}