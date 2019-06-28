using System.Collections.Generic;
using Amazon.DynamoDBv2.Model;
using Members.Infrastructure.DTOs;

namespace Members.Infrastructure.Mappers
{
    public interface IMapper
    {
        IEnumerable<MembersDTO> ToMembersDTO(ScanResponse response);
        IEnumerable<MembersDTO> ToMembersDTO(QueryResponse response);
        MembersDTO ToMembersDTO(GetItemResponse response);
    }
}