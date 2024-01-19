using ECS.CodeBase.Components;
using ECS.System.SystemCalbacks;
using System;
using UnityEngine;

namespace ECS.CodeBase.System
{
    [Serializable]
    public sealed class MovementSystem : IFixedUpdateSystem
    {
        private ComponentPool<MoveStateComponent> statePool;
        private ComponentPool<MoveSpeedComponent> speedPool;
        private ComponentPool<TransformComponent> transformPool;


        void IFixedUpdateSystem.FixedUpdate(int entity) 
        {
            if (!this.statePool.HasComponent(entity)) return; 

            ref MoveStateComponent stateComponent = ref this.statePool.GetComponent(entity);

            if (!stateComponent.MoveRequired) return;

            ref TransformComponent transformComponent = ref this.transformPool.GetComponent(entity);
            ref MoveSpeedComponent moveSpeedComponent = ref this.speedPool.GetComponent(entity);

            var offset = 
                stateComponent.Direction * 
                moveSpeedComponent.SpeedValue * 
                Time.fixedDeltaTime;

            transformComponent.Transform.position += offset;

            stateComponent.MoveRequired = false;
        }
    }
}
