using System;
using Server.Fronteira.Tutorial.WispGuia;
using Server.Regions;
using Server.Spells.First;

namespace Server.Mobiles
{
    [CorpseName("a wisp corpse")]
    public class NovoWispGuia : WispGuia
    {
        public static Guia Guia = new Guia();

        public override void OnDamage(int amount, Mobile from, bool willKill)
        {
            base.OnDamage(amount, from, willKill);
            if (!this.IsCooldown("ai"))
            {
                this.SetCooldown("ai", TimeSpan.FromMinutes(1));
                Fala("Ouch, essa doeu.");
            }
        }

        [Constructable]
        public NovoWispGuia(PlayerMobile player)
            : base(player)
        {
            this.Name = "Fada dos Noobs";
            this.Body = 58;
            this.Jogador = player;
            this.SetStr(1, 1);
            this.SetDex(1, 1);
            this.SetInt(1, 1);

            this.SetHits(1000);

            this.SetDamage(1, 1);

            this.SetDamageType(ResistanceType.Physical, 50);
            this.SetDamageType(ResistanceType.Energy, 50);

            this.SetResistance(ResistanceType.Physical, 35, 45);
            this.SetResistance(ResistanceType.Fire, 20, 40);
            this.SetResistance(ResistanceType.Cold, 10, 30);
            this.SetResistance(ResistanceType.Poison, 5, 10);
            this.SetResistance(ResistanceType.Energy, 50, 70);

            this.SetSkill(SkillName.EvalInt, 80.0);
            this.SetSkill(SkillName.Magery, 80.0);
            this.SetSkill(SkillName.MagicResist, 80.0);
            this.SetSkill(SkillName.Tactics, 80.0);
            this.SetSkill(SkillName.Wrestling, 80.0);

            this.Fame = 4000;
            this.Karma = 0;

            this.VirtualArmor = 40;
            this.Passo = (int)PassoTutorial.PEGAR_CAVALO;
            SetCooldown("pensa", TimeSpan.FromSeconds(10));

        }

        public bool ValidaChegouNoLugar(ObjetivoGuia obj)
        {
            if (!obj.PrecisaEvento && obj.Local != Point3D.Zero && Jogador.GetDistance(obj.Local) <= 6)
            {
                return true;
            }
            return false;
        }

        private void Completa(ObjetivoGuia obj)
        {
            if (obj.Completar != null)
            {
                obj.Completar(Jogador);
                if (obj.FraseCompletar != null)
                {
                    Fala(obj.FraseCompletar);
                }
                if(Jogador.QuestArrow != null)
                {
                    Jogador.QuestArrow.Stop();
                    Jogador.QuestArrow = null;
                }
            }
            if (obj.Proximo != PassoTutorial.NADA)
            {
                this.DebugSay("Avancando objetivo para " + obj.Proximo);
                SetCooldown("pensa", TimeSpan.FromSeconds(5));
                this.Passo = (int)PassoTutorial.NADA;
                Timer.DelayCall(TimeSpan.FromSeconds(3), () =>
                {
                    this.Passo = (int)obj.Proximo;
                    var objProximo = Guia.Objetivos[obj.Proximo];
                    if (objProximo.FraseIniciar != null)
                    {
                        Fala(objProximo.FraseIniciar);
                        SetCooldown("fala", TimeSpan.FromSeconds(20));
                    }
                });
            }
        }

        public void FazAlgo()
        {
            this.DebugSay("Fazendo algo");
            var objetivoAtual = (PassoTutorial)this.Passo;
            var obj = Guia.Objetivos[objetivoAtual];

            if (ValidaChegouNoLugar(obj))
            {
                Completa(obj);
                return;
            }

            if (obj.Local != Point3D.Zero)
            {
                if (!(Jogador.Region is DungeonRegion && Jogador.Region is DungeonGuardedRegion))
                {
                    Jogador.QuestArrow = new QuestArrow(Jogador, obj.Local);
                    Jogador.QuestArrow.Update();
                } 
            }

            if (obj.LocalDungeon != Point3D.Zero)
            {
                if ((Jogador.Region is DungeonRegion || Jogador.Region is DungeonGuardedRegion))
                {
                    Jogador.QuestArrow = new QuestArrow(Jogador, obj.LocalDungeon);
                    Jogador.QuestArrow.Update();
                }     
            }

            if (!IsCooldown("fala") && obj.FraseProgresso != null)
            {
                SetCooldown("fala", TimeSpan.FromSeconds(20));
                Fala(obj.FraseProgresso);
            }
        }

        public override void OnThink()
        {
            if (Jogador == null)
            {
                if (ControlMaster != null && ControlMaster is PlayerMobile)
                {
                    Jogador = (PlayerMobile)ControlMaster;
                    Jogador.Wisp = this;
                    if (!IsPetFriend(Jogador))
                        AddPetFriend(Jogador);
                    this.AIObject.EndPickTarget(Jogador, Jogador, OrderType.Follow);
                    this.AceitaOrdens = false;
                }
                else
                {
                    return;
                }
            }

            if (Jogador.Profession == 0)
                return;

            if (IsCooldown("pensa"))
                return;

            var dist = this.GetDistance(Jogador);
            if (dist > 20)
            {
                this.DebugSay("Teleportando");
                this.MoveToWorld(Jogador.Location, Jogador.Map);
            }
            else
            {
                this.DebugSay("Seguindo");
                this.AIObject.EndPickTarget(Jogador, Jogador, OrderType.Follow);
            }

            if (Jogador.Region is DungeonRegion || Jogador.Region is DungeonGuardedRegion)
            {
                if (!Jogador.HasAction(typeof(LightCycle)))
                {
                    var spell = new NightSightSpell(this, null);
                    spell.InstantTarget = Jogador;
                    spell.Cast();
                }
            }
            FazAlgo();
            SetCooldown("pensa", TimeSpan.FromSeconds(4));
            SetHits(10000);
        }

        public override void AbreBanco()
        {
            if (Passo == (int)PassoTutorial.IR_BANCO)
                Completa(Guia.Objetivos[PassoTutorial.IR_BANCO]);
        }

        public override void QuestNoob()
        {
            if (Passo == (int)PassoTutorial.PEGAR_QUEST)
                Completa(Guia.Objetivos[PassoTutorial.PEGAR_QUEST]);
        }

        public NovoWispGuia(Serial serial)
            : base(serial)
        {
        }
    }
}
