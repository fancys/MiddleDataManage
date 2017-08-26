namespace IntegratedManagement.Core.DataConvertEx
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/3/31 16:32:07
	===============================================================================================================================*/
    public static class DataConvertEx
    {
      


        #region 数据类型转换
        /// <summary>
        /// 将string类型转换成int类型，转换失败，返回默认值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaulteValue"></param>
        /// <returns></returns>
        public static int TryConvertParse(string value,int defaulteValue)
        {
            int returnValue;
            if (int.TryParse(value, out returnValue))
                return returnValue;
            else
                return defaulteValue;
        }
        #endregion
    }
}
