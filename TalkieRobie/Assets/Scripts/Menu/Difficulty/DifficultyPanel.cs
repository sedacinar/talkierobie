using UnityEngine;
using UnityEngine.UI;
using Sienar.TalkieRobie.DataCenter;
using Sienar.Unity.Core.Zenject.Core;
using TMPro;
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

        [SerializeField]
        TMP_Text LoadTxt;

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
            CloseButton();
        }

        void Normal() 
        {
            playfabData.GetCatalogsData(Difficulty.Normal);
            CloseButton();
        }

        void Hard() 
        {
            playfabData.GetCatalogsData(Difficulty.Hard);
            CloseButton();
        }

        void CloseButton()
        {
            LoadTxt.gameObject.SetActive(true);
            EasyBtn.interactable = false;
            NormalBtn.interactable = false;
            HardBtn.interactable = false;
        }
    }
}