using System;
using Server.Engines.XmlSpawner2;
using Server.Fronteira.Idade;
using Server.Mobiles;
using Server.Network;

namespace Server.Fronteira.QualidadeDefeitos.ListaDefeitos
{
    public class EndividadoDefeito : Timer
    {
        public EndividadoDefeito() : base(TimeSpan.FromHours(6), TimeSpan.FromHours(6))
        {
            Priority = TimerPriority.OneMinute;
        }

        public static void Initialize()
        {
            new AmaldicoadoTimer().Start();
        }

        protected override void OnTick()
        {
            foreach (NetState state in NetState.Instances)
            {
                //Procuro todos os mobiles do tipo PlayerMobile
                if (state != null && state.Mobile != null && state.Mobile is PlayerMobile)
                {
                    PlayerMobile playerMobile = state.Mobile as PlayerMobile;

                    DefeitosXmlAttachment defeitosXmlAttachment =
                        XmlAttach.FindAttachment(playerMobile, typeof(DefeitosXmlAttachment)) as DefeitosXmlAttachment;
                    if (defeitosXmlAttachment != null && defeitosXmlAttachment.Defeitos.TemDefeito(Defeito.Endividado))
                    {
                        //Procura todos os players com tempo de jogo com números divisiveis por 7
                        if (IdadeUtilidade.IdadeEmDias(playerMobile.CreationTime) % 7 == 0)
                        {
                            if (!Banker.Withdraw(playerMobile, 200 * defeitosXmlAttachment.EndividadoCount))
                            {
                                if (defeitosXmlAttachment.EndividadoCount == 2)
                                {
                                    //TODO Remover todos os alugueis do player
                                    //TODO Fazer a logica de divida
                                }
                                else if (defeitosXmlAttachment.EndividadoCount >= 4)
                                {
                                    //Tag de criminal permanente, até pagar
                                    playerMobile.Criminal = true;
                                }
                                ++defeitosXmlAttachment.EndividadoCount;
                            }
                            else
                            {
                                if (defeitosXmlAttachment.EndividadoCount >= 4)
                                {
                                    //TODO Verificar o valor correto de ganho de karma
                                    playerMobile.Karma = 2000;
                                }
                                defeitosXmlAttachment.EndividadoCount = 1;
                                playerMobile.Criminal = false;
                            }
                        }
                    }
                }
            }
        }
    }
}
