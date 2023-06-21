namespace Core.Features.Students.Commands.Validations;

public class AddStudentValidator : AbstractValidator<AddStudentCommand>
{
    public AddStudentValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("{PropertyName} not be Empty")
            .NotNull().WithMessage("{PropertyValue} not be Null")
            .MaximumLength(250).WithMessage("Max Length 250");


        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("{PropertyName} not be Empty")
            .NotNull().WithMessage("{PropertyValue} not be Null")
            .MaximumLength(250).WithMessage("Max Length 250");
    }
}
