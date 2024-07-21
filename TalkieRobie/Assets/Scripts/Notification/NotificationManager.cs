using System;
using UnityEngine;
namespace Sienar.TalkieRobie.Notification
{
    public class NotificationManager : MonoBehaviour
    {
        NotificationCanvas notificationCanvas;
        private void Start()
        {
            notificationCanvas = FindObjectOfType<NotificationCanvas>();
        }
        public void Show(NotificationType notificationType, string msg = null)
        {
            var message = GetNotificationMessage(notificationType);
            notificationCanvas.Show(message + Environment.NewLine + msg);
        }
        string GetNotificationMessage(NotificationType notificationType)
        {
            var txt = "";
            if (notificationType == NotificationType.LoginError)
            {
                txt = "Login Error!";
                return txt;
            }
            else if(notificationType == NotificationType.CatalogError) 
            {
                txt = "Level Data Cannot Get!";
            }
            else if (notificationType == NotificationType.AudioError)
            {
                txt = "Audio Data Cannot Found!";
            }
            else if (notificationType == NotificationType.ImageError)
            {
                txt = "Image Data Cannot Found!";
            }
            return txt;
        }
    }
}