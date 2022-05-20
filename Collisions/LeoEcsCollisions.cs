using Leopotam.Ecs;
namespace AffenCode
{
    public static class LeoEcsCollisions
    {
        public static EcsSystems AddCollisionOneFrameComponents(this EcsSystems ecsSystems)
        {
            return ecsSystems
                    .OneFrame<CollisionEnter>()
                    .OneFrame<CollisionEnter2D>()
                ;
        }
    }
}
