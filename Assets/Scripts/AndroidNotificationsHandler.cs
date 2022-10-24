using System;
using System.Collections;
using System.Collections.Generic;

#if UNITY_ANDROID
using Unity.Notifications.Android;
#endif

using UnityEngine;

public class AndroidNotificationsHandler : MonoBehaviour
{
#if UNITY_ANDROID
    private const string _channelId = "notification_channel";
    public void ScheduleNotification(DateTime dateTime)
    {
        AndroidNotificationChannel notificationChannel = new AndroidNotificationChannel
        {
            Id = _channelId,
            Name = "Notification Channel",
            Importance = Importance.Default
        };
        
        AndroidNotificationCenter.RegisterNotificationChannel(notificationChannel);

        AndroidNotification notification = new AndroidNotification
        {
            Title = "Energy Recharged!",
            Text = "You are able to play again, your energy is full!",
            SmallIcon = "default",
            LargeIcon = "default",
            FireTime = dateTime
        };

        AndroidNotificationCenter.SendNotification(notification, _channelId);
    }
#endif
}
