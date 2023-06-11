using MediatR;
using Microsoft.AspNetCore.SignalR;
using RepositoryAplication.Comments;

namespace API.SignalR
{
    public class ChatHub : Hub
    {
        private readonly IMediator mediator;

        public ChatHub(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task SendComment(Create.Comand comand)
        {
            //send the comment to save it in database
            //and get the CommentDTO back
            var comment= await mediator.Send
                (comand);

            //send the comment to everyone connected to the Hub (group member)
            await Clients.Groups(comand.ActivityId.ToString())
                .SendAsync("reciveComment",comment.data);
        }

        //when a client connect to the Hub
        //we want them to join a group
        public override async Task OnConnectedAsync()
        {
            //getting the ActivityId from the Query string since in SignalR
            //we do not have root parameter

            var httpContext = Context.GetHttpContext();

            //the querry string from client needs to have "ActivityId" as a Key

            var activityId = httpContext.Request.Query["activityId"];

            //adding to the group
            await Groups.AddToGroupAsync(Context.ConnectionId,activityId);


            //to get the comments after connecting
            var result = await mediator.Send(new List.Query { ActivityId = Guid.Parse(activityId) });
            await Clients.Caller.SendAsync("LoadComments", result.data);
        }
    }
}
