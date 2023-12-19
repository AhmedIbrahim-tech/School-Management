namespace Core.Features.Students.Queries.Responses;

public record GetStudentPaginationListResponse(int StudID, string? Name, string? Address, string? Phone, string? DepartmentName);
