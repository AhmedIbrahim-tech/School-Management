using Core.Features.Department.Queries.Models;
using Core.Features.Department.Queries.Results;

namespace Core.Features.Department.Queries.Handlers;

public class DepartmentQueryHandler : GenericBaseResponseHandler, IRequestHandler<GetSingleDepartmentQuery, GenericBaseResponse<GetSingleDepartmentResponse>>
{
    public DepartmentQueryHandler(IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
    {
    }

    public Task<GenericBaseResponse<GetSingleDepartmentResponse>> Handle(GetSingleDepartmentQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
