namespace Core.Features.Students.Queries.Responses;

//public record GetStudentPaginationListResponse(int StudID, string? Name, string? Address, string? Phone, string? DepartmentName);
public class GetStudentPaginationListResponse
{
    public int StudID { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? DepartmentName { get; set; }
}
