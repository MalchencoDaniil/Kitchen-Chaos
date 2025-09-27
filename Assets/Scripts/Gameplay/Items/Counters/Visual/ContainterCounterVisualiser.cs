using KitchenChaos.Items.Counters.Cutting;
using UnityEngine;

namespace KitchenChaos.Items.Counters.Visual
{
    public class ContainterCounterVisualiser : CounterVisual
    {
        public const string OPEN_CLOSE = "OpenClose";

        [SerializeField] private ContainerCounter _containterCounter;
        [SerializeField] private Animator _counterAnimator;
        private void Awake()
        {
            _containterCounter.OnGrabledObject += VisualView;
        }

        public override void VisualView(object sender, System.EventArgs eventArgs)
        {
            _counterAnimator.SetTrigger(OPEN_CLOSE);
        }

        private void OnDestroy()
        {
            _containterCounter.OnGrabledObject -= VisualView;
        }
    }
}