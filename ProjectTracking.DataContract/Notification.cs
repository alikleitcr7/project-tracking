using ProjectTracking.DataContract.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectTracking.DataContract
{

    public class Notification
    {
        public int ID { get; set; }
        public string FromUserId { get; set; }
        public string Message { get; set; }
        //public bool IsRead { get; set; }

        public NotificationType NotificationType
        {
            get
            {
                return (NotificationType)NotificationTypeCode;
            }
        }

        public short NotificationTypeCode { get; set; }
        public DateTime DateSent { get; set; }

        public string FromUserDisplay
        {
            get
            {
                return FromUser == null ? "-" : FromUser.FullName;
            }
        }

        public string DateSentDisplay
        {
            get
            {
                return DateSent.ToString("yyyy-MM-dd HH:mm:ss");

            }
        }

        public string DateSentTimeEllapsedDisplay
        {
            get
            {

                string display = "";

                TimeSpan diff = DateTime.Now - DateSent;

                if (diff.Days > 30)
                {
                    return DateSentDisplay;
                }

                if (diff.Days > 0)
                {
                    display = $"{diff.Days}d";
                }

                if (diff.Hours < 1)
                {
                    if (diff.Minutes < 1)
                    {
                        display += $"{diff.Seconds}s";
                    }
                    else
                    {
                        display += $"{diff.Minutes}m";
                    }
                }
                else
                {
                    display += $"{diff.Hours}h";
                }

                return display + " ago";
            }
        }

        public string NotificationTypeDisplay
        {
            get
            {
                return NotificationType.ToString();
            }
        }

        public User FromUser { get; set; }
    }

    public enum NotificationType { Default, Information, Important }
}