using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Server.Accounting;
using Server.Commands;
using Server.Engines.XmlSpawner2;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Fronteira.QualidadeDefeitos
{
    public class TesteQualidadesDefeitos
    {
        public static void Initialize()
        {
            CommandSystem.Register("TestaQD", AccessLevel.GameMaster, e =>
            {
                string tipoQualidadeDefeito = "";
                if (e.Length > 0)
                {
                    tipoQualidadeDefeito = e.GetString(0);
                }

                e.Mobile.Target = new SelecionaJogadorTestesTarget(tipoQualidadeDefeito);
            });
        }

        private class SelecionaJogadorTestesTarget : Target
        {
            private string _TipoQualidadeDefeito;

            public SelecionaJogadorTestesTarget(string tipoQualidadeDefeito)
                : base(15, false, TargetFlags.None)
            {
                _TipoQualidadeDefeito = tipoQualidadeDefeito;
            }

            protected override void OnTarget(Mobile @from, object targeted)
            {
                if (targeted == null || !(targeted is PlayerMobile))
                {
                    @from.SendMessage("Voce tem que selecionar um jogador");
                    return;
                }

                switch (_TipoQualidadeDefeito)
                {
                    case "conhecedor":
                    {
                        Qualidades qualidades = new Qualidades();
                        qualidades.Adquire(Qualidade.Conhecedor);
                        QualidadesXmlAttachment attachment = new QualidadesXmlAttachment(targeted as PlayerMobile, qualidades);
                        XmlAttach.AttachTo(targeted as PlayerMobile, attachment);
                    } break;

                    case "herdeiro":
                    {
                        Qualidades qualidades = new Qualidades();
                        qualidades.Adquire(Qualidade.Herdeiro);
                        QualidadesXmlAttachment attachment = new QualidadesXmlAttachment(targeted as PlayerMobile, qualidades);
                        XmlAttach.AttachTo(targeted as PlayerMobile, attachment);
                    } break;

                    case "imunidade":
                    {
                        Qualidades qualidades = new Qualidades();
                        qualidades.Adquire(Qualidade.ImunidadeFerro);
                        QualidadesXmlAttachment attachment = new QualidadesXmlAttachment(targeted as PlayerMobile, qualidades);
                        XmlAttach.AttachTo(targeted as PlayerMobile, attachment);
                    } break;

                    case "memoria":
                    {
                        Qualidades qualidades = new Qualidades();
                        qualidades.Adquire(Qualidade.MemoriaFotografica);
                        QualidadesXmlAttachment attachment = new QualidadesXmlAttachment(targeted as PlayerMobile, qualidades);
                        XmlAttach.AttachTo(targeted as PlayerMobile, attachment);
                    } break;

                    case "mingau":
                    {
                        Qualidades qualidades = new Qualidades();
                        qualidades.Adquire(Qualidade.MingauGarantido);
                        QualidadesXmlAttachment attachment = new QualidadesXmlAttachment(targeted as PlayerMobile, qualidades);
                        XmlAttach.AttachTo(targeted as PlayerMobile, attachment);
                    } break;

                    case "sadismo":
                    {
                        Qualidades qualidades = new Qualidades();
                        qualidades.Adquire(Qualidade.Sadismo);
                        QualidadesXmlAttachment attachment = new QualidadesXmlAttachment(targeted as PlayerMobile, qualidades);
                        XmlAttach.AttachTo(targeted as PlayerMobile, attachment);
                    } break;

                    default:
                    {
                        QualidadesXmlAttachment qualidadesXmlAttachment =
                        XmlAttach.FindAttachment(targeted as PlayerMobile, typeof(QualidadesXmlAttachment)) as QualidadesXmlAttachment;
                        qualidadesXmlAttachment.Delete();

                        DefeitosXmlAttachment defeitosXmlAttachment =
                            XmlAttach.FindAttachment(targeted as PlayerMobile, typeof(DefeitosXmlAttachment)) as DefeitosXmlAttachment;
                        defeitosXmlAttachment.Delete();
                    }
                        break;

                }
            }
        }
    }
}
