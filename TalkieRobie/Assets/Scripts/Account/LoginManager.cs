using PlayFab;
using UnityEngine;
using PlayFab.ClientModels;
using Sienar.Unity.Core.Zenject.Core;
namespace Sienar.TalkieRobie.Account
{
    public class LoginManager : MonoBehaviour
    {
        public delegate void LoginEvent();
        event LoginEvent onLogin;

        string deviceID;
        DeviceID deviceSettings;
        private void Start()
        {
            deviceSettings = DependencyContext.Get<DeviceID>();
            deviceID = deviceSettings.GetDeviceID();

            LoginWithDevice(deviceID);
        }

        void LoginWithDevice(string id)
        {
            var request = new LoginWithCustomIDRequest
            {
                CustomId = deviceID,
                CreateAccount = true,
            };
            PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);
        }

        void OnSuccess(LoginResult result)
        {
            Debug.Log("Account login");
            onLogin?.Invoke();
        }
        void OnError(PlayFabError result)
        {

        }

        public void BindOnLogin(LoginEvent login)
        {
            if (login == null)
                return;
            onLogin += login;
        }
        public void UnBindOnLogin(LoginEvent login)
        {
            if(login == null) 
                return;
            onLogin -= login;
        }
    }
}