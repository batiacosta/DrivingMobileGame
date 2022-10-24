using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_IOS
using System;
using Unity.Notifications.iOS;
#endif

public class iOSNotificationHandler : MonoBehaviour
{
#if UNITY_IOS
    public void ScheduleNotification(int minutes)
    {
        iOSNotification notification = new iOSNotification
        {
            Title = "Energy recharged!",
            Subtitle = "Your energy is recharged!",
            Body = "Your energy has been recharge! lets play again!",
            ShowInForeground = true,
            ForegroundPresentationOption = (PresentationOption.Alert | PresentationOption.Sound),
            CategoryIdentifier = "category_a",
            ThreadIdentifier = "thread1",
            Trigger = new iOSNotificationTimeIntervalTrigger
            {
                TimeInterval = new System.TimeSpan(0, minutes, 0),
                Repeats = false
            }
        };
        
        iOSNotificationCenter.ScheduleNotification(notification);
    }
#endif
}
