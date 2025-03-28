﻿using FenneigSurvivors.Scripts.Components;
using FenneigSurvivors.Scripts.Components.BattleComponents;
using FenneigSurvivors.Scripts.Configs;
using Leopotam.Ecs;
using UnityEngine;

namespace FenneigSurvivors.Scripts.Systems.BattleSystems
{
    public class HitEffectTimerSystem : IEcsRunSystem
    {
        private EcsFilter<HitEffectTimerComponent> _filter;
        private EcsFilter<PauseComponent> _pauseFilter;

        private Config _config;

        public HitEffectTimerSystem(Config config)
        {
            _config = config;
        }
        
        public void Run()
        {
            if (_pauseFilter.IsEmpty() == false)
                return;

            foreach (int i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var hitEffectTimer = ref _filter.Get1(i);
                if (hitEffectTimer.SwitchStateTimeRemaining <= 0)
                    hitEffectTimer.SwitchStateTimeRemaining = _config.HitSwitchDuration;
                
                hitEffectTimer.EffectTimeRemaining -= Time.deltaTime;
                hitEffectTimer.SwitchStateTimeRemaining -= Time.deltaTime;

                if (hitEffectTimer.EffectTimeRemaining <= 0)
                    entity.Del<HitEffectTimerComponent>();
            }
        }
    }
}
