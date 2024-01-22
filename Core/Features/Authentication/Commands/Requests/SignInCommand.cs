using Azure;
using Data.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Features.Authentication.Commands.Requests
{
    public class SignInCommand : IRequest<GenericBaseResponse<string>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
