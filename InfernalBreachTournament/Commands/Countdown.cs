using System;
using System.Linq;
using CommandSystem;
using CustomPlayerEffects;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using MEC;

namespace InfernalBreachTournament.Commands;

[CommandHandler(typeof(RemoteAdminCommandHandler))]
public class Countdown : ICommand
{
    public string Command { get; } = "countdown";
    public string[] Aliases { get; } = { "cd" };
    public string Description { get; } = "Starts a countdown for the tournament";

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        var player = Player.Get((CommandSender)sender);
        
        if (!sender.CheckPermission("ibt.countdown"))
        {
            response = "You don't have permission to use this command.";
            return false;
        }

        foreach (var ply in Player.List.Where(x => !x.IsTutorial))
            ply.EnableEffect<Ensnared>();
        
        Timing.CallDelayed(10f, () =>
        {
            foreach (var ply in Player.List.Where(x => !x.IsTutorial))
                ply.DisableEffect<Ensnared>();
        });
        
        response = "Countdown started.";
        return true;
    }
}