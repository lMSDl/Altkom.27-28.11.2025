using Bogus;
using Models;

namespace Services.Bogus
{
    public class UsersBogusService : BogusService<Models.User>
    {
        public UsersBogusService(Faker<User> faker) : base(faker)
        {
            Create(new User { Name = "admin", Password = "<b>admin</b>" });
        }
    }
}
