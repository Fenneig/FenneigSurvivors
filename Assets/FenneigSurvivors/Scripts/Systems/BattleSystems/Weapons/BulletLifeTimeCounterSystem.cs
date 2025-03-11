using FenneigSurvivors.FenneigSurvivors.Scripts.Components.BattleComponents;
using Leopotam.Ecs;
using UnityEngine;

namespace FenneigSurvivors.FenneigSurvivors.Scripts.Systems.BattleSystems.Weapons
{
    public class BulletLifeTimeCounterSystem : IEcsRunSystem
    {
        private EcsFilter<BulletLifeTimeComponent>.Exclude<DestroyBulletComponent> _filter;

        public void Run()
        {
            foreach (int i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var lifeTime = ref _filter.Get1(i);
                lifeTime.RemainTime -= Time.deltaTime;
                if (lifeTime.RemainTime <= 0)
                {
                    entity.Get<DestroyBulletComponent>();
                    _filter.GetEntity(i).Del<BulletLifeTimeComponent>();
                }
            }
        }
    }
}
