using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CreditApprover.Models;

namespace CreditApprover.Controllers
{
    public class ValuesController : ApiController
    {
        private LoundraMateDbEntities db;
        public ValuesController()
        {
            db = new LoundraMateDbEntities();

        }    // GET api/values
        public IEnumerable<Customer> Get()
        {

            return db.Customers;
        }

        // GET api/values/5
        public Customer Get(int id)
        {
            var customer = db.Customers.FirstOrDefault(s => s.Id == id);

            return customer;
        }

        // POST api/values
        public void Post(Customer value)
        {
            value.Id = Convert.ToInt32(db.Customers.Count()) + 1;
            db.Customers.Add(value);
            db.SaveChanges();

        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
