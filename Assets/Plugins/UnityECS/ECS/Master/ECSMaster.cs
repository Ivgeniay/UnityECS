using UnityEngine;

namespace ECS.Master
{
    public abstract class ECSMaster : MonoBehaviour
    {
        protected readonly EcsWorld m_World = new EcsWorld();

        protected virtual void Awake() { }

        public void BindComponent<T>() where T : struct => m_World.BindComponent<T>();
        public void BindSystem<T>() where T : ISystem, new() => m_World.BindSystem<T>();

        public virtual void Install()
        {
            m_World.Install();
        }

        protected virtual void Update() => m_World.Update();
        protected virtual void FixedUpdate() => m_World.FixedUpdate();
        protected virtual void LateUpdate() => m_World.LateUpdate();
    }
}
