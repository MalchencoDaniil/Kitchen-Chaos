using System;

namespace KitchenChaos.Kitchen.Counters
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