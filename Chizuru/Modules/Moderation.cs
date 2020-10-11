using Chizuru.Attributes;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace Chizuru.Modules
{
    public class Moderation : ModuleBase<SocketCommandContext>
    {
        [Command("kick")]
        [RequireUserPermission(GuildPermission.KickMembers)]
        [RequireBotPermission(GuildPermission.KickMembers)]
        public async Task KickCommand([HierarchyCheck] SocketGuildUser user, [Remainder] string reason = "No reason specified")
        {
            await user.KickAsync(reason);
            await Context.Channel.SendMessageAsync($"``{user.Username}#{user.Discriminator}`` has been kicked for ``{reason}``.");
        }

        [Command("ban")]
        [RequireUserPermission(GuildPermission.BanMembers)]
        [RequireBotPermission(GuildPermission.BanMembers)]
        public async Task BanCommand([HierarchyCheck] SocketGuildUser user, [Remainder] string reason = "No reason specified")
        {
            await user.BanAsync(reason: reason);
            await Context.Channel.SendMessageAsync($"``{user.Username}#{user.Discriminator}`` has been banned for ``{reason}``.");
        }

        [Command("unban")]
        [RequireUserPermission(GuildPermission.BanMembers)]
        [RequireBotPermission(GuildPermission.BanMembers)]
        public async Task UnbanCommand(ulong userID)
        {
            await Context.Guild.RemoveBanAsync(userID);
            await Context.Channel.SendMessageAsync($"``{userID}`` has been unbanned.");
        }
    }
}
