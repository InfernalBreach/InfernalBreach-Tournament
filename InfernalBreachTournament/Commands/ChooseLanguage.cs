using System;
using CommandSystem;
using Exiled.API.Features;
using InfernalBreachTournament.Enums;

namespace InfernalBreachTournament.Commands;

[CommandHandler(typeof(ClientCommandHandler))]
public class ChooseLanguage : ICommand
{
    public string Command { get; } = "language";
    
    public string[] Aliases { get; } = { "lang" };
    
    public string Description { get; } = "Choose your language / Elige tu idioma";

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        var player = Player.Get((CommandSender)sender);
        
        if (arguments.Count != 1)
        {
            response = "Usage: .lang <English/Spanish>";
            return false;
        }

        Enum.TryParse(arguments.At(0), true, out Language lang);

        if (lang == Language.Spanish)
        {
            MainClass.Instance.TournamentManager.SelectLanguage(player, Language.Spanish);
            response = "Tu idioma local para el torneo ha sido cambiado a Español";
            return true;
        }
        
        if (lang == Language.English)
        {
            MainClass.Instance.TournamentManager.SelectLanguage(player, Language.English);
            response = "Your local language for the tournament has been changed to English";
            return true;
        }
        
        response = "Usage: .lang <English/Spanish>";
        return false;
    }
}