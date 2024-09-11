namespace Core.BaseResponse;

public class GenericBaseResponse<T>
{


    public GenericBaseResponse()
    {

    }

    public GenericBaseResponse(T data, string message = null)
    {
        Succeeded = true;
        Message = message;
        Data = data;
    }

    public GenericBaseResponse(string message)
    {
        Succeeded = false;
        Message = message;
    }

    public GenericBaseResponse(bool succeeded, string message)
    {
        Succeeded = succeeded;
        Message = message;
    }
    
    public GenericBaseResponse(bool succeeded, HttpStatusCode statusCode, string message, T data, List<string> errors, object meta)
    {
        StatusCode = statusCode;
        Meta = meta;
        Message = message;
        Succeeded = succeeded;
        Errors = errors;
        Data = data;
    }

    public HttpStatusCode StatusCode { get; set; }
    public object Meta { get; set; }
    public string Message { get; set; }
    public bool Succeeded { get; set; }
    public List<string> Errors { get; set; }
    public T Data { get; set; }
}
