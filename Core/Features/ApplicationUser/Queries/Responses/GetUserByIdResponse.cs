using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Features.ApplicationUser.Queries.Responses;

public class GetUserByIdResponse : GetUserPaginationReponse
{
    public string? UserName { get; set; }
    public string? PhoneNumber { get; set; }
}
