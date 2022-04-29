using System.Threading.Tasks;
using Task20.Models;

namespace Task20
{
    public class ContentInitializer
    {
        public static void InitializeAsync(ApplicationContext contex)
        {
            PhoneBook phoneBook = new PhoneBook() { 
                FirstName = "Петр", 
                SecondName = "Петров",
                FatherName = "Петрович",
                PhoneNumber = "12345678",
                Address = "Рюмочная",
                Description = "-"};

            var coll = contex.PhoneBooks;

            bool bl = false;
            foreach (var item in coll)
            {
                if (item.PhoneNumber == phoneBook.PhoneNumber) bl = true;
            }

            if (!bl)
            {
                contex.PhoneBooks.Add(phoneBook);
                contex.SaveChanges();
            }
        }
    }
}
