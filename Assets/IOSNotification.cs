using UnityEngine;
using System.Collections;
using System;

public class IOSNotification : MonoBehaviour
{
    public static void RegisterForNotif()
    {
#if UNITY_IOS && !UNITY_EDITOR
        UnityEngine.iOS.NotificationServices.RegisterForNotifications(UnityEngine.iOS.NotificationType.Alert 
            | UnityEngine.iOS.NotificationType.Badge | UnityEngine.iOS.NotificationType.Sound);
#endif
    }

    public static void ScheduleNotification(int longDelay, bool repeat, string title, string message)
    {
#if UNITY_IOS && !UNITY_EDITOR
        UnityEngine.iOS.LocalNotification notif = new UnityEngine.iOS.LocalNotification();
        notif.fireDate = DateTime.Now.AddSeconds(longDelay);
        notif.alertAction = title;
        notif.alertBody = message;
        if(repeat)notif.repeatInterval = UnityEngine.iOS.CalendarUnit.Day;
        UnityEngine.iOS.NotificationServices.ScheduleLocalNotification(notif);
#endif
    }

    public static void CancelNotifications()
    {
#if UNITY_IOS && !UNITY_EDITOR
        UnityEngine.iOS.NotificationServices.ClearLocalNotifications();
        UnityEngine.iOS.NotificationServices.CancelAllLocalNotifications();
#endif
    }
}
