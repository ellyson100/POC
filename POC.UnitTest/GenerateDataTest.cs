using Bogus;
using POC.API.Model;

namespace POC.UnitTest
{
    public class GenerateDataTest
    {
        public Product GetValid()
        {
            var pessoaGenerator = new Faker<Product>("pt_BR")
                .RuleFor(u => u.Id, (f, u) => 0)
                .RuleFor(u => u.Name, (f, u) => f.Lorem.Word())
                .RuleFor(u => u.Description, (f, u) => f.Lorem.Paragraph())
                .RuleFor(u => u.Price, (f, u) => f.Random.Decimal(1, 10000000000000000));

            return pessoaGenerator.Generate();
        }

        public Product GetInvalid()
        {
            return new Product(-1, "1", "1", -1);
        }
    }
}
