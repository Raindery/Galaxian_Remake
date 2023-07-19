using Galaxian.Common;
using Galaxian.EnemySpawner;
using Galaxian.Spaceship;
using TMPro;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private EnemySpawner _enemySpawner;

    private int _enemyCount;
    
    private void Awake()
    {
        _enemyCount = _enemySpawner.EnemySpawnCount;
        _text.text = _enemyCount.ToString();
    }

    private void OnEnable()
    {
        GameEventHolder.OnEnemySpaceshipDestroyed.AddListener(DecreaseCounter);
    }

    private void DecreaseCounter(BaseSpaceship arg0)
    {
        _enemyCount--;
        _text.text = _enemyCount.ToString();
    }

    private void OnDisable()
    {
        GameEventHolder.OnEnemySpaceshipDestroyed.RemoveListener(DecreaseCounter);
    }
}
