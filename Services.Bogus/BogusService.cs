using Bogus;
using Services.InMemory;

namespace Services.Bogus
{
    public class BogusService<T> : Service<T> where T : Models.Entity
    {
        public BogusService(Faker<T> faker) : base(faker.Generate(15))
        {

        }
    }
}
