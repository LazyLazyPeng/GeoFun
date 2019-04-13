using System;
using System.Collections;
using System.Collections.Generic;

namespace GeoFun.GNSS
{
    public class SP3Header
    {
        //��һ��
        /// <summary>
        /// �汾��ʶ��
        /// </summary>
        public string Version;
        /// <summary>
        /// λ�ã�P����λ��/�ٶȣ�V����ʶ��
        /// </summary>
        public string P_V_Flag;
        /// <summary>
        /// �����������Ԫʱ��
        /// </summary>
        public GPST StartTime;
        /// <summary>
        /// ����Ԫ��Ŀ
        /// </summary>
        public int EpochNum;
        /// <summary>
        /// ���ݴ��������õ���������
        /// </summary>
        public string Data_Used;
        /// <summary>
        /// ������������������ϵ
        /// </summary>
        public string Coordinate_Sys;
        /// <summary>
        /// �������
        /// </summary>
        public string OrbitType;
        /// <summary>
        /// ��������
        /// </summary>
        public string Agency;
        //�ڶ���
        /// <summary>
        /// ����ԪGPSʱ��
        /// </summary>
        public GPST StartGPSTime;
        /// <summary>
        /// ��Ԫ���
        /// </summary>
        public double Epoch_Interval;
        //��3-7��
        /// <summary>
        /// ����������漰���ǵ�����
        /// </summary>
        public int Num_Sats;
        /// <summary>
        /// ���ǵ�PRN��
        /// </summary>
        public List<string> SatPRN;
        //��8-12��
        /// <summary>
        /// ���ǵľ���
        /// </summary>
        public List<string> SatAccuracy;

        /// <summary>
        /// �����Ԫʱ��
        /// </summary>
        public GPST EndTime;
    }
}