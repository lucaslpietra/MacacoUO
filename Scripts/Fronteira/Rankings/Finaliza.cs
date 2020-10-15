using Server.Commands;
using Server.Engines.Points;
using Server.Items;
using Server.Mobiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Ziden.Rankingss
{
    public class Finaliza
    {
        /*
         *   AddItem(132, 171, 4648, 0x8A5);
                AddItem(132, 228, 4646);
                AddItem(132, 277, 4644, 2415);
                */

        public static void Initialize()
        {
            CommandSystem.Register("fimranking", AccessLevel.Administrator, new CommandEventHandler(Comando));
        }

        [Usage("Ranking")]
        [Description("Makes a call to your custom gump.")]
        public static void Comando(CommandEventArgs e)
        {
            Termina();
            Anuncio.Anuncia("Rankings Zerados - Premios Dados");
        }

        public static void Termina()
        {
            var rankWork = PointsSystem.PontosTrabalho.GetOrCalculateRank();
            var rankPvm = PointsSystem.PontosPvmEterno.GetOrCalculateRank();

            PrimeiroLugar(rankWork, "Worker");
            PrimeiroLugar(rankPvm, "PvM");

            SegundoLugar(rankWork, "Worker");
            SegundoLugar(rankPvm, "PvM");

            TerceiroLugar(rankWork, "Worker");
            TerceiroLugar(rankPvm, "PvM");

            PointsSystem.PontosTrabalho.Clear();
            PointsSystem.PontosPvmEterno.Clear();
    
        }

        public static void TerceiroLugar(List<PointsEntry> rank, string rankName)
        {
            var trofeu1 = new Item(4644);
            trofeu1.Hue = 2415;
            var p1 = Dupe.DupeItem(trofeu1);
            var player = rank[2].Player;
            p1.Name = "3o Lugar Ranking " + rankName + " 10/2020 - " + player.Name;
            player.BankBox.DropItem(p1);
        }

        public static void SegundoLugar(List<PointsEntry> rank, string rankName)
        {
            var trofeu1 = new Item(4646);
            var p1 = Dupe.DupeItem(trofeu1);
            var player = rank[1].Player;
            p1.Name = "2o Lugar Ranking " + rankName + " 10/2020 - " + player.Name;
            player.BankBox.DropItem(p1);
            var robe = new Robe();
            robe.Name = "2o Lugar Ranking " + rankName + " 10/2020 - " + player.Name;
            robe.Hue = Loot.RandomRareDye();
            player.BankBox.DropItem(robe);
        }

        public static void PrimeiroLugar(List<PointsEntry> rank, string rankName)
        {
            var trofeu1 = new Item(4648);
            trofeu1.Hue = 0x8A5;
            var p1 = Dupe.DupeItem(trofeu1);
            var player = rank[0].Player;
            p1.Name = "1o Lugar Ranking " + rankName + " 10/2020 - " + player.Name;
            player.BankBox.DropItem(p1);
            var robe = new Robe();
            robe.Name = "1o Lugar Ranking " + rankName + " 10/2020 - " + player.Name;
            robe.Hue = Loot.RandomRareDye();
            player.BankBox.DropItem(robe);
            player.BankBox.DropItem(new WindrunnerStatue());
        }
    }


}
