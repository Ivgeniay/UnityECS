using System;

namespace ECS
{
    public class ComponentPool<T>  : IComponentPool where T : struct
    {
        private Component[] components = new Component[4];
        public ref T GetComponent(int entity)
        {
            ref var component = ref components[entity];
            return ref component.Value;
        }

        public void SetComponent(int entity, ref T data)
        {
            ref var component = ref this.components[entity];
            component.Exists = true;
            component.Value = data;
        }

        public bool HasComponent(int entity) => this.components[entity].Exists;
        public void RemoveComponent(int entity)
        {
            ref var component = ref this.components[entity];
            component = default;
        }

        void IComponentPool.AllocateComponent() =>
            Array.Resize(ref this.components, this.components.Length * 2); 

        private struct Component
        {
            public T Value;
            public bool Exists;
        }
    } 
}
