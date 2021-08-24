using System;
using System.Collections.Generic;
using System.Text;

namespace pressFtoZlata
{
    class Clock
    {
       public int hours;
       public int seconds;
       public int minutes;
        int time;

        public Clock()
        {
            time = 0;
            seconds = 0;
            hours = 0;
            minutes = 0;
        }

        public Clock(int time)
        {
            this.time = time;
            seconds = this.time;
            minutes = 0;
            hours = 0;
            while(seconds >= 60)
            {
                seconds -= 60;
                minutes++;
                if(minutes >= 60)
                {
                    minutes -= 60;
                    hours++;
                }
            }

        }

        public int GetTime()
        {
            return time;
        }

        public void IncTime()
        {
            time++;
            seconds++;
            while (seconds >= 60)
            {
                seconds -= 60;
                minutes++;
                if (minutes >= 60)
                {
                    minutes -= 60;
                    hours++;
                }
            }
        }

    }
}
