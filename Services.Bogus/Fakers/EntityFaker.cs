using Bogus;

namespace Services.Bogus.Fakers
{
    public abstract class EntityFaker<T> : Faker<T> where T : Models.Entity
    {
        public EntityFaker() : base("pl")
        {
            RuleFor(e => e.Id, f => f.IndexFaker + 1);
        }
    }
}
