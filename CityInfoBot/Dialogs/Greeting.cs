using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace CityInfoBot.Dialogs
{
    [Serializable]
    public class Greeting : IDialog
    {
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Hello I am city bot :)");
            context.Wait(MessageRecievedAsync);
            // throw new NotImplementedException();
        }

        public virtual async Task MessageRecievedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var message = await argument;
            var userName = string.Empty;
            var getName = false;

            context.UserData.TryGetValue("Name", out userName);
            context.UserData.TryGetValue<bool>("GetName", out getName);

            if (getName)
            {
                userName = message.Text;
                context.UserData.SetValue("Name", userName);
                context.UserData.SetValue<bool>("GetName", false);
            }
            
            if (string.IsNullOrEmpty(userName))
            {
                await context.PostAsync("What is your name?");
                context.UserData.SetValue<bool>("GetName", true);
               
            }
            else
            {
                await context.PostAsync($"Hi {userName}. How can I help you today?");
            }

            context.Wait(MessageRecievedAsync);
        }
    }
}