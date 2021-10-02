using AccountService.DTOs.Account;
using FluentValidation;

namespace AccountService.Validators.Role
{
    public class AccountPutDtoValidator : AbstractValidator<AccountPutDTO>
    {
        public AccountPutDtoValidator()
        {
            RuleFor(e => e.Id)
                .NotNull();
            RuleFor(e => e.FirstName)
                .MinimumLength(1)
                .MaximumLength(15);
            RuleFor(e => e.LastName)
                .MinimumLength(1)
                .MaximumLength(15);
            RuleFor(e => e.StreetAddress)
                .MinimumLength(1)
                .MaximumLength(35);
            RuleFor(e => e.Email)
                .MinimumLength(1)
                .MaximumLength(35);
            RuleFor(e => e.Password)
                .MinimumLength(1)
                .MaximumLength(15);
            RuleFor(e => e.RoleId)
                .NotNull();
        }
    }
}
