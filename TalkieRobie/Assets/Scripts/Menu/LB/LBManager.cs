using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Sienar.TalkieRobie.DataCenter;
using Sienar.Unity.Core.Zenject.Core;
namespace Sienar.TalkieRobie.Menu.LB
{
    public class LBManager : MonoBehaviour
    {
        [SerializeField]
        GameObject LBPanel;

        [SerializeField]
        Transform ViewContent;

        [SerializeField]
        GameObject LBItem;

        [SerializeField]
        Button Close;

        PlayfabDataManager playfabData;
        private void Start()
        {
            playfabData = DependencyContext.Get<PlayfabDataManager>();
            playfabData.BindOnLoadLeaderboard(LoadLeaderBoard);
            Close.onClick.AddListener(() => {LBPanel.SetActive(false);});
            LBPanel.SetActive(false);
        }
        private void OnDestroy()
        {
            playfabData.UnBindOnLoadLeaderboard(LoadLeaderBoard);
        }
        void LoadLeaderBoard(List<LeaderboardModel> leaderboards)
        {
            ClearContent();
            foreach(var item in leaderboards) 
            {
                var itemObj = Instantiate(LBItem, ViewContent);
                var itemComp = itemObj.GetComponent<LBItem>();
                itemComp.SetLB(item);
            }
            LBPanel.SetActive(true);
        }
        private void ClearContent() 
        {
            var lbItems = ViewContent.GetComponentsInChildren<LBItem>();
            foreach(var item in lbItems)
            {
                Destroy(item.gameObject);
            }
        }

        public void Show()
        {
            playfabData.GetLeaderboard();
        }
    }
}