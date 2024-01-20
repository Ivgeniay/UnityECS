namespace ECS.System.SystemCalbacks
{
    public interface IUpdateSystem : ISystem
    {
        public void Update(int entity);
    }
}
