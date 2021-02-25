using Server.Gumps;
using Server.Misc;
using System;
using System.Collections.Generic;
using System.IO;

namespace Server.Fronteira.Clima
{
    public class Clima
    {
        public static readonly int MAX = 20;

        public static readonly int MIN = -20;

        private static HashSet<Mobile> _emTemp = new HashSet<Mobile>();

        private static Dictionary<string, int> _temperaturas = new Dictionary<string, int>();

        private static string FilePath = Path.Combine("Saves/Climas", "Climas.bin");

        public static void Configure()
        {
            EventSink.OnEnterRegion += EnterRegion;
            EventSink.Login += Loga;
            EventSink.WorldLoad += OnLoad;
            EventSink.WorldSave += OnSave;
            var timer = new Loop();
            timer.Start();
        }

        public static void OnSave(WorldSaveEventArgs e)
        {
            Console.WriteLine("Salvando Climas");
            Persistence.Serialize(FilePath, Salva);
        }

        public static void OnLoad()
        {
            Console.WriteLine("Carregando Climas");
            Persistence.Deserialize(FilePath, Carrega);
        }

        private static void Salva(GenericWriter writer)
        {
            writer.Write(_temperaturas.Count);
            foreach (var temp in _temperaturas)
            {
                writer.Write(temp.Key);
                writer.Write(temp.Value);
            }
        }

        private static void Carrega(GenericReader reader)
        {
            var ct = reader.ReadInt();
            for (var x = 0; x < ct; x++)
            {
                var k = reader.ReadString();
                var v = reader.ReadInt();
                _temperaturas[k] = v;
            }
        }

        public static void Loga(LoginEventArgs e)
        {
            //var
        }

        public static int GetProtecao(Mobile m)
        {
            return m.ColdResistance;
        }

        private class Loop : Timer
        {
            public Loop() : base(TimeSpan.FromSeconds(20), TimeSpan.FromSeconds(20))
            {

            }

            private static Dictionary<Mobile, int> Danos = new Dictionary<Mobile, int>();

            protected override void OnTick()
            {
                if (Shard.DebugEnabled)
                    Shard.Debug("Timer");

                Danos.Clear();
                foreach (var player in _emTemp)
                {
                    if (!player.Alive)
                        continue;

                    if (Shard.DebugEnabled)
                        Shard.Debug("Vendo temperatura do manolo " + player.Name);

                    if (player.Temperatura < 0)
                    {
                        var dano = (-player.Temperatura - player.ColdResistance) * 3;
                        if (dano > 25)
                            dano = 25;
                        if (dano > 0)
                        {
                            player.Stam -= dano;
                            player.SendMessage("Voce esta com frio e precisa de roupas mais quentes para se esquentar");

                            if (player.Female)
                                player.PlaySound(0x332);
                            else
                                player.PlaySound(0x444);

                            if (player.AvisoTemperatura)
                            {
                                Danos.Add(player, dano);
                            }
                            else
                            {
                                player.SendMessage(78, "Se voce permanecer muito tempo em um local frio sem roupas adequadas sofrera dano e perda de stamina");
                                player.AvisoTemperatura = true;
                            }

                        }
                    }
                    else if (player.Temperatura > 0)
                    {
                        var resistFinal = player.FireResistance - player.ColdResistance;
                        var dano = (player.Temperatura - resistFinal) * 2;
                        if (dano > 25) dano = 25;

                        if (dano > 0)
                        {
                            player.SendMessage("Voce esta com calor");
                            FomeSedeDecay.ThirstDecay(player);
                            if (player.AvisoTemperatura)
                            {
                                Danos.Add(player, dano);
                            }
                            else
                            {
                                player.SendMessage(78, "Se voce permanecer muito tempo em um local quente sem roupas apropriadas ira receber dano e ficar com sede mais rapidamente");
                                player.AvisoTemperatura = true;
                            }
                        }

                    }
                }

                foreach (var m in Danos)
                {
                    AOS.Damage(m.Key, m.Value, DamageType.Spell);
                }
            }
        }

        private static void EnterRegion(OnEnterRegionEventArgs e)
        {
            var tempVeia = Clima.GetTemperatura(e.OldRegion);
            var tempNova = Clima.GetTemperatura(e.NewRegion);
            e.From.Temperatura = tempNova;

            if (tempNova != 0)
            {
                e.From.AvisoTemperatura = false;
                _emTemp.Add(e.From);
                Shard.Debug("Trackeando Temp", e.From);
                e.From.SendGump(new GumpTemperatura(e.From));
            }
            else
            {
                Shard.Debug("Parei trackear Temp", e.From);
                _emTemp.Remove(e.From);
            }


            if (tempVeia < tempNova)
            {
                e.From.SendMessage("Voce sente o clima esquentar");
            }
            else if (tempVeia > tempNova)
            {
                e.From.SendMessage("Voce sente o clima esfriar");
            }

        }

        public static void SetTemperatura(Region region, int graus)
        {
            _temperaturas[region.Name] = graus;
        }

        public static int GetTemperatura(Region region)
        {
            int temperatura = 0; // neutro
            if (region.Name != null)
                _temperaturas.TryGetValue(region.Name, out temperatura);
            return temperatura;
        }

    }
}
