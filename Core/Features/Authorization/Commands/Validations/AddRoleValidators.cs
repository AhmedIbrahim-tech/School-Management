namespace Core.Features.Authorization.Commands.Validations;

public class AddRoleValidators : AbstractValidator<AddRoleCommand>
{
    #region Fields
    private readonly IStringLocalizer<SharedResources> _stringLocalizer;
    private readonly IAuthorizationServiceAsync _authorizationService;
    #endregion
    #region Constructors

    #endregion
    public AddRoleValidators(IStringLocalizer<SharedResources> stringLocalizer,
                             IAuthorizationServiceAsync authorizationService)
    {
        _stringLocalizer = stringLocalizer;
        _authorizationService = authorizationService;
        ApplyValidationsRules();
        ApplyCustomValidationsRules();
    }

    #region Actions
    public void ApplyValidationsRules()
    {
        RuleFor(x => x.RoleName)
             .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
             .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required]);
    }

    public void ApplyCustomValidationsRules()
    {
        RuleFor(x => x.RoleName)
            .MustAsync(async (Key, CancellationToken) => !await _authorizationService.IsRoleExistByName(Key))
            .WithMessage(_stringLocalizer[SharedResourcesKeys.IsExist]);
    }

    #endregion
}
