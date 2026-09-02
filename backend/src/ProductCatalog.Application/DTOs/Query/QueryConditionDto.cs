namespace ProductCatalog.Application.DTOs.Query
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

        public List<FilterDto> Filters { get; set; } = [];
        // public List<IConditionalModel> Where
        // {
        //     get
        //     {
        //         var data = new List<IConditionalModel>();
        //         if (Filters == null || !Filters.Any())
        //         {
        //             return data;
        //         }

        //         foreach (var filter in Filters)
        //         {
        //             if (string.IsNullOrWhiteSpace(filter.FieldName))
        //             {
        //                 continue;
        //             }

        //             data.Add(new ConditionalModel()
        //             {
        //                 FieldName = filter.FieldName,
        //                 ConditionalType = filter.ConditionalType,
        //                 FieldValue = filter.FieldValue
        //             });
        //         }
        //         return data;
        //     }
        // }
        public List<SortDto> Sorts { get; set; } = [];

        // private string orderBy = string.Empty;
        // public string OrderBy
        // {
        //     get
        //     {
        //         if (Sorts == null || !Sorts.Any())
        //         {
        //             return orderBy;
        //         }

        //         var validSorts = Sorts.Where(o => !string.IsNullOrWhiteSpace(o.FieldName)).ToList();
        //         if (!validSorts.Any())
        //         {
        //             return string.Empty;
        //         }

        //         return string.Join(",", validSorts.Select(a => $"{a.FieldName} {a.OrderDirection}"));
        //     }

        //     set
        //     {
        //         if(!string.IsNullOrEmpty(value))
        //         {
        //             orderBy = value;
        //             Sorts = null;
        //         }
        //     }
        // }
    }
}