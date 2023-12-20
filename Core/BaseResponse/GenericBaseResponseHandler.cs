namespace Core.BaseResponse;

public class GenericBaseResponseHandler
{
    private readonly IStringLocalizer<SharedResources> _stringLocalizer;

    public GenericBaseResponseHandler(IStringLocalizer<SharedResources> stringLocalizer)
    {
        _stringLocalizer = stringLocalizer;
    }

    public GenericBaseResponse<T> Delete<T>()
    {
        return new GenericBaseResponse<T>()
        {
            StatusCode = HttpStatusCode.OK,
            Succeeded = true,
            Message = _stringLocalizer[SharedResourcesKeys.Deleted]
        };
    }

    public GenericBaseResponse<T> Success<T>(T entity, object Meta = null)
    {
        return new GenericBaseResponse<T>()
        {
            StatusCode = HttpStatusCode.OK,
            Succeeded = true,
            Message = _stringLocalizer[SharedResourcesKeys.Success],
            Data = entity,
            Meta = Meta
        };
    }

    public GenericBaseResponse<T> Unauthorized<T>()
    {
        return new GenericBaseResponse<T>()
        {
            StatusCode = HttpStatusCode.Unauthorized,
            Succeeded = true,
            Message = _stringLocalizer[SharedResourcesKeys.UnAuthorized]
        };
    }
    public GenericBaseResponse<T> BadRequest<T>(string Message = null)
    {
        return new GenericBaseResponse<T>()
        {
            StatusCode = HttpStatusCode.BadRequest,
            Succeeded = false,
            Message = Message == null ? _stringLocalizer[SharedResourcesKeys.BadRequest] : Message
        };
    }

    public GenericBaseResponse<T> UnprocessableEntity<T>(string Message = null)
    {
        return new GenericBaseResponse<T>()
        {
            StatusCode = HttpStatusCode.UnprocessableEntity,
            Succeeded = false,
            Message = Message == null ? "Un-processable Entity" : Message
        };
    }

    //public GenericBaseResponse<T> AlreadyExit<T>(string Message = null)
    //{
    //    return new GenericBaseResponse<T>()
    //    {
    //        StatusCode = HttpStatusCode.OK,
    //        Succeeded = false,
    //        Message = Message == null ? _stringLocalizer[SharedResourcesKeys.AlreadyExit] : Message
    //    };
    //}

    public GenericBaseResponse<T> NotFound<T>(string message = null)
    {
        return new GenericBaseResponse<T>()
        {
            StatusCode = HttpStatusCode.NotFound,
            Succeeded = false,
            Message = message == null ? _stringLocalizer[SharedResourcesKeys.NotFound] : message
        };
    }

    public GenericBaseResponse<T> Created<T>(T entity, object Meta = null)
    {
        return new GenericBaseResponse<T>()
        {
            Data = entity,
            StatusCode = HttpStatusCode.Created,
            Succeeded = true,
            Message = _stringLocalizer[SharedResourcesKeys.Created],
            Meta = Meta
        };
    }

    public GenericBaseResponse<T> Updated<T>(T entity, object Meta = null)
    {
        return new GenericBaseResponse<T>()
        {
            Data = entity,
            StatusCode = HttpStatusCode.OK,
            Succeeded = true,
            Message = _stringLocalizer[SharedResourcesKeys.Updated],
            Meta = Meta
        };
    }
}
