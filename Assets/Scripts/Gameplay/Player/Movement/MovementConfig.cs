using System;
using UnityEngine;

namespace Horror.Gameplay.Player
{
    [CreateAssetMenu(menuName = "Configs/Movement", fileName = "new MovementConfig")]
    public class MovementConfig : ScriptableObject
    {
        [field: SerializeField] public float WalkSpeed { get; private set; }
        [field: SerializeField] public float RunSpeed { get; private set; }
        [field: SerializeField] public float CrouchSpeed { get; private set; }
    }
}