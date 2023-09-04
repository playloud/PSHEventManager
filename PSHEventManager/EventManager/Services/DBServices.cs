using LiteDB;
using Microsoft.Win32;
using PSHEventManager.EventManager.Model;
using PSHEventManager.EventManager.Model.PostBody;
using PSHEventManager.Pages;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace PSHEventManager.EventManager.Services;

// this service doesnt need to be distributed
// single instance is enough
public class DBServices
{
    // PSH 09/03/23 : You can specify DB location
    //private string dbFilePath = @"C:\Users\SEHOPARK\source\repos\PSHEventManager\PSHEventManager\litedb\eventdb.db";
    private string dbFilePath = @"eventdb.db";

    private DBServices()
    {

    }

    private static DBServices instance = null;


    public static DBServices GetInstance()
    {
        if (instance == null)
            instance = new DBServices();
        return instance;
    }


    public async Task<bool> Autheticate(string userName, string password)
    {
        return true;
    }

    public async Task<bool> CreateUser(UserBody userb)
    {
        using (var db = new LiteDatabase(this.dbFilePath))
        {
            var collection = db.GetCollection<User>(nameof(User));

            int userCheck = collection.Count(a => a.Email == userb.Email);
            if (userCheck > 0) 
                return false;
            
            var user = new User()
            {
                Id = Guid.NewGuid().ToString("N"),
                Email = userb.Email,
                Name = userb.Name,
                Password = userb.Password //need to save it
            };
            collection.Insert(user);
            return true;
        }
        return false;
    }


    public User GetUser(string userId)
    {
        using (var db = new LiteDatabase(this.dbFilePath))
        {
            User user = db.GetCollection<User>(nameof(User)).Query().Where(a => a.Id == userId).First();
            return user;
        }
        return null;
    }

    public User GetUserByEmail(string userEmail)
    {
        using (var db = new LiteDatabase(this.dbFilePath))
        {
            User user = db.GetCollection<User>(nameof(User)).Query().Where(a => a.Email == userEmail).First();
            return user;
        }
        return null;
    }

    public Event CreateEvent(EventBody eventBody)
    {
        var evt = new Event
        {
            Address = eventBody.Address,
            Capacity = 123,
            CreatedOn = DateTime.Now,
            Description = eventBody.Description,
            EndDate = eventBody.EndDate,
            Id = Guid.NewGuid().ToString("N"),
            OrganizerId = 1,
            StartDate = eventBody.StartDate,
            Title = eventBody.Title
        };


        using (var db = new LiteDatabase(this.dbFilePath))
        {
            var collection = db.GetCollection<Event>("Events");
            collection.Insert(evt);
        }
        return evt;
    }

    public Event GetEvent(string eventId)
    {
        using (var db = new LiteDatabase(this.dbFilePath))
        {
            var collection = db.GetCollection<Event>("Events");
            var results = collection.Query().Where(a=>a.Id == eventId);
            if(results.Exists())
                return results.First();
        }
        return null;
    }

    public IEnumerable<Event> GetAllEvents()
    {
        using (var db = new LiteDatabase(this.dbFilePath))
        {
            var collection = db.GetCollection<Event>("Events");
            var list = new List<Event>();
            list.AddRange(collection.FindAll());
            return list;
        }

        return null;
    }

    public bool UpdateEvent(Event myEvent)
    {
        using (var db = new LiteDatabase(this.dbFilePath))
        {
            var collection = db.GetCollection<Event>("Events");
            Event exist = collection.FindOne(a => a.Id == myEvent.Id);
            if (exist != null)
            {
                collection.Upsert(myEvent);
            }
            else
            {
                collection.Insert(myEvent);
            }
        }
        return true;
    }

    public IEnumerable<User> GetAllUsers()
    {
        using (var db = new LiteDatabase(this.dbFilePath))
        {
            var users = new List<User>();
            users.AddRange(db.GetCollection<User>(nameof(User)).Query().ToEnumerable());
            return users;
        }
        return null;
    }

    public IEnumerable<Event> GetEventByDate(DateTime? date)
    {
        var results = new List<Event>();

        using (var db = new LiteDatabase(this.dbFilePath))
        {
            var collection = db.GetCollection<Event>("Events");
            results.AddRange(collection.Query().Where(a => a.StartDate <= date && date <= a.EndDate).ToEnumerable());
        }
        return results;
    }

    public Event GeteventById(string eventId)
    {
        Event evt = null;
        using (var db = new LiteDatabase(this.dbFilePath))
        {
            var collection = db.GetCollection<Event>("Events");
            evt = collection.Query().Where(a => a.Id == eventId).FirstOrDefault();
        }

        return evt;
    }

    public Registration CreateRegistration(string eventId, string userEmail)
    {
        var register = new Registration()
        {
            Id = Guid.NewGuid().ToString("N"),
            CreatedOn = DateTime.Now,
            EventId = eventId,
            IsDeleted = false,
            Useremail = userEmail
        };
        using (var db = new LiteDatabase(this.dbFilePath))
        {
            var collection = db.GetCollection<Registration>(nameof(Registration));
            collection.Insert(register);
        }
        return register;
    }

    public IEnumerable<Registration> GetRegistrationByUserEmail(string useremail)
    {
        var results = new List<Registration>();
        using (var db = new LiteDatabase(this.dbFilePath))
        {
            var collection = db.GetCollection<Registration>(nameof(Registration));
            results.AddRange(collection.Query().Where(r => r.Useremail == useremail && !r.IsDeleted).ToEnumerable());
        }
        return results;
    }

    public IEnumerable<Registration> GetRegistrationByEventId(string eventId)
    {
        var results = new List<Registration>();
        using (var db = new LiteDatabase(this.dbFilePath))
        {
            var collection = db.GetCollection<Registration>(nameof(Registration));
            results.AddRange(collection.Query().Where(r => r.EventId == eventId && !r.IsDeleted).ToEnumerable());
        }
        return results;
    }

    public IEnumerable<Registration> GetRegistrationByEventIdUserEmail(string eventId, string userEmail)
    {
        var results = new List<Registration>();
        using (var db = new LiteDatabase(this.dbFilePath))
        {
            var collection = db.GetCollection<Registration>(nameof(Registration));
            results.AddRange(collection
                .Query()
                .Where(r => r.EventId == eventId && r.Useremail == userEmail && !r.IsDeleted)
                .ToEnumerable());
        }
        return results;
    }

    public IEnumerable<Registration> CancelRegistrationByEventIdUserEmail(string eventId, string userEmail)
    {
        var results = new List<Registration>();
        using (var db = new LiteDatabase(this.dbFilePath))
        {
            var collection = db.GetCollection<Registration>(nameof(Registration));

            var regis = collection.Query().Where(r => r.EventId == eventId && r.Useremail == userEmail).ToEnumerable();
            foreach (Registration r in regis)
            {
                r.IsDeleted = true;
                collection.Upsert(r);
            }
            results.AddRange(collection.Query().Where(r => r.EventId == eventId && r.Useremail == userEmail).ToEnumerable());
        }
        return results;
    }


}