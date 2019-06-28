using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Amazon.DynamoDBv2.Model;
using Members.Infrastructure.DTOs;

namespace Members.Infrastructure.Mappers
{
    public class Mapper : IMapper
    {
        public IEnumerable<MembersDTO> ToMembersDTO(ScanResponse response)
        {
            return response?.Items.Select(ToMembersDTO);
        }

        public IEnumerable<MembersDTO> ToMembersDTO(QueryResponse response)
        {
            return response?.Items.Select(ToMembersDTO);
        }

        public MembersDTO ToMembersDTO(GetItemResponse response)
        {
            if (response is null)
                return null;

            return new MembersDTO
            {
                Email = response.Item["Email"].S,
                Gender = response.Item["Gender"].S,
                Name = response.Item["Name"].S,
                Phone = response.Item["Phone"].S,
                Cell = response.Item["Cell"].S,
                Address = response.Item["Address"].S,
            };
        }

        private MembersDTO ToMembersDTO(Dictionary<string, AttributeValue> item)
        {
            if (item is null)
                return null;

            return new MembersDTO
            {
                Email = item["Email"].S,
                Gender = item["Gender"].S,
                Name = item["Name"].S,
                Phone = item["Phone"].S,
                Cell = item["Cell"].S,
                Address = item["Address"].S,
            };
        }

    }
}
