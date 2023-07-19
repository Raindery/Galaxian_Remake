using System;
using System.Collections;
using Galaxian.Projectiles;
using Galaxian.Projectiles.Data;
using Galaxian.Spaceships.Data;
using UnityEngine;
using UnityEngine.Pool;


namespace Galaxian.Spaceship
{
    [RequireComponent(typeof(CharacterController))]
    public abstract class BaseSpaceship : MonoBehaviour
    {
        public event Action<BaseSpaceship> onSpaceshipDestroyed; 
        
        [Header("Spaceship Settings")] 
        [SerializeField] private SpaceshipData _spaceshipData;
        [SerializeField] private Transform _shootPosition;


        private Transform _cachedTransform;
        private Transform _projectileHolder;
        private CharacterController _characterController;
        private ProjectileData _projectileData;
        private ObjectPool<Projectile> _projectilesPool;

        public  bool IsCanShoot { get; set; }
        protected int Health { get;  set; }
        public CharacterController SpaceshipCharacterController
        {
            get
            {
                if (_characterController == null)
                    _characterController = GetComponent<CharacterController>();
                return _characterController;
            }
        }

        public Transform CachedTransform
        {
            get
            {
                if (_cachedTransform == null)
                    _cachedTransform = transform;
                return transform;
            }
        }

        private ProjectileData ProjectileData
        {
            get
            {
                if (_projectileData == null)
                    _projectileData = _spaceshipData.ProjectileData;
                return _projectileData;
            }
        }

        public SpaceshipData SpaceshipData => _spaceshipData;


        private void Awake()
        {
            IsCanShoot = true;
            Health = SpaceshipData.Health;
            
            _projectileHolder = new GameObject("Projectile Holder").transform;
            _projectilesPool = new ObjectPool<Projectile>(
                CreateProjectile,
                projectile =>
                {
                    projectile.CachedTransform.position = _shootPosition.position;
                    projectile.gameObject.SetActive(true);
                },
                projectile => {projectile.gameObject.SetActive(false);}, 
                DestroyProjectile, 
                false,
                25,
                100);
        }


        public void Shoot(Vector3 direction)
        {
            var projectile = _projectilesPool.Get();
            projectile.Fire(
                ProjectileData, 
                _spaceshipData.Damage,
                direction, 
                projectileDestroyed =>
                {
                    _projectilesPool.Release(projectileDestroyed);
                }
            );

            IsCanShoot = false;
            StartCoroutine(WaitForCanShootCoroutine());
        }

        protected void DestroySpaceship()
        {
            Instantiate(_spaceshipData.DestroyEffect, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
            onSpaceshipDestroyed?.Invoke(this);
        }

        private IEnumerator WaitForCanShootCoroutine()
        {
            float shootDelay = _spaceshipData.ShootDelay;
            float elapsedShootDelay = 0f;
            float secondsForWait = 0.1f;

            while (elapsedShootDelay <= shootDelay)
            {
                yield return new WaitForSecondsRealtime(secondsForWait);
                elapsedShootDelay += secondsForWait;
            }

            IsCanShoot = true;
        }

        private Projectile CreateProjectile()
        {
            return Instantiate(ProjectileData.ProjectileObject, _shootPosition.position, Quaternion.identity, _projectileHolder);
        }

        private void DestroyProjectile(Projectile projectile)
        {
            Destroy(projectile.gameObject);
        }
    }
}


