


using Microsoft.AspNetCore.Mvc;
using sosialClone;
using RepositoryAplication.Activities;
using Microsoft.AspNetCore.Authorization;
using Azure.Core;
using Microsoft.AspNetCore.Identity;
using API.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace API.Controllers
{

    public class ActivitiesController : BaseController
    {
       



        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return result( await Mediator.Send(new list.Query()));


        }


        [HttpGet("{Id}")]
        public async Task<IActionResult> GetEn(Guid Id)
        {/*
           var entity=await Mediator.Send(new details.Query { Id= Id});
            if(entity==null) return NotFound();
            return entity;
            */
           return result(await Mediator.Send(new details.Query { Id= Id }));

        }


        [HttpPost]

        public async Task<IActionResult> Post(Entities entities)
        {
            //var user= await userManager.FindByEmailAsync(HttpContext.User.FindFirstValue(ClaimTypes.Email));
           return result(await Mediator.Send(new Create.Comand { entities = entities }));
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Put(Guid Id, Entities entities)
        {
            entities.Id = Id;
            return result(await Mediator.Send(new update.Comand { entities= entities }));

        }


        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            return result(await Mediator.Send(new Delete.Comand { Id = Id }));

        }




       
        }
    }
