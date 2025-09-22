using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

namespace Horror.Lobby
{
    public class NetworkUIManager : MonoBehaviour
    {
        [SerializeField] private Button _hostButton;
        [SerializeField] private Button _serverButton;
        [SerializeField] private Button _clientButton;

        private void Awake()
        {
            ButtonInitialize();
        }

        private void ButtonInitialize()
        {
            _hostButton.onClick.AddListener(
                () => NetworkManager.Singleton.StartHost());

            _serverButton.onClick.AddListener(
                () => NetworkManager.Singleton.StartServer());

            _clientButton.onClick.AddListener(
                () => NetworkManager.Singleton.StartClient());
        }
    }
}