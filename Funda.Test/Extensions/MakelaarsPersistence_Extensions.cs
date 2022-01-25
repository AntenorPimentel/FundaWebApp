using Bogus;
using Funda.Data.DTO;
using System.Linq;

namespace Funda.Test.Extensions
{
    public static class MakelaarsPersistence_Extensions
    {
        public static MakelaarsPersistence Build(this MakelaarsPersistence instance)
        {
            instance = new MakelaarsPersistence()
            {
                AccountStatus = 0,
                EmailNotConfirmed = false,
                ValidationFailed = false,
                ValidationReport = null,
                Website = 0,
                Metadata = new Metadata { ObjectType = "Koopwoningen", Omschrijving = "Koopwoningen > Amsterdam", Titel = "Huizen te koop in Amsterdam" },
                HouseForSale = new Faker<HouseForSalePersistence>()
                                    .RuleFor(b => b.MakelaarId, new Faker().IndexFaker)
                                    .RuleFor(b => b.MakelaarNaam, new Faker().Name.FullName().ToString())
                                    .Generate(25),
                Paging = new Paging() { AantalPaginas = 10, HuidigePagina = 1, VolgendeUrl = "/~/koop/amsterdam/p2/", VorigeUrl = null },
                TotaalAantalObjecten = (int)new Faker().Random.UInt()
            };

            return instance;
        }

        public static MakelaarsPersistence WithNull(this MakelaarsPersistence instance)
        {
            instance = null;

            return instance;
        }

        public static MakelaarsPersistence WithEmptyMakelaarNaam(this MakelaarsPersistence instance)
        {
            var houseForSale = new Faker<HouseForSalePersistence>()
                                .RuleFor(b => b.MakelaarId, new Faker().UniqueIndex)
                                .RuleFor(b => b.MakelaarNaam, string.Empty)
                                .Generate(1).ToList();

            instance.HouseForSale = houseForSale;

            return instance;
        }
    }
}