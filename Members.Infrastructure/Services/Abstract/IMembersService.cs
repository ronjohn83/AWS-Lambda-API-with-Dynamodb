using System.Collections.Generic;
using System.Threading.Tasks;
using Members.Infrastructure.DTOs;

namespace Members.Infrastructure.Services.Abstract
{
    public interface IMembersService
    {
        Task<IEnumerable<MembersDTO>> GetAllMembers();

        Task<MembersDTO> GetMemberById(string email);

        Task Add();

        Task Update(string email, Core.Domain.Entities.Members member);

        Task Delete(string email);
    }
}