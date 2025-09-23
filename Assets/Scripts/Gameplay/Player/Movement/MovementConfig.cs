using System;
using UnityEngine;

namespace KitchenChaos.Player
{
    [CreateAssetMenu(menuName = "Configs/Movement", fileName = "new MovementConfig")]
    public class MovementConfig : ScriptableObject
    {
        [field: SerializeField] public float MovementSpeed { get; private set; }
        [field: SerializeField] public float RotationSpeed { get; private set; }
    }
}