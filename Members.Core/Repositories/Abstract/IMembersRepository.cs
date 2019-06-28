using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.Model;

namespace Members.Core.Repositories.Abstract
{
    public interface IMembersRepository
    {
        Task<ScanResponse> GetAll();

        Task<GetItemResponse> GetMemberById(string email);

        Task Add(Domain.Entities.Members member);

        Task Update(string email, Domain.Entities.Members updateRequest);

        Task Delete(string email);
    }
}
