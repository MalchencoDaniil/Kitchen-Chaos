using KitchenChaos.Items.Counters.Cutting;
using UnityEngine;
using UnityEngine.UI;

namespace KitchenChaos.Items.Counters
{
    public class ProgressBarUI : MonoBehaviour
    {
        [SerializeField] private Image _barImage;
        [SerializeField] private CuttingCounter _cuttingCounter;

        private void Start()
        {
            _cuttingCounter.OnProgressChanged += CuttingCounterOnProgressChanged;

            _barImage.fillAmount = 0;
            Hide();
        }

        private void CuttingCounterOnProgressChanged(object _sender, CuttingCounter.OnProgressChangedEventArgs e)
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