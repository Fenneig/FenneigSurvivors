using FenneigSurvivors.Scripts.Components;
using FenneigSurvivors.Scripts.Components.LevelComponents;
using FenneigSurvivors.Scripts.Objects;
using Leopotam.Ecs;
using UnityEngine;

namespace FenneigSurvivors.Scripts.Spawners
{
    public class XpOrbSpawner : AbstractSpawner<XpOrb>
    {
        public override void CreateAtPosition(Vector3 position, Vector3 direction)
        {
            var entity = World.NewEntity();

            XpOrb xpOrb = Pool.Get();
            xpOrb.transform.position = position;

            SetupTransform(entity, xpOrb);
            SetupOrb(entity, xpOrb);
        }

        private void SetupTransform(EcsEntity orbEntity, XpOrb orb)
        {
            orbEntity.Replace(new TransformComponent { Value = orb.transform });
        }
        
        private void SetupOrb(EcsEntity orbEntity, XpOrb orb)
        {
            orbEntity.Replace(new XpOrbComponent { XpOrb = orb, XpAmount = Random.Range(1, 5) });
            orbEntity.Replace(new LightPickUpComponent());
        }
    }
}
