namespace E_Commerce.Domain.Entities.OrderAggregate
{
    public class Address
    {
        #region Constructors

        public Address()
        {

        }

        public Address(string fristName, string lastName, string street, string city, string country)
        {
            FristName = fristName;
            LastName = lastName;
            Street = street;
            City = city;
            Country = country;
        }

        #endregion

        #region Properties

        public string FristName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty; 

        #endregion
    }
}
