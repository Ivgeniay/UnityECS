using System;
using UnityEngine;

namespace ECS.CodeBase.Components
{
    [Serializable]
    internal struct MoveStateComponent
    {
        public bool MoveRequired;
        public Vector3 Direction;
    }
}
