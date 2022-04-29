using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Task22WPFClient.Model;

namespace Task22WPFClient.Views
{
    /// <summary>
    /// Логика взаимодействия для AddPhoneBook.xaml
    /// </summary>
    public partial class AddPhoneBook : Window
    {
        public AddPhoneBook()
        {
            InitializeComponent();
        }

        public AddPhoneBook(PhoneBook phoneBook)
        {
            InitializeComponent();
            FirstName.Text = phoneBook.FirstName;
            SecondName.Text = phoneBook.SecondName; 
            FatherName.Text = phoneBook.FatherName;
            Address.Text = phoneBook.Address; 
            PhoneNumber.Text = phoneBook.PhoneNumber;
            Description.Text = phoneBook.Description;
        }


        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
