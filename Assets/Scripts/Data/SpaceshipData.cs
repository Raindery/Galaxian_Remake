using Galaxian.Projectiles.Data;
using NaughtyAttributes;
using UnityEngine;

namespace Galaxian.Spaceships.Data
{
    [CreateAssetMenu(fileName = "Spaceship Data", menuName = "Galaxian/Create Spaceship Data")]
    public class SpaceshipData: ScriptableObject
    {
        [Header("General Parameters")]
        [SerializeField, MinValue(1)] private int _health = 1;
        [SerializeField, MinValue(0.1f)] private float _speed = 1f;
        [SerializeField, MinValue(1)] private int _damage = 1;
        [SerializeField, MinValue(0)] private float _shootDelay = 1f;
        [SerializeField] private GameObject _destroyEffect;

        [Header("Projectile")] 
        [SerializeField] private ProjectileData _projectileData;
        
        public int Health => _health;
        public float Speed => _speed;
        public int Damage => _damage;

        public float ShootDelay => _shootDelay;

        public ProjectileData ProjectileData => _projectileData;
        public GameObject DestroyEffect => _destroyEffect;
    }
}