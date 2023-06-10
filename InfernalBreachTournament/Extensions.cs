using System.Collections.Generic;
using Exiled.API.Features;

namespace InfernalBreachTournament;

public static class Extensions
{
    public static void AddRange<T>(this List<T> list, IEnumerable<T> items)
    {
        foreach (var item in items)
            list.Add(item);
    }
    
    public static void RemoveRange<T>(this List<T> list, IEnumerable<T> items)
    {
        foreach (var item in items)
            list.Remove(item);
    }

    public static bool IsEnglish(this Player player) => MainClass.Instance.TournamentManager.EnglishPlayers.Contains(player);
    
    public static bool IsSpanish(this Player player) => MainClass.Instance.TournamentManager.SpanishPlayers.Contains(player);
}