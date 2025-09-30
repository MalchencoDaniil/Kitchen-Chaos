using UnityEngine;
using UnityEngine.UI;

namespace KitchenChaos.Kitchen.Counters
{
    public class ProgressBarUI : MonoBehaviour
    {
        [SerializeField] private Image _barImage;
        [SerializeField] private BaseCounter _hasCounter;

        private IHasProgress _hasProgress;

        private void Start()
        {
            if (_hasCounter.GetComponent<IHasProgress>() != null)
                _hasProgress = _hasCounter.GetComponent<IHasProgress>();

            _hasProgress.OnProgressChanged += HasProgressOnChanged;

            _barImage.fillAmount = 0;
            Hide();
        }

        private void HasProgressOnChanged(object _sender, IHasProgress.OnProgressChangedEventArgs e)
        {
            _barImage.fillAmount = e._progressNormalized;

            if (e._progressNormalized == 0 || e._progressNormalized == 1)
                Hide();
            else
                Show();
        }

        private void Show()
        {
            gameObject.SetActive(true);
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}