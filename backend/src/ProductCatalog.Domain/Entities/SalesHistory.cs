using SqlSugar;

namespace ProductCatalog.Domain.Entities
{
    [SugarTable("product_sales")]
    public class SalesHistory
    {
        [SugarColumn(IsPrimaryKey = true)]
        public Guid Id { get; set; }

        [SugarColumn(IsNullable = false)]
        public Guid ProductId{ get; set; }

        public DateTime SaleDate { get; set; }

        public decimal QuantitySold { get; set; }

        [SugarColumn(ColumnDataType = "numeric(12,2)")]
        public decimal TotalSaleAmount { get; set; }
    }
}


//Cada linha na tabela sales_history representa uma venda
//cada produto possui o seu histórico de vendas associdado
//3A sales history vinculado ao id do Produto, dizendo que naquela venda vendi 3 produtos A 
//cada venda vai criar uma entrada na tabela sales_history