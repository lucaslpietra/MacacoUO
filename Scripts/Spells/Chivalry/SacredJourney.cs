using System;
using Server.Items;
using Server.Mobiles;
using Server.Multis;
using Server.Network;
using Server.Spells.Third;
using Server.Targeting;

namespace Server.Spells.Chivalry
{
    public class SacredJourneySpell : PaladinSpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Sacred Journey", "Sanctum Viatas",
            -1,
            9002);
        private readonly RunebookEntry m_Entry;
        private readonly Runebook m_Book;
        public SacredJourneySpell(Mobile caster, Item scroll)
            : this(caster, scroll, null, null)
        {
        }

        public override bool PunishSpellMovementIfRepeated { get { return false; } }

        public SacredJourneySpell(Mobile caster, Item scroll, RunebookEntry entry, Runebook book)
            : base(caster, scroll, m_Info)
        {
            m_Entry = entry;
            m_Book = book;
        }

        public override TimeSpan CastDelayBase
        {
            get
            {
                return TimeSpan.FromSeconds(1.5);
            }
        }
        public override double RequiredSkill
        {
            get
            {
                return 15;
            }
        }
        public override int RequiredMana
        {
            get
            {
                return 9;
            }
        }
        public override int RequiredTithing
        {
            get
            {
                return 15;
            }
        }
        public override int MantraNumber
        {
            get
            {
                return 1060727;
            }
        }// Sanctum Viatas
        public override bool BlocksMovement
        {
            get
            {
                return false;
            }
        }
        public override void OnCast()
        {
            if (m_Entry == null)
            {
                Caster.SendMessage("Seleciona runa, livro, local ou criatura para se mover.");
                Caster.Target = new InternalTarget(this);
            } 
            else
                Effect(m_Entry.Location, m_Entry.Map, true, m_Entry.Galleon != null);
        }

        public override bool CheckCast()
        {
            if (!base.CheckCast())
                return false;

            if (Factions.Sigil.ExistsOn(Caster))
            {
                Caster.SendLocalizedMessage(1061632); // You can't do that while carrying the sigil.
                return false;
            }
            /*
            else if (Server.Misc.WeightOverloading.IsOverloaded(Caster))
            {
                Caster.SendMessage("Voce esta muito pesado"); // Thou art too encumbered to move.
                return false;
            }
            */
            return SpellHelper.CheckTravel(Caster, TravelCheckType.RecallFrom);
        }

        public void Effect(Point3D loc, Map map, bool checkMulti, bool isboatkey = false)
        {
            if (Factions.Sigil.ExistsOn(Caster))
            {
                Caster.SendLocalizedMessage(1061632); // You can't do that while carrying the sigil.
            }
            else if (map == null || (!Core.AOS && Caster.Map != map))
            {
                Caster.SendLocalizedMessage(1005569); // You can not recall to another facet.
            }
            else if (Caster.Criminal)
            {
                Caster.SendLocalizedMessage("Voce nao pode usar isto enquanto esta criminoso"); // Thou'rt a criminal and cannot escape so easily.
            }
            else if (!SpellHelper.CheckTravel(Caster, TravelCheckType.RecallFrom))
            {
            }
            else if (!SpellHelper.CheckTravel(Caster, map, loc, TravelCheckType.RecallTo))
            {
            }
            /*
            else if (map == Map.Felucca && Caster is PlayerMobile && ((PlayerMobile)Caster).Young)
            {
                Caster.SendLocalizedMessage(1049543); // You decide against traveling to Felucca while you are still young.
            }
            */
            else if (SpellHelper.RestrictRedTravel && Caster.Murderer && map.Rules != MapRules.FeluccaRules)
            {
                Caster.SendLocalizedMessage(1019004); // You are not allowed to travel there.
            }
            else if (Caster.Criminal)
            {
                Caster.SendLocalizedMessage(1005561, "", 0x22); // Thou'rt a criminal and cannot escape so easily.
            }
            else if (SpellHelper.CheckCombat(Caster))
            {
                Caster.SendLocalizedMessage(1061282); // You cannot use the Sacred Journey ability to flee from combat.
            }
            else if (SpellHelper.CheckCombat(Caster))
            {
                Caster.SendMessage("Voce esta em combate"); // You cannot use the Sacred Journey ability to flee from combat.
            }
            else if (Server.Misc.WeightOverloading.IsOverloaded(Caster))
            {
                Caster.SendLocalizedMessage(502359, "", 0x22); // Thou art too encumbered to move.
            }
            else if (!map.CanSpawnMobile(loc.X, loc.Y, loc.Z) && !isboatkey)
            {
                Caster.SendLocalizedMessage(501942); // That location is blocked.
            }
            else if ((checkMulti && SpellHelper.CheckMulti(loc, map)) && !isboatkey)
            {
                Caster.SendLocalizedMessage(501942); // That location is blocked.
            }
            else if (m_Book != null && m_Book.CurCharges <= 0)
            {
                Caster.SendLocalizedMessage(502412); // There are no charges left on that item.
            }
            else if (Server.Engines.CityLoyalty.CityTradeSystem.HasTrade(Caster))
            {
                Caster.SendLocalizedMessage(1151733); // You cannot do that while carrying a Trade Order.
            }
            else if (CheckSequence())
            {
                BaseCreature.TeleportPets(Caster, loc, map, true);

                if (m_Book != null)
                    --m_Book.CurCharges;

                Effects.SendLocationParticles(EffectItem.Create(Caster.Location, Caster.Map, EffectItem.DefaultDuration), 0, 0, 0, 5033);

                Caster.PlaySound(0x1FC);
                Caster.MoveToWorld(loc, map);
                Caster.PlaySound(0x1FC);
            }

            FinishSequence();
        }

        private class InternalTarget : Target
        {
            private readonly SacredJourneySpell m_Owner;
            public InternalTarget(SacredJourneySpell owner)
                : base(8, true, TargetFlags.None)
            {
                m_Owner = owner;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is RecallRune)
                {
                    RecallRune rune = (RecallRune)o;

                    if (rune.Marked)
                        m_Owner.Effect(rune.Target, rune.TargetMap, true);
                    else
                        from.SendLocalizedMessage(501805); // That rune is not yet marked.
                }
                else if (o is Runebook)
                {
                    RunebookEntry e = ((Runebook)o).Default;

                    if (e != null)
                        m_Owner.Effect(e.Location, e.Map, true);
                    else
                        from.SendLocalizedMessage(502354); // Target is not marked.
                }
                else if (o is Key && ((Key)o).KeyValue != 0 && ((Key)o).Link is BaseBoat)
                {
                    BaseBoat boat = ((Key)o).Link as BaseBoat;

                    if (!boat.Deleted && boat.CheckKey(((Key)o).KeyValue))
                        m_Owner.Effect(boat.GetMarkedLocation(), boat.Map, false);
                    else
                        from.Send(new MessageLocalized(from.Serial, from.Body, MessageType.Regular, 0x3B2, 3, 502357, from.Name, "")); // I can not recall from that object.
                }
                else if (o is HouseRaffleDeed && ((HouseRaffleDeed)o).ValidLocation())
                {
                    HouseRaffleDeed deed = (HouseRaffleDeed)o;

                    m_Owner.Effect(deed.PlotLocation, deed.PlotFacet, true);
                } else if(o is IPoint3D)
                {
                    //TeleportSpell.Teleporta(from, (IPoint3D)o);
                    //from.Mana -= 9;
                }

                #region High Seas
                else if (o is ShipRune && ((ShipRune)o).Galleon != null)
                {
                    BaseGalleon galleon = ((ShipRune)o).Galleon;

                    if (!galleon.Deleted && galleon.Map != null && galleon.HasAccess(from))
                        m_Owner.Effect(galleon.GetMarkedLocation(), galleon.Map, false, true);
                    else
                        from.Send(new MessageLocalized(from.Serial, from.Body, MessageType.Regular, 0x3B2, 3, 502357, from.Name, "")); // I can not recall from that object.
                }
                #endregion

                #region New Magincia
                else if (o is Server.Engines.NewMagincia.WritOfLease)
                {
                    Server.Engines.NewMagincia.WritOfLease lease = (Server.Engines.NewMagincia.WritOfLease)o;

                    if (lease.RecallLoc != Point3D.Zero && lease.Facet != null && lease.Facet != Map.Internal)
                        m_Owner.Effect(lease.RecallLoc, lease.Facet, false);
                    else
                        from.Send(new MessageLocalized(from.Serial, from.Body, MessageType.Regular, 0x3B2, 3, 502357, from.Name, "")); // I can not recall from that object.
                }
                #endregion

                else
                {
                    from.Send(new MessageLocalized(from.Serial, from.Body, MessageType.Regular, 0x3B2, 3, 502357, from.Name, "")); // I can not recall from that object.
                }
            }

            protected override void OnNonlocalTarget(Mobile from, object o)
            {
            }

            protected override void OnTargetFinish(Mobile from)
            {
                m_Owner.FinishSequence();
            }
        }
    }
}
