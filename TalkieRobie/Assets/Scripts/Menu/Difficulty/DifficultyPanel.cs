using UnityEngine;
using UnityEngine.UI;
using Sienar.TalkieRobie.DataCenter;
using Sienar.Unity.Core.Zenject.Core;
namespace Sienar.TalkieRobie.Menu.Difficulty
{
    public class DifficultyPanel : MonoBehaviour
    {
        [SerializeField]
        Button EasyBtn;

        [SerializeField]
        Button NormalBtn;

        [SerializeField]
        Button HardBtn;

        PlayfabDataManager playfabData;

        private void Start()
        {
            EasyBtn.onClick.AddListener(Easy);
            NormalBtn.onClick.AddListener(Normal);
            HardBtn.onClick.AddListener(Hard);

            playfabData = DependencyContext.Get<PlayfabDataManager>();
        }

        void Easy()
        {
            playfabData.GetCatalogsData(Difficulty.Easy);
        }

        void Normal() 
        {
            playfabData.GetCatalogsData(Difficulty.Normal);
        }

        void Hard() 
        {
            playfabData.GetCatalogsData(Difficulty.Hard);
        }
    }
}