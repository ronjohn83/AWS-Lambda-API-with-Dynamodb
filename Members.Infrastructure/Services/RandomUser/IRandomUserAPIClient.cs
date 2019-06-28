using System.Threading.Tasks;
using Members.Infrastructure.DTOs;

namespace Members.Infrastructure.Services.RandomUser
{
    public interface IRandomUserApiClient
    {
        Task<JsonResult> GetData();
    }
}