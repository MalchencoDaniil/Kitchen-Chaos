using System;

namespace KitchenChaos.Items.Counters
{
    public interface IHasProgress
    {
        public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
        public class OnProgressChangedEventArgs : EventArgs
        {
            public float _progressNormalized;
        }
    }
}