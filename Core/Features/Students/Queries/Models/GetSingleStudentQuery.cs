namespace Core.Features.Students.Queries.Models;

public class GetSingleStudentQuery : IRequest<GenericBaseResponse<GetSingleStudentResponse>>
{
    public int Id { get; set; }
    public GetSingleStudentQuery(int id)
    {
        this.Id = id;  
    }
}
