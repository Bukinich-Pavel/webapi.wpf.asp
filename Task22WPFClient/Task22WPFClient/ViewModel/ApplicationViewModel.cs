using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Task22WPFClient.Model;
using Task22WPFClient.Views;

namespace Task22WPFClient.ViewModel
{
    internal class ApplicationViewModel : INotifyPropertyChanged
    {
        private string url;
        private HttpClient HttpClient { get; set; }


        /// <summary>
        /// Выбранный объект
        /// </summary>
        private PhoneBook selectPhoneBook;
        public PhoneBook SelectPhoneBook
        {
            get { return selectPhoneBook; }
            set
            {
                selectPhoneBook = value;
                OnPropertyChanged("SelectPhoneBook");
            }
        }


        #region Commands

        /// <summary>
        /// Команда добавления PhoneBook в WebApi
        /// </summary>
        private RelayCommand addPhoneBook;
        public RelayCommand AddPhoneBook
        {
            get
            {
                return addPhoneBook ?? (addPhoneBook = new RelayCommand(obg =>
                {
                    try
                    {
                        AddPhoneBook addPhoneBook = new AddPhoneBook();
                        addPhoneBook.ShowDialog();

                        var phoneBook = CreatePhoneBook(addPhoneBook);

                        //HttpPost
                        var r = HttpClient.PostAsync(
                            requestUri: url + @"/values/",
                            content: new StringContent(JsonConvert.SerializeObject(phoneBook), Encoding.UTF8,
                            mediaType: "application/json")
                            ).Result;

                        //обновить коллекцию
                        ListPhoneBook = new ObservableCollection<PhoneBook>();
                        var list = new ObservableCollection<PhoneBook>(GetAllPhoneBook().ToList());
                        ListPhoneBook = list;
                    }
                    catch 
                    {

                    }

                }));
            }
        }


        /// <summary>
        /// Команда изменения PhoneBook в WebApi
        /// </summary>
        private RelayCommand commandEditPhoneBook;
        public RelayCommand CommandEditPhoneBook
        {
            get
            {
                return commandEditPhoneBook ?? (commandEditPhoneBook = new RelayCommand(obg =>
                {
                    try
                    {
                        if (SelectPhoneBook == null) return;

                        AddPhoneBook addPhoneBook = new AddPhoneBook(SelectPhoneBook);
                        addPhoneBook.ShowDialog();

                        var phoneBook = CreatePhoneBook(addPhoneBook);
                        phoneBook.Id = SelectPhoneBook.Id;

                        //HttpPut
                        var r = HttpClient.PutAsync(
                            requestUri: url + @"/values",
                            content: new StringContent(JsonConvert.SerializeObject(phoneBook), Encoding.UTF8,
                            mediaType: "application/json")
                            ).Result;

                        //обновить коллекцию
                        ListPhoneBook = new ObservableCollection<PhoneBook>();
                        var list = new ObservableCollection<PhoneBook>(GetAllPhoneBook().ToList());
                        ListPhoneBook = list;

                    }
                    catch
                    {

                    }
                }));
            }
        }


        /// <summary>
        /// Команда удаление PhoneBook в WebApi
        /// </summary>
        private RelayCommand commandDeletePhoneBook;
        public RelayCommand CommandDeletePhoneBook
        {
            get
            {
                return commandDeletePhoneBook ?? (commandDeletePhoneBook = new RelayCommand(obg =>
                {
                    try
                    {
                        if (SelectPhoneBook == null) return;
                        //HttpDelete
                        var r = HttpClient.DeleteAsync(url + @"/values/" + $"{SelectPhoneBook.Id}").Result;

                        //обновить коллекцию
                        ListPhoneBook = new ObservableCollection<PhoneBook>();
                        var list = new ObservableCollection<PhoneBook>(GetAllPhoneBook().ToList());
                        ListPhoneBook = list;
                    }
                    catch 
                    {

                    }
                }));
            }
        }

        #endregion


        private ObservableCollection<PhoneBook> listPhoneBook;
        public ObservableCollection<PhoneBook> ListPhoneBook
        {
            get { return listPhoneBook; }
            set
            {
                listPhoneBook = value;
                OnPropertyChanged("ListPhoneBook");
            }
        } 


        public ApplicationViewModel()
        {
            url = @"https://localhost:44352/api";
            HttpClient = new HttpClient();

            ListPhoneBook = new ObservableCollection<PhoneBook>(GetAllPhoneBook().ToList());
        }




        /// <summary>
        /// Получить все записи из webApi
        /// </summary>
        /// <returns></returns>
        private IEnumerable<PhoneBook> GetAllPhoneBook()
        {
            try
            {
                string json = HttpClient.GetStringAsync(url + @"/values").Result;
                return JsonConvert.DeserializeObject<IEnumerable<PhoneBook>>(json);
            }
            catch 
            {
                var list = new List<PhoneBook>();
                var allarm = new PhoneBook()
                {
                    Id = 0,
                    FirstName = "Ошибка",
                    SecondName = "Ошибка",
                    FatherName = "Ошибка",
                    PhoneNumber = "Ошибка",
                    Address = "Ошибка",
                    Description = "Ошибка",
                };
                list.Add(allarm);

                return list;
            }
        }


        /// <summary>
        /// Создает экземпляр PhoneBook из данных окна AddPhoneBook
        /// </summary>
        /// <param name="addPhoneBook"></param>
        /// <returns></returns>
        private PhoneBook CreatePhoneBook(AddPhoneBook addPhoneBook)
        {
            string firstName, secondName, fatherName, phoneNumber, address, description;
            firstName = addPhoneBook.FirstName.Text;
            secondName = addPhoneBook.SecondName.Text;
            fatherName = addPhoneBook.FatherName.Text;
            phoneNumber = addPhoneBook.PhoneNumber.Text;
            address = addPhoneBook.Address.Text;
            description = addPhoneBook.Description.Text;

            PhoneBook phoneBook = new PhoneBook()
            {
                FirstName = firstName,
                SecondName = secondName,
                FatherName = fatherName,
                PhoneNumber = phoneNumber,
                Address = address,
                Description = description
            };

            return phoneBook;
        }




        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
