using ECS.System.SystemCalbacks;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace ECS
{
    public class EcsWorld
    {
        private List<ISystem> systems = new List<ISystem>();

        private List<IUpdateSystem> updateSystems = new List<IUpdateSystem>();
        private List<IFixedUpdateSystem> fixedUpdateSystems = new List<IFixedUpdateSystem>();
        private List<ILateUpdateSystem> lateUpdateSystems = new List<ILateUpdateSystem>();

        private Dictionary<Type, IComponentPool> componentPools = new Dictionary<Type, IComponentPool>();
        private List<bool> entities = new List<bool>();


        #region Entity
        internal int CreateEntity()
        {
            var id = 0;
            var count = entities.Count;

            for (int i = 0; i < count; i++)
            {
                if (!entities[i])
                {
                    entities[i] = true;
                    return id;
                }
            }

            id = count;
            entities.Add(true);

            foreach(var pool in componentPools.Values)
                pool.AllocateComponent();

            return id;
        }
        internal void DestroyEntity(int entity)
        {
            entities[entity] = false;
            foreach (var pool in componentPools.Values)
                pool.RemoveComponent(entity);
        }
        #endregion

        #region Component
        internal void BindComponent<T>() where T : struct
        {
            Type type = typeof(T);
            componentPools[type] = new ComponentPool<T>();
        }
        internal ref T GetComponent<T>(int id) where T : struct
        { 
            var pool = componentPools[typeof(T)] as ComponentPool<T>;
            return ref pool.GetComponent(id);
        }
        internal void SetComponent<T>(int id, ref T component) where T : struct 
        {
            var pool = componentPools[typeof(T)] as ComponentPool<T>;
            pool.SetComponent(id, ref component);
        }
        #endregion

        #region System
        public void BindSystem<T>() where T : ISystem, new()
        {
            var system = new T();
            this.systems.Add(system);

            if (system is IUpdateSystem updateSystem)
            {
                this.updateSystems.Add(updateSystem);
            }

            if (system is IFixedUpdateSystem fixedUpdateSystem)
            {
                this.fixedUpdateSystems.Add(fixedUpdateSystem);
            }

            if (system is ILateUpdateSystem lateUpdateSystem)
            {
                this.lateUpdateSystems.Add(lateUpdateSystem);
            }
        }

        internal void FixedUpdate()
        {
            for (int i = 0, count = fixedUpdateSystems.Count; i < count; i++)
            {
                var system = fixedUpdateSystems[i];
                for (int entity = 0; entity < entities.Count; entity++)
                {
                    if (entities[entity])
                        system.FixedUpdate(entity);
                }
            }
        } 
        internal void LateUpdate()
        {
            for (int i = 0, count = lateUpdateSystems.Count; i < count; i++)
            {
                var system = lateUpdateSystems[i];
                for (int entity = 0; entity < entities.Count; entity++)
                {
                    if (entities[entity])
                        system.LateUpdate(entity);
                }
            }
        } 
        internal void Update() 
        {
            for (int i = 0, count = updateSystems.Count; i < count; i++)
            {
                var system = updateSystems[i];
                for (int entity = 0; entity < entities.Count; entity++)
                {
                    if (entities[entity])
                        system.Update(entity);
                }
            }
        }
        #endregion 

        public void Install()
        {
            foreach (var system in systems)
            {
                Type systemType = system.GetType();
                FieldInfo[] fields = systemType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic) ;
                foreach (FieldInfo field in fields)
                {
                    var compType = field.FieldType.GenericTypeArguments[0];
                    var compPool = componentPools[compType];
                    field.SetValue(system, compPool);
                }
            }
        }
    }
}
