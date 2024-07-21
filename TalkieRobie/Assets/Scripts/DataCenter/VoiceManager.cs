using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.Networking;
using System.Collections.Generic;
using Sienar.TalkieRobie.Managers;
using Sienar.Unity.Core.Zenject.Core;
using Sienar.TalkieRobie.Notification;
namespace Sienar.TalkieRobie.DataCenter
{
    public class VoiceManager : MonoBehaviour
    {
        PlayfabDataManager dataManager;
        NotificationManager notification;

        private void Start()
        {
            dataManager = DependencyContext.Get<PlayfabDataManager>();
            notification = DependencyContext.Get<NotificationManager>();
            dataManager?.BindOnLoadCatalog(LoadAllVoices);
        }
        private void OnDestroy()
        {
            dataManager?.UnBindOnLoadCatalog(LoadAllVoices);
        }
        async void LoadAllVoices(List<CatalogModel> model)
        {
            List<CatalogModel> voicesCatalog = new List<CatalogModel>();
            foreach (var modelItem in model)
            {
                if (modelItem.VoiceUrl != null)
                {

                    modelItem.AudioClip = await DownloadAudio(modelItem.VoiceUrl);
                    if (modelItem.AudioClip != null)
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
    }
}