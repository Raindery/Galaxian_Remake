using System.Collections;
using Galaxian.Common;
using Galaxian.Spaceship;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Galaxian.EnemySpawner
{
    public class EnemySpawner : MonoBehaviour
    {
        [Header("General")]
        [SerializeField] private EnemySpaceship[] _enemySpaceshipForSpawn;
        [SerializeField, MinValue(1)] private int _enemySpawnCount = 30;
        [SerializeField, MinValue(1)] private int _enemySpawnCountOneTime = 5;
        [SerializeField, MinValue(0.1f)] private float _spawnRadius = 3.5f;
        [SerializeField] private PlayerSpaceship _player;
        private int _spawnedEnemyCount;
        private int _enemySpawnCountInternal;


        public int EnemySpawnCount => _enemySpawnCount;


        private void Start()
        {
            _enemySpawnCountInternal = _enemySpawnCount;   
            StartCoroutine(SpawnEnemyCoroutine());
        }


        private IEnumerator SpawnEnemyCoroutine()
        {
            while (_enemySpawnCount > 0)
            {
                for (int i = 0; i < _enemySpawnCountOneTime; i++)
                {
                    var enemySpawnPosition = Random.insideUnitSphere * _spawnRadius + _player.CachedTransform.position;

                    var enemySpaceship = Instantiate(
                        _enemySpaceshipForSpawn[Random.Range(0, _enemySpaceshipForSpawn.Length - 1)],
                        enemySpawnPosition, Quaternion.identity);
                    enemySpaceship.Init(_player);
                    enemySpaceship.onSpaceshipDestroyed += OnEnemySpaceshipDestroyed;

                    _spawnedEnemyCount++;
                    yield return null;
                }

                yield return new WaitWhile(() => _spawnedEnemyCount > 0);
                
            }
        }

        private void OnEnemySpaceshipDestroyed(BaseSpaceship spaceship)
        {
            _enemySpawnCountInternal--;
            _spawnedEnemyCount--;
            
            if(_enemySpawnCountInternal <= 0)
                GameEventHolder.OnAllEnemiesDestroyed.Invoke();
            
            spaceship.onSpaceshipDestroyed -= OnEnemySpaceshipDestroyed;

        }
    }
}