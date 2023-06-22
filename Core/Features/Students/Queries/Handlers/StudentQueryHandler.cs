﻿using Core.Wrappers;
using Data.Entities;
using System.Linq.Expressions;

namespace Core.Features.Students.Queries.Handlers;

public class StudentQueryHandler : GenericBaseResponseHandler,
                                   IRequestHandler<GetStudentListQuery, GenericBaseResponse<List<GetStudentListResponse>>>,
                                   IRequestHandler<GetSingleStudentQuery, GenericBaseResponse<GetSingleStudentResponse>>,
                                   IRequestHandler<GetStudentPaginationListQuery, PaginationResult<GetStudentPaginationListResponse>>
{
    #region Fields
    private readonly IStudentServices _studentServices;
    private readonly IMapper _mapper;
    #endregion

    #region Contractor
    public StudentQueryHandler(IStudentServices studentServices, IMapper mapper)
    {
        this._studentServices = studentServices;
        this._mapper = mapper;
    }
    #endregion

    #region Handler Function
    public async Task<GenericBaseResponse<List<GetStudentListResponse>>> Handle(GetStudentListQuery request, CancellationToken cancellationToken)
    {
        var response = await _studentServices.GetStudentsListAsync();
        var MapperObj = _mapper.Map<List<GetStudentListResponse>>(response);
        return Success(MapperObj);
    }

    public async Task<GenericBaseResponse<GetSingleStudentResponse>> Handle(GetSingleStudentQuery request, CancellationToken cancellationToken)
    {
        var response = await _studentServices.GetStudentsByIdAsync(request.Id);
        if (response == null) return NotFound<GetSingleStudentResponse>("Object Not Found");
        var MapperObj = _mapper.Map<GetSingleStudentResponse>(response);
        return Success(MapperObj);

    }

    public Task<PaginationResult<GetStudentPaginationListResponse>> Handle(GetStudentPaginationListQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<Student, GetStudentPaginationListResponse>> expression = e => new GetStudentPaginationListResponse(e.StudID, e.Name, e.Address, e.Phone, e.Department.DName);
        var FilterQuery = _studentServices.FilterStudentsPaginationQueryAbleAsync(request.Search);
        var PaginationList = FilterQuery.Select(expression).ToPaginationListAsync(request.PageNumber, request.PageSize);
        return PaginationList;
    }
    #endregion
}
