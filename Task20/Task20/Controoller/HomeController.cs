using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Task20.Models;

namespace Task20.Controoller
{
    public class HomeController : Controller
    {
        private HttpClient HttpClient { get; set; }
        private readonly ApplicationContext phoneBooks;
        private string url;

        #region
        public List<PhoneBook> list = new List<PhoneBook>();
        public PhoneBook allarm = new PhoneBook()
        {
            Id = -1,
            FirstName = "Ошибка",
            SecondName = "Ошибка",
            FatherName = "Ошибка",
            PhoneNumber = "Ошибка",
            Address = "Ошибка",
            Description = "Ошибка",
        };

        #endregion

        public HomeController(ApplicationContext phoneBooks)
        {
            this.phoneBooks = phoneBooks;
            HttpClient = new HttpClient();
            url = @"https://localhost:44352/api";
            this.list.Add(allarm);
        }


        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                string json = HttpClient.GetStringAsync(url + @"/values").Result;
                return View(JsonConvert.DeserializeObject<IEnumerable<PhoneBook>>(json));
            }
            catch
            {
                return View();
            }


            #region
            //return View(phoneBooks.PhoneBooks);
            #endregion
        }


        [HttpGet]
        public IActionResult FullInfo(int Id)
        {
            try
            {
                string json = HttpClient.GetStringAsync(url + @"/values/" + $"{Id}").Result;
                return View(JsonConvert.DeserializeObject<PhoneBook>(json));
            }
            catch
            {
                return View(allarm);
            }

            #region
            //return View(phoneBooks.PhoneBooks.FirstOrDefault<PhoneBook>(p => p.Id == Id));
            #endregion
        }


        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult Delete(int Id)
        {
            try
            {
                var r = HttpClient.DeleteAsync(url + @"/values/" + $"{Id}").Result;
            }
            catch
            {

            }
            return RedirectToAction("Index", "Home");

            #region
            //PhoneBook phoneBook = new PhoneBook() { Id = Id };
            //phoneBooks.PhoneBooks.Remove(phoneBook);
            //phoneBooks.SaveChanges();
            //return RedirectToAction("Index", "Home");
            #endregion
        }


        [Authorize]
        [HttpGet]
        public IActionResult AddPhoneBook()
        {
            return View();
        }


        [Authorize]
        [HttpPost]
        public IActionResult AddPhoneBook(PhoneBook phoneBook)
        {
            try
            {
                var r = HttpClient.PostAsync(
                    requestUri: url + @"/values/",
                    content: new StringContent(JsonConvert.SerializeObject(phoneBook), Encoding.UTF8,
                    mediaType: "application/json")
                    ).Result;
            }
            catch
            {

            }
            return RedirectToAction("Index", "Home");

            #region
            //if (ModelState.IsValid)
            //{
            //    phoneBooks.PhoneBooks.Add(phoneBook);
            //    phoneBooks.SaveChanges();
            //}
            //return RedirectToAction("Index", "Home");
            #endregion
        }


        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult EditPhoneBook(int? Id)
        {
            try
            {
                if (Id != null)
                {
                    string json = HttpClient.GetStringAsync(url + @"/values/" + $"{Id}").Result;
                    var phoneBook = JsonConvert.DeserializeObject<PhoneBook>(json);

                    if (phoneBook != null) return View(phoneBook);
                }
            }
            catch
            {

            }
            return NotFound();
        }


        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult EditPhoneBook(PhoneBook phoneBook)
        {
            var r = HttpClient.PutAsync(
                requestUri: url + @"/values",
                content: new StringContent(JsonConvert.SerializeObject(phoneBook), Encoding.UTF8,
                mediaType: "application/json")
                ).Result;
            return RedirectToAction("Index", "Home");

            #region
            //if (ModelState.IsValid)
            //{
            //    phoneBooks.PhoneBooks.Update(phoneBook);
            //    phoneBooks.SaveChanges();
            //}
            //return RedirectToAction("Index", "Home");
            #endregion
        }
    }
}
