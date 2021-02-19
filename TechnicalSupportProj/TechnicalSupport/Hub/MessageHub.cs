using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading;
using System.Security.Principal;
using TechnicalSupport.Data;
using Microsoft.EntityFrameworkCore;
using TechnicalSupport.Models;

namespace TechnicalSupport
{
    public class MessageHub : Hub
    {
        private ChatContext _context;

       
        public MessageHub (ChatContext context)
        {
            _context = context;

           
        }


        // [Authorize]
        public async Task Send(Message message)
        {



            message.SenderType = "out";

            var dialog = _context.Dialogs.FirstOrDefault(em => em.UserId.ToString() == Context.UserIdentifier);

                if (dialog == null)
                {

                    Employee name = _context.Employees.Where(e => e.StatusOnline == true).FirstOrDefault();

                            if (name != null)
                            {
                            Guid dialogId = Guid.NewGuid();
                                _context.Dialogs.Add(new Dialog() { UserId = Guid.Parse(Context.UserIdentifier), DialogId = dialogId, EmployeeId = name.Id });
                            message.DialogId = dialogId;
                            await Clients.User(name.Id.ToString()).SendAsync("Receive", message);

                              }
                            else
                            {
                            _context.Dialogs.Add(new Dialog() { UserId = Guid.Parse(Context.UserIdentifier), DialogId = Guid.NewGuid(), EmployeeId = Guid.Parse("00000000 - 0000 - 0000 - 0000 - 000000000000") });

                           // Message to bot


                             }
                }
                else
                {
                message.DialogId = dialog.DialogId;
                

                await Clients.User(dialog.EmployeeId.ToString()).SendAsync("Receive", message);
                }

            message.SenderType = "in";

            await Clients.User(Context.UserIdentifier.ToString()).SendAsync("Receive", message);
           
           
          




 


         
        }

        public async Task SendTechnical(Message message)
        {



            var user = _context.Users.FirstOrDefault(em => em.Id.ToString() == Context.UserIdentifier);

            if (user == null)
            {
             

                _context.SaveChanges();


            }


        }


        [Authorize(Roles = "admin")]
        public async Task SendAdmin (Message message)
        {
            if (Context.User.Identity.IsAuthenticated)

            {
              
                await Clients.User(Context.UserIdentifier.ToString()).SendAsync("Receive", message);
                var dialog = _context.Dialogs.Find(message.DialogId);
                if (dialog != null)
                {
                    message.SenderType = "out";
                    await Clients.User(dialog.UserId.ToString()).SendAsync("Receive", message);
               
                }

            }
        }

       
        public override async Task OnConnectedAsync()
        {

            if (Context.User.Identity.IsAuthenticated && Context.User.HasClaim(c => c.Value == "admin"))
            {
                Employee employee = await _context.Employees
                  .Include(u => u.Role)
                  .SingleOrDefaultAsync(u => u.Email == Context.User.Identity.Name);

                if (employee != null)
                {
                    employee.StatusOnline = true;
                    await Clients.User(employee.Id.ToString()).SendAsync("Receive", new Message() { Name = "Bot", Text = "Hello Admin" });

                }


            }
            else
            {
                    if (Context.User.Identity.IsAuthenticated)
                    {

                    User user = await _context.Users
                    .Include(u => u.Role)
                    .SingleOrDefaultAsync(u => u.Id == Guid.Parse(Context.UserIdentifier));

                            if (user != null)
                            {

                                 Guid guidDialog = Guid.NewGuid();
                                Employee name = _context.Employees.Where(e => e.StatusOnline == true).FirstOrDefault();
                               await Clients.User(user.Id.ToString()).SendAsync("Receive", new Message() { Name = "Bot", Text = name != null ? " Hello user":"No available employees!", DialogId = guidDialog });

                                
                                if (name != null)
                                {

                                    _context.Dialogs.Add(new Dialog() { UserId = user.Id, DialogId = guidDialog, EmployeeId = name.Id });

                                }
                                else
                                {
                                    _context.Dialogs.Add(new Dialog() { UserId = user.Id, DialogId = guidDialog, EmployeeId = Guid.Parse("00000000 - 0000 - 0000 - 0000 - 000000000000") });


                                }


                    }



                     }
                     else
                     {
                            Guid Id = Guid.Parse(Context.UserIdentifier);

                       //   await Clients.User(Id.ToString()).SendAsync("Receive", new Message() { Name = "Bot", Text = "No available employees!" });
    
                    //      await Groups.AddToGroupAsync(Context.ConnectionId, Id.ToString());

                              _context.Users.Add(new User() { Id = Id });


                            Employee name = _context.Employees.Where(e => e.StatusOnline == true).FirstOrDefault();
                            Guid guidDialog = Guid.NewGuid();

                            if (name != null)
                            {
                        await Clients.User(Id.ToString()).SendAsync("Receive", new Message() { Name = "Bot", Text = "Hello user", DialogId = guidDialog });

                        _context.Dialogs.Add(new Dialog() { UserId = Id, DialogId = guidDialog, EmployeeId = name.Id });
                            
                            }
                            else
                            {
                      
                        await Clients.User(Id.ToString()).SendAsync("Receive", new Message() { Name = "Bot", Text = "No available employees!", DialogId = guidDialog });

                        _context.Dialogs.Add(new Dialog() { UserId = Id, DialogId = guidDialog, EmployeeId = Guid.Parse("00000000 - 0000 - 0000 - 0000 - 000000000000") });
                             
                    }



                }
            }

            _context.SaveChanges();




         //   await Clients.All.SendAsync("Notify", new Message() { Name = "Test", Text = Context.ConnectionId.ToString() });
            await base.OnConnectedAsync();
        }
      
        public override async Task OnDisconnectedAsync(Exception exception)
        {

            if (Context.User.Identity.IsAuthenticated && Context.User.HasClaim(c => c.Value == "admin"))
            {
                Employee employee = await _context.Employees
              .Include(u => u.Role)
              .SingleOrDefaultAsync(u => u.Id == Guid.Parse(Context.UserIdentifier));

                if (employee != null)
                {
                    employee.StatusOnline = false;
                  //  await Clients.User(employee.Id.ToString()).SendAsync("Receive", new Message() { Name = "Disconected", Text = "Disconected" });

                }

            }
            else
            {

                User user =  _context.Users
                .Include(u => u.Role)
                .SingleOrDefaultAsync(u => u.Id == Guid.Parse(Context.UserIdentifier)).Result;


                if (user != null)
                {
                    var dialog = _context.Dialogs.Where(w => w.UserId == Guid.Parse(Context.UserIdentifier)).FirstOrDefault();


                    if(dialog != null)
                    {
                        await Clients.User(dialog.EmployeeId.ToString()).SendAsync("Receive", new Message() { Name = "Disconected", Text = $"Disconected {Context.UserIdentifier}" });
                        _context.Dialogs.Remove(_context.Dialogs.FirstOrDefault(f => f.UserId == Guid.Parse(Context.UserIdentifier)));

                      if (user.Email == null)  _context.Users.Remove(user);
                    }



                }

          

            }
            _context.SaveChanges();
            //  await Clients.All.SendAsync("Notify", $"{Context.ConnectionId} покинул в чат");
            await base.OnDisconnectedAsync(exception);
        }

    }
}
