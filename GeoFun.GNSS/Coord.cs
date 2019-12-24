using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun.GNSS
{
    public class Coord
    {
        /// <summary>
        /// 计算测站的法向向量
        /// </summary>
        /// <param name="positionNEU">测站地理坐标 latitude/longitude/height</param>
        /// <returns></returns>
        public static double[,] getGroundStationOrientation(double[] positionNEU)
        {
            double[,] orien = new double[3, 3];

            // North
            orien[0, 0] = -Math.Sin(positionNEU[0]) * Math.Cos(positionNEU[1]);
            orien[0, 1] = -Math.Sin(positionNEU[0]) * Math.Sin(positionNEU[1]);
            orien[0, 2] = Math.Cos(positionNEU[0]);

            // East
            orien[1, 0] = -Math.Sin(positionNEU[1]);
            orien[1, 1] = Math.Cos(positionNEU[1]);
            orien[1, 2] = 0;

            // Up (Zenith)
            orien[2, 0] = Math.Cos(positionNEU[0]) * Math.Cos(positionNEU[1]);
            orien[2, 1] = Math.Cos(positionNEU[0]) * Math.Sin(positionNEU[1]);
            orien[2, 2] = Math.Sin(positionNEU[0]);

            return null;
        }

        /// <summary>
        /// 计算方位角，高度角
        /// </summary>
        /// <param name="orientation">测站法向量</param>
        /// <param name="receiverPosition">接收机坐标</param>
        /// <param name="satellitePosition">卫星坐标</param>
        /// <param name="azimuth">方位角(弧度)</param>
        /// <param name="elevation">高度角(弧度)</param>
        void getAzimuthElevation(double[,] orientation,
            double[] receiverPosition, double[] satellitePosition,
            out double azimuth, out double elevation)
        {
            azimuth = 0d;
            elevation = 0d;

            int i = 0;
            double[] relativeLoS = new double[3];
            double distance = 0d;

            distance = 0;

            for (i = 0; i < 3; i++)
            {
                distance += (satellitePosition[i] - receiverPosition[i]) * (satellitePosition[i] - receiverPosition[i]);
            }

            distance = Math.Sqrt(distance);
            for (i = 0; i < 3; i++)
            {
                relativeLoS[i] = ((satellitePosition[0] - receiverPosition[0]) * orientation[i,0] +
                                  (satellitePosition[1] - receiverPosition[1]) * orientation[i,1] +
                                  (satellitePosition[2] - receiverPosition[2]) * orientation[i,2]) / distance;
            }

            azimuth = Math.Atan2(relativeLoS[1], relativeLoS[0]);
            elevation = Math.Asin(relativeLoS[2]);
        }
    }
}
