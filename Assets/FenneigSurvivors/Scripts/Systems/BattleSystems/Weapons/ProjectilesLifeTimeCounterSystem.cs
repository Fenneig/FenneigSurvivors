using FenneigSurvivors.Scripts.Components.BattleComponents.Weapon;
using FenneigSurvivors.Scripts.Components.BattleComponents.Weapon.Bullets;
using Leopotam.Ecs;
using UnityEngine;

namespace FenneigSurvivors.Scripts.Systems.BattleSystems.Weapons
{
    public class ProjectilesLifeTimeCounterSystem : IEcsRunSystem
    {
        private EcsFilter<ProjectileLifeTimeComponent>.Exclude<DestroyBulletComponent> _filter;
        private EcsFilter<DestroyBulletComponent> _pauseFilter;

        public void Run()
        {
            if (_pauseFilter.IsEmpty() == false)
                return;

            foreach (int i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var lifeTime = ref _filter.Get1(i);
                lifeTime.RemainTime -= Time.deltaTime;
                if (lifeTime.RemainTime <= 0)
                {
                    entity.Get<DestroyBulletComponent>();
                    _filter.GetEntity(i).Del<ProjectileLifeTimeComponent>();
                }
            }
        }
    }
}
