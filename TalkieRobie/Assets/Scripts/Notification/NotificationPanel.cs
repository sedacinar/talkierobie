using TMPro;
using UnityEngine;
namespace Sienar.TalkieRobie.Notification
{
    public class NotificationPanel : MonoBehaviour
    {
        [SerializeField]
        TMP_Text notificationTxt;
        [SerializeField]
        TMP_Text headerTxt;
        public void SetText(string txt)
        {
            notificationTxt.text = txt;
        }
        public void ShowTxt()
        {
            notificationTxt.gameObject.SetActive(true);
            headerTxt.gameObject.SetActive(true);
        }
        public void HideTxt()
        {
            notificationTxt.gameObject.SetActive(false);
            headerTxt.gameObject.SetActive(false);
        }
    }
}