using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Features.Authentication.Queries.Requests
{
    public class AuthorizeUserQuery : IRequest<GenericBaseResponse<string>>
    {
        public string AccessToken { get; set; }
    }
}
