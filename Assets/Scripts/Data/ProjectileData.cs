using NaughtyAttributes;
using UnityEngine;

namespace Galaxian.Projectiles.Data
{
    [CreateAssetMenu(fileName = "Projectile Data", menuName = "Galaxian/Create Projectile Data")]
    public class ProjectileData: ScriptableObject
    {
        [SerializeField] private Projectile _projectileObject;
        [SerializeField] private GameObject _hitEffectObject;
        [SerializeField, MinValue(0.1f)] private float _speed;
        [SerializeField, MinValue(0.1f)] private float _lifeTime;
        
        public Projectile ProjectileObject => _projectileObject;

        public GameObject HitEffectObject => _hitEffectObject;

        public float Speed => _speed;
        public float LifeTime => _lifeTime;
    }
}