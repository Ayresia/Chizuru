using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace Chizuru.Attributes
{
    public class HierarchyCheckAttribute : ParameterPreconditionAttribute
    {
        public async override Task<PreconditionResult> CheckPermissionsAsync(ICommandContext context, ParameterInfo parameter, object value, IServiceProvider services)
        {
            if (value is SocketGuildUser user)
            {
                SocketGuildUser targetedUser = user;
                SocketGuildUser executorUser = await context.Guild.GetUserAsync(context.User.Id) as SocketGuildUser;

                if (executorUser.Hierarchy > targetedUser.Hierarchy && executorUser.Id != targetedUser.Id)  return PreconditionResult.FromSuccess();

                return PreconditionResult.FromError("You can't use that command on that user.");
            }

            return PreconditionResult.FromError("An error has occured, please contact Ayresia#0001.");
        }
    }
}

