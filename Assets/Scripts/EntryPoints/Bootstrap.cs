using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using System;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private string _defaultUserName = "Player";

    private SceneHandlerService _sceneService;

    private async void Awake()
    {
        _sceneService = FindObjectOfType<SceneHandlerService>();

        await UnityServices.InitializeAsync();

        if (UnityServices.State == ServicesInitializationState.Initialized)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();

            AuthenticationService.Instance.SignedIn += OnSignedIn;

            if (AuthenticationService.Instance.IsSignedIn)
            {
                string _username = PlayerPrefs.GetString("Username");

                if (_username == "")
                {
                    _username = _defaultUserName;
                    PlayerPrefs.SetString("Username", _defaultUserName);
                }
            }

            _sceneService.SceneLoad("MainMenu");
        }
    }

    private void OnSignedIn()
    {
        Debug.Log(message: $"Token: {AuthenticationService.Instance.AccessToken}");
        Debug.Log(message: $"Player ID: {AuthenticationService.Instance.PlayerId}");
    }
}