namespace ECS
{
    public interface IComponentPool
    {
        public int Count();
        public bool HasComponent(int entity);
        public void RemoveComponent(int entity);
        public void AllocateComponent();
    }
}
