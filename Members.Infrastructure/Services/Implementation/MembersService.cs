using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Members.Core.Domain.Entities;
using Members.Core.Repositories.Abstract;
using Members.Core.Repositories.Implementation;
using Members.Infrastructure.DTOs;
using Members.Infrastructure.Mappers;
using Members.Infrastructure.Services.Abstract;
using Members.Infrastructure.Services.RandomUser;
using Members = Members.Core.Domain.Entities.Members;

namespace Members.Infrastructure.Services.Implementation
{
    public class MembersService : IMembersService
    {
        private readonly IMembersRepository _repo;
        private readonly IRandomUserApiClient _apiClient;
        private readonly IMapper _mapper;

        public MembersService(
            IMembersRepository repo, 
            IRandomUserApiClient apiClient,
            IMapper mapper)
        {
            _repo = repo;
            _apiClient = apiClient;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MembersDTO>> GetAllMembers()
        {
            var members = await _repo.GetAll();

            return _mapper.ToMembersDTO(members);
        }

        public async Task<MembersDTO> GetMemberById(string email)
        {
            var members = await _repo.GetMemberById(email);

            return _mapper.ToMembersDTO(members);
        }

        public async Task Add()
        {
            await _repo.Add(await GetMemberFromApi());
        }

        public async Task Update(string email, Core.Domain.Entities.Members member)
        {
            await _repo.Update(email, member);
        }

        public async Task Delete(string email)
        {
            await _repo.Delete(email);
        }

        private async Task<Core.Domain.Entities.Members> GetMemberFromApi()
        {
            var result = await _apiClient.GetData();

            return new Core.Domain.Entities.Members
            {
                Name = new Core.Domain.Entities.Name()
                {
                    Title = $"{result.Results[0].Name.Title}",
                    First = $"{result.Results[0].Name.First}",
                    Last = $"{result.Results[0].Name.Last}"
                },
                Email = $"{result.Results[0].Email}",
                Gender = $"{result.Results[0].Gender}",
                Phone = $"{result.Results[0].Phone}",
                Cell = $"{result.Results[0].Cell}",
                Location = new Core.Domain.Entities.Location
                {
                    Street = $"{result.Results[0].Location.Street}",
                    City = $"{result.Results[0].Location.City}",
                    State = $"{result.Results[0].Location.State}",
                    PostCode = $"{result.Results[0].Location.PostCode}",
                }
            };
        }

    }
}
