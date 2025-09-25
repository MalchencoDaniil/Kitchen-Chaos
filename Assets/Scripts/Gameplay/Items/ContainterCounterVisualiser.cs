using UnityEngine;

namespace KitchenChaos.Items.Counters
{
    public class ContainterCounterVisualiser : MonoBehaviour
    {
        public const string OPEN_CLOSE = "OpenClose";

        [SerializeField] private ContainerCounter _containterCounter;
        [SerializeField] private Animator _counterAnimator;

        private void Awake()
        {
            _containterCounter.OnGrabledObject += CounterVisualView;
        }

        private void CounterVisualView(object sender, System.EventArgs eventArgs) => _counterAnimator.SetTrigger(OPEN_CLOSE);
    }
}