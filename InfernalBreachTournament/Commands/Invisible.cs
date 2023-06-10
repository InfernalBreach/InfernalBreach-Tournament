using System;
using CommandSystem;
using Exiled.API.Features;
using Exiled.API.Features.Roles;

namespace InfernalBreachTournament.Commands;

[CommandHandler(typeof(RemoteAdminCommandHandler))]
public class Invisible : ICommand
{
    public string Command { get; } = "invisible";
    public string[] Aliases { get; } = { "invis" };
    public string Description { get; } = "Makes you invisible to other players.";

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        var ply = Player.Get(sender);
        
        if (ply.Role.Is(out FpcRole fpc) && fpc.IsInvisible)
        {
            fpc.IsInvisible = false;
            response = "You are no longer invisible.";
            return true;
        }
        
        if (ply.Role.Is(out FpcRole fpc2) && !fpc2.IsInvisible)
        {
            fpc2.IsInvisible = true;
            response = "You are now invisible.";
            return true;
        }
        
        response = "Unknown error ocurred";
        return false;
    }
}