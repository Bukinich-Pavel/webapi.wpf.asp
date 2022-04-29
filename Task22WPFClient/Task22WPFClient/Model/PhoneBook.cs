using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task22WPFClient.Model
{
    public class PhoneBook
    {
        public int Id { get; set; }
        public string SecondName { get; set; }
        public string FirstName { get; set; }
        public string FatherName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return $"{Id}  {SecondName}  {FirstName}  {FatherName}  {PhoneNumber}  {Address}  {Description}";
        }

    }
}
