using System;
using UnityEngine;
using KitchenChaos.Kitchen.Counters.Cutting;

namespace KitchenChaos.Kitchen.Counters.Visual
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