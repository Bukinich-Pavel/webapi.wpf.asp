using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Task22WebApi.Models;

namespace Task22WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase
    {
        private readonly ApplicationContext phoneBooks;
        private HttpClient HttpClient { get; set; }

        public ValuesController(ApplicationContext context)
        {
            phoneBooks = context;
            HttpClient = new HttpClient();
        }


        // GET api/values
        [HttpGet]
        public IEnumerable<PhoneBook> Get()
        {
            return phoneBooks.PhoneBooks;
        }


        // GET api/values/5
        [HttpGet("{id}")]
        public PhoneBook GetPhoneBooksById(int id)
        {
            var list = phoneBooks.PhoneBooks.ToList();
            return list.Find(x => x.Id == id);
        }


        // POST api/values
        [HttpPost]
        public void Post([FromBody] PhoneBook value)
        {
            phoneBooks.PhoneBooks.Add(value);
            phoneBooks.SaveChanges();
        }

        // PUT api/values
        [HttpPut]
        public void Put([FromBody] PhoneBook phoneBook)
        {
            phoneBooks.PhoneBooks.Update(phoneBook);
            phoneBooks.SaveChanges();

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            PhoneBook phoneBook = new PhoneBook() { Id = id };
            phoneBooks.PhoneBooks.Remove(phoneBook);
            phoneBooks.SaveChanges();
        }

    }
}
