using UnityEngine;

namespace KitchenChaos.Items.Counters
{
    public abstract class CounterVisual : MonoBehaviour
    {
        public Animator CounterAnimator;

        public virtual void VisualView(object sender, System.EventArgs eventArgs) { }
    }
}