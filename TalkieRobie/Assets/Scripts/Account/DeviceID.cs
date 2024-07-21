using UnityEngine;
using Sienar.TalkieRobie.Managers; 
namespace Sienar.TalkieRobie.Account
{
    public class DeviceID : MonoBehaviour
    {
        bool isFacebookLogin;
        internal string GetDeviceID()
        {
            var deviceId = "";
#if UNITY_EDITOR             
            deviceId = CustomID();
#elif UNITY_ANDROID
            deviceId = AndroidID();
#elif UNITY_IOS
            deviceId = IosID();
#elif UNITY_WEBGL
            deviceId = CustomID();
#endif
            if (GetFacebookID() != null)
            {
                deviceId = GetFacebookID();
                isFacebookLogin = true;
            }
            else
            {
                isFacebookLogin = false;
            }
            return deviceId;
        }

        string AndroidID()
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                AndroidJavaClass up = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                AndroidJavaObject currentActivity = up.GetStatic<AndroidJavaObject>("currentActivity");
                AndroidJavaObject contentResolver = currentActivity.Call<AndroidJavaObject>("getContentResolver");
                AndroidJavaClass secure = new AndroidJavaClass("android.provider.Settings$Secure");
                var android_id = secure.CallStatic<string>("getString", contentResolver, "android_id");
                return android_id;
            }
            return CustomID();
        }
#if UNITY_IOS
        string IosID()
        {
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                return UnityEngine.iOS.Device.vendorIdentifier;
            }
            return CustomID();
        }
#endif
        string WebGLID()
        {
            return CustomID();
        }
        string GetFacebookID()
        {
            string cryptedId = null;
            string facebook_id = null;

            cryptedId = PlayerPrefs.GetString(PlayerPrefsData.FacebookID.ToString());

            if (cryptedId != null && cryptedId != "")
            {
                facebook_id = Crypto.Decrypt(cryptedId);
            }


            if (facebook_id != null && facebook_id != "")
            {
                return facebook_id;
            }
            return null;
        }
        internal void SetFacebookID(string id)
        {
            isFacebookLogin = true;
            var cryptedId = Crypto.Encrypt(id);
            PlayerPrefs.SetString(PlayerPrefsData.FacebookID.ToString(), cryptedId);
        }
        internal void RemoveFacebookID()
        {
            isFacebookLogin = false;
            PlayerPrefs.DeleteKey(PlayerPrefsData.FacebookID.ToString());
        }
        internal bool IsFacebookLogin()
        {
            return isFacebookLogin;
        }
        string CustomID()
        {
            string custom_id = null;
            string crypted_id = null;

            crypted_id = PlayerPrefs.GetString(PlayerPrefsData.DeviceID.ToString());
            if (crypted_id != null && crypted_id != "")
            {
                custom_id = Crypto.Decrypt(crypted_id);
            }

            if (custom_id == null || custom_id == "")
            {
                custom_id = System.Guid.NewGuid().ToString();
                crypted_id = Crypto.Encrypt(custom_id);
                PlayerPrefs.SetString(PlayerPrefsData.DeviceID.ToString(), crypted_id);
            }
            return custom_id;
        }
    }
}