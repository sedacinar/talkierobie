using DG.Tweening;
using UnityEngine;
using Sienar.Unity.Core.Singleton;
namespace Sienar.TalkieRobie.Notification
{
    public class NotificationCanvas : Singleton<NotificationCanvas>
    {
        Vector2 initialSize;
        RectTransform panelRect;
        NotificationPanel notificationPanel;
        private void Awake()
        {
            var instance = FindObjectOfType<NotificationCanvas>();
            if (instance == this)
            {
                DontDestroyOnLoad(this.gameObject);
            }
            else 
            {
                Destroy(this.gameObject);
            }
        }
        private void Start()
        {
            notificationPanel = GetComponentInChildren<NotificationPanel>();
            panelRect = notificationPanel.GetComponent<RectTransform>();
            initialSize = panelRect.sizeDelta;
            Hide();
        }
        public void Show(string txt)
        {
            notificationPanel.SetText(txt);
            notificationPanel.gameObject.SetActive(true);
            Sequence sequence = DOTween.Sequence();
            sequence.Append(panelRect.DOSizeDelta(initialSize, 0.5f).OnComplete(() => notificationPanel.ShowTxt()));
            sequence.AppendInterval(3).OnComplete(() => notificationPanel.HideTxt());
            sequence.AppendCallback(notificationPanel.HideTxt);
            sequence.Append(panelRect.DOSizeDelta(Vector2.zero, 0.5f).OnComplete(() => Hide()));
        }
        void Hide()
        {
            notificationPanel.HideTxt();
            notificationPanel.gameObject.SetActive(false);
            panelRect.DOSizeDelta(Vector2.zero, 0.5f);
        }
    }
}