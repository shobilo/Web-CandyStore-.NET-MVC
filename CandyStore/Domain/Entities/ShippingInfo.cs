using System.ComponentModel.DataAnnotations;


namespace Domain.Entities
{
    public class ShippingInfo
    {
        [Required(ErrorMessage = "Entry your Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Entry your Surname")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Entry the Country")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Entry the City")]
        public string City { get; set; }

        [Required(ErrorMessage = "Entry the first Address")]
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
    }
}
