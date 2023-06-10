using System;
using Exiled.API.Features;

namespace InfernalBreachTournament;

public class MainClass : Plugin<Config>
{
    public override string Author => "xNexusACS";
    public override string Name => "InfernalBreachTournament";
    public override string Prefix => "IBT";
    public override Version Version { get; } = new(1, 0, 0);
    public override Version RequiredExiledVersion { get; } = new(7, 0, 0);
    
    public TournamentManager TournamentManager { get; private set; }
    public static MainClass Instance;

    public override void OnEnabled()
    {
        Instance = this;
        TournamentManager = new TournamentManager(this);

        Exiled.Events.Handlers.Player.Dying += TournamentManager.OnDying;
        Exiled.Events.Handlers.Server.RespawningTeam += TournamentManager.OnRespawning;
        Exiled.Events.Handlers.Map.Decontaminating += TournamentManager.OnDecontaminating;
        Exiled.Events.Handlers.Map.AnnouncingNtfEntrance += TournamentManager.OnAnnouncingNtfEntrance;

        base.OnEnabled();
    }

    public override void OnDisabled()
    {
        Exiled.Events.Handlers.Map.AnnouncingNtfEntrance -= TournamentManager.OnAnnouncingNtfEntrance;
        Exiled.Events.Handlers.Map.Decontaminating -= TournamentManager.OnDecontaminating;
        Exiled.Events.Handlers.Server.RespawningTeam -= TournamentManager.OnRespawning;
        Exiled.Events.Handlers.Player.Dying -= TournamentManager.OnDying;

        TournamentManager = null;
        Instance = null;
        base.OnDisabled();
    }
}