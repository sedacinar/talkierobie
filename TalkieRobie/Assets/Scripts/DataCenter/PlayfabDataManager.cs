using PlayFab;
using UnityEngine;
using PlayFab.ClientModels;
using System.Collections.Generic;
using Sienar.Unity.Core.Zenject.Core; 
using Sienar.TalkieRobie.Notification;
using Sienar.TalkieRobie.Menu.Difficulty;
using Newtonsoft.Json;
namespace Sienar.TalkieRobie.DataCenter
{
    public class PlayfabDataManager : MonoBehaviour
    {
        public delegate void PlayfabDataEvent(List<CatalogModel> catalogModel);
        event PlayfabDataEvent onLoadCatalog;

        List<CatalogModel> catalogs;
        NotificationManager notification;
        private void Start()
        {
            notification = DependencyContext.Get<NotificationManager>();
        }
        public void GetCatalogsData(Difficulty difficulty)
        {
            var request = new GetCatalogItemsRequest
            {
                CatalogVersion = difficulty.ToString()
            };
            PlayFabClientAPI.GetCatalogItems(request, OnGetCatalogItemsSuccess, OnGetCatalogItemsFailure);
        }
        private void OnGetCatalogItemsSuccess(GetCatalogItemsResult result)
        {
            catalogs = new List<CatalogModel>();
            foreach (var item in result.Catalog)
            {
                var catalogItem = new CatalogModel();
                catalogItem.Id = item.ItemId;
                catalogItem.Name = item.DisplayName;
                catalogItem.ImageUrl = item.ItemImageUrl;

                var deserializedData = JsonConvert.DeserializeObject<CustomData>(item.CustomData);

                catalogItem.Hint = deserializedData.Hint;
                catalogItem.VoiceUrl = deserializedData.Voice;

                catalogs.Add(catalogItem);
            }
            if(catalogs != null) 
            {
                onLoadCatalog?.Invoke(catalogs);
            }
        }

        private void OnGetCatalogItemsFailure(PlayFabError error)
        {
            Debug.LogError("GetCatalogItems failed: " + error.GenerateErrorReport());
            notification.Show(NotificationType.CatalogError);
        }

        public void BindOnLoadCatalog(PlayfabDataEvent dataEvent)
        {
            if(dataEvent == null)
                return;
            onLoadCatalog += dataEvent;
        }
        public void UnBindOnLoadCatalog(PlayfabDataEvent dataEvent)
        {
            if(dataEvent == null)
                return ;
            onLoadCatalog -= dataEvent;
        }

    }
}