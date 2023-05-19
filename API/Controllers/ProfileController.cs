
using API.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryAplication.Activities;
using RepositoryAplication.DTO;

namespace API.Controllers
{
  
    public class ProfileController : BaseController
    {

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] AddPhoto.Comand comand)
        {
            return result(await Mediator.Send(comand));
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> delete(string id)
        {
            return result(await Mediator.Send(new deletePhoto.Comand { publicId=id}));
        }

        [HttpPost("{Id}/updateMain")]
        public async Task<IActionResult> Main(string id)
        {
            return result(await Mediator.Send(new AddMainPhoto.Comand { Id = id }));
        }


        [HttpGet ("{username}")]
        public async Task<IActionResult> get(string username)
        {
            return result(await Mediator.Send(new userProfile.Query { username= username}));
        }


        [AllowAnonymous]
        [HttpPut]
        public async Task<IActionResult> Put(UpdateProfileDTO updateProfileDTO) 

        {

            
            return result(await Mediator.Send(new updateProfile.Comand { UpdateProfileDTO= updateProfileDTO }));

        }

    }
}
