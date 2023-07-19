using Galaxian.Spaceship;
using UnityEngine.Events;

namespace Galaxian.Common
{
    public static class GameEventHolder
    {
        public static UnityEvent<PlayerSpaceship> OnPlayerSpaceshipInited { get; } = new UnityEvent<PlayerSpaceship>();
        public static UnityEvent<BaseSpaceship> OnEnemySpaceshipDestroyed { get; } = new UnityEvent<BaseSpaceship>();
        public static UnityEvent<PlayerSpaceship> OnPlayerSpaceshipDestroyed { get; } = new UnityEvent<PlayerSpaceship>();
        public static UnityEvent OnAllEnemiesDestroyed { get; } = new UnityEvent();
    }
}