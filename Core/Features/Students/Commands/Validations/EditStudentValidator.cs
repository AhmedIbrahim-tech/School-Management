﻿namespace Core.Features.Students.Commands.Validations;

public class EditStudentValidator : AbstractValidator<EditStudentCommand>
{
    #region Fields
    private readonly IStudentServices _studentServices;
    private readonly IStringLocalizer<SharedResources> _stringLocalizer;
    #endregion

    #region Contractor (s)
    public EditStudentValidator(IStudentServices studentServices, IStringLocalizer<SharedResources> stringLocalizer)
    {
        _studentServices = studentServices;
        _stringLocalizer = stringLocalizer;
        ApplyStudentValidatorRules();
        ApplyCutomeStudentValidatorRules();
    }
    #endregion

    #region Handler Function
    public void ApplyStudentValidatorRules()
    {
        RuleFor(x => x.NameEn)
            .NotEmpty().WithMessage("{PropertyName} : " + _stringLocalizer[SharedResourcesKeys.NotEmpty])
            .NotNull().WithMessage("{PropertyValue} : " + _stringLocalizer[SharedResourcesKeys.Required])
            .MaximumLength(250).WithMessage("Max Length 250");


        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("{PropertyName} : " + _stringLocalizer[SharedResourcesKeys.NotEmpty])
            .NotNull().WithMessage("{PropertyValue} : " + _stringLocalizer[SharedResourcesKeys.Required])
            .MaximumLength(250).WithMessage("Max Length 250");

    }

    public void ApplyCutomeStudentValidatorRules()
    {
        RuleFor(x => x.NameEn)
            .MustAsync(async (model, key, CancellationToken) => (!await _studentServices.IsExistNameExcuteSelfAsync(key, model.Id)))
            .WithMessage("{PropertyName} Is Already Exist");

        RuleFor(x => x.NameAr)
    .MustAsync(async (model, key, CancellationToken) => (!await _studentServices.IsExistNameExcuteSelfAsync(key, model.Id)))
    .WithMessage("{PropertyName} Is Already Exist");

    }
    #endregion

}
