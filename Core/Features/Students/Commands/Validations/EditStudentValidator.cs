namespace Core.Features.Students.Commands.Validations;

public class EditStudentValidator : AbstractValidator<EditStudentCommand>
{
    #region Fields
    private readonly IStudentServices _studentServices;
    #endregion

    #region Contractor (s)
    public EditStudentValidator(IStudentServices studentServices)
    {
        _studentServices = studentServices;
        ApplyStudentValidatorRules();
        ApplyCutomeStudentValidatorRules();
    }
    #endregion

    #region Handler Function
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
            .MustAsync(async (model, key, CancellationToken) => (!await _studentServices.IsExistNameExcuteSelfAsync(key, model.Id)))
            .WithMessage("{PropertyName} Is Already Exist");
    }
    #endregion

}
