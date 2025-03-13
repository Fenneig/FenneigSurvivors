using FenneigSurvivors.Scripts.Objects;

namespace FenneigSurvivors.Scripts.Spawners.Pools
{
    public class BulletPool : AbstractPool<Bullet>
    {
        public override void ReturnToPool(Bullet instance)
        {
            instance.Trail.Clear();
            base.ReturnToPool(instance);
        }
    }
}
