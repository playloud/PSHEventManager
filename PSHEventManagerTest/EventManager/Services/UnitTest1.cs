using Microsoft.VisualStudio.TestTools.UnitTesting;
using PSHEventManager.EventManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PSHEventManager.EventManager.Model;
using PSHEventManager.EventManager.Model.PostBody;

namespace PSHEventManager.EventManager.Services.Tests
{
    [TestClass()]
    public class UnitTest1
    {
        [TestMethod()]
        public void UpdateEventTest()
        {
            Event evt = new Event()
            {
                Address = "my new address",
                Capacity = 123,
                CreatedOn = DateTime.Now,
                Description = "234 should be changed",
                Id = "234"
            };

            DBServices.GetInstance().UpdateEvent(evt);
        }

        [TestMethod()]
        public void GetEventTest()
        {
            var evt =  DBServices.GetInstance().GetEvent("234");
            Console.WriteLine(evt.Description);
        }

        [TestMethod()]
        public void GetAllEventTest()
        {
            var evt = DBServices.GetInstance().GetAllEvents();
            foreach (Event e in evt)
            {
                e.Dump();
            }
        }

        [TestMethod()]
        public void guidTest()
        {
            Guid.NewGuid().ToString("N").Dump();
        }

        [TestMethod()]
        public void eventTest()
        {
            EventBody eb = new EventBody();
            eb.Dump();
        }


    }
}