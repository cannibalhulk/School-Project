using FluentValidation;
using LibraryWebApi.Models;

namespace LibraryWebApi.Services;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(user => user.FirstName)
            .NotEmpty().WithMessage("Имя обязательно для заполнения.")
            .MaximumLength(50).WithMessage("Максимальная длина имени - 50 символов.");

        RuleFor(user => user.LastName)
            .NotEmpty().WithMessage("Фамилия обязательна для заполнения.")
            .MaximumLength(50).WithMessage("Максимальная длина фамилии - 50 символов.");
    }
}