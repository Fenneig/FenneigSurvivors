using FenneigSurvivors.Scripts.Configs;
using FenneigSurvivors.Scripts.Input;
using FenneigSurvivors.Scripts.Objects.Effects;
using FenneigSurvivors.Scripts.Spawners;
using FenneigSurvivors.Scripts.Spawners.Pools;
using FenneigSurvivors.Scripts.Spawners.Weapons;
using FenneigSurvivors.Scripts.Systems;
using FenneigSurvivors.Scripts.Systems.BattleSystems;
using FenneigSurvivors.Scripts.Systems.BattleSystems.Weapons;
using FenneigSurvivors.Scripts.Systems.BattleSystems.Weapons.Bullets;
using FenneigSurvivors.Scripts.Systems.BattleSystems.Weapons.Fireballs;
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
        //[SerializeField] private PopupManager _popupManager;
        [SerializeField] private PlayerSpawner _playerSpawner;
        [SerializeField] private BulletSpawner _bulletSpawner;
        [SerializeField] private FireballSpawner _fireballSpawner;
        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private XpOrbSpawner _xpOrbSpawner;

        [Inject] private IInputService _inputService;
        [Inject] private Config _config;
        [Inject] private EnemiesConfig _enemiesConfig;
        [Inject] private BulletConfig _bulletConfig;
        [Inject] private FireballConfig _fireballConfig;
        [Inject] private EnemyPool _enemyPool;
        [Inject] private XpOrbsPool _xpOrbPool;
        [Inject] private ProjectilePool _projectilePool;
        [Inject] private VFXSpawner _vfxSpawner;

        private EcsWorld _world;
        private EcsSystems _systems;

        private void Awake()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);

            _playerSpawner.Init(_world);
            _bulletSpawner.Init(_world);
            _fireballSpawner.Init(_world);
            _enemySpawner.Init(_world, _enemyPool);
            _xpOrbSpawner.Init(_world, _xpOrbPool);

            _systems
                .Add(new PlayerInputSystem(_inputService))
                .Add(new GameStateSystem(_world, _enemiesConfig))

                .Add(new MoveSystem())
                .Add(new MoveTowardPlayerSystem())

                .Add(new PlayerAutoAttackSystem())
                .Add(new EnemyBodyAttackSystem())
                .Add(new BulletCollisionSystem())
                .Add(new FireballCollisionSystem())

                .Add(new BulletSpawnerSystem())
                .Add(new FireballSpawnerSystem())
                .Add(new EnemySpawnSystem())

                .Add(new FireballExplosionSystem())
                .Add(new DamageSystem())
                .Add(new EnemyHitSystem())
                .Add(new PlayerHitSystem())
                
                .Add(new XpOrbDropSystem())
                .Add(new RepelSystem())
                .Add(new AttractSystem())
                .Add(new CollectLightItemsSystem())
                .Add(new CollectItemsSystem())
                .Add(new ApplyExpOrbEffectSystem())
                //.Add(new LevelUpSystem(_world))
                
                .Add(new CleanUsedOrbsSystem())
                .Add(new CleanBulletsSystem())
                .Add(new CleanFireballsSystem())
                .Add(new EnemyDeathSystem())
                
                .Add(new SpawnCooldownSystem())
                .Add(new ProjectilesLifeTimeCounterSystem())
                .Add(new InvulnerableSystem())
                .Add(new UpdateHpBarsSystem())
                .Add(new UpdateXpViewSystem())
                .Add(new EndGameSystem())
                
                .Inject(_world)
                
                .Inject(_bulletSpawner)
                .Inject(_fireballSpawner)
                .Inject(_enemySpawner)
                .Inject(_xpOrbSpawner)
                .Inject(_vfxSpawner)
                
                .Inject(_enemiesConfig)
                .Inject(_config)
                .Inject(_bulletConfig)
                .Inject(_fireballConfig)
                .Inject(_enemiesConfig)
                
                .Inject(_xpOrbPool)
                .Inject(_projectilePool)
                .Inject(_enemyPool)
                
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
