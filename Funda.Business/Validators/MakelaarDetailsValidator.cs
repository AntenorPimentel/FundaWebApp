using FluentValidation;
using Funda.Data.DTO;

namespace Funda.Business.Validators
{
    public class MakelaarsPersistenceValidator : AbstractValidator<MakelaarsPersistence>
    {
        public MakelaarsPersistenceValidator()
        {
            RuleForEach(x => x.HouseForSale).SetValidator(new HouseForSaleValidator());
        }
    }

    public class HouseForSaleValidator : AbstractValidator<HouseForSalePersistence>
    {
        public HouseForSaleValidator()
        {
            RuleFor(c => c.MakelaarId).NotNull().NotEmpty().WithMessage("MakelaarId is required");
            RuleFor(c => c.MakelaarNaam).NotNull().NotEmpty().WithMessage("MakelaarNaam is required");
        }
    }
}