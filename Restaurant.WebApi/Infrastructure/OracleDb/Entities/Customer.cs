using System;

namespace Restaurant.WebApi.Infrastructure.OracleDb.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public char Gender { get; set; }
        public int AddressId { get; set; }
        public int UserId { get; set; }
    }
}
