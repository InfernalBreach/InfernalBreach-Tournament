using System;
using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using InfernalBreachTournament.Enums;
using MEC;

namespace InfernalBreachTournament.Commands;

[CommandHandler(typeof(RemoteAdminCommandHandler))]
public class ActivateModifier : ICommand
{
    public string Command { get; } = "modifier";
    public string[] Aliases { get; } = { "mod" };
    public string Description { get; } = "Activates a modifier for the tournament";

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        var player = Player.Get((CommandSender)sender);
        
        if (!sender.CheckPermission("ibt.modifier"))
        {
            response = "You don't have permission to use this command.";
            return false;
        }

        Enum.TryParse(arguments.At(0), true, out ModifierType modifierType);

        if (modifierType is ModifierType.None)
        {
            Timing.KillCoroutines(MainClass.Instance.TournamentManager.HintCoroutine);
            response = "Modifier deactivated.";
            return true;
        }
        
        MainClass.Instance.TournamentManager.SelectModifier(modifierType);

        foreach (var ply in Player.List)
        {
            if (modifierType != ModifierType.None)
            {
                if (ply.IsEnglish())
                    ply.Broadcast(7, "Modifier " + modifierType + " activated.");
                if (ply.IsSpanish())
                    ply.Broadcast(7, "Modificador " + modifierType + " activado.");
            }
        }
        
        response = $"Modifier {modifierType} activated.";
        MainClass.Instance.TournamentManager.HintCoroutine = Timing.RunCoroutine(MainClass.Instance.TournamentManager.ShowHints(player, modifierType));
        return true;
    }
}