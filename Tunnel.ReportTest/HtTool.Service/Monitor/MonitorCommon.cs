using System;
using System.Collections.Generic;
using HtTool.Service.Contracts;

namespace HtTool.Service.Monitor
{
    public static class MonitorCommon
    {
        public static string[] StrManLeve = { "III", "II", "I" };
        public static string[] StrStatus = { "可正常施工", "应加强支护", "应采取特殊措施" };
        public static string[] StrVelInfo = { "围岩基本达到稳定", "围岩仍处于缓慢变形增长阶段，应观注围岩动态，检查支护裂纹状态!", 
                                                "处于急剧变形状态，应加强初期支护系统！" };
        public static string[] StrAccInfo =
            {
                "速率不断下降，围岩趋于稳定状态。", 
                "围岩不稳定，需加强安全巡查，必要时应加强支护！",
                //"速率逐渐增大，黄色预警（第一次），建议加强观察！（注：若累计位移、位移速率较小时，采用加强观测处理。）",
                //"速率逐渐增大，橙色预警（第二次），加强观测观察！（注：若累计位移、位移速率较小时，采用加强观测处理。）",
                "速率逐渐增大，围岩进入危险状态，必须立即停止掘进，加强支护！"
            };


        public static int DayOrHours = 1;
        /// <summary>
        /// 根据每一条测线的数据生成point数组
        /// </summary>
        /// <param name="lines"></param>
        /// <returns></returns>
        public static MPoint[] AddNewPoint(List<SectionLine> lines, bool isRt = false)
        {
            string strStart = "";
            string strEnd = "";
            float xmin = 0;
            double xmax = 0;

            int npts = 0;

            if (lines == null || lines.Count == 0)
            {
                return new MPoint[0];
            }

            if (lines[0].RecordTime != null && lines[0].RecordTime.ToString() != "")
            {
                strStart = lines[0].RecordTime.ToString(); //得到开始时间                  
            }

            //var data = (MPoint[])new MPoint[lines.Count];
            var data = new List<MPoint>();
            // float dx = (float)12.5; //李总要求修改前

            float dx = (float)1.0; //修改后

            for (int i = 0; i < lines.Count; i++)
            {
                if (Math.Abs(lines[i].AvgResult) > 0)
                {
                    var x = TotleTime(lines[0].RecordTime.ToString(), lines[i].RecordTime.ToString(),isRt);
                    var mp = new MPoint();
                    mp.X = xmin + dx * (float)x; //时间差
                    mp.Y = (float)Convert.ToDouble(lines[i].CumSpeed);//累积位移
                    data.Add(mp);
                }
            }

            return data.ToArray();
        }

        /// <summary>
        /// 根据每一条测线的数据生成point数组
        /// </summary>
        /// <param name="lines"></param>
        /// <returns></returns>
        public static ChartPoint[] AddNewChartPoint(List<SectionLine> lines)
        {
            string strStart = "";
            string strEnd = "";
            float xmin = 0;
            double xmax = 0;

            int npts = 0;

            if (lines == null || lines.Count == 0)
            {
                return new ChartPoint[0];
            }


            var data = (ChartPoint[])new ChartPoint[lines.Count];
            // float dx = (float)12.5; //李总要求修改前

            float dx = (float)1.0; //修改后

            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].AvgResult == 0)//这个地方如果没数据怎么办
                    continue;
                if (lines[i].RecordTime != null)
                {
                    var mp = new ChartPoint();
                    mp.X = lines[i].RecordTime.Value; //时间差
                    mp.Y = (float)Convert.ToDouble(lines[i].CumSpeed);//累积位移
                    data[i] = mp;
                }
            }

            return data;
        }

        /// <summary>
        /// 计算天数
        /// </summary>
        /// <param name="strFirst"></param>
        /// <param name="strSecond"></param>
        /// <returns></returns>
        public static double TotleTime(string strFirst, string strSecond, bool isRt = false)
        {
            double iTotle = 0;
            DateTime endDate;
            DateTime startDate;
            if (string.IsNullOrEmpty(strFirst) || string.IsNullOrEmpty(strSecond)
                || !DateTime.TryParse(strFirst, out startDate) || !DateTime.TryParse(strSecond, out endDate))
            {
                return iTotle;
            }
            DateTime start = Convert.ToDateTime(startDate);
            DateTime end = Convert.ToDateTime(endDate);

            TimeSpan sp = end.Subtract(start);

            //int iDays = Convert.ToDateTime(strSecond.Substring(0, 10)).Subtract(Convert.ToDateTime(strFirst.Substring(0, 10))).Days;

            //int iTime = SecondPart(strSecond) - SecondPart(strFirst);


            // iTotle = iDays * 4 + iTime;  //李总要求修改，只计算天数
            //if (DayOrHours == 2)
            return isRt ? sp.TotalMinutes : sp.TotalDays;
            //else
            //iTotle = sp.TotalDays;

        }

        /// <summary>
        /// 计算对数曲线参数y = a + bLnx
        /// </summary>
        /// <returns></returns>
        public static List<CurvePara> CalculateLogParameters(List<PointData> pointDataList)
        {
            //计算对数曲线参数
            //y = a + bLnx

            var ia1 = 0;
            var b1 = 0.0;
            var a1 = 0.0;
            var r1ab = 0.0;

            var logParaList = new List<CurvePara>();
            if (pointDataList.Count > 0)
            {
                for (int j = 0; j < pointDataList.Count; j++)
                {
                    double sumx1 = 0, sumx12 = 0, sumy = 0, sumy2 = 0, sumx1y = 0, ssx1 = 0, ssy = 0, spx1y = 0, xbar1 = 0, ybar = 0, sumyyhat2 = 0, sumyybar2 = 0;
                    var logPare = new CurvePara();
                    try
                    {
                        var gPts = pointDataList[j].MData;

                        for (int i = 2; i < gPts.Length; i++)
                        {
                            sumx1 = sumx1 + Math.Log(gPts[i].X);
                            sumx12 = sumx12 + Math.Log(gPts[i].X) * Math.Log(gPts[i].X);
                            sumy = sumy + gPts[i].Y;
                            sumy2 = sumy2 + gPts[i].Y * gPts[i].Y;
                            sumx1y = sumx1y + Math.Log(gPts[i].X) * gPts[i].Y;
                        }

                        ssx1 = sumx12 - sumx1 * sumx1 / (double)(gPts.Length - 2);
                        ssy = sumy2 - sumy * sumy / (double)(gPts.Length - 2);
                        spx1y = sumx1y - sumx1 * sumy / (double)(gPts.Length - 2);
                        xbar1 = sumx1 / (double)(gPts.Length - 2);
                        ybar = sumy / (double)(gPts.Length - 2);

                        b1 = spx1y / ssx1;
                        a1 = ybar - b1 * xbar1;

                        ia1 = 1;

                        for (int i = 2; i < gPts.Length - 2; i++)
                        {
                            sumyyhat2 = sumyyhat2 +
                                        (gPts[i].Y - a1 - b1 * Math.Log(gPts[i].X)) *
                                        (gPts[i].Y - a1 - b1 * Math.Log(gPts[i].X));
                            sumyybar2 = sumyybar2 + (gPts[i].Y - ybar) * (gPts[i].Y - ybar);
                        }

                        double tempRest = 1 - sumyyhat2 / sumyybar2;

                        if (tempRest > 0)
                            r1ab = Math.Sqrt(tempRest);
                        else
                            r1ab = Math.Sqrt(-tempRest);

                    }
                    catch (Exception ex1)
                    {
                        r1ab = 0;
                        ia1 = 0;
                    }
                    finally
                    {
                        logPare.A = double.IsNaN(a1)?0:a1;
                        logPare.B = double.IsNaN(b1) ? 0 : b1; 
                        logPare.Ia = ia1;
                        logPare.R = double.IsNaN(r1ab) ? 0 : r1ab; 
                        logPare.Name = pointDataList[j].Name;
                        logPare.Type = pointDataList[j].Type;
                        logParaList.Add(logPare);
                    }

                }

            }
            return logParaList;
        }

        /// <summary>
        /// 隧道设计预留变形量作为极限位移
        /// </summary>
        /// <param name="fInputY"></param>
        /// <param name="dA"></param>
        /// <param name="dB"></param>
        /// <param name="gStrU0"></param>
        /// <param name="disValue"></param>
        /// <returns></returns>
        public static int FLastPointOff(float fInputY, double dA, double dB, double gStrU0, out double disValue)
        {
            double dTempRest = fInputY;
            int iRet = 0;

            if (Math.Abs(dTempRest) < gStrU0 / 3)//施工状态
                iRet = 0;
            else if (Math.Abs(dTempRest) <= gStrU0 * 2 / 3)//警戒，及时并加强支护
                iRet = 1;
            else
                iRet = 2;//应采取特殊措施

            disValue = dTempRest;
            return iRet;
        }

        /// <summary>
        /// 隧道设计预留变形量作为极限位移
        /// </summary>
        /// <param name="fInputY"></param>
        /// <param name="dA"></param>
        /// <param name="dB"></param>
        /// <param name="gStrU0"></param>
        /// <returns></returns>
        public static int FLastPointOff(float fInputY, double dA, double dB, double gStrU0)
        {
            double dTempRest = fInputY;
            int iRet = 0;

            if (Math.Abs(dTempRest) < gStrU0 / 3)//施工状态
                iRet = 0;
            else if (Math.Abs(dTempRest) <= gStrU0 * 2 / 3)//警戒，及时并加强支护
                iRet = 1;
            else
                iRet = 2;//应采取特殊措施

            return iRet;
        }

        /// <summary>
        /// 按围岩变形速率值评判标准
        /// </summary>
        /// <param name="fInputX"></param>
        /// <param name="fInputY"></param>
        /// <param name="dA"></param>
        /// <param name="dB"></param>
        /// <param name="iFunctionType"></param>
        /// <param name="vccValue"></param>
        /// <returns></returns>
        public static int FLastPointVec(float fInputX, float fInputY, double dA, double dB, int iFunctionType, out double vccValue)
        {
            double dTempRest = 0;
            int iRet = 0;
            switch (iFunctionType)
            {
                case 1:
                    dTempRest = dB / fInputX;
                    break;
                case 2:
                    dTempRest = dA * dB * Math.Exp(dB * fInputX);
                    break;
                case 3:
                    dTempRest = dB / (-fInputX * fInputX);
                    break;
                default:
                    //不应该到这里
                    break;
            }
            vccValue = dTempRest;

            if (fInputY > 0)
            {
                if (dTempRest < 0)
                    iRet = 0;
                else
                {
                    if (dTempRest > 5)
                        iRet = 2;
                    else if (dTempRest > 3)
                        iRet = 2;
                    else if (dTempRest > 1)
                        iRet = 1;
                    else if (dTempRest >= 0.2)
                        iRet = 1;
                    else
                        iRet = 0;
                }
            }
            else
            {
                if (dTempRest > 0)
                    iRet = 0;
                else
                {
                    dTempRest = -dTempRest;

                    if (dTempRest > 5)
                        iRet = 2;
                    else if (dTempRest > 3)
                        iRet = 2;
                    else if (dTempRest > 1)
                        iRet = 1;
                    else if (dTempRest >= 0.2)
                        iRet = 1;
                    else
                        iRet = 0;

                }

            }
            return iRet;
        }

        /// <summary>
        /// 按围岩变形速率值评判标准
        /// </summary>
        /// <param name="fInputX"></param>
        /// <param name="fInputY"></param>
        /// <param name="dA"></param>
        /// <param name="dB"></param>
        /// <param name="iFunctionType"></param>
        /// <returns></returns>
        public static int FLastPointVec(float fInputX, float fInputY, double dA, double dB, int iFunctionType)
        {
            double dTempRest = 0;
            int iRet = 0;
            switch (iFunctionType)
            {
                case 1:
                    dTempRest = dB / fInputX;
                    break;
                case 2:
                    dTempRest = dA * dB * Math.Exp(dB * fInputX);
                    break;
                case 3:
                    dTempRest = dB / (-fInputX * fInputX);
                    break;
                default:
                    //不应该到这里
                    break;
            }

            if (fInputY > 0)
            {
                if (dTempRest < 0)
                    iRet = 0;
                else
                {
                    if (dTempRest > 5)
                        iRet = 2;
                    else if (dTempRest > 3)
                        iRet = 2;
                    else if (dTempRest > 1)
                        iRet = 1;
                    else if (dTempRest >= 0.2)
                        iRet = 1;
                    else
                        iRet = 0;
                }
            }
            else
            {
                if (dTempRest > 0)
                    iRet = 0;
                else
                {
                    dTempRest = -dTempRest;

                    if (dTempRest > 5)
                        iRet = 2;
                    else if (dTempRest > 3)
                        iRet = 2;
                    else if (dTempRest > 1)
                        iRet = 1;
                    else if (dTempRest >= 0.2)
                        iRet = 1;
                    else
                        iRet = 0;

                }

            }
            return iRet;
        }

        /// <summary>
        /// 按变形曲线形态判断围岩稳定标准
        /// </summary>
        /// <param name="fInputX"></param>
        /// <param name="fInputX2"></param>
        /// <param name="fInputX3"></param>
        /// <param name="fInputY"></param>
        /// <param name="fInputY2"></param>
        /// <param name="fInputY3"></param>
        /// <param name="dA"></param>
        /// <param name="dB"></param>
        /// <param name="iFunctionType"></param>
        /// <returns></returns>
        public static int FLastPointAcc(float fInputX, float fInputX2, float fInputX3, float fInputY, float fInputY2, float fInputY3, double dA, double dB, int iFunctionType)
        {
            double dTempRest = 0;
            int iRet = 0;


            switch (iFunctionType)
            {
                case 1:
                    dTempRest = dB / (-fInputX * fInputX);
                    break;
                case 2:
                    dTempRest = dA * dB * dB * Math.Exp(dB * fInputX);
                    break;
                case 3:
                    dTempRest = dB / (0.5 * fInputX * fInputX * fInputX);
                    break;

                default:
                    //不应该到这里
                    break;
            }

            double originalValue = fInputY;// dk[0] + dk[1] * fInputX + dk[2] * fInputX * fInputX + dk[3] * fInputX * fInputX * fInputX + dk[4] * fInputX * fInputX * fInputX * fInputX + dk[5] * fInputX * fInputX * fInputX * fInputX * fInputX + dk[6] * fInputX * fInputX * fInputX * fInputX * fInputX * fInputX;



            if (dTempRest > 0.0005)  //这个肯定是 >0
            {
                if (originalValue < 0)
                    iRet = 0;
                else
                {
                    //速度 > 0, 位移 > 0第一次了

                    //第二次
                    switch (iFunctionType)
                    {
                        case 1:
                            dTempRest = dB / (-fInputX2 * fInputX2);
                            break;
                        case 2:
                            dTempRest = dA * dB * dB * Math.Exp(dB * fInputX2);
                            break;
                        case 3:
                            dTempRest = dB / (0.5 * fInputX2 * fInputX2 * fInputX2);
                            break;

                        default:
                            //不应该到这里
                            break;
                    }

                    originalValue = fInputY2;// dk[0] + dk[1] * fInputX2 + dk[2] * fInputX2 * fInputX2 + dk[3] * fInputX2 * fInputX2 * fInputX2 + dk[4] * fInputX2 * fInputX2 * fInputX2 * fInputX2 + dk[5] * fInputX2 * fInputX2 * fInputX2 * fInputX2 * fInputX2 + dk[6] * fInputX2 * fInputX2 * fInputX2 * fInputX2 * fInputX2 * fInputX2;

                    if (dTempRest > 0.0005) //第二层
                    {
                        if (originalValue < 0)
                            iRet = 1;
                        else
                        { //第三次

                            switch (iFunctionType)
                            {
                                case 1:
                                    dTempRest = dB / (-fInputX3 * fInputX3);
                                    break;
                                case 2:
                                    dTempRest = dA * dB * dB * Math.Exp(dB * fInputX3);
                                    break;
                                case 3:
                                    dTempRest = dB / (0.5 * fInputX3 * fInputX3 * fInputX3);
                                    break;

                                default:
                                    //不应该到这里
                                    break;
                            }
                            originalValue = fInputY3;//  dk[0] + dk[1] * fInputX3 + dk[2] * fInputX3 * fInputX3 + dk[3] * fInputX3 * fInputX3 * fInputX3 + dk[4] * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[5] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[6] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3;

                            if (dTempRest > 0.0005) //第三层
                            {
                                if (originalValue < 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }
                            else if (dTempRest > 0)//第三层
                            {
                                if (originalValue < 0)
                                    iRet = 1;
                                else
                                    iRet = 2;

                            }
                            else if (dTempRest > -0.0005)//第三层
                            {

                                if (originalValue > 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }
                            else// if (dTempRest < -0.0005)
                            {
                                if (originalValue > 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }

                            //第三次 完成
                        }
                    }
                    else if (dTempRest > 0)//第二层  0~0.0005
                    {
                        if (originalValue < 0)
                            iRet = 1;
                        else
                        {
                            //下面是原来的处理
                            //加速度在-0.0005 ~ 0 之间，无论如何都是初步报警，因此看下一层
                            //第三次

                            switch (iFunctionType)
                            {
                                case 1:
                                    dTempRest = dB / (-fInputX3 * fInputX3);
                                    break;
                                case 2:
                                    dTempRest = dA * dB * dB * Math.Exp(dB * fInputX3);
                                    break;
                                case 3:
                                    dTempRest = dB / (0.5 * fInputX3 * fInputX3 * fInputX3);
                                    break;

                                default:
                                    //不应该到这里
                                    break;
                            }

                            originalValue = fInputY3;//  dk[0] + dk[1] * fInputX3 + dk[2] * fInputX3 * fInputX3 + dk[3] * fInputX3 * fInputX3 * fInputX3 + dk[4] * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[5] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[6] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3;

                            if (dTempRest > 0.0005) //第三层
                            {
                                if (originalValue < 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }
                            else if (dTempRest > 0)//第三层
                            {

                                if (originalValue < 0)
                                    iRet = 1;
                                else
                                    iRet = 2;

                            }
                            else if (dTempRest > -0.0005)//第三层
                            {

                                if (originalValue > 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }
                            else// if (dTempRest < -0.0005)
                            {
                                if (originalValue > 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }

                            //第三次 完成
                        }

                    }
                    else if (dTempRest > -0.0005)//第二层
                    {
                        if (originalValue > 0)
                            iRet = 1;
                        else
                        {

                            //第三次

                            switch (iFunctionType)
                            {
                                case 1:
                                    dTempRest = dB / (-fInputX3 * fInputX3);
                                    break;
                                case 2:
                                    dTempRest = dA * dB * dB * Math.Exp(dB * fInputX3);
                                    break;
                                case 3:
                                    dTempRest = dB / (0.5 * fInputX3 * fInputX3 * fInputX3);
                                    break;

                                default:
                                    //不应该到这里
                                    break;
                            }

                            originalValue = fInputY3;//  dk[0] + dk[1] * fInputX3 + dk[2] * fInputX3 * fInputX3 + dk[3] * fInputX3 * fInputX3 * fInputX3 + dk[4] * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[5] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[6] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3;

                            if (dTempRest > 0.0005) //第三层
                            {
                                if (originalValue < 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }
                            else if (dTempRest > 0)//第三层
                            {
                                if (originalValue < 0)
                                    iRet = 1;
                                else
                                    iRet = 2;

                            }
                            else if (dTempRest > -0.0005)//第三层
                            {

                                if (originalValue > 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }
                            else// if (dTempRest < -0.0005)
                            {
                                if (originalValue > 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }

                            //第三次 完成

                        }

                    }
                    else// if (dTempRest < -0.0005) //第二层
                    {
                        if (originalValue > 0)
                            iRet = 1;
                        else
                        {  //第三次
                            switch (iFunctionType)
                            {
                                case 1:
                                    dTempRest = dB / (-fInputX3 * fInputX3);
                                    break;
                                case 2:
                                    dTempRest = dA * dB * dB * Math.Exp(dB * fInputX3);
                                    break;
                                case 3:
                                    dTempRest = dB / (0.5 * fInputX3 * fInputX3 * fInputX3);
                                    break;

                                default:
                                    //不应该到这里
                                    break;
                            }

                            originalValue = fInputY3;//  dk[0] + dk[1] * fInputX3 + dk[2] * fInputX3 * fInputX3 + dk[3] * fInputX3 * fInputX3 * fInputX3 + dk[4] * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[5] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[6] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3;

                            if (dTempRest > 0.0005) //第三层
                            {
                                if (originalValue < 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }
                            else if (dTempRest > 0)//第三层
                            {
                                if (originalValue < 0)
                                    iRet = 1;
                                else
                                    iRet = 2;

                            }
                            else if (dTempRest > -0.0005)//第三层
                            {

                                if (originalValue > 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }
                            else// if (dTempRest < -0.0005)
                            {
                                if (originalValue > 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }

                            //第三次 完成
                        }
                    }


                    //第二次完成
                }
            }//第一层
            else if (dTempRest > 0)
            { //加速度0~0.0005，不管累积位移是多少，都应该初步报警

                if (originalValue < 0)
                    iRet = 0;
                else
                {  //已经有一次报警了

                    //第二次

                    switch (iFunctionType)
                    {
                        case 1:
                            dTempRest = dB / (-fInputX2 * fInputX2);
                            break;
                        case 2:
                            dTempRest = dA * dB * dB * Math.Exp(dB * fInputX2);
                            break;
                        case 3:
                            dTempRest = dB / (0.5 * fInputX2 * fInputX2 * fInputX2);
                            break;

                        default:
                            //不应该到这里
                            break;
                    }

                    originalValue = fInputY2;// dk[0] + dk[1] * fInputX2 + dk[2] * fInputX2 * fInputX2 + dk[3] * fInputX2 * fInputX2 * fInputX2 + dk[4] * fInputX2 * fInputX2 * fInputX2 * fInputX2 + dk[5] * fInputX2 * fInputX2 * fInputX2 * fInputX2 * fInputX2 + dk[6] * fInputX2 * fInputX2 * fInputX2 * fInputX2 * fInputX2 * fInputX2;

                    if (dTempRest > 0.0005) //第二层
                    {
                        if (originalValue < 0)
                            iRet = 1;
                        else
                        { //第三次

                            switch (iFunctionType)
                            {
                                case 1:
                                    dTempRest = dB / (-fInputX3 * fInputX3);
                                    break;
                                case 2:
                                    dTempRest = dA * dB * dB * Math.Exp(dB * fInputX3);
                                    break;
                                case 3:
                                    dTempRest = dB / (0.5 * fInputX3 * fInputX3 * fInputX3);
                                    break;

                                default:
                                    //不应该到这里
                                    break;
                            }

                            originalValue = fInputY3;//  dk[0] + dk[1] * fInputX3 + dk[2] * fInputX3 * fInputX3 + dk[3] * fInputX3 * fInputX3 * fInputX3 + dk[4] * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[5] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[6] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3;

                            if (dTempRest > 0.0005) //第三层
                            {
                                if (originalValue < 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }
                            else if (dTempRest > 0)//第三层
                            {
                                if (originalValue < 0)
                                    iRet = 1;
                                else
                                    iRet = 2;

                            }
                            else if (dTempRest > -0.0005)//第三层
                            {

                                if (originalValue > 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }
                            else// if (dTempRest < -0.0005)
                            {
                                if (originalValue > 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }

                            //第三次 完成
                        }
                    }
                    else if (dTempRest > 0)//第二层  0~0.0005
                    {
                        if (originalValue < 0)
                            iRet = 1;
                        else
                        {
                            //下面是原来的处理
                            //加速度在-0.0005 ~ 0 之间，无论如何都是初步报警，因此看下一层
                            //第三次

                            switch (iFunctionType)
                            {
                                case 1:
                                    dTempRest = dB / (-fInputX3 * fInputX3);
                                    break;
                                case 2:
                                    dTempRest = dA * dB * dB * Math.Exp(dB * fInputX3);
                                    break;
                                case 3:
                                    dTempRest = dB / (0.5 * fInputX3 * fInputX3 * fInputX3);
                                    break;

                                default:
                                    //不应该到这里
                                    break;
                            }

                            originalValue = fInputY3;//  dk[0] + dk[1] * fInputX3 + dk[2] * fInputX3 * fInputX3 + dk[3] * fInputX3 * fInputX3 * fInputX3 + dk[4] * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[5] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[6] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3;

                            if (dTempRest > 0.0005) //第三层
                            {
                                if (originalValue < 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }
                            else if (dTempRest > 0)//第三层
                            {

                                if (originalValue < 0)
                                    iRet = 1;
                                else
                                    iRet = 2;

                            }
                            else if (dTempRest > -0.0005)//第三层
                            {

                                if (originalValue > 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }
                            else// if (dTempRest < -0.0005)
                            {
                                if (originalValue > 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }

                            //第三次 完成
                        }

                    }
                    else if (dTempRest > -0.0005)//第二层
                    {
                        if (originalValue > 0)
                            iRet = 1;
                        else
                        {

                            //第三次

                            switch (iFunctionType)
                            {
                                case 1:
                                    dTempRest = dB / (-fInputX3 * fInputX3);
                                    break;
                                case 2:
                                    dTempRest = dA * dB * dB * Math.Exp(dB * fInputX3);
                                    break;
                                case 3:
                                    dTempRest = dB / (0.5 * fInputX3 * fInputX3 * fInputX3);
                                    break;

                                default:
                                    //不应该到这里
                                    break;
                            }

                            originalValue = fInputY3;//  dk[0] + dk[1] * fInputX3 + dk[2] * fInputX3 * fInputX3 + dk[3] * fInputX3 * fInputX3 * fInputX3 + dk[4] * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[5] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[6] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3;

                            if (dTempRest > 0.0005) //第三层
                            {
                                if (originalValue < 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }
                            else if (dTempRest > 0)//第三层
                            {
                                if (originalValue < 0)
                                    iRet = 1;
                                else
                                    iRet = 2;

                            }
                            else if (dTempRest > -0.0005)//第三层
                            {

                                if (originalValue > 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }
                            else// if (dTempRest < -0.0005)
                            {
                                if (originalValue > 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }

                            //第三次 完成

                        }

                    }
                    else// if (dTempRest < -0.0005) //第二层
                    {
                        if (originalValue > 0)
                            iRet = 1;
                        else
                        {  //第三次

                            switch (iFunctionType)
                            {
                                case 1:
                                    dTempRest = dB / (-fInputX3 * fInputX3);
                                    break;
                                case 2:
                                    dTempRest = dA * dB * dB * Math.Exp(dB * fInputX3);
                                    break;
                                case 3:
                                    dTempRest = dB / (0.5 * fInputX3 * fInputX3 * fInputX3);
                                    break;

                                default:
                                    //不应该到这里
                                    break;
                            }

                            originalValue = fInputY3;// dk[0] + dk[1] * fInputX3 + dk[2] * fInputX3 * fInputX3 + dk[3] * fInputX3 * fInputX3 * fInputX3 + dk[4] * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[5] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[6] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3;

                            if (dTempRest > 0.0005) //第三层
                            {
                                if (originalValue < 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }
                            else if (dTempRest > 0)//第三层
                            {
                                if (originalValue < 0)
                                    iRet = 1;
                                else
                                    iRet = 2;

                            }
                            else if (dTempRest > -0.0005)//第三层
                            {

                                if (originalValue > 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }
                            else// if (dTempRest < -0.0005)
                            {
                                if (originalValue > 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }

                            //第三次 完成
                        }
                    }


                    //第二次完成


                    //第二次完成
                }

            }//第一层
            else if (dTempRest > -0.0005)
            {
                if (originalValue > 0)
                    iRet = 0;
                else
                { //第二层


                    //第二次

                    switch (iFunctionType)
                    {
                        case 1:
                            dTempRest = dB / (-fInputX2 * fInputX2);
                            break;
                        case 2:
                            dTempRest = dA * dB * dB * Math.Exp(dB * fInputX2);
                            break;
                        case 3:
                            dTempRest = dB / (0.5 * fInputX2 * fInputX2 * fInputX2);
                            break;

                        default:
                            //不应该到这里
                            break;
                    }
                    originalValue = fInputY2;//  dk[0] + dk[1] * fInputX2 + dk[2] * fInputX2 * fInputX2 + dk[3] * fInputX2 * fInputX2 * fInputX2 + dk[4] * fInputX2 * fInputX2 * fInputX2 * fInputX2 + dk[5] * fInputX2 * fInputX2 * fInputX2 * fInputX2 * fInputX2 + dk[6] * fInputX2 * fInputX2 * fInputX2 * fInputX2 * fInputX2 * fInputX2;

                    if (dTempRest > 0.0005) //第二层
                    {
                        if (originalValue < 0)
                            iRet = 1;
                        else
                        { //第三次


                            switch (iFunctionType)
                            {
                                case 1:
                                    dTempRest = dB / (-fInputX3 * fInputX3);
                                    break;
                                case 2:
                                    dTempRest = dA * dB * dB * Math.Exp(dB * fInputX3);
                                    break;
                                case 3:
                                    dTempRest = dB / (0.5 * fInputX3 * fInputX3 * fInputX3);
                                    break;

                                default:
                                    //不应该到这里
                                    break;
                            }

                            originalValue = fInputY3;// dk[0] + dk[1] * fInputX3 + dk[2] * fInputX3 * fInputX3 + dk[3] * fInputX3 * fInputX3 * fInputX3 + dk[4] * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[5] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[6] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3;

                            if (dTempRest > 0.0005) //第三层
                            {
                                if (originalValue < 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }
                            else if (dTempRest > 0)//第三层
                            {
                                if (originalValue < 0)
                                    iRet = 1;
                                else
                                    iRet = 2;

                            }
                            else if (dTempRest > -0.0005)//第三层
                            {

                                if (originalValue > 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }
                            else// if (dTempRest < -0.0005)
                            {
                                if (originalValue > 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }

                            //第三次 完成
                        }
                    }
                    else if (dTempRest > 0)//第二层  0~0.0005
                    {
                        if (originalValue < 0)
                            iRet = 1;
                        else
                        {
                            //下面是原来的处理
                            //加速度在-0.0005 ~ 0 之间，无论如何都是初步报警，因此看下一层
                            //第三次

                            switch (iFunctionType)
                            {
                                case 1:
                                    dTempRest = dB / (-fInputX3 * fInputX3);
                                    break;
                                case 2:
                                    dTempRest = dA * dB * dB * Math.Exp(dB * fInputX3);
                                    break;
                                case 3:
                                    dTempRest = dB / (0.5 * fInputX3 * fInputX3 * fInputX3);
                                    break;

                                default:
                                    //不应该到这里
                                    break;
                            }

                            originalValue = fInputY3;// dk[0] + dk[1] * fInputX3 + dk[2] * fInputX3 * fInputX3 + dk[3] * fInputX3 * fInputX3 * fInputX3 + dk[4] * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[5] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[6] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3;

                            if (dTempRest > 0.0005) //第三层
                            {
                                if (originalValue < 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }
                            else if (dTempRest > 0)//第三层
                            {

                                if (originalValue < 0)
                                    iRet = 1;
                                else
                                    iRet = 2;

                            }
                            else if (dTempRest > -0.0005)//第三层
                            {

                                if (originalValue > 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }
                            else// if (dTempRest < -0.0005)
                            {
                                if (originalValue > 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }

                            //第三次 完成
                        }

                    }
                    else if (dTempRest > -0.0005)//第二层
                    {
                        if (originalValue > 0)
                            iRet = 1;
                        else
                        {

                            //第三次

                            switch (iFunctionType)
                            {
                                case 1:
                                    dTempRest = dB / (-fInputX3 * fInputX3);
                                    break;
                                case 2:
                                    dTempRest = dA * dB * dB * Math.Exp(dB * fInputX3);
                                    break;
                                case 3:
                                    dTempRest = dB / (0.5 * fInputX3 * fInputX3 * fInputX3);
                                    break;

                                default:
                                    //不应该到这里
                                    break;
                            }

                            originalValue = fInputY3;//  dk[0] + dk[1] * fInputX3 + dk[2] * fInputX3 * fInputX3 + dk[3] * fInputX3 * fInputX3 * fInputX3 + dk[4] * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[5] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[6] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3;

                            if (dTempRest > 0.0005) //第三层
                            {
                                if (originalValue < 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }
                            else if (dTempRest > 0)//第三层
                            {
                                if (originalValue < 0)
                                    iRet = 1;
                                else
                                    iRet = 2;

                            }
                            else if (dTempRest > -0.0005)//第三层
                            {

                                if (originalValue > 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }
                            else// if (dTempRest < -0.0005)
                            {
                                if (originalValue > 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }

                            //第三次 完成

                        }

                    }
                    else// if (dTempRest < -0.0005) //第二层
                    {
                        if (originalValue > 0)
                            iRet = 1;
                        else
                        {  //第三次

                            switch (iFunctionType)
                            {
                                case 1:
                                    dTempRest = dB / (-fInputX3 * fInputX3);
                                    break;
                                case 2:
                                    dTempRest = dA * dB * dB * Math.Exp(dB * fInputX3);
                                    break;
                                case 3:
                                    dTempRest = dB / (0.5 * fInputX3 * fInputX3 * fInputX3);
                                    break;

                                default:
                                    //不应该到这里
                                    break;
                            }

                            originalValue = fInputY3;//  dk[0] + dk[1] * fInputX3 + dk[2] * fInputX3 * fInputX3 + dk[3] * fInputX3 * fInputX3 * fInputX3 + dk[4] * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[5] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[6] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3;

                            if (dTempRest > 0.0005) //第三层
                            {
                                if (originalValue < 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }
                            else if (dTempRest > 0)//第三层
                            {
                                if (originalValue < 0)
                                    iRet = 1;
                                else
                                    iRet = 2;

                            }
                            else if (dTempRest > -0.0005)//第三层
                            {

                                if (originalValue > 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }
                            else// if (dTempRest < -0.0005)
                            {
                                if (originalValue > 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }

                            //第三次 完成
                        }
                    }


                    //第二次完成






                }



            }//第一层
            else //dTempRest < -0.0005)
            {
                if (originalValue > 0)
                    iRet = 0;
                else
                {

                    //第二次

                    //第二次

                    switch (iFunctionType)
                    {
                        case 1:
                            dTempRest = dB / (-fInputX2 * fInputX2);
                            break;
                        case 2:
                            dTempRest = dA * dB * dB * Math.Exp(dB * fInputX2);
                            break;
                        case 3:
                            dTempRest = dB / (0.5 * fInputX2 * fInputX2 * fInputX2);
                            break;

                        default:
                            //不应该到这里
                            break;
                    }

                    originalValue = fInputY2;//  dk[0] + dk[1] * fInputX2 + dk[2] * fInputX2 * fInputX2 + dk[3] * fInputX2 * fInputX2 * fInputX2 + dk[4] * fInputX2 * fInputX2 * fInputX2 * fInputX2 + dk[5] * fInputX2 * fInputX2 * fInputX2 * fInputX2 * fInputX2 + dk[6] * fInputX2 * fInputX2 * fInputX2 * fInputX2 * fInputX2 * fInputX2;

                    if (dTempRest > 0.0005) //第二层
                    {
                        if (originalValue < 0)
                            iRet = 1;
                        else
                        { //第三次

                            switch (iFunctionType)
                            {
                                case 1:
                                    dTempRest = dB / (-fInputX3 * fInputX3);
                                    break;
                                case 2:
                                    dTempRest = dA * dB * dB * Math.Exp(dB * fInputX3);
                                    break;
                                case 3:
                                    dTempRest = dB / (0.5 * fInputX3 * fInputX3 * fInputX3);
                                    break;

                                default:
                                    //不应该到这里
                                    break;
                            }
                            originalValue = fInputY3;//  dk[0] + dk[1] * fInputX3 + dk[2] * fInputX3 * fInputX3 + dk[3] * fInputX3 * fInputX3 * fInputX3 + dk[4] * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[5] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[6] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3;

                            if (dTempRest > 0.0005) //第三层
                            {
                                if (originalValue < 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }
                            else if (dTempRest > 0)//第三层
                            {
                                if (originalValue < 0)
                                    iRet = 1;
                                else
                                    iRet = 2;

                            }
                            else if (dTempRest > -0.0005)//第三层
                            {

                                if (originalValue > 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }
                            else// if (dTempRest < -0.0005)
                            {
                                if (originalValue > 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }

                            //第三次 完成
                        }
                    }
                    else if (dTempRest > 0)//第二层  0~0.0005
                    {
                        if (originalValue < 0)
                            iRet = 1;
                        else
                        {
                            //下面是原来的处理
                            //加速度在-0.0005 ~ 0 之间，无论如何都是初步报警，因此看下一层
                            //第三次

                            switch (iFunctionType)
                            {
                                case 1:
                                    dTempRest = dB / (-fInputX3 * fInputX3);
                                    break;
                                case 2:
                                    dTempRest = dA * dB * dB * Math.Exp(dB * fInputX3);
                                    break;
                                case 3:
                                    dTempRest = dB / (0.5 * fInputX3 * fInputX3 * fInputX3);
                                    break;

                                default:
                                    //不应该到这里
                                    break;
                            }

                            originalValue = fInputY3;// dk[0] + dk[1] * fInputX3 + dk[2] * fInputX3 * fInputX3 + dk[3] * fInputX3 * fInputX3 * fInputX3 + dk[4] * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[5] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[6] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3;

                            if (dTempRest > 0.0005) //第三层
                            {
                                if (originalValue < 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }
                            else if (dTempRest > 0)//第三层
                            {

                                if (originalValue < 0)
                                    iRet = 1;
                                else
                                    iRet = 2;

                            }
                            else if (dTempRest > -0.0005)//第三层
                            {

                                if (originalValue > 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }
                            else// if (dTempRest < -0.0005)
                            {
                                if (originalValue > 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }

                            //第三次 完成
                        }

                    }
                    else if (dTempRest > -0.0005)//第二层
                    {
                        if (originalValue > 0)
                            iRet = 1;
                        else
                        {

                            //第三次

                            switch (iFunctionType)
                            {
                                case 1:
                                    dTempRest = dB / (-fInputX3 * fInputX3);
                                    break;
                                case 2:
                                    dTempRest = dA * dB * dB * Math.Exp(dB * fInputX3);
                                    break;
                                case 3:
                                    dTempRest = dB / (0.5 * fInputX3 * fInputX3 * fInputX3);
                                    break;

                                default:
                                    //不应该到这里
                                    break;
                            }

                            originalValue = fInputY3;//  dk[0] + dk[1] * fInputX3 + dk[2] * fInputX3 * fInputX3 + dk[3] * fInputX3 * fInputX3 * fInputX3 + dk[4] * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[5] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[6] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3;

                            if (dTempRest > 0.0005) //第三层
                            {
                                if (originalValue < 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }
                            else if (dTempRest > 0)//第三层
                            {
                                if (originalValue < 0)
                                    iRet = 1;
                                else
                                    iRet = 2;

                            }
                            else if (dTempRest > -0.0005)//第三层
                            {

                                if (originalValue > 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }
                            else// if (dTempRest < -0.0005)
                            {
                                if (originalValue > 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }

                            //第三次 完成

                        }

                    }
                    else// if (dTempRest < -0.0005) //第二层
                    {
                        if (originalValue > 0)
                            iRet = 1;
                        else
                        {  //第三次

                            switch (iFunctionType)
                            {
                                case 1:
                                    dTempRest = dB / (-fInputX3 * fInputX3);
                                    break;
                                case 2:
                                    dTempRest = dA * dB * dB * Math.Exp(dB * fInputX3);
                                    break;
                                case 3:
                                    dTempRest = dB / (0.5 * fInputX3 * fInputX3 * fInputX3);
                                    break;

                                default:
                                    //不应该到这里
                                    break;
                            }

                            originalValue = fInputY3;//  dk[0] + dk[1] * fInputX3 + dk[2] * fInputX3 * fInputX3 + dk[3] * fInputX3 * fInputX3 * fInputX3 + dk[4] * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[5] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[6] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3;

                            if (dTempRest > 0.0005) //第三层
                            {
                                if (originalValue < 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }
                            else if (dTempRest > 0)//第三层
                            {
                                if (originalValue < 0)
                                    iRet = 1;
                                else
                                    iRet = 2;

                            }
                            else if (dTempRest > -0.0005)//第三层
                            {

                                if (originalValue > 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }
                            else// if (dTempRest < -0.0005)
                            {
                                if (originalValue > 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }

                            //第三次 完成
                        }
                    }




                    //第二次完成

                }
            }



            return iRet;
        }

        /// <summary>
        /// 按变形曲线形态判断围岩稳定标准
        /// </summary>
        /// <param name="fInputX"></param>
        /// <param name="fInputX2"></param>
        /// <param name="fInputX3"></param>
        /// <param name="fInputY"></param>
        /// <param name="fInputY2"></param>
        /// <param name="fInputY3"></param>
        /// <param name="dA"></param>
        /// <param name="dB"></param>
        /// <param name="iFunctionType"></param>
        /// <param name="accValue"></param>
        /// <returns></returns>
        public static int FLastPointAcc(float fInputX, float fInputX2, float fInputX3, float fInputY, float fInputY2, float fInputY3, double dA, double dB, int iFunctionType, out double accValue)
        {
            double dTempRest = 0;
            int iRet = 0;


            switch (iFunctionType)
            {
                case 1:
                    dTempRest = dB / (-fInputX * fInputX);
                    break;
                case 2:
                    dTempRest = dA * dB * dB * Math.Exp(dB * fInputX);
                    break;
                case 3:
                    dTempRest = dB / (0.5 * fInputX * fInputX * fInputX);
                    break;

                default:
                    //不应该到这里
                    break;
            }

            double originalValue = fInputY;// dk[0] + dk[1] * fInputX + dk[2] * fInputX * fInputX + dk[3] * fInputX * fInputX * fInputX + dk[4] * fInputX * fInputX * fInputX * fInputX + dk[5] * fInputX * fInputX * fInputX * fInputX * fInputX + dk[6] * fInputX * fInputX * fInputX * fInputX * fInputX * fInputX;

            accValue = dTempRest;

            if (dTempRest > 0.0005)  //这个肯定是 >0
            {
                if (originalValue < 0)
                    iRet = 0;
                else
                {
                    //速度 > 0, 位移 > 0第一次了

                    //第二次
                    switch (iFunctionType)
                    {
                        case 1:
                            dTempRest = dB / (-fInputX2 * fInputX2);
                            break;
                        case 2:
                            dTempRest = dA * dB * dB * Math.Exp(dB * fInputX2);
                            break;
                        case 3:
                            dTempRest = dB / (0.5 * fInputX2 * fInputX2 * fInputX2);
                            break;

                        default:
                            //不应该到这里
                            break;
                    }

                    originalValue = fInputY2;// dk[0] + dk[1] * fInputX2 + dk[2] * fInputX2 * fInputX2 + dk[3] * fInputX2 * fInputX2 * fInputX2 + dk[4] * fInputX2 * fInputX2 * fInputX2 * fInputX2 + dk[5] * fInputX2 * fInputX2 * fInputX2 * fInputX2 * fInputX2 + dk[6] * fInputX2 * fInputX2 * fInputX2 * fInputX2 * fInputX2 * fInputX2;

                    if (dTempRest > 0.0005) //第二层
                    {
                        if (originalValue < 0)
                            iRet = 1;
                        else
                        { //第三次

                            switch (iFunctionType)
                            {
                                case 1:
                                    dTempRest = dB / (-fInputX3 * fInputX3);
                                    break;
                                case 2:
                                    dTempRest = dA * dB * dB * Math.Exp(dB * fInputX3);
                                    break;
                                case 3:
                                    dTempRest = dB / (0.5 * fInputX3 * fInputX3 * fInputX3);
                                    break;

                                default:
                                    //不应该到这里
                                    break;
                            }
                            originalValue = fInputY3;//  dk[0] + dk[1] * fInputX3 + dk[2] * fInputX3 * fInputX3 + dk[3] * fInputX3 * fInputX3 * fInputX3 + dk[4] * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[5] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[6] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3;

                            if (dTempRest > 0.0005) //第三层
                            {
                                if (originalValue < 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }
                            else if (dTempRest > 0)//第三层
                            {
                                if (originalValue < 0)
                                    iRet = 1;
                                else
                                    iRet = 2;

                            }
                            else if (dTempRest > -0.0005)//第三层
                            {

                                if (originalValue > 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }
                            else// if (dTempRest < -0.0005)
                            {
                                if (originalValue > 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }

                            //第三次 完成
                        }
                    }
                    else if (dTempRest > 0)//第二层  0~0.0005
                    {
                        if (originalValue < 0)
                            iRet = 1;
                        else
                        {
                            //下面是原来的处理
                            //加速度在-0.0005 ~ 0 之间，无论如何都是初步报警，因此看下一层
                            //第三次

                            switch (iFunctionType)
                            {
                                case 1:
                                    dTempRest = dB / (-fInputX3 * fInputX3);
                                    break;
                                case 2:
                                    dTempRest = dA * dB * dB * Math.Exp(dB * fInputX3);
                                    break;
                                case 3:
                                    dTempRest = dB / (0.5 * fInputX3 * fInputX3 * fInputX3);
                                    break;

                                default:
                                    //不应该到这里
                                    break;
                            }

                            originalValue = fInputY3;//  dk[0] + dk[1] * fInputX3 + dk[2] * fInputX3 * fInputX3 + dk[3] * fInputX3 * fInputX3 * fInputX3 + dk[4] * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[5] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[6] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3;

                            if (dTempRest > 0.0005) //第三层
                            {
                                if (originalValue < 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }
                            else if (dTempRest > 0)//第三层
                            {

                                if (originalValue < 0)
                                    iRet = 1;
                                else
                                    iRet = 2;

                            }
                            else if (dTempRest > -0.0005)//第三层
                            {

                                if (originalValue > 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }
                            else// if (dTempRest < -0.0005)
                            {
                                if (originalValue > 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }

                            //第三次 完成
                        }

                    }
                    else if (dTempRest > -0.0005)//第二层
                    {
                        if (originalValue > 0)
                            iRet = 1;
                        else
                        {

                            //第三次

                            switch (iFunctionType)
                            {
                                case 1:
                                    dTempRest = dB / (-fInputX3 * fInputX3);
                                    break;
                                case 2:
                                    dTempRest = dA * dB * dB * Math.Exp(dB * fInputX3);
                                    break;
                                case 3:
                                    dTempRest = dB / (0.5 * fInputX3 * fInputX3 * fInputX3);
                                    break;

                                default:
                                    //不应该到这里
                                    break;
                            }

                            originalValue = fInputY3;//  dk[0] + dk[1] * fInputX3 + dk[2] * fInputX3 * fInputX3 + dk[3] * fInputX3 * fInputX3 * fInputX3 + dk[4] * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[5] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[6] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3;

                            if (dTempRest > 0.0005) //第三层
                            {
                                if (originalValue < 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }
                            else if (dTempRest > 0)//第三层
                            {
                                if (originalValue < 0)
                                    iRet = 1;
                                else
                                    iRet = 2;

                            }
                            else if (dTempRest > -0.0005)//第三层
                            {

                                if (originalValue > 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }
                            else// if (dTempRest < -0.0005)
                            {
                                if (originalValue > 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }

                            //第三次 完成

                        }

                    }
                    else// if (dTempRest < -0.0005) //第二层
                    {
                        if (originalValue > 0)
                            iRet = 1;
                        else
                        {  //第三次
                            switch (iFunctionType)
                            {
                                case 1:
                                    dTempRest = dB / (-fInputX3 * fInputX3);
                                    break;
                                case 2:
                                    dTempRest = dA * dB * dB * Math.Exp(dB * fInputX3);
                                    break;
                                case 3:
                                    dTempRest = dB / (0.5 * fInputX3 * fInputX3 * fInputX3);
                                    break;

                                default:
                                    //不应该到这里
                                    break;
                            }

                            originalValue = fInputY3;//  dk[0] + dk[1] * fInputX3 + dk[2] * fInputX3 * fInputX3 + dk[3] * fInputX3 * fInputX3 * fInputX3 + dk[4] * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[5] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[6] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3;

                            if (dTempRest > 0.0005) //第三层
                            {
                                if (originalValue < 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }
                            else if (dTempRest > 0)//第三层
                            {
                                if (originalValue < 0)
                                    iRet = 1;
                                else
                                    iRet = 2;

                            }
                            else if (dTempRest > -0.0005)//第三层
                            {

                                if (originalValue > 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }
                            else// if (dTempRest < -0.0005)
                            {
                                if (originalValue > 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }

                            //第三次 完成
                        }
                    }


                    //第二次完成
                }
            }//第一层
            else if (dTempRest > 0)
            { //加速度0~0.0005，不管累积位移是多少，都应该初步报警

                if (originalValue < 0)
                    iRet = 0;
                else
                {  //已经有一次报警了

                    //第二次

                    switch (iFunctionType)
                    {
                        case 1:
                            dTempRest = dB / (-fInputX2 * fInputX2);
                            break;
                        case 2:
                            dTempRest = dA * dB * dB * Math.Exp(dB * fInputX2);
                            break;
                        case 3:
                            dTempRest = dB / (0.5 * fInputX2 * fInputX2 * fInputX2);
                            break;

                        default:
                            //不应该到这里
                            break;
                    }

                    originalValue = fInputY2;// dk[0] + dk[1] * fInputX2 + dk[2] * fInputX2 * fInputX2 + dk[3] * fInputX2 * fInputX2 * fInputX2 + dk[4] * fInputX2 * fInputX2 * fInputX2 * fInputX2 + dk[5] * fInputX2 * fInputX2 * fInputX2 * fInputX2 * fInputX2 + dk[6] * fInputX2 * fInputX2 * fInputX2 * fInputX2 * fInputX2 * fInputX2;

                    if (dTempRest > 0.0005) //第二层
                    {
                        if (originalValue < 0)
                            iRet = 1;
                        else
                        { //第三次

                            switch (iFunctionType)
                            {
                                case 1:
                                    dTempRest = dB / (-fInputX3 * fInputX3);
                                    break;
                                case 2:
                                    dTempRest = dA * dB * dB * Math.Exp(dB * fInputX3);
                                    break;
                                case 3:
                                    dTempRest = dB / (0.5 * fInputX3 * fInputX3 * fInputX3);
                                    break;

                                default:
                                    //不应该到这里
                                    break;
                            }

                            originalValue = fInputY3;//  dk[0] + dk[1] * fInputX3 + dk[2] * fInputX3 * fInputX3 + dk[3] * fInputX3 * fInputX3 * fInputX3 + dk[4] * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[5] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[6] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3;

                            if (dTempRest > 0.0005) //第三层
                            {
                                if (originalValue < 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }
                            else if (dTempRest > 0)//第三层
                            {
                                if (originalValue < 0)
                                    iRet = 1;
                                else
                                    iRet = 2;

                            }
                            else if (dTempRest > -0.0005)//第三层
                            {

                                if (originalValue > 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }
                            else// if (dTempRest < -0.0005)
                            {
                                if (originalValue > 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }

                            //第三次 完成
                        }
                    }
                    else if (dTempRest > 0)//第二层  0~0.0005
                    {
                        if (originalValue < 0)
                            iRet = 1;
                        else
                        {
                            //下面是原来的处理
                            //加速度在-0.0005 ~ 0 之间，无论如何都是初步报警，因此看下一层
                            //第三次

                            switch (iFunctionType)
                            {
                                case 1:
                                    dTempRest = dB / (-fInputX3 * fInputX3);
                                    break;
                                case 2:
                                    dTempRest = dA * dB * dB * Math.Exp(dB * fInputX3);
                                    break;
                                case 3:
                                    dTempRest = dB / (0.5 * fInputX3 * fInputX3 * fInputX3);
                                    break;

                                default:
                                    //不应该到这里
                                    break;
                            }

                            originalValue = fInputY3;//  dk[0] + dk[1] * fInputX3 + dk[2] * fInputX3 * fInputX3 + dk[3] * fInputX3 * fInputX3 * fInputX3 + dk[4] * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[5] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[6] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3;

                            if (dTempRest > 0.0005) //第三层
                            {
                                if (originalValue < 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }
                            else if (dTempRest > 0)//第三层
                            {

                                if (originalValue < 0)
                                    iRet = 1;
                                else
                                    iRet = 2;

                            }
                            else if (dTempRest > -0.0005)//第三层
                            {

                                if (originalValue > 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }
                            else// if (dTempRest < -0.0005)
                            {
                                if (originalValue > 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }

                            //第三次 完成
                        }

                    }
                    else if (dTempRest > -0.0005)//第二层
                    {
                        if (originalValue > 0)
                            iRet = 1;
                        else
                        {

                            //第三次

                            switch (iFunctionType)
                            {
                                case 1:
                                    dTempRest = dB / (-fInputX3 * fInputX3);
                                    break;
                                case 2:
                                    dTempRest = dA * dB * dB * Math.Exp(dB * fInputX3);
                                    break;
                                case 3:
                                    dTempRest = dB / (0.5 * fInputX3 * fInputX3 * fInputX3);
                                    break;

                                default:
                                    //不应该到这里
                                    break;
                            }

                            originalValue = fInputY3;//  dk[0] + dk[1] * fInputX3 + dk[2] * fInputX3 * fInputX3 + dk[3] * fInputX3 * fInputX3 * fInputX3 + dk[4] * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[5] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[6] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3;

                            if (dTempRest > 0.0005) //第三层
                            {
                                if (originalValue < 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }
                            else if (dTempRest > 0)//第三层
                            {
                                if (originalValue < 0)
                                    iRet = 1;
                                else
                                    iRet = 2;

                            }
                            else if (dTempRest > -0.0005)//第三层
                            {

                                if (originalValue > 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }
                            else// if (dTempRest < -0.0005)
                            {
                                if (originalValue > 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }

                            //第三次 完成

                        }

                    }
                    else// if (dTempRest < -0.0005) //第二层
                    {
                        if (originalValue > 0)
                            iRet = 1;
                        else
                        {  //第三次

                            switch (iFunctionType)
                            {
                                case 1:
                                    dTempRest = dB / (-fInputX3 * fInputX3);
                                    break;
                                case 2:
                                    dTempRest = dA * dB * dB * Math.Exp(dB * fInputX3);
                                    break;
                                case 3:
                                    dTempRest = dB / (0.5 * fInputX3 * fInputX3 * fInputX3);
                                    break;

                                default:
                                    //不应该到这里
                                    break;
                            }

                            originalValue = fInputY3;// dk[0] + dk[1] * fInputX3 + dk[2] * fInputX3 * fInputX3 + dk[3] * fInputX3 * fInputX3 * fInputX3 + dk[4] * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[5] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[6] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3;

                            if (dTempRest > 0.0005) //第三层
                            {
                                if (originalValue < 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }
                            else if (dTempRest > 0)//第三层
                            {
                                if (originalValue < 0)
                                    iRet = 1;
                                else
                                    iRet = 2;

                            }
                            else if (dTempRest > -0.0005)//第三层
                            {

                                if (originalValue > 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }
                            else// if (dTempRest < -0.0005)
                            {
                                if (originalValue > 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }

                            //第三次 完成
                        }
                    }


                    //第二次完成


                    //第二次完成
                }

            }//第一层
            else if (dTempRest > -0.0005)
            {
                if (originalValue > 0)
                    iRet = 0;
                else
                { //第二层


                    //第二次

                    switch (iFunctionType)
                    {
                        case 1:
                            dTempRest = dB / (-fInputX2 * fInputX2);
                            break;
                        case 2:
                            dTempRest = dA * dB * dB * Math.Exp(dB * fInputX2);
                            break;
                        case 3:
                            dTempRest = dB / (0.5 * fInputX2 * fInputX2 * fInputX2);
                            break;

                        default:
                            //不应该到这里
                            break;
                    }
                    originalValue = fInputY2;//  dk[0] + dk[1] * fInputX2 + dk[2] * fInputX2 * fInputX2 + dk[3] * fInputX2 * fInputX2 * fInputX2 + dk[4] * fInputX2 * fInputX2 * fInputX2 * fInputX2 + dk[5] * fInputX2 * fInputX2 * fInputX2 * fInputX2 * fInputX2 + dk[6] * fInputX2 * fInputX2 * fInputX2 * fInputX2 * fInputX2 * fInputX2;

                    if (dTempRest > 0.0005) //第二层
                    {
                        if (originalValue < 0)
                            iRet = 1;
                        else
                        { //第三次


                            switch (iFunctionType)
                            {
                                case 1:
                                    dTempRest = dB / (-fInputX3 * fInputX3);
                                    break;
                                case 2:
                                    dTempRest = dA * dB * dB * Math.Exp(dB * fInputX3);
                                    break;
                                case 3:
                                    dTempRest = dB / (0.5 * fInputX3 * fInputX3 * fInputX3);
                                    break;

                                default:
                                    //不应该到这里
                                    break;
                            }

                            originalValue = fInputY3;// dk[0] + dk[1] * fInputX3 + dk[2] * fInputX3 * fInputX3 + dk[3] * fInputX3 * fInputX3 * fInputX3 + dk[4] * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[5] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[6] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3;

                            if (dTempRest > 0.0005) //第三层
                            {
                                if (originalValue < 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }
                            else if (dTempRest > 0)//第三层
                            {
                                if (originalValue < 0)
                                    iRet = 1;
                                else
                                    iRet = 2;

                            }
                            else if (dTempRest > -0.0005)//第三层
                            {

                                if (originalValue > 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }
                            else// if (dTempRest < -0.0005)
                            {
                                if (originalValue > 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }

                            //第三次 完成
                        }
                    }
                    else if (dTempRest > 0)//第二层  0~0.0005
                    {
                        if (originalValue < 0)
                            iRet = 1;
                        else
                        {
                            //下面是原来的处理
                            //加速度在-0.0005 ~ 0 之间，无论如何都是初步报警，因此看下一层
                            //第三次

                            switch (iFunctionType)
                            {
                                case 1:
                                    dTempRest = dB / (-fInputX3 * fInputX3);
                                    break;
                                case 2:
                                    dTempRest = dA * dB * dB * Math.Exp(dB * fInputX3);
                                    break;
                                case 3:
                                    dTempRest = dB / (0.5 * fInputX3 * fInputX3 * fInputX3);
                                    break;

                                default:
                                    //不应该到这里
                                    break;
                            }

                            originalValue = fInputY3;// dk[0] + dk[1] * fInputX3 + dk[2] * fInputX3 * fInputX3 + dk[3] * fInputX3 * fInputX3 * fInputX3 + dk[4] * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[5] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[6] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3;

                            if (dTempRest > 0.0005) //第三层
                            {
                                if (originalValue < 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }
                            else if (dTempRest > 0)//第三层
                            {

                                if (originalValue < 0)
                                    iRet = 1;
                                else
                                    iRet = 2;

                            }
                            else if (dTempRest > -0.0005)//第三层
                            {

                                if (originalValue > 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }
                            else// if (dTempRest < -0.0005)
                            {
                                if (originalValue > 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }

                            //第三次 完成
                        }

                    }
                    else if (dTempRest > -0.0005)//第二层
                    {
                        if (originalValue > 0)
                            iRet = 1;
                        else
                        {

                            //第三次

                            switch (iFunctionType)
                            {
                                case 1:
                                    dTempRest = dB / (-fInputX3 * fInputX3);
                                    break;
                                case 2:
                                    dTempRest = dA * dB * dB * Math.Exp(dB * fInputX3);
                                    break;
                                case 3:
                                    dTempRest = dB / (0.5 * fInputX3 * fInputX3 * fInputX3);
                                    break;

                                default:
                                    //不应该到这里
                                    break;
                            }

                            originalValue = fInputY3;//  dk[0] + dk[1] * fInputX3 + dk[2] * fInputX3 * fInputX3 + dk[3] * fInputX3 * fInputX3 * fInputX3 + dk[4] * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[5] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[6] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3;

                            if (dTempRest > 0.0005) //第三层
                            {
                                if (originalValue < 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }
                            else if (dTempRest > 0)//第三层
                            {
                                if (originalValue < 0)
                                    iRet = 1;
                                else
                                    iRet = 2;

                            }
                            else if (dTempRest > -0.0005)//第三层
                            {

                                if (originalValue > 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }
                            else// if (dTempRest < -0.0005)
                            {
                                if (originalValue > 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }

                            //第三次 完成

                        }

                    }
                    else// if (dTempRest < -0.0005) //第二层
                    {
                        if (originalValue > 0)
                            iRet = 1;
                        else
                        {  //第三次

                            switch (iFunctionType)
                            {
                                case 1:
                                    dTempRest = dB / (-fInputX3 * fInputX3);
                                    break;
                                case 2:
                                    dTempRest = dA * dB * dB * Math.Exp(dB * fInputX3);
                                    break;
                                case 3:
                                    dTempRest = dB / (0.5 * fInputX3 * fInputX3 * fInputX3);
                                    break;

                                default:
                                    //不应该到这里
                                    break;
                            }

                            originalValue = fInputY3;//  dk[0] + dk[1] * fInputX3 + dk[2] * fInputX3 * fInputX3 + dk[3] * fInputX3 * fInputX3 * fInputX3 + dk[4] * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[5] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[6] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3;

                            if (dTempRest > 0.0005) //第三层
                            {
                                if (originalValue < 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }
                            else if (dTempRest > 0)//第三层
                            {
                                if (originalValue < 0)
                                    iRet = 1;
                                else
                                    iRet = 2;

                            }
                            else if (dTempRest > -0.0005)//第三层
                            {

                                if (originalValue > 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }
                            else// if (dTempRest < -0.0005)
                            {
                                if (originalValue > 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }

                            //第三次 完成
                        }
                    }


                    //第二次完成






                }



            }//第一层
            else //dTempRest < -0.0005)
            {
                if (originalValue > 0)
                    iRet = 0;
                else
                {

                    //第二次

                    //第二次

                    switch (iFunctionType)
                    {
                        case 1:
                            dTempRest = dB / (-fInputX2 * fInputX2);
                            break;
                        case 2:
                            dTempRest = dA * dB * dB * Math.Exp(dB * fInputX2);
                            break;
                        case 3:
                            dTempRest = dB / (0.5 * fInputX2 * fInputX2 * fInputX2);
                            break;

                        default:
                            //不应该到这里
                            break;
                    }

                    originalValue = fInputY2;//  dk[0] + dk[1] * fInputX2 + dk[2] * fInputX2 * fInputX2 + dk[3] * fInputX2 * fInputX2 * fInputX2 + dk[4] * fInputX2 * fInputX2 * fInputX2 * fInputX2 + dk[5] * fInputX2 * fInputX2 * fInputX2 * fInputX2 * fInputX2 + dk[6] * fInputX2 * fInputX2 * fInputX2 * fInputX2 * fInputX2 * fInputX2;

                    if (dTempRest > 0.0005) //第二层
                    {
                        if (originalValue < 0)
                            iRet = 1;
                        else
                        { //第三次

                            switch (iFunctionType)
                            {
                                case 1:
                                    dTempRest = dB / (-fInputX3 * fInputX3);
                                    break;
                                case 2:
                                    dTempRest = dA * dB * dB * Math.Exp(dB * fInputX3);
                                    break;
                                case 3:
                                    dTempRest = dB / (0.5 * fInputX3 * fInputX3 * fInputX3);
                                    break;

                                default:
                                    //不应该到这里
                                    break;
                            }
                            originalValue = fInputY3;//  dk[0] + dk[1] * fInputX3 + dk[2] * fInputX3 * fInputX3 + dk[3] * fInputX3 * fInputX3 * fInputX3 + dk[4] * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[5] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[6] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3;

                            if (dTempRest > 0.0005) //第三层
                            {
                                if (originalValue < 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }
                            else if (dTempRest > 0)//第三层
                            {
                                if (originalValue < 0)
                                    iRet = 1;
                                else
                                    iRet = 2;

                            }
                            else if (dTempRest > -0.0005)//第三层
                            {

                                if (originalValue > 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }
                            else// if (dTempRest < -0.0005)
                            {
                                if (originalValue > 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }

                            //第三次 完成
                        }
                    }
                    else if (dTempRest > 0)//第二层  0~0.0005
                    {
                        if (originalValue < 0)
                            iRet = 1;
                        else
                        {
                            //下面是原来的处理
                            //加速度在-0.0005 ~ 0 之间，无论如何都是初步报警，因此看下一层
                            //第三次

                            switch (iFunctionType)
                            {
                                case 1:
                                    dTempRest = dB / (-fInputX3 * fInputX3);
                                    break;
                                case 2:
                                    dTempRest = dA * dB * dB * Math.Exp(dB * fInputX3);
                                    break;
                                case 3:
                                    dTempRest = dB / (0.5 * fInputX3 * fInputX3 * fInputX3);
                                    break;

                                default:
                                    //不应该到这里
                                    break;
                            }

                            originalValue = fInputY3;// dk[0] + dk[1] * fInputX3 + dk[2] * fInputX3 * fInputX3 + dk[3] * fInputX3 * fInputX3 * fInputX3 + dk[4] * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[5] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[6] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3;

                            if (dTempRest > 0.0005) //第三层
                            {
                                if (originalValue < 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }
                            else if (dTempRest > 0)//第三层
                            {

                                if (originalValue < 0)
                                    iRet = 1;
                                else
                                    iRet = 2;

                            }
                            else if (dTempRest > -0.0005)//第三层
                            {

                                if (originalValue > 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }
                            else// if (dTempRest < -0.0005)
                            {
                                if (originalValue > 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }

                            //第三次 完成
                        }

                    }
                    else if (dTempRest > -0.0005)//第二层
                    {
                        if (originalValue > 0)
                            iRet = 1;
                        else
                        {

                            //第三次

                            switch (iFunctionType)
                            {
                                case 1:
                                    dTempRest = dB / (-fInputX3 * fInputX3);
                                    break;
                                case 2:
                                    dTempRest = dA * dB * dB * Math.Exp(dB * fInputX3);
                                    break;
                                case 3:
                                    dTempRest = dB / (0.5 * fInputX3 * fInputX3 * fInputX3);
                                    break;

                                default:
                                    //不应该到这里
                                    break;
                            }

                            originalValue = fInputY3;//  dk[0] + dk[1] * fInputX3 + dk[2] * fInputX3 * fInputX3 + dk[3] * fInputX3 * fInputX3 * fInputX3 + dk[4] * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[5] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[6] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3;

                            if (dTempRest > 0.0005) //第三层
                            {
                                if (originalValue < 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }
                            else if (dTempRest > 0)//第三层
                            {
                                if (originalValue < 0)
                                    iRet = 1;
                                else
                                    iRet = 2;

                            }
                            else if (dTempRest > -0.0005)//第三层
                            {

                                if (originalValue > 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }
                            else// if (dTempRest < -0.0005)
                            {
                                if (originalValue > 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }

                            //第三次 完成

                        }

                    }
                    else// if (dTempRest < -0.0005) //第二层
                    {
                        if (originalValue > 0)
                            iRet = 1;
                        else
                        {  //第三次

                            switch (iFunctionType)
                            {
                                case 1:
                                    dTempRest = dB / (-fInputX3 * fInputX3);
                                    break;
                                case 2:
                                    dTempRest = dA * dB * dB * Math.Exp(dB * fInputX3);
                                    break;
                                case 3:
                                    dTempRest = dB / (0.5 * fInputX3 * fInputX3 * fInputX3);
                                    break;

                                default:
                                    //不应该到这里
                                    break;
                            }

                            originalValue = fInputY3;//  dk[0] + dk[1] * fInputX3 + dk[2] * fInputX3 * fInputX3 + dk[3] * fInputX3 * fInputX3 * fInputX3 + dk[4] * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[5] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[6] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3;

                            if (dTempRest > 0.0005) //第三层
                            {
                                if (originalValue < 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }
                            else if (dTempRest > 0)//第三层
                            {
                                if (originalValue < 0)
                                    iRet = 1;
                                else
                                    iRet = 2;

                            }
                            else if (dTempRest > -0.0005)//第三层
                            {

                                if (originalValue > 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }
                            else// if (dTempRest < -0.0005)
                            {
                                if (originalValue > 0)
                                    iRet = 1;
                                else
                                    iRet = 2;
                            }

                            //第三次 完成
                        }
                    }




                    //第二次完成

                }
            }



            return iRet;
        }

        /// <summary>
        /// 计算累积位移
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="pf"></param>
        /// <param name="para"></param>
        public static ChartPoint[] DisFunction(List<SectionLine> lines, MPoint[] pf, CurvePara para)
        {
            var cp = new List<ChartPoint>(); ;

            if (lines[0].RecordTime != null && lines[0].RecordTime.ToString() != "" && pf[0] != null)
            {
                //cp = new ChartPoint[pf.Length];

                DateTime starTime = lines[0].RecordTime.Value;
                for (int i = 0; i < pf.Length; i++)
                {
                    if (pf[i] == null)
                    {
                        continue;
                    }
                    //var time = DayOrHours == 1 ? starTime.AddDays(pf[i].X) : starTime.AddHours(pf[i].X);
                    var time =  starTime.AddHours(pf[i].X);
                    var c = new ChartPoint
                    {
                        X = time,
                        Y = pf[i].Y
                    };
                    //cp[i] = c;
                    cp.Add(c);
                }
            }

            return cp.ToArray();
        }

        /// <summary>
        /// 计算对数函数回归位移
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="pf"></param>
        /// <param name="para"></param>
        public static ChartPoint[] DoLogDisFunction(List<SectionLine> lines, MPoint[] pf, CurvePara para)
        {
            var cp = new List<ChartPoint>(); ;

            if (lines[0].RecordTime != null && lines[0].RecordTime.ToString() != "" && pf[0] != null)
            {
                //cp = new ChartPoint[pf.Length];

                DateTime starTime = lines[0].RecordTime.Value;
                for (int i = 0; i < pf.Length; i++)
                {
                    if (pf[i] == null)
                    {
                        continue;
                    }
                    var time =  starTime.AddHours(pf[i].X);
                    var c = new ChartPoint
                    {
                        X = time,
                        Y = (float)(double.IsInfinity((para.A + para.B * Math.Log(pf[i].X))) || double.IsNaN((para.A + para.B * Math.Log(pf[i].X))) ? 0 : (para.A + para.B * Math.Log(pf[i].X)))
                    };
                    //cp[i] = c;
                    cp.Add(c);
                }
            }

            return cp.ToArray();
        }

        /// <summary>
        /// 计算对数函数回归速度
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="pf"></param>
        /// <param name="para"></param>
        public static ChartPoint[] DoLogVelFunction(List<SectionLine> lines, MPoint[] pf, CurvePara para)
        {
            var cp = new List<ChartPoint>();

            if (lines[0].RecordTime != null && lines[0].RecordTime.ToString() != "" && pf[0] != null)
            {
                //cp = new ChartPoint[pf.Length];
                DateTime starTime = lines[0].RecordTime.Value;
                for (int i = 0; i < pf.Length; i++)
                {
                    if (pf[i] == null)
                    {
                        continue;
                    }
                    var time =starTime.AddHours(pf[i].X);
                    var c = new ChartPoint
                    {
                        X = time,
                        Y = (float)(double.IsInfinity((para.B / pf[i].X)) || double.IsNaN((para.B / pf[i].X)) ? 0 : ((para.B / pf[i].X)))
                    };
                    cp.Add(c);
                }
            }
            return cp.ToArray();
        }

        /// <summary>
        /// 计算对数函数回归加速度
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="pf"></param>
        /// <param name="para"></param>
        public static ChartPoint[] DoLogAccFunction(List<SectionLine> lines, MPoint[] pf, CurvePara para)
        {
            var cp = new List<ChartPoint>();
            string fmt2 = "F3";

            if (lines[0].RecordTime != null && lines[0].RecordTime.ToString() != "" && pf[0] != null)
            {
                //cp = new ChartPoint[pf.Length];
                DateTime starTime = lines[0].RecordTime.Value;
                for (int i = 0; i < pf.Length; i++)
                {
                    if (pf[i] == null)
                    {
                        continue;
                    }
                    var time =starTime.AddHours(pf[i].X);
                    var c = new ChartPoint
                    {
                        X = time,
                        Y = (float)(double.IsInfinity((para.B / (-pf[i].X * pf[i].X))) || double.IsNaN((para.B / (-pf[i].X * pf[i].X))) ? 0 : ((para.B / (-pf[i].X * pf[i].X))))
                    };
                    if (c.Y < -0.0005)
                        c.Y = c.Y;
                    else if (c.Y < 0)
                        c.Y = (float)-0.000;
                    else if (c.Y < 0.0005)
                        c.Y = (float)0.000;
                    else
                        c.Y = c.Y;
                    //cp[i] = c;
                    cp.Add(c);
                }
            }
            return cp.ToArray();
        }

        #region 实时数据的回归分析

        /// <summary>
        /// 计算累积位移
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="pf"></param>
        /// <param name="para"></param>
        public static ChartPoint[] DisFunctionRt(List<SectionLine> lines, MPoint[] pf, CurvePara para)
        {
            var cp = new List<ChartPoint>(); ;

            if (lines[0].RecordTime != null && lines[0].RecordTime.ToString() != "" && pf[0] != null)
            {
                //cp = new ChartPoint[pf.Length];

                DateTime starTime = lines[0].RecordTime.Value;
                for (int i = 0; i < pf.Length; i++)
                {
                    if (pf[i] == null)
                    {
                        continue;
                    }
                    //var time = DayOrHours == 1 ? starTime.AddDays(pf[i].X) : starTime.AddHours(pf[i].X);
                    var time = starTime.AddMinutes(pf[i].X);
                    var c = new ChartPoint
                    {
                        X = time,
                        Y = (float)Math.Round(pf[i].Y, 0)
                    };
                    //cp[i] = c;
                    cp.Add(c);
                }
            }

            return cp.ToArray();
        }

        /// <summary>
        /// 计算对数函数回归位移
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="pf"></param>
        /// <param name="para"></param>
        public static ChartPoint[] DoLogDisFunctionRt(List<SectionLine> lines, MPoint[] pf, CurvePara para)
        {
            var cp = new List<ChartPoint>(); ;

            if (lines[0].RecordTime != null && lines[0].RecordTime.ToString() != "" && pf[0] != null)
            {
                //cp = new ChartPoint[pf.Length];

                DateTime starTime = lines[0].RecordTime.Value;
                for (int i = 0; i < pf.Length; i++)
                {
                    if (pf[i] == null)
                    {
                        continue;
                    }
                    var time = starTime.AddMinutes(pf[i].X);
                    var c = new ChartPoint
                    {
                        X = time,
                        Y = (float)(double.IsInfinity((para.A + para.B * Math.Log(pf[i].X))) || double.IsNaN((para.A + para.B * Math.Log(pf[i].X))) ? 0 : (para.A + para.B * Math.Log(pf[i].X)))
                    };
                    //cp[i] = c;
                    cp.Add(c);
                }
            }

            return cp.ToArray();
        }

        /// <summary>
        /// 计算对数函数回归速度
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="pf"></param>
        /// <param name="para"></param>
        public static ChartPoint[] DoLogVelFunctionRt(List<SectionLine> lines, MPoint[] pf, CurvePara para)
        {
            var cp = new List<ChartPoint>();

            if (lines[0].RecordTime != null && lines[0].RecordTime.ToString() != "" && pf[0] != null)
            {
                //cp = new ChartPoint[pf.Length];
                DateTime starTime = lines[0].RecordTime.Value;
                for (int i = 0; i < pf.Length; i++)
                {
                    if (pf[i] == null)
                    {
                        continue;
                    }
                    var time = starTime.AddMinutes(pf[i].X);
                    var c = new ChartPoint
                    {
                        X = time,
                        Y = (float)Math.Round((double.IsInfinity((para.B / pf[i].X)) || double.IsNaN((para.B / pf[i].X)) ? 0 : ((para.B / pf[i].X))), 1)
                    };
                    cp.Add(c);
                }
            }
            return cp.ToArray();
        }

        /// <summary>
        /// 计算对数函数回归加速度
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="pf"></param>
        /// <param name="para"></param>
        public static ChartPoint[] DoLogAccFunctionRt(List<SectionLine> lines, MPoint[] pf, CurvePara para)
        {
            var cp = new List<ChartPoint>();
            string fmt2 = "F3";

            if (lines[0].RecordTime != null && lines[0].RecordTime.ToString() != "" && pf[0] != null)
            {
                //cp = new ChartPoint[pf.Length];
                DateTime starTime = lines[0].RecordTime.Value;
                for (int i = 0; i < pf.Length; i++)
                {
                    if (pf[i] == null)
                    {
                        continue;
                    }
                    var time = starTime.AddMinutes(pf[i].X);
                    var c = new ChartPoint
                    {
                        X = time,
                        Y = (float)Math.Round((double.IsInfinity((para.B / (-pf[i].X * pf[i].X))) || double.IsNaN((para.B / (-pf[i].X * pf[i].X))) ? 0 : ((para.B / (-pf[i].X * pf[i].X)))), 1)
                    };
                    if (c.Y < -0.0005)
                        c.Y = c.Y;
                    else if (c.Y < 0)
                        c.Y = (float)-0.000;
                    else if (c.Y < 0.0005)
                        c.Y = (float)0.000;
                    else
                        c.Y = c.Y;
                    //cp[i] = c;
                    cp.Add(c);
                }
            }
            return cp.ToArray();
        }
        #endregion
    }
}
