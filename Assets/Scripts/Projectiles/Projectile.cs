using System.Collections;
using UnityEngine;
using System;
using Galaxian.Common;
using Galaxian.Projectiles.Data;

namespace Galaxian.Projectiles
{
    public class Projectile: MonoBehaviour
    {
        private event Action<Projectile> _onDestroyed;

        [SerializeField] private Collider _projectileCollider;

        private GameObject _hitEffectGameObject;
        private int _damage;
        
        
        private Transform _cachedTransform;
        public Transform CachedTransform
        {
            get
            {
                if (_cachedTransform == null)
                    _cachedTransform = transform;
                return _cachedTransform;
            }
        }

        private void Awake()
        {
            if(_projectileCollider == null)
                throw new ArgumentNullException(nameof(_projectileCollider), "Collider is not assigned to projectile object!");
            
            _projectileCollider.isTrigger = true;
        }


        public void Fire(ProjectileData projectileData, int damage, Vector3 direction, Action<Projectile> onDestroyCallback = null)
        {
            _damage = damage;
            _hitEffectGameObject = projectileData.HitEffectObject;
            
            if (onDestroyCallback != null)
                _onDestroyed += onDestroyCallback;
            
            StartCoroutine(FireCoroutine(direction, projectileData.Speed, projectileData.LifeTime));
        }

        private IEnumerator FireCoroutine( Vector3 direction, float speed, float lifeTime)
        {
            float time = 0;
            do
            {
                yield return null;
                CachedTransform.Translate(Time.deltaTime * speed * direction, Space.World);
                time += Time.deltaTime;
            } while (time < lifeTime);
            
            InvokeAction();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out IDamageable damageable)) 
                return;
            
            damageable.ReceiveDamage(_damage);
            
            Instantiate(_hitEffectGameObject, CachedTransform.position, Quaternion.identity);
            InvokeAction();
        }

        private void InvokeAction()
        {
            if (_onDestroyed != null)
            {
                _onDestroyed.Invoke(this);
                _onDestroyed = null;
            }
            else
                Destroy(gameObject);
        }
    }
}