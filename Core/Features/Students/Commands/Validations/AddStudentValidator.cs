
namespace Core.Features.Students.Commands.Validations;

public class AddStudentValidator : AbstractValidator<AddStudentCommand>
{
    private readonly IStudentServices _studentServices;

    public AddStudentValidator(IStudentServices studentServices)
    {
        _studentServices = studentServices;
        ApplyStudentValidatorRules();
        ApplyCutomeStudentValidatorRules();
    }

    public void ApplyStudentValidatorRules()
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

    public void ApplyCutomeStudentValidatorRules()
    {
        RuleFor(x => x.Name)
            .MustAsync(async (key, CancellationToken) => (!await _studentServices.IsExistNameAsync(key))).WithMessage("{PropertyName} Is Already Exist");
    }
}
