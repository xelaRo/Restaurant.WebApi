namespace Restaurant.WebApi.Infrastructure.OracleDb.Entities.DW
{
    public class Bill
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public bool IsShipping { get; set; }
        public float Total { get; set; }
    }
}
