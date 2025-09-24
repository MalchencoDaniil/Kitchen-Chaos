using UnityEngine;

namespace KitchenChaos.Player
{
    public class PlayerInput
    {
        public Vector2 MovementInput()
        {
            return InputService.Instance.InputActions.Player.Move.ReadValue<Vector2>();
        }

        public bool CanInteract()
        {
            return InputService.Instance.InputActions.Player.Interact.triggered;
        }
    }
}