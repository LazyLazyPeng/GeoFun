using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun.GNSS
{
    public class SP3Files
    {
        public int StartWeek { get; set; } = 0;
        public int StartDay { get; set; } = 0;

        public GPST startTime = null;
        public GPST StartTime
        {
            get
            {
                return startTime;
            }
        }


        public GPST endTime = null;
        public GPST EndTime
        {
            get
            {
                return endTime;
           }
        }


        public List<SP3File> Files = new List<SP3File>();

        public bool Merge(SP3File file)
        {
            if (Files is null) Files = new List<SP3File>();

            bool flag = false;
            if(Files.Count<1)
            {
                Files.Add(file);
                flag = true;
            }

            for (int i = 0; i < Files.Count; i++)
            {
                if (file.Week < Files[i].Week)
                {
                    Files.Insert(i, file);
                    flag = true;
                }
                else if(file.Week == Files[i].Week)
                {
                    if(file.Day<Files[i].Day)
                    {
                        Files.Insert(i,file);
                        flag = true;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            if(flag)
            {
                startTime = Files[0].StartTime;
                endTime = Files.Last().AllEpoch.Last().Epoch;
            }

            return flag;
        }

        public Coor3 GetSatCoor(GPST time)
        {
            return new Coor3();
        }
    }
}
