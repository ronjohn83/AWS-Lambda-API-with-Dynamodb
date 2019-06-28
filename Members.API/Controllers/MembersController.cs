using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Members.Infrastructure.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Members.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IMembersService _membersService;

        public MembersController(IMembersService membersService)
        {
            _membersService = membersService;
        }

        /// <summary>
        /// Retrieves all members.
        /// </summary>
        [HttpGet(Name = "GetAllMembers")]
        public async Task<IActionResult> GetMembers()
        {
            var members = await _membersService.GetAllMembers();

            return Ok(members);
        }


        /// <summary>
        /// Retrieves a specific member by email.
        /// </summary>
        /// <param name="email"></param>
        [HttpGet("{email}", Name = "GetMembersByEmail")]
        public async Task<IActionResult> GetMemberById(string email)
        {
            // Could be refactored for re usability. 
            var members = await _membersService.GetMemberById(email);
            if (members == null)
                return NotFound($"{email} does not exist. Please try again.");

            return Ok(members);
        }

        /// <summary>
        /// Creates a member by calling into https://randomuser.me/api/.
        /// </summary>
        [HttpPost(Name = "CreateMember")]
        public async Task<IActionResult> AddMember()
        {
            await _membersService.Add();

            return Ok();
        }


        /// <summary>
        /// Updates a specific member by email.
        /// </summary>
        /// <param name="email"></param>
        [HttpPut("{email}", Name = "UpdateMember")]
        public async Task<IActionResult> UpdateMember(string email, [FromBody] Core.Domain.Entities.Members member)
        {
            var memberFromRepo = await _membersService.GetMemberById(email);
            if (memberFromRepo == null)
                return NotFound($"{email} does not exist. Please try again.");

            await _membersService.Update(email, member);

            return NoContent();
        }


        /// <summary>
        /// Deletes a specific member by email.
        /// </summary>
        /// <param name="email"></param>
        [HttpDelete("{email}", Name = "DeleteMember")]
        public async Task<IActionResult> DeleteMember(string email)
        {
            var members = await _membersService.GetMemberById(email);
            if (members == null)
                return NotFound($"{email} does not exist. Please try again.");

            await _membersService.Delete(email);

            return Ok();
        }

    }
}