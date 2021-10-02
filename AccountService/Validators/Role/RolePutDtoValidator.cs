using AccountService.DTOs.AccRole;
using FluentValidation;

namespace AccountService.Validators.Role
{
    public class RolePutDtoValidator : AbstractValidator<AccRolePutDTO>
    {
        public RolePutDtoValidator()
        {
            RuleFor(r => r.Id)
                .NotNull();
            RuleFor(r => r.Name)
                .MinimumLength(1)
                .MaximumLength(15);
        }
    }
}
