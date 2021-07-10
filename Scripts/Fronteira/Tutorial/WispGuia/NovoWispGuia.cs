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
            if (!obj.PrecisaEvento)
            {
                if(obj.Local != Point3D.Zero && Jogador.GetDistance(obj.Local) <= 6)
                    return true;
                if (obj.LocalDungeon != Point3D.Zero && Jogador.GetDistance(obj.LocalDungeon) <= 6)
                    return true;
            }
            return false;
        }

        private void Completa(ObjetivoGuia obj)
        {
            if (Jogador.QuestArrow != null)
            {
                Jogador.QuestArrow.Stop();
                Jogador.QuestArrow = null;
            }

            if (obj.Completar != null)
            {
                obj.Completar(Jogador);
                if (obj.FraseCompletar != null)
                {
                    Fala(obj.FraseCompletar);
                }
            }
            var proximo = obj.Proximo;
            if(obj.GetProximo != null)
            {
                proximo = obj.GetProximo(Jogador);
            }
            if (proximo != PassoTutorial.NADA && proximo != PassoTutorial.FIM)
            {
                this.DebugSay("Avancando objetivo para " + proximo);
                SetCooldown("pensa", TimeSpan.FromSeconds(5));
                this.Passo = (int)PassoTutorial.NADA;
                Timer.DelayCall(TimeSpan.FromSeconds(3), () =>
                {
                    this.Passo = (int)proximo;
                    var objProximo = Guia.Objetivos[proximo];
                    if (objProximo.FraseIniciar != null)
                    {
                        Fala(objProximo.FraseIniciar);
                        SetCooldown("fala", TimeSpan.FromSeconds(20));
                    }
                });
            } else if(proximo == PassoTutorial.FIM)
            {
                this.Passo = (int)PassoTutorial.FIM;
            }
        }

        public void FazAlgo()
        {
            this.DebugSay("Fazendo algo");
            var objetivoAtual = (PassoTutorial)this.Passo;

            if (objetivoAtual == PassoTutorial.FIM)
            {
                if (!IsCooldown("fala"))
                {
                    SetCooldown("fala", TimeSpan.FromSeconds(20));
                    switch (Utility.Random(10))
                    {
                        case 0: Fala("Upe sempre em dungeons, voce aprende as skills mais rapido matando monstros !"); break;
                        case 1: Fala("Quando tiver 70 nas skills das armas, voce pode usar as habilidades de arma clicando no seu paperdoll (ALT + P) e depois no livro lilas no seu pe. !"); break;
                        case 2: Fala("Sabia que as moedas de ouro , quando usando .grupo, sao mais faceis de conseguir ?"); break;
                        case 3: Fala("Voce pode formar grupos usando o comando .grupo, o ouro de dungeons sera dividido entre o grupo !"); break;
                        case 4: Fala("Se voce quiser trabalhar, voce pode fazer Ordens de Compra nos npcs se tiver skills de trabalho !"); break;
                        case 5: Fala("Sabia que voce pode ter sua casa e sua fazenda ? Mas vai precisar juntar dinheiro..."); break;
                        case 6: Fala("Ganhei dinheiro trabalhando ou matando monstros, voce que sabe..."); break;
                        case 7: Fala("Voce pode minerar na mina dos orcs, mas vai precisar fazer com que eles nao te ataquem..."); break;
                        case 8: Fala("Uma boa maneira de se juntar um dinheiro inicial e a skill BEGGING..."); break;
                        case 9: Fala("Fale com a NPC JILL no centro da cidade para pegar uma quest epica!"); break;
                    }
                }
                return;
            }

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

        public override void EntregaSapato()
        {
            if (Passo == (int)PassoTutorial.VOLTAR_QUEST)
                Completa(Guia.Objetivos[PassoTutorial.VOLTAR_QUEST]);
        }

        public override void MataMerda()
        {
            if (Passo == (int)PassoTutorial.BIXO_ESGOTO)
                Completa(Guia.Objetivos[PassoTutorial.BIXO_ESGOTO]);
        }

        public override void FalaJill()
        {
            if (Passo == (int)PassoTutorial.JILL)
                Completa(Guia.Objetivos[PassoTutorial.JILL]);
        }

        public override void MataMago()
        {
            if (Passo == (int)PassoTutorial.MATAR_MAGO)
                Completa(Guia.Objetivos[PassoTutorial.MATAR_MAGO]);
        }



        public NovoWispGuia(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
        }
    }
}
