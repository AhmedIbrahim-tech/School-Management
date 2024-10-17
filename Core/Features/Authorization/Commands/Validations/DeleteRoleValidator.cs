namespace Core.Features.Authorization.Commands.Validations;

public class DeleteRoleValidator : AbstractValidator<DeleteRoleCommand>
{
    #region Fields
    private readonly IStringLocalizer<SharedResources> _stringLocalizer;
    public readonly IAuthorizationServiceAsync _authorizationService;

    #endregion
    #region Constructors
    public DeleteRoleValidator(IStringLocalizer<SharedResources> stringLocalizer, IAuthorizationServiceAsync authorizationService)
    {
        _stringLocalizer = stringLocalizer;
        _authorizationService = authorizationService;
        ApplyValidationsRules();
        ApplyCustomValidationsRules();
    }
    #endregion
    #region  Functions
    public void ApplyValidationsRules()
    {
        RuleFor(x => x.Id)
             .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
             .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required]);
    }
    public void ApplyCustomValidationsRules()
    {
        //RuleFor(x => x.Id)
        //    .MustAsync(async (Key, CancellationToken) => await _authorizationService.IsRoleExistById(Key))
        //    .WithMessage(_stringLocalizer[SharedResourcesKeys.RoleNotExist]);
    }
    #endregion
}
