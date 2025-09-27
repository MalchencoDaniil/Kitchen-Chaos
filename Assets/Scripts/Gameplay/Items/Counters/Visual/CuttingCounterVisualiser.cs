using KitchenChaos.Items.Counters.Cutting;
using KitchenChaos.Items.Counters.Stove;
using System;
using UnityEngine;

namespace KitchenChaos.Items.Counters.Visual
{
    public class CuttingCounterVisualiser : CounterVisual
    {
        private const string CUT = "Cut";

        [SerializeField] private CuttingCounter _cuttingCounter;
        [SerializeField] private Animator _counterAnimator;
        private void Awake()
        {
            _cuttingCounter.OnCut += VisualView;
        }

        public override void VisualView(object sender, EventArgs eventArgs)
        {
            _counterAnimator.SetTrigger(CUT);
        }

        private void OnDestroy()
        {
            _cuttingCounter.OnCut -= VisualView;
        }
    }
}