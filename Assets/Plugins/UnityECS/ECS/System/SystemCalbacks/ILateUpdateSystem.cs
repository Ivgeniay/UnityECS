namespace ECS.System.SystemCalbacks
{
    public interface ILateUpdateSystem
    {
        public void LateUpdate(int entity);
    }
}
