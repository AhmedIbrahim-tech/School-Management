
namespace Core.Features.Students.Commands.Validations;

public class AddStudentValidator : AbstractValidator<AddStudentCommand>
{
    private readonly IStudentServices _studentServices;
    private readonly IStringLocalizer<SharedResources> _stringLocalizer;

    public AddStudentValidator(IStudentServices studentServices, IStringLocalizer<SharedResources> stringLocalizer)
    {
        _studentServices = studentServices;
        _stringLocalizer = stringLocalizer;
        ApplyStudentValidatorRules();
        ApplyCutomeStudentValidatorRules();
    }

    public void ApplyStudentValidatorRules()
    {
        RuleFor(x => x.NameEn)
            .NotEmpty().WithMessage("{PropertyName} : " + _stringLocalizer[SharedResourcesKeys.NotEmpty])
            .NotNull().WithMessage("{PropertyValue} : " + _stringLocalizer[SharedResourcesKeys.NotNull])
            .MaximumLength(250).WithMessage("Max Length 250");


        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("{PropertyName} : " + _stringLocalizer[SharedResourcesKeys.NotEmpty])
            .NotNull().WithMessage("{PropertyValue} : " + _stringLocalizer[SharedResourcesKeys.NotNull])
            .MaximumLength(250).WithMessage("Max Length 250");

    }

    public void ApplyCutomeStudentValidatorRules()
    {
        RuleFor(x => x.NameEn)
            .MustAsync(async (key, CancellationToken) => (!await _studentServices.IsExistNameAsync(key))).WithMessage("{PropertyName} Is Already Exist");

        RuleFor(x => x.NameAr)
            .MustAsync(async (key, CancellationToken) => (!await _studentServices.IsExistNameAsync(key))).WithMessage("{PropertyName} Is Already Exist");

    }
}
