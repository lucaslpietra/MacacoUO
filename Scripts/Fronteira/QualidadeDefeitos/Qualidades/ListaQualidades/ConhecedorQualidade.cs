using System;
using System.Collections;
using System.Collections.Generic;
using Server.Engines.XmlSpawner2;
using Server.Items;
using Server.Mobiles;

namespace Server.Fronteira.QualidadeDefeitos.ListaQualidades
{
    public class ConhecedorQualidade
    {
        public static void Initialize()
        {
            //TODO Adicionar evento de finalizar criacao do char
            //EventSink.FinalizadaCriacaoChar += new FinalizadaCriacaoCharEventHandler(OnCharCriado);
        }

        // private static void OnCharCriado(FinalizadaCriacaoCharEventHandler e)
        // {
        //     if (e.Mobile == null)
        //         return;
        //
        //     if (!e.Mobile.Player)
        //         return;
        //
        //     PlayerMobile playerMobile = e.Mobile as PlayerMobile;
        //     QualidadesXmlAttachment qualidadesXmlAttachment =
        //         XmlAttach.FindAttachment(playerMobile, typeof(QualidadesXmlAttachment)) as QualidadesXmlAttachment;
        //
        //     //Comeca com 3 skills aleatorias da sua classe ja upadas ate 20
        //     if (qualidadesXmlAttachment != null && qualidadesXmlAttachment.Qualidades.TemQualidade(Qualidade.Conhecedor))
        //     {
        //         var playerMobileSkills = new ArrayList();
        //         int contator = 0;
        //         do
        //         {
        //             var random = Utility.Random(0, playerMobile.Skills.Length - 1);
        //             if (!playerMobileSkills.Contains(playerMobile.Skills[random]) && playerMobile.Skills[random].Cap > 20)
        //             {
        //                 playerMobile.Skills[random].Base = 20;
        //                 playerMobileSkills.Add(playerMobile.Skills[random]);
        //                 ++contator;
        //             }
        //         } while (contator < 3);
        //     }
        // }
    }
}
