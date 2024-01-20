using System;
using System.Linq;

namespace ECS
{
    public class ComponentPool<T>  : IComponentPool where T : struct
    {
        private Component[] components = new Component[256];
        private int size;

        public ref T GetComponent(int entity)
        {
            ref var component = ref components[entity];
            return ref component.Value;
        }
        public void SetComponent(int entity, ref T data)
        {
            ref var component = ref components[entity];
            component.Exists = true;
            component.Value = data;
        }
        public void RemoveComponent(int entity)
        {
            ref var component = ref components[entity];
            component = default;
        }
        public bool HasComponent(int entity) =>
            this.components[entity].Exists;

        public int Count() =>
            components.Count(e => e.Exists);

        void IComponentPool.AllocateComponent()
        {
            if (size + 1 >= components.Length)
                Array.Resize(ref components, components.Length * 2);

            components[size] = new Component()
            {
                Exists = true,
                Value = default
            };

            size++;
        }

        private struct Component
        {
            public T Value;
            public bool Exists;
        }
    } 
}
