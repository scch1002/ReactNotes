using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grace.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using ReactNotes.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReactNotes.Controllers
{
    [Route("api/[controller]")]
    public class NotesController : Controller
    {
        [HttpGet]
        public IEnumerable<Reminder> Get()
        {
            var  tableConfiguration = new TableConfiguration
            {
                ConfigurationString = @"DefaultEndpointsProtocol=https;AccountName=grace1002;AccountKey=oxFfz+dgsWONgXbVTX0KKeH0L5X4kBANp10siMiobJTt9Q6P8TXHw9wb7RDx3f3ALwMCjS4rpYu9Zwe8trYNPg==;EndpointSuffix=core.windows.net",
                TableName = "Reminders"
            };

            var repository = new ReminderRepository(tableConfiguration);

            return repository.GetReminders(new DateTime(1950, 01, 01), DateTime.Now);
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
