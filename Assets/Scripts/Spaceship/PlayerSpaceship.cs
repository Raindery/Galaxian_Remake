using Galaxian.Common;

namespace Galaxian.Spaceship
{
    public class PlayerSpaceship: BaseSpaceship, IDamageable
    {
        private void Start()
        {
            GameEventHolder.OnPlayerSpaceshipInited.Invoke(this);
        }


        public void ReceiveDamage(int damage)
        {
            if(damage <= 0)
                return;

            Health -= damage;

            if (Health <= 0)
            {
                GameEventHolder.OnPlayerSpaceshipDestroyed.Invoke(this);
                DestroySpaceship();
            }
                
            
            
        }
    }
}