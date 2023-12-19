namespace Core.Features.Students.Queries.Requests;

public class GetSingleStudentQuery : IRequest<GenericBaseResponse<GetSingleStudentResponse>>
{
    public int Id { get; set; }
    public GetSingleStudentQuery(int id)
    {
        this.Id = id;  
    }
}
