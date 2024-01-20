using UnityEngine;

namespace ECS.System.SystemCalbacks
{
    public interface IFixedUpdateSystem : ISystem
    {
        public void FixedUpdate(int entity);
    }
}
