using Server.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Fronteira.Recursos
{
    public class ColetaTimer : Timer
    {
        public Recurso _recurso;
        public Mobile _from;
        public int n;

        public ColetaTimer(Recurso s, Mobile f) : base(TimeSpan.FromSeconds(3), TimeSpan.FromSeconds(3), 4)
        {

            if(s.Coletando != null && s.Coletando != f)
            {
                return;
            }
            s.Coletando = f;

            _from = f;
            _recurso = s;
            Anim();

            if((_recurso.Metal() && !(_from.Weapon is Pickaxe)) || (!_recurso.Metal() && !(_from.Weapon is BaseAxe)))
            {
                _from.SendMessage("Voce talvez precise de outra ferramenta em suas maos para coletar isto");
                Stop();
            }
        }

        public void Anim()
        {
            _from.PlayAttackAnimation();
            if (_recurso.Metal())
                _from.PlaySound(0x125);
            else
                _from.PlaySound(0x13E);
        }

        protected override void OnTick()
        {
            n++;
            if (!_from.Alive || _from.GetDistanceToSqrt(_recurso) > 3)
                return;

            if (_recurso == null || _recurso.Deleted || (_recurso.Coletando != null && _recurso.Coletando != _from))
                return;

            Anim();
            if(n==3)
            {
                _recurso.Coletando = null;
                _recurso.Coleta(_from);
                Stop();
            }
        }
    }
}
