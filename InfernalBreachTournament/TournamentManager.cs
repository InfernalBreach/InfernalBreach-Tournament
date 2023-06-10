using System.Collections.Generic;
using System.Linq;
using CustomPlayerEffects;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Map;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Server;
using InfernalBreachTournament.Enums;
using MEC;

namespace InfernalBreachTournament;

public class TournamentManager
{
    private readonly MainClass Plugin;
    public TournamentManager(MainClass plugin) => Plugin = plugin;
    
    public List<Player> EnglishPlayers { get; } = new();
    public List<Player> SpanishPlayers { get; } = new();

    public CoroutineHandle HintCoroutine;

    public void SelectModifier(ModifierType modifier)
    {
        if (modifier == ModifierType.Revolver)
        {
            foreach (var ply in Player.List.Where(x => !x.IsTutorial))
            {
                ply.ClearInventory();
                ply.AddItem(ItemType.GunRevolver);
                ply.AddItem(ItemType.Medkit);
                ply.AddItem(ItemType.ArmorCombat);
                ply.AddAmmo(AmmoType.Ammo44Cal, 250);
            }
        }
        
        else if (modifier == ModifierType.Com45)
        {
            foreach (var ply in Player.List.Where(x => !x.IsTutorial))
            {
                ply.ClearInventory();
                ply.AddItem(ItemType.GunCom45);
                ply.AddItem(ItemType.Medkit);
                ply.AddItem(ItemType.ArmorCombat);
                ply.AddAmmo(AmmoType.Nato9, 250);
            }
        }

        else if (modifier == ModifierType.Com18)
        {
            foreach (var ply in Player.List.Where(x => !x.IsTutorial))
            {
                ply.ClearInventory();
                ply.AddItem(ItemType.GunCOM18);
                ply.AddItem(ItemType.Medkit);
                ply.AddItem(ItemType.ArmorCombat);
                ply.AddAmmo(AmmoType.Nato9, 250);
            }
        }

        else if (modifier == ModifierType.Epsilon11)
        {
            foreach (var ply in Player.List.Where(x => !x.IsTutorial))
            {
                ply.ClearInventory();
                ply.AddItem(ItemType.GunE11SR);
                ply.AddItem(ItemType.Medkit);
                ply.AddItem(ItemType.ArmorCombat);
                ply.AddAmmo(AmmoType.Nato556, 250);
            }
        }

        else if (modifier == ModifierType.Jailbird)
        {
            foreach (var ply in Player.List.Where(x => !x.IsTutorial))
            {
                ply.ClearInventory();
                ply.AddItem(ItemType.Jailbird);
                ply.AddItem(ItemType.Jailbird);

                if (modifier is not ModifierType.Hp50)
                    ply.Health = 250;
            }
        }

        else if (modifier == ModifierType.Particle)
        {
            foreach (var ply in Player.List.Where(x => !x.IsTutorial))
            {
                ply.ClearInventory();
                ply.AddItem(ItemType.ParticleDisruptor);
                ply.AddItem(ItemType.ParticleDisruptor);
            }
        }

        else if (modifier == ModifierType.Escopeta)
        {
            foreach (var ply in Player.List.Where(x => !x.IsTutorial))
            {
                ply.ClearInventory();
                ply.AddItem(ItemType.GunShotgun);
                ply.AddItem(ItemType.Medkit);
                ply.AddItem(ItemType.ArmorCombat);
                ply.AddAmmo(AmmoType.Ammo12Gauge, 250);
            }
        }

        else if (modifier == ModifierType.Granada)
        {
            foreach (var ply in Player.List.Where(x => !x.IsTutorial))
            {
                ply.ClearInventory();
                ply.AddItem(ItemType.GrenadeHE);
                ply.AddItem(ItemType.GrenadeHE);
                ply.AddItem(ItemType.GrenadeHE);
                ply.AddItem(ItemType.GrenadeHE);
                ply.AddItem(ItemType.GrenadeHE);
                ply.AddItem(ItemType.GrenadeHE);
                ply.AddItem(ItemType.GrenadeHE);
                ply.AddItem(ItemType.GrenadeHE);
            }
        }

        else if (modifier == ModifierType.Deshabilitado)
        {
            foreach (var ply in Player.List.Where(x => !x.IsTutorial))
            {
                ply.EnableEffect<Exhausted>();
                ply.EnableEffect<Concussed>();
                ply.EnableEffect<Deafened>();
            }
        }

        else if (modifier == ModifierType.Hp50)
            foreach (var ply in Player.List.Where(x => !x.IsTutorial))
                ply.Health = 50;
    }

    public void SelectLanguage(Player player, Language language)
    {
        if (language == Language.Spanish)
        {
            SpanishPlayers.Add(player);
            EnglishPlayers.Remove(player);
        }
        else if (language == Language.English)
        {
            EnglishPlayers.Add(player);
            SpanishPlayers.Remove(player);
        }
    }

    public void OnDying(DyingEventArgs ev)
    {
        if (ev.Attacker is null || ev.Attacker == ev.Player) return;

        if (ev.Player.IsEnglish())
            ev.Attacker.ShowHint($"<b><i><color=#969696><size=68%>You have killed</color> {ev.Player.Nickname}</size></i></b>", 4);
        if (ev.Player.IsSpanish())
            ev.Attacker.ShowHint($"<b><i><color=#969696><size=68%>Has matado a</color> {ev.Player.Nickname}</size></i></b>", 4);
        
        ev.Player.DisableEffect<Exhausted>();
        ev.Player.DisableEffect<Concussed>();
        ev.Player.DisableEffect<Deafened>();
    }

    public void OnRespawning(RespawningTeamEventArgs ev)
    {
        ev.IsAllowed = false;
    }

    public void OnAnnouncingNtfEntrance(AnnouncingNtfEntranceEventArgs ev)
    {
        ev.IsAllowed = false;
    }

    public void OnDecontaminating(DecontaminatingEventArgs ev)
    {
        ev.IsAllowed = false;
    }

    public IEnumerator<float> ShowHints(Player player, ModifierType modifierType)
    {
        // ReSharper disable IteratorNeverReturns
        
        while (true)
        {
            if (modifierType is ModifierType.Scp096)
            {
                if (player.IsEnglish())
                {
                    player.ShowHint($"<b><voffset=-750>Active Modifier: {modifierType}</voffset></b>", 1.1f);
                }

                if (player.IsSpanish())
                {
                    player.ShowHint($"<b><voffset=-750>Modificador Activo: {modifierType}</voffset></b>", 1.1f);
                }
            }
            else if (modifierType is ModifierType.p2vs2)
            {
                if (player.IsEnglish())
                {
                    player.ShowHint($"<b><voffset=-750>Active Modifier: {modifierType}</voffset></b>", 1.1f);
                }

                if (player.IsSpanish())
                {
                    player.ShowHint($"<b><voffset=-750>Modificador Activo: {modifierType}</voffset></b>", 1.1f);
                }
            }
            else if (modifierType is ModifierType.Hp50)
            {
                if (player.IsEnglish())
                {
                    player.ShowHint($"<b><voffset=-750>Active Modifier: {modifierType}</voffset></b>", 1.1f);
                }

                if (player.IsSpanish())
                {
                    player.ShowHint($"<b><voffset=-750>Modificador Activo: {modifierType}</voffset></b>", 1.1f);
                }
            }
            else if (modifierType is ModifierType.Humos)
            {
                if (player.IsEnglish())
                {
                    player.ShowHint($"<b><voffset=-750>Active Modifier: Smokes</voffset></b>", 1.1f);
                }

                if (player.IsSpanish())
                {
                    player.ShowHint($"<b><voffset=-750>Modificador Activo: {modifierType}</voffset></b>", 1.1f);
                }
            }
            else if (modifierType is ModifierType.Deshabilitado)
            {
                if (player.IsEnglish())
                {
                    player.ShowHint($"<b><voffset=-750>Active Modifier: {modifierType}</voffset></b>", 1.1f);
                }

                if (player.IsSpanish())
                {
                    player.ShowHint($"<b><voffset=-750>Modificador Activo: {modifierType}</voffset></b>", 1.1f);
                }
            }

            yield return Timing.WaitForSeconds(1.1f);
        }
    }
}