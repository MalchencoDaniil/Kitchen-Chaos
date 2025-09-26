using KitchenChaos.Items.Counters.Cutting;
using System;
using UnityEngine;

namespace KitchenChaos.Items.Counters
{
    public class CuttingCounterVisualiser : CounterVisual
    {
        private const string CUT = "Cut";

        [SerializeField] private CuttingCounter _cuttingCounter;

        private void Awake()
        {
            _cuttingCounter.OnCut += VisualView;
        }

        public override void VisualView(object sender, EventArgs eventArgs)
        {
            CounterAnimator.SetTrigger(CUT);
        }
    }
}