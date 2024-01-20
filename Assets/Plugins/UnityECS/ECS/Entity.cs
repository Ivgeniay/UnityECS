using System;
using UnityEngine;

namespace ECS
{
    public abstract class Entity : MonoBehaviour, IDisposable
    {
        public int Id { get => Id; }
        private int id;
        protected EcsWorld world;

        public void Initialize(EcsWorld ecsWorld)
        {
            this.world = ecsWorld;
            this.id = world.CreateEntity();
            this.OnInit();
        }

        protected abstract void OnInit();

        public void Dispose()
        {
            world.DestroyEntity(this.id);
            world = null;
            id = -1;
        }

        public Entity SetComponents<T>(T component) where T : struct 
        {
            this.world.SetComponent(this.id, ref component);
            return this;
        }
        public ref T GetData<T>() where T : struct =>
            ref world.GetComponent<T>(id);
    }
}
