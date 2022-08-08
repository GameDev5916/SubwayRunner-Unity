using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CallNotifications : MonoBehaviour {

    [Header("Day of Week")]
    public List<DayOfWeek> dayOfWeek;

    public int minDate = 4;
    public int timeDelay = 10;//10 giay sau khi thuc thi lenh
    public bool loopRemind = true;
    void Start()
    {
        LocalNotification.CancelNotification(1);
        System.DateTime datetime = System.DateTime.Today;
        int today = (int)datetime.DayOfWeek;
        int dateRemind = 0;
        dateRemind = today + minDate;
        dateRemind = minDate + CountToDate(dateRemind);
        timeDelay = dateRemind * 24 * 60 * 60 + SecondOnDate(dateRemind - 1);
        print("=====\nRimind after: " + dateRemind + " days count down: " + timeDelay + " secs\n======");
#if !UNITY_EDITOR
        string titleNotification = AllLanguages.notifiTitle[Modules.indexLanguage];
        string contentNotification = AllLanguages.notifiContent[Modules.indexLanguage];
#endif
#if UNITY_IOS && !UNITY_EDITOR
        IOSNotification.RegisterForNotif();
        IOSNotification.CancelNotifications();
        IOSNotification.ScheduleNotification(timeDelay, loopRemind, titleNotification, contentNotification);
#elif UNITY_ANDROID && !UNITY_EDITOR
        if(loopRemind)
        LocalNotification.SendRepeatingNotification(1, timeDelay, timeDelay, titleNotification, contentNotification, new Color32(0xff, 0x44, 0x44, 255), true, true, true, "app_icon");
        else
        LocalNotification.SendNotification(1, timeDelay, titleNotification, contentNotification, new Color32(0xff, 0x44, 0x44, 255), true, true, true, "app_icon");
#endif
    }

    int CountToDate(int minDateRemind) {

        int selected = -1, countDate = -1;
        if (minDateRemind > 7) selected = minDateRemind - 7;
        else selected = minDateRemind;


        for (int i = selected - 1; i < dayOfWeek.Count; i++) {
            if (dayOfWeek[i].alowRemind)
                break;
            else 
                countDate += 1;
        }

        if (countDate == -1) {
            for (int i = 0; i < selected - 1; i++) {
                if (dayOfWeek[i].alowRemind)
                    break;
                else countDate += 1;
            }
        }

        return countDate + 1;
    }

    int SecondOnDate(int index) {
        int second = 0;
        System.DateTime current = System.DateTime.Now;
        System.DateTime setting;
        System.DateTime.TryParse(dayOfWeek[index].time, out setting);
        second = (setting.Hour * 3600 + setting.Minute * 60 + setting.Second) - (current.Hour * 3600 + current.Minute * 60 + current.Second);
        return second;
    }
}

[System.Serializable]
public class DayOfWeek{
    public string dayName;
    public bool alowRemind;
    public string time;
}
