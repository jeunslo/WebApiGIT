using KazenLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KazenService.Models
{
    public class InMemoryDb
    {
        private static Dictionary<int, Kaas> kazen;
        static InMemoryDb()
        {
            Kaas OudBrugge = new Kaas { Id = 1, Naam = "Oud Brugge", Type = "Hard", Smaak = "Pittig" };
            Kaas Watou = new Kaas { Id = 2, Naam = "Watou", Type = "Hardhard", Smaak = "Zacht" };
            Kaas WynenDaele = new Kaas { Id = 3, Naam = "WynenDaele", Type = "Zacht", Smaak = "Pittig" };
            Kaas LaPrihel = new Kaas { Id = 4, Naam = "La Prihel", Type = "Vers", Smaak = "Pittig" };
            Kaas GrevenBroucker = new Kaas { Id = 5, Naam = "GrevenBroucker", Type = "Blauwgeaderd", Smaak = "Romig" };
            kazen = new Dictionary<int, Kaas> { { OudBrugge.Id, OudBrugge }, { Watou.Id, Watou }, { WynenDaele.Id, WynenDaele }, { LaPrihel.Id, LaPrihel }, { GrevenBroucker.Id, GrevenBroucker } };
        }

        public static Dictionary<int, Kaas> Kazen
        {
            get { return kazen; }
        }

    }
}