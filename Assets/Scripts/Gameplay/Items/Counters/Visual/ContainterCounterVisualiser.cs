using UnityEngine;

namespace KitchenChaos.Items.Counters
{
    public class ContainterCounterVisualiser : CounterVisual
    {
        public const string OPEN_CLOSE = "OpenClose";

        [SerializeField] private ContainerCounter _containterCounter;

        private void Awake()
        {
            _containterCounter.OnGrabledObject += VisualView;
        }

        public override void VisualView(object sender, System.EventArgs eventArgs)
        {
            CounterAnimator.SetTrigger(OPEN_CLOSE);
        }
    }
}