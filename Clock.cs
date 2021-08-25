﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MabinogiClock
{
    class Clock : INotifyPropertyChanged
    {
        public static DateTime Real2Mabinogi(DateTime realTime)
        {
            //爱琳时间24小时=现实36分钟，60分钟=90秒，1分钟=1.5秒
            var seconds = (realTime.Hour * 60 + realTime.Minute)*60 + realTime.Second + realTime.Millisecond/1000d;
            var mabiMinutes = (int)Math.Round(seconds / 1.5);
            var mabiHour = (mabiMinutes / 60) % 24;
            var mabiMinute = mabiMinutes % 60;
            return new DateTime(2021, 1, 1, mabiHour, mabiMinute, 0);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        bool _isEnabled;
        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                _isEnabled = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsEnabled"));
            }
        }
        public DateTime MabinogiTime { get; private set; }
        string _timeText;
        public string TimeText
        {
            get => _timeText;
            set
            {
                _timeText = value;
                try
                {
                    var t = value.Split(':','：');
                    MabinogiTime = new DateTime(2021, 1, 1, int.Parse(t[0]), int.Parse(t[1]), 0);
                    IsInvalid = false;
                }
                catch
                {
                    IsInvalid = true;
                }
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
            }
        }
        public bool IsInvalid { get; set; }

        MessageWindow window;
        public void ShowMessageBox()
        {
            if (window == null) window = new MessageWindow();
            window.t.Text = "洛奇时间" + TimeText;
            window.Show();
        }
    }
}
