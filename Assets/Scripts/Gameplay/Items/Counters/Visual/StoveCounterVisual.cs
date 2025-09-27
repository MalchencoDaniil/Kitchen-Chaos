using KitchenChaos.Items.Counters.Stove;
using System;
using UnityEngine;

namespace KitchenChaos.Items.Counters.Visual
{
    public class StoveCounterVisual : CounterVisual
    {
        [SerializeField] private StoveCounter _stoveCounter;

        [Space(15)]
        [SerializeField] private Transform _stoveOn;
        [SerializeField] private Transform _stoveParticles;

        private void Awake()
        {
            _stoveCounter.OnStateChanged += VisualView;
        }

        public override void VisualView(object sender, EventArgs eventArgs)
        {
            bool _showVisual = !(_stoveCounter._stoveStateMachine._currentState is Idle);

            _stoveOn.gameObject.SetActive(_showVisual);
            _stoveParticles.gameObject.SetActive(_showVisual);
        }

        private void OnDestroy()
        {
            _stoveCounter.OnStateChanged -= VisualView;
        }
    }
}