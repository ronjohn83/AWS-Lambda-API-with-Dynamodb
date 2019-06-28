using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Members.Core.Repositories.Abstract;
using Members.Core.Domain.Entities;

namespace Members.Core.Repositories.Implementation
{

    public class MembersRepository : IMembersRepository
    {
        private const string TableName = "Members";
        private readonly IAmazonDynamoDB _dynamoDbClient;

        public MembersRepository(IAmazonDynamoDB dynamoDbClient)
        {
            _dynamoDbClient = dynamoDbClient;
        }

        public async Task<ScanResponse> GetAll()
        {
            return await _dynamoDbClient
                .ScanAsync(new ScanRequest(TableName));
        }

        public async Task<GetItemResponse> GetMemberById(string email)
        {
            var request = new GetItemRequest
            {
                TableName = TableName,
                Key = new Dictionary<string, AttributeValue>
                {
                    {"Email", new AttributeValue {S = email}}
                },
            };

            var result = await _dynamoDbClient.GetItemAsync(request);

            return result.Item.Count > 0 ? result : null;
        }

        public async Task Add(Domain.Entities.Members member)
        {
            var request = new PutItemRequest
             {
                TableName = TableName,
                Item = new Dictionary<string, AttributeValue>
                {
                    {"Email", new AttributeValue {S = member.Email}},
                    {"Gender", new AttributeValue {S = $"{char.ToUpper(member.Gender[0]) + member.Gender.Substring(1)}"}},
                    {"Name", new AttributeValue {S = 
                        $"{char.ToUpper(member.Name.Title[0]) + member.Name.Title.Substring(1)}. " +
                        $"{char.ToUpper(member.Name.First[0]) + member.Name.First.Substring(1)} " +
                        $"{char.ToUpper(member.Name.Last[0]) + member.Name.Last.Substring(1)}"}
                    },
                    {"Phone", new AttributeValue {S = member.Phone}},
                    {"Cell", new AttributeValue{S = member.Cell}},
                    {"Address", new AttributeValue{S = 
                        $"{member.Location.Street} {member.Location.City} " +
                        $"{member.Location.State} {member.Location.PostCode}" 

                    }}
                }
             };

             await _dynamoDbClient.PutItemAsync(request);
            }

        public async Task Update(string email, Domain.Entities.Members updateRequest)
        {
            var request = new UpdateItemRequest
                {
                    TableName = TableName,
                    Key = new Dictionary<string, AttributeValue>
                {
                    {"Email", new AttributeValue {S = email}}
                },
                    AttributeUpdates = new Dictionary<string, AttributeValueUpdate>
                {
                { "Name", new AttributeValueUpdate
                    {
                        Action = AttributeAction.PUT,
                        Value = new AttributeValue {S = $"{char.ToUpper(updateRequest.Name.Title[0]) + updateRequest.Name.Title.Substring(1)}. " +
                                                        $"{char.ToUpper(updateRequest.Name.First[0]) + updateRequest.Name.First.Substring(1)} " +
                                                        $"{char.ToUpper(updateRequest.Name.Last[0]) + updateRequest.Name.Last.Substring(1)}" }
                    }
                },
                { "Gender", new AttributeValueUpdate
                    {
                        Action = AttributeAction.PUT,
                        Value = new AttributeValue {S = $"{char.ToUpper(updateRequest.Gender[0]) + updateRequest.Gender.Substring(1)}"}
                    }
                },
                { "Phone", new AttributeValueUpdate
                    {
                        Action = AttributeAction.PUT,
                        Value = new AttributeValue {S = updateRequest.Phone}
                    }
                },
                { "Cell", new AttributeValueUpdate
                    {
                        Action = AttributeAction.PUT,
                        Value = new AttributeValue {S = updateRequest.Cell}
                    }
                },
                { "Address", new AttributeValueUpdate
                    {
                        Action = AttributeAction.PUT,
                        Value = new AttributeValue {S = $"{updateRequest.Location.Street} {updateRequest.Location.City} " +
                                                        $"{updateRequest.Location.State} {updateRequest.Location.PostCode}" }
                    }
                }
                }
                };

                await _dynamoDbClient.UpdateItemAsync(request);
            }

        public async Task Delete(string email)
        {
            var request = new DeleteItemRequest()
            {
                TableName = TableName,
                Key = new Dictionary<string, AttributeValue>
                {
                    {"Email", new AttributeValue {S = email}}
                },

            };

            await _dynamoDbClient.DeleteItemAsync(request);
        }
    }
}
