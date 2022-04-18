namespace Restaurant.WebApi.Models.SecondDb
{
    public class ItemEditViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int VendorId
        {
            get; set;
        }
    }
}
