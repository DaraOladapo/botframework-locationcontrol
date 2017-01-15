﻿using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Location;
using Microsoft.Bot.Connector;
using System;
using System.Threading.Tasks;

namespace LocationBotDemo.Dialogs
{
    [Serializable]
    public class MainDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedStart);
        }
        public async Task MessageReceivedStart(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var message = await argument;

            var apiKey = Keys.bingMapsKey;
            var prompt = "Where should I ship your order? Type or say an address.";
            var locationDialog = new LocationDialog(apiKey, message.ChannelId, prompt, LocationOptions.None, LocationRequiredFields.StreetAddress | LocationRequiredFields.PostalCode);
            context.Call(locationDialog, AfterLocationProvided);            
        }

        private async Task AfterLocationProvided(IDialogContext context, IAwaitable<object> result)
        {
            var message = await result;
            // loop back to beginning
            context.Wait(MessageReceivedStart);
        }
    }
}