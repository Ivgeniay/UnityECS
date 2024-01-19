using UnityEngine;

namespace ECS.System.SystemCalbacks
{
    public interface IFixedUpdateSystem
    {
        public void FixedUpdate(int entity);
    }
}
