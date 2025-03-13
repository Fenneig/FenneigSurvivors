using FenneigSurvivors.Scripts.Input;
using FenneigSurvivors.Scripts.Spawners;
using FenneigSurvivors.Scripts.Spawners.Pools;
using FenneigSurvivors.Scripts.Systems.BattleSystems;
using FenneigSurvivors.Scripts.Systems.BattleSystems.Weapons;
using FenneigSurvivors.Scripts.Systems.CommonSystems;
using FenneigSurvivors.Scripts.Systems.EnemiesSystems;
using FenneigSurvivors.Scripts.Systems.LevelSystems;
using Leopotam.Ecs;
using UnityEngine;
using Zenject;

namespace FenneigSurvivors.Scripts
{
    public class GameStartup : MonoBehaviour
    {
        [SerializeField] private PlayerSpawner _playerSpawner;
        [SerializeField] private BulletSpawner _bulletSpawner;
        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private XpOrbSpawner _xpOrbSpawner;

        [Inject] private IInputService _inputService;
        [Inject] private Config _config;
        [Inject] private BulletPool _bulletPool;
        [Inject] private EnemyPool _enemyPool;
        [Inject] private XpOrbsPool _xpOrbPool;

        private EcsWorld _world;
        private EcsSystems _systems;

        private void Awake()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);

            _playerSpawner.Init(_world);
            _bulletSpawner.Init(_world, _bulletPool);
            _enemySpawner.Init(_world, _enemyPool);
            _xpOrbSpawner.Init(_world, _xpOrbPool);

            _systems
                .Add(new PlayerInputSystem(_inputService))

                .Add(new MoveSystem())
                .Add(new MoveTowardPlayerSystem())

                //.Add(new PlayerAttackSystem(_world))
                .Add(new PlayerAutoAttackSystem(_world, _config))
                .Add(new EnemyBodyAttackSystem(_config))
                .Add(new BulletCollisionSystem())

                .Add(new BulletSpawnerSystem(_bulletSpawner))
                .Add(new EnemySpawnSystem(_enemySpawner, _config))

                .Add(new DamageSystem())
                .Add(new EnemyHitSystem(_config))
                .Add(new PlayerHitSystem(_config))
                
                .Add(new XpOrbDropSystem(_xpOrbSpawner))
                .Add(new RepelSystem())
                .Add(new AttractSystem())
                .Add(new CollectLightItemsSystem())
                .Add(new CollectItemsSystem())
                .Add(new ApplyExpOrbEffect())
                .Add(new LevelUpSystem())
                
                .Add(new CleanUsedOrbsSystem(_xpOrbPool))
                .Add(new CleanBulletsSystem(_bulletPool))
                .Add(new EnemyDeathSystem(_enemyPool))
                .Add(new HitEffectSystem(_config))

                .Add(new HitEffectTimerSystem(_config))
                .Add(new SpawnCooldownSystem())
                .Add(new BulletLifeTimeCounterSystem())
                .Add(new InvulnerableSystem())
                .Add(new UpdateHpBarsSystems())
                .Init();
        }

        private void Update()
        {
            _systems?.Run();
        }

        private void OnDestroy()
        {
            _systems?.Destroy();
            _world.Destroy();
        }
    }
}
