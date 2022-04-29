using System.ComponentModel.DataAnnotations;

namespace Task22WebApi.Models
{
    public class PhoneBook
    {
        public int Id { get; set; }

        [Required]
        public string SecondName { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string FatherName { get; set; }

        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return $"{Id} {SecondName} {FirstName} {FatherName}";
        }

    }
}
