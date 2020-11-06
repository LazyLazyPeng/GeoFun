namespace GeoFun.GNSS
{
    public enum enumDCBOption
    {
        /// <summary>
        /// 作为未知数估计
        /// </summary>
        Estimation,

        /// <summary>
        /// 已经估计过，直接从文件读取
        /// </summary>
        ReadFromFile,

        /// <summary>
        /// 忽略/估计
        /// </summary>
        Regardless,
    }
}