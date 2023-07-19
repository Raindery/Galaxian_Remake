using Galaxian.Common;

namespace Galaxian.Spaceship
{
    public class EnemySpaceship : BaseSpaceship, IDamageable
    {
        private BaseSpaceship _playerSpaceship;
        
        private void Update()
        {
            if(_playerSpaceship == null)
                return;
            
            CachedTransform.LookAt(_playerSpaceship.CachedTransform);
            
            if(IsCanShoot)
                Shoot(CachedTransform.forward);
        }

        public void Init(BaseSpaceship playerSpaceship)
        {
            _playerSpaceship = playerSpaceship;
        }
        
        public void ReceiveDamage(int damage)
        {
            if(damage <= 0)
                return;

            Health -= damage;

            if (Health <= 0)
            {
                GameEventHolder.OnEnemySpaceshipDestroyed.Invoke(this);
                DestroySpaceship();
            }
                
            
            
        }

        
    }
}