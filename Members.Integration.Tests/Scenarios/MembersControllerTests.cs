using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Members.Integration.Tests.Setup;
using Xunit;

namespace Members.Integration.Tests.Scenarios
{
    [Collection("api")]
    public class MembersControllerTests
    {
        private readonly TestContext _sut;

        public MembersControllerTests(TestContext sut)
        {
            _sut = sut;
        }

        [Fact]
        public async Task GetAllMembers_WhenCalled_ReturnMembers()
        {
            const string email = "test123@test.com";

            await AddMemberData(email);

            var response = await _sut.Client.GetAsync("members");

            Core.Domain.Entities.Members[] result;
            using (var content = response.Content.ReadAsStringAsync())
            {
                result = JsonConvert.DeserializeObject<Core.Domain.Entities.Members[]>(await content);
            }

            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetMember_WhenCalled_ReturnMember()
        {
            const string email = "test123@test.com";

            await AddMemberData(email);

            var response = await _sut.Client.GetAsync($"movies/{email}");

            Core.Domain.Entities.Members result;
            using (var content = response.Content.ReadAsStringAsync())
            {
                result = JsonConvert.DeserializeObject<Core.Domain.Entities.Members>(await content);
            }

//            Assert.Equal("", result);
        }

        [Fact]
        public async Task AddMember_ReturnsOkStatus()
        {
            const string email = "test123@test.com";
                    
            var response = await AddMemberData(email);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        private async Task<HttpResponseMessage> AddMemberData(string email)
        {
            var member = new Core.Domain.Entities.Members
            {
                Email = email,
                Gender = "Female",
//                Name = "Jasmine Lane"
            };

            var json = JsonConvert.SerializeObject(member);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            return await _sut.Client.PostAsync($"members/{email}", stringContent);
        }


    }
}
