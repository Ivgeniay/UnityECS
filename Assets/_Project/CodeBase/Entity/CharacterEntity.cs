using ECS.CodeBase.Components;
using System;
using UnityEngine;

namespace ECS.CodeBase.Entities
{
    internal class CharacterEntity : Entity
    {
        [SerializeField] private float speed = 5f;

        protected override void OnInit()
        {
            this
                .SetComponents(new MoveSpeedComponent() { SpeedValue = speed })
                .SetComponents(new TransformComponent() { Transform = this.transform })
                .SetComponents(new MoveStateComponent() );
        }
    }
}
