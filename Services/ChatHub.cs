
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Hubs
{
    [Authorize]
    public class ChatHub : Hub //: Hub<IMessageHub>
    {
        // protected readonly MyContext _context;
        // public ChatHub(MyContext context)
        // {
        //     _context = context;
        // }

        public async Task JoinGroupPlace(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public override async Task OnConnectedAsync()
        {
            try
            {
                int idUser = int.Parse(Context.User?.Identity?.Name);
                var idRole = int.Parse(Context.User.Claims.SingleOrDefault(e => e.Type == ClaimTypes.Role)?.Value);
                
                ConnectedUser.dict.Add(Context.ConnectionId, new InfoUser{ IdUser = idUser, IdRole = idRole, ConnectionId = Context.ConnectionId});

                await base.OnConnectedAsync();
            }
            catch (System.Exception e)
            {
                await Clients.All.SendAsync("InnerException", e.Message);
            }
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            try
            {
                ConnectedUser.dict.Remove(Context.ConnectionId);

                await base.OnDisconnectedAsync(exception);
            }
            catch (System.Exception e)
            {
                await Clients.All.SendAsync("InnerException", e.InnerException);
            }
        }
    }

    public static class ConnectedUser
    {
        public static Dictionary<string, InfoUser> dict = new Dictionary<string, InfoUser>();
    }

    public class InfoUser {
        public int? IdUser { get; set; }
        public int? IdRole { get; set; }
        public string ConnectionId { get; set; }
    }
}