using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.Networking;
using System.Collections.Generic;
using Sienar.TalkieRobie.Managers;
using Sienar.Unity.Core.Zenject.Core;
using Sienar.TalkieRobie.Notification;
namespace Sienar.TalkieRobie.DataCenter
{
    public class ExternalManager : MonoBehaviour
    {
        PlayfabDataManager dataManager;
        NotificationManager notification;

        private void Start()
        {
            dataManager = DependencyContext.Get<PlayfabDataManager>();
            notification = DependencyContext.Get<NotificationManager>();
            dataManager?.BindOnLoadCatalog(LoadAllExternal);
        }
        private void OnDestroy()
        {
            dataManager?.UnBindOnLoadCatalog(LoadAllExternal);
        }
        async void LoadAllExternal(List<CatalogModel> model)
        {
            List<CatalogModel> voicesCatalog = new List<CatalogModel>();
            foreach (var modelItem in model)
            {
                if (modelItem.VoiceUrl != null || modelItem.ImageUrl != null)
                {

                    modelItem.Image = await DownloadImage(modelItem.ImageUrl);
                    modelItem.AudioClip = await DownloadAudio(modelItem.VoiceUrl);
                    if (modelItem.AudioClip != null || modelItem.Image != null)
                    {
                        voicesCatalog.Add(modelItem);
                    }
                }
            }

            GameManager.Instance.SetCatalog(voicesCatalog);
        }

        async Task<AudioClip> DownloadAudio(string url)
        {
            using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG))
            {
                var operation = www.SendWebRequest();

                while (!operation.isDone)
                {
                    await Task.Yield();
                }

                if (www.result != UnityWebRequest.Result.Success)
                {
                    notification.Show(NotificationType.AudioError);
                    return null;
                }
                else
                {

                    var clip = DownloadHandlerAudioClip.GetContent(www);
                    return clip;
                }
            }
        }

        async Task<Texture2D> DownloadImage(string url)
        {
            using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(url))
            {
                var operation = www.SendWebRequest();

                while (!operation.isDone)
                {
                    await Task.Yield();
                }

                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError(www.error);
                    notification.Show(NotificationType.ImageError);
                    return null;
                }
                else
                {
                    Texture2D texture = DownloadHandlerTexture.GetContent(www);
                    return texture;
                }
            }
        }
    }
}