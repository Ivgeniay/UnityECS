using ECS.CodeBase.Components;
using ECS.CodeBase.System;
using ECS.Master;
using UnityEngine;

namespace ECS.CodeBase.World
{
    internal class SampleMaster : ECSMaster
    {
        [SerializeField]
        private Entity[] entities;

        protected override void Awake()
        {
            base.Awake();
            BindComponent<MoveStateComponent>();
            BindComponent<MoveSpeedComponent>();
            BindComponent<TransformComponent>();

            BindSystem<MovementSystem>();

            Install();
        }

        public override void Install()
        {
            base.Install();

            foreach (var item in entities)
            {
                item.Initialize(m_World);
            }
        }
    }
}
