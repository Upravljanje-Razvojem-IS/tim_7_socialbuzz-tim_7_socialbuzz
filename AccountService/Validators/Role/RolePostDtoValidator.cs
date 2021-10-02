using AccountService.DTOs.AccRole;
using FluentValidation;

namespace AccountService.Validators.Role
{
    public class RolePostDtoValidator : AbstractValidator<AccRolePostDTO>
    {
        public RolePostDtoValidator()
        {
            RuleFor(r => r.Name)
                .MinimumLength(1)
                .MaximumLength(15);
        }
    }
}
