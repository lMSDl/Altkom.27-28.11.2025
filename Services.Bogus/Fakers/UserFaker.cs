using Models;

namespace Services.Bogus.Fakers
{
    public class UserFaker : EntityFaker<User>
    {
        public UserFaker()
        {
            RuleFor(u => u.Name, f => f.Internet.UserName());
            RuleFor(u => u.Password, f => f.Internet.Password());
        }
    }
}
