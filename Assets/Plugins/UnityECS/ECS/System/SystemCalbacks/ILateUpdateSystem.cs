namespace ECS.System.SystemCalbacks
{
    public interface ILateUpdateSystem : ISystem
    {
        public void LateUpdate(int entity);
    }
}
