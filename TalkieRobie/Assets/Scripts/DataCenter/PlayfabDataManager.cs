using PlayFab;
using UnityEngine;
using Newtonsoft.Json;
using PlayFab.ClientModels;
using System.Collections.Generic;
using Sienar.Unity.Core.Zenject.Core;
using Sienar.TalkieRobie.Notification;
using Sienar.TalkieRobie.Menu.Difficulty;
using PlayFab.MultiplayerModels;
namespace Sienar.TalkieRobie.DataCenter
{
    public class PlayfabDataManager : MonoBehaviour
    {
        public delegate void CatalogEvent(List<CatalogModel> catalogModel);
        public delegate void LeaderboardEvent(List<LeaderboardModel> leaderboardModel);
        event CatalogEvent onLoadCatalog;
        event LeaderboardEvent onLoadLeaderboard;

        List<CatalogModel> catalogs;
        NotificationManager notification;
        private void Start()
        {
            notification = DependencyContext.Get<NotificationManager>();
        }
        #region Catalog
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
            if (catalogs.Count > 0)
            {
                onLoadCatalog?.Invoke(catalogs);
            }
        }

        private void OnGetCatalogItemsFailure(PlayFabError error)
        {
            Debug.LogError("GetCatalogItems failed: " + error.GenerateErrorReport());
            notification.Show(NotificationType.CatalogError);
        }

        public void BindOnLoadCatalog(CatalogEvent dataEvent)
        {
            if (dataEvent == null)
                return;
            onLoadCatalog += dataEvent;
        }
        public void UnBindOnLoadCatalog(CatalogEvent dataEvent)
        {
            if (dataEvent == null)
                return;
            onLoadCatalog -= dataEvent;
        }
        #endregion

        #region LeaderBoard
        public void SendLeaderboard(int score)
        {
            var statistic = new StatisticUpdate();
            statistic.StatisticName = "Board";
            statistic.Value = score;

            var request = new UpdatePlayerStatisticsRequest { Statistics = new List<StatisticUpdate> { statistic } };
            
            PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdateSuccess, OnLeaderboardUpdateFailure);
        }
        private void OnLeaderboardUpdateSuccess(UpdatePlayerStatisticsResult result)
        {
            Debug.Log("Leaderboard update successful!");
        }

        private void OnLeaderboardUpdateFailure(PlayFabError error)
        {
            Debug.LogError("Leaderboard update failed: " + error.GenerateErrorReport());
        }

        public void GetLeaderboard()
        {
            var request = new GetLeaderboardRequest
            {
                StatisticName = "Board",
                StartPosition = 0,
                MaxResultsCount = 10
            };

            PlayFabClientAPI.GetLeaderboard(request, OnGetLeaderboardSuccess, OnGetLeaderboardFailure);
        }
        private void OnGetLeaderboardSuccess(GetLeaderboardResult result)
        {
            Debug.Log("Leaderboard data retrieved successfully!");
            List<LeaderboardModel> leaderboardList = new List<LeaderboardModel>();
            foreach (var item in result.Leaderboard)
            {
                LeaderboardModel modelItem = new LeaderboardModel();
                modelItem.Position = item.Position;
                modelItem.Name = item.PlayFabId;
                modelItem.Skor = item.StatValue;
                leaderboardList.Add(modelItem);
            }
            if(leaderboardList.Count > 0) 
            {
                onLoadLeaderboard.Invoke(leaderboardList);
            }
        }

        private void OnGetLeaderboardFailure(PlayFabError error)
        {
            Debug.LogError("Failed to retrieve leaderboard data: " + error.GenerateErrorReport());
        }

        public void BindOnLoadLeaderboard(LeaderboardEvent leaderboard)
        {
            if (leaderboard == null)
                return;
            onLoadLeaderboard += leaderboard;
        }
        public void UnBindOnLoadLeaderboard(LeaderboardEvent leaderboard)
        {
            if (leaderboard == null)
                return;
            onLoadLeaderboard -= leaderboard;
        }
        #endregion
    }
}