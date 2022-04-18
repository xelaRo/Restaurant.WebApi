namespace Restaurant.WebApi.Models
{
    public class CustomerViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string DateOfBirth { get; set; }
        public string Gender { get; set; }
        public int AddressId { get; set; }
        public int UserId { get; set; }
    }
}
