using System;
using System.Collections.Generic;
using System.Text;
using C1.Win.C1Chart;

namespace HtTool.Service.Monitor
{
    public static class MonitorService
    {
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
                        var gPts = pointDataList[j].Data;
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
                        logPare.A = a1;
                        logPare.B = b1;
                        logPare.Ia = ia1;
                        logPare.R = r1ab;
                        logPare.Name = pointDataList[j].Name;
                        logParaList.Add(logPare);
                    }

                }

            }
            return logParaList;
        }

        /// <summary>
        /// 指数函数参数
        /// </summary>
        /// <returns></returns>
        public static List<CurvePara> CalculateExpParameters(List<PointData> pointDataList)
        {
            var ia1 = 0;
            var b1 = 0.0;
            var a1 = 0.0;
            var r2ab = 0.0;

            var logParaList = new List<CurvePara>();
            if (pointDataList.Count > 0)
            {
                for (int j = 0; j < pointDataList.Count; j++)
                {
                    double sumx = 0, sumx2 = 0, sumy1 = 0, sumy12 = 0, sumxy1 = 0, ssx = 0, ssy1 = 0, spxy1 = 0, xbar = 0, ybar1 = 0, avgY = 0, sumyyhat2 = 0, sumyybar2 = 0;

                    var logPare = new CurvePara();

                    try
                    {
                        var gPts = pointDataList[j].Data;
                        for (int i = 2; i < gPts.Length; i++)
                        {
                            sumx = sumx + gPts[i].X;
                            sumx2 = sumx2 + gPts[i].X * gPts[i].X;
                            sumy1 = sumy1 + Math.Log(gPts[i].Y);
                            sumy12 = sumy12 + Math.Log(gPts[i].Y) * Math.Log(gPts[i].Y);
                            sumxy1 = sumxy1 + gPts[i].X * Math.Log(gPts[i].Y);
                        }

                        ssx = sumx2 - sumx * sumx / (double)(gPts.Length - 2);
                        ssy1 = sumy12 - sumy1 * sumy1 / (double)(gPts.Length - 2);
                        spxy1 = sumxy1 - sumx * sumy1 / (double)(gPts.Length - 2);
                        xbar = sumx / (double)(gPts.Length - 2);
                        ybar1 = sumy1 / (double)(gPts.Length - 2);

                        b1 = spxy1 / ssx;
                        a1 = Math.Exp(ybar1 - b1 * xbar);
                        ia1 = 1;

                        for (int i = 2; i < gPts.Length; i++)
                            avgY += gPts[i].Y;
                        avgY = avgY / (double)(gPts.Length - 2);

                        for (int i = 2; i < gPts.Length - 2; i++)
                        {
                            sumyyhat2 = sumyyhat2 +
                                        (gPts[i].Y - a1 * Math.Exp(b1 * gPts[i].X)) * (gPts[i].Y - a1 * Math.Exp(b1 * gPts[i].X));
                            sumyybar2 = sumyybar2 + (gPts[i].Y - avgY) * (gPts[i].Y - avgY);
                        }

                        double tempRest = 1 - sumyyhat2 / sumyybar2;

                        if (tempRest > 0)
                            r2ab = Math.Sqrt(tempRest);
                        else
                            r2ab = Math.Sqrt(-tempRest);

                    }
                    catch (Exception ex1)
                    {
                        r2ab = 0;
                        ia1 = 0;
                    }
                    finally
                    {
                        logPare.A = a1;
                        logPare.B = b1;
                        logPare.Ia = ia1;
                        logPare.R = r2ab;
                        logPare.Name = pointDataList[j].Name;
                        logParaList.Add(logPare);
                    }
                }
            }
            return logParaList;

        }

        /// <summary>
        /// 计算双曲函数曲线参数y=a+b/x
        /// </summary>
        /// <param name="pointDataList"></param>
        /// <returns></returns>
        public static List<CurvePara> CalculateTangParameters(List<PointData> pointDataList)
        {
            //计算双曲函数曲线参数
            //y=a+b/x

            var ia1 = 0;
            var b1 = 0.0;
            var a1 = 0.0;
            var r3ab = 0.0;

            var logParaList = new List<CurvePara>();
            if (pointDataList.Count > 0)
            {
                for (int j = 0; j < pointDataList.Count; j++)
                {
                    double sumx1 = 0, sumx12 = 0, sumy = 0, sumy2 = 0, sumx1y = 0, ssx1 = 0, ssy = 0, spx1y = 0, xbar1 = 0, ybar = 0, avgY = 0, sumyyhat2 = 0, sumyybar2 = 0;
                    var logPare = new CurvePara();

                    try
                    {
                        var gPts = pointDataList[j].Data;
                        for (int i = 2; i < gPts.Length; i++)
                        {
                            sumx1 = sumx1 + 1 / gPts[i].X;
                            sumx12 = sumx12 + 1 / (gPts[i].X * gPts[i].X);
                            sumy = sumy + gPts[i].Y;
                            sumy2 = sumy2 + gPts[i].Y * gPts[i].Y;
                            sumx1y = sumx1y + 1 / gPts[i].X * gPts[i].Y;
                        }

                        ssx1 = sumx12 - sumx1 * sumx1 / (double)(gPts.Length - 2);
                        ssy = sumy2 - sumy * sumy / (double)(gPts.Length - 2); //好像可以不要的
                        spx1y = sumx1y - sumx1 * sumy / (double)(gPts.Length - 2);
                        xbar1 = sumx1 / (double)(gPts.Length - 2);
                        ybar = sumy / (double)(gPts.Length - 2);

                        b1 = spx1y / ssx1;
                        a1 = ybar - b1 * xbar1;
                        ia1 = 1;

                        for (int i = 2; i < gPts.Length; i++)
                            avgY += gPts[i].Y;
                        avgY = avgY / (double)(gPts.Length - 2);

                        for (int i = 2; i < gPts.Length - 2; i++)
                        {
                            sumyyhat2 = sumyyhat2 + (gPts[i].Y - a1 - b1 / gPts[i].X) * (gPts[i].Y - a1 - b1 / gPts[i].X);
                            sumyybar2 = sumyybar2 + (gPts[i].Y - avgY) * (gPts[i].Y - avgY);
                        }


                        double tempRest = 1 - sumyyhat2 / sumyybar2;

                        if (tempRest > 0)
                            r3ab = Math.Sqrt(tempRest);
                        else
                            r3ab = Math.Sqrt(-tempRest);

                    }
                    catch (Exception ex1)
                    {
                        r3ab = 0;
                        ia1 = 0;

                    }
                    finally
                    {
                        logPare.A = a1;
                        logPare.B = b1;
                        logPare.Ia = ia1;
                        logPare.R = r3ab;
                        logPare.Name = pointDataList[j].Name;
                        logParaList.Add(logPare);
                    }
                }
            }
            return logParaList;

        }

        /// <summary>
        /// 多项式函数
        /// </summary>
        /// <param name="c1Chart2"></param>
        /// <param name="pointDataList"></param>
        /// <returns></returns>
        public static List<CurvePara> CalculateFunctionParameters(C1Chart c1Chart2, List<PointData> pointDataList)
        { //得到回归多项式函数的系数,分别为k1,k2,k3,k4,k5,k6对应每条线

            var ik1 = 0;
            double[] k1 = { 0, 0, 0, 0, 0, 0, 0 };
            var r4ab = 0.0;

            ChartData cd2 = c1Chart2.ChartGroups[0].ChartData;

            var logParaList = new List<CurvePara>();

            if (pointDataList.Count > 0)
            {
                cd2.SeriesList.Clear();
                cd2.TrendsList.Clear();

                //添加曲线数据
                for (int j = 0; j < pointDataList.Count; j++)
                {
                    var gPts = pointDataList[j].Data;
                    ChartDataSeries ds1;

                    ds1 = cd2.SeriesList.AddNewSeries();
                    ds1.PointData.CopyDataIn(gPts);
                }
                //计算总体斜率
                for (int j = 0; j < pointDataList.Count; j++)
                {
                    var logPare = new CurvePara();
                    var gPts = pointDataList[j].Data;
                    TrendLine tl1 = null;
                    ik1 = 0;

                    try
                    {
                        cd2.TrendsList.Clear();
                        TrendLine tl21 = cd2.TrendsList.AddNewTrendLine();
                        tl21.SeriesIndex = 0;
                        tl21.TrendLineType = TrendLineTypeEnum.Polynom;
                        tl21.NumTerms = 7;

                        if (c1Chart2.ChartGroups[0].ChartData.TrendsList.Count > 0)
                        {
                            tl1 = c1Chart2.ChartGroups[0].ChartData.TrendsList[0];
                            RegressionStatistics rs1 = tl1.RegressionStatistics;
                            if (rs1 != null)
                            {
                                ik1 = 1;
                                k1 = rs1.Coeffs;
                            }
                        }
                    }
                    catch (Exception ex1)
                    {
                        ik1 = 0;
                    }
                    finally
                    {
                        logPare.Ik = ik1;
                        logPare.K = k1;
                        logPare.Name = pointDataList[j].Name;
                    }

                    double avgY1 = 0, avgY2 = 0, avgY3 = 0, sumyyhat2 = 0, sumyybar2 = 0;

                    try
                    {
                        for (int i = 2; i < gPts.Length; i++)
                        {
                            avgY1 += gPts[i].Y;
                        }
                        avgY1 = avgY1 / (gPts.Length - 2);

                        for (int i = 2; i < gPts.Length - 2; i++)
                        {
                            //下面是sum((y-y')^2)和sum((y-avg(y))^2)
                            sumyyhat2 = sumyyhat2 +
                                        (gPts[i].Y - k1[0] - k1[1] * gPts[i].X - k1[2] * gPts[i].X * gPts[i].X -
                                         k1[3] * gPts[i].X * gPts[i].X * gPts[i].X -
                                         k1[4] * gPts[i].X * gPts[i].X * gPts[i].X * gPts[i].X -
                                         k1[5] * gPts[i].X * gPts[i].X * gPts[i].X * gPts[i].X * gPts[i].X -
                                         k1[6] * gPts[i].X * gPts[i].X * gPts[i].X * gPts[i].X * gPts[i].X * gPts[i].X) *
                                        (gPts[i].Y - k1[0] - k1[1] * gPts[i].X - k1[2] * gPts[i].X * gPts[i].X -
                                         k1[3] * gPts[i].X * gPts[i].X * gPts[i].X -
                                         k1[4] * gPts[i].X * gPts[i].X * gPts[i].X * gPts[i].X -
                                         k1[5] * gPts[i].X * gPts[i].X * gPts[i].X * gPts[i].X * gPts[i].X -
                                         k1[6] * gPts[i].X * gPts[i].X * gPts[i].X * gPts[i].X * gPts[i].X * gPts[i].X);
                            sumyybar2 = sumyybar2 + (gPts[i].Y - avgY1) * (gPts[i].Y - avgY1);
                        }
                        r4ab = Math.Sqrt(1 - sumyyhat2 / sumyybar2);
                    }
                    catch (Exception ex4)
                    {
                        r4ab = 0;
                    }
                    finally
                    {
                        logPare.R = r4ab;
                    }
                    logParaList.Add(logPare);
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
        /// <returns></returns>
        public static int FLastPointOff(float fInputY, double dA, double dB, string gStrU0)
        {
            double dTempRest = fInputY;
            int iRet = 0;

            if (Math.Abs(dTempRest) < Convert.ToDouble(gStrU0) / 3)
                iRet = 0;
            else if (Math.Abs(dTempRest) <= Convert.ToDouble(gStrU0) * 2 / 3)
                iRet = 1;
            else
                iRet = 2;

            return iRet;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fInputY"></param>
        /// <param name="dk"></param>
        /// <param name="gStrU0"></param>
        /// <returns></returns>
        public static int FLastPointOff(float fInputY, double[] dk, string gStrU0)
        {
            double dTempRest = fInputY;
            int iRet = 0;

            if (Math.Abs(dTempRest) < Convert.ToDouble(gStrU0) / 3)
                iRet = 0;
            else if (Math.Abs(dTempRest) <= Convert.ToDouble(gStrU0) * 2 / 3)
                iRet = 1;
            else
                iRet = 2;

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
                    iRet = 4;
                else
                {
                    if (dTempRest > 5)
                        iRet = 0;
                    else if (dTempRest > 3)
                        iRet = 1;
                    else if (dTempRest > 1)
                        iRet = 2;
                    else if (dTempRest >= 0.2)
                        iRet = 3;
                    else
                        iRet = 4;
                }
            }
            else
            {
                if (dTempRest > 0)
                    iRet = 4;
                else
                {
                    dTempRest = -dTempRest;

                    if (dTempRest > 5)
                        iRet = 0;
                    else if (dTempRest > 3)
                        iRet = 1;
                    else if (dTempRest > 1)
                        iRet = 2;
                    else if (dTempRest >= 0.2)
                        iRet = 3;
                    else
                        iRet = 4;

                }

            }
            return iRet;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fInputX"></param>
        /// <param name="fInputY"></param>
        /// <param name="dk"></param>
        /// <returns></returns>
        public static int FLastPointVec(float fInputX, float fInputY, double[] dk)
        {
            double dTempRest = 0;
            int iRet = 0;

            dTempRest = dk[1] + 2 * dk[2] * fInputX + 3 * dk[3] * fInputX * fInputX + 4 * dk[4] * fInputX * fInputX * fInputX
                + 5 * dk[5] * fInputX * fInputX * fInputX * fInputX + 6 * dk[6] * fInputX * fInputX * fInputX * fInputX * fInputX;

            if (fInputY > 0)
            {
                if (dTempRest < 0)
                    iRet = 4;
                else
                {

                    if (dTempRest > 5)
                        iRet = 0;
                    else if (dTempRest > 3)
                        iRet = 1;
                    else if (dTempRest > 1)
                        iRet = 2;
                    else if (dTempRest >= 0.2)
                        iRet = 3;
                    else
                        iRet = 4;
                }
            }
            else
            {
                if (dTempRest > 0)
                    iRet = 4;
                else
                {
                    dTempRest = -dTempRest;

                    if (dTempRest > 5)
                        iRet = 0;
                    else if (dTempRest > 3)
                        iRet = 1;
                    else if (dTempRest > 1)
                        iRet = 2;
                    else if (dTempRest >= 0.2)
                        iRet = 3;
                    else
                        iRet = 4;

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
                            iRet = 2;
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
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }
                            else if (dTempRest > 0)//第三层
                            {
                                if (originalValue < 0)
                                    iRet = 3;
                                else
                                    iRet = 4;

                            }
                            else if (dTempRest > -0.0005)//第三层
                            {

                                if (originalValue > 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }
                            else// if (dTempRest < -0.0005)
                            {
                                if (originalValue > 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }

                            //第三次 完成
                        }
                    }
                    else if (dTempRest > 0)//第二层  0~0.0005
                    {
                        if (originalValue < 0)
                            iRet = 2;
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
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }
                            else if (dTempRest > 0)//第三层
                            {

                                if (originalValue < 0)
                                    iRet = 3;
                                else
                                    iRet = 4;

                            }
                            else if (dTempRest > -0.0005)//第三层
                            {

                                if (originalValue > 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }
                            else// if (dTempRest < -0.0005)
                            {
                                if (originalValue > 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }

                            //第三次 完成
                        }

                    }
                    else if (dTempRest > -0.0005)//第二层
                    {
                        if (originalValue > 0)
                            iRet = 2;
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
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }
                            else if (dTempRest > 0)//第三层
                            {
                                if (originalValue < 0)
                                    iRet = 3;
                                else
                                    iRet = 4;

                            }
                            else if (dTempRest > -0.0005)//第三层
                            {

                                if (originalValue > 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }
                            else// if (dTempRest < -0.0005)
                            {
                                if (originalValue > 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }

                            //第三次 完成

                        }

                    }
                    else// if (dTempRest < -0.0005) //第二层
                    {
                        if (originalValue > 0)
                            iRet = 2;
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
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }
                            else if (dTempRest > 0)//第三层
                            {
                                if (originalValue < 0)
                                    iRet = 3;
                                else
                                    iRet = 4;

                            }
                            else if (dTempRest > -0.0005)//第三层
                            {

                                if (originalValue > 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }
                            else// if (dTempRest < -0.0005)
                            {
                                if (originalValue > 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
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
                            iRet = 2;
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
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }
                            else if (dTempRest > 0)//第三层
                            {
                                if (originalValue < 0)
                                    iRet = 3;
                                else
                                    iRet = 4;

                            }
                            else if (dTempRest > -0.0005)//第三层
                            {

                                if (originalValue > 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }
                            else// if (dTempRest < -0.0005)
                            {
                                if (originalValue > 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }

                            //第三次 完成
                        }
                    }
                    else if (dTempRest > 0)//第二层  0~0.0005
                    {
                        if (originalValue < 0)
                            iRet = 2;
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
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }
                            else if (dTempRest > 0)//第三层
                            {

                                if (originalValue < 0)
                                    iRet = 3;
                                else
                                    iRet = 4;

                            }
                            else if (dTempRest > -0.0005)//第三层
                            {

                                if (originalValue > 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }
                            else// if (dTempRest < -0.0005)
                            {
                                if (originalValue > 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }

                            //第三次 完成
                        }

                    }
                    else if (dTempRest > -0.0005)//第二层
                    {
                        if (originalValue > 0)
                            iRet = 2;
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
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }
                            else if (dTempRest > 0)//第三层
                            {
                                if (originalValue < 0)
                                    iRet = 3;
                                else
                                    iRet = 4;

                            }
                            else if (dTempRest > -0.0005)//第三层
                            {

                                if (originalValue > 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }
                            else// if (dTempRest < -0.0005)
                            {
                                if (originalValue > 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }

                            //第三次 完成

                        }

                    }
                    else// if (dTempRest < -0.0005) //第二层
                    {
                        if (originalValue > 0)
                            iRet = 2;
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
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }
                            else if (dTempRest > 0)//第三层
                            {
                                if (originalValue < 0)
                                    iRet = 3;
                                else
                                    iRet = 4;

                            }
                            else if (dTempRest > -0.0005)//第三层
                            {

                                if (originalValue > 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }
                            else// if (dTempRest < -0.0005)
                            {
                                if (originalValue > 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
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
                            iRet = 2;
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
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }
                            else if (dTempRest > 0)//第三层
                            {
                                if (originalValue < 0)
                                    iRet = 3;
                                else
                                    iRet = 4;

                            }
                            else if (dTempRest > -0.0005)//第三层
                            {

                                if (originalValue > 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }
                            else// if (dTempRest < -0.0005)
                            {
                                if (originalValue > 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }

                            //第三次 完成
                        }
                    }
                    else if (dTempRest > 0)//第二层  0~0.0005
                    {
                        if (originalValue < 0)
                            iRet = 2;
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
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }
                            else if (dTempRest > 0)//第三层
                            {

                                if (originalValue < 0)
                                    iRet = 3;
                                else
                                    iRet = 4;

                            }
                            else if (dTempRest > -0.0005)//第三层
                            {

                                if (originalValue > 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }
                            else// if (dTempRest < -0.0005)
                            {
                                if (originalValue > 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }

                            //第三次 完成
                        }

                    }
                    else if (dTempRest > -0.0005)//第二层
                    {
                        if (originalValue > 0)
                            iRet = 2;
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
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }
                            else if (dTempRest > 0)//第三层
                            {
                                if (originalValue < 0)
                                    iRet = 3;
                                else
                                    iRet = 4;

                            }
                            else if (dTempRest > -0.0005)//第三层
                            {

                                if (originalValue > 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }
                            else// if (dTempRest < -0.0005)
                            {
                                if (originalValue > 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }

                            //第三次 完成

                        }

                    }
                    else// if (dTempRest < -0.0005) //第二层
                    {
                        if (originalValue > 0)
                            iRet = 2;
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
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }
                            else if (dTempRest > 0)//第三层
                            {
                                if (originalValue < 0)
                                    iRet = 3;
                                else
                                    iRet = 4;

                            }
                            else if (dTempRest > -0.0005)//第三层
                            {

                                if (originalValue > 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }
                            else// if (dTempRest < -0.0005)
                            {
                                if (originalValue > 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
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
                            iRet = 2;
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
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }
                            else if (dTempRest > 0)//第三层
                            {
                                if (originalValue < 0)
                                    iRet = 3;
                                else
                                    iRet = 4;

                            }
                            else if (dTempRest > -0.0005)//第三层
                            {

                                if (originalValue > 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }
                            else// if (dTempRest < -0.0005)
                            {
                                if (originalValue > 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }

                            //第三次 完成
                        }
                    }
                    else if (dTempRest > 0)//第二层  0~0.0005
                    {
                        if (originalValue < 0)
                            iRet = 2;
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
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }
                            else if (dTempRest > 0)//第三层
                            {

                                if (originalValue < 0)
                                    iRet = 3;
                                else
                                    iRet = 4;

                            }
                            else if (dTempRest > -0.0005)//第三层
                            {

                                if (originalValue > 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }
                            else// if (dTempRest < -0.0005)
                            {
                                if (originalValue > 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }

                            //第三次 完成
                        }

                    }
                    else if (dTempRest > -0.0005)//第二层
                    {
                        if (originalValue > 0)
                            iRet = 2;
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
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }
                            else if (dTempRest > 0)//第三层
                            {
                                if (originalValue < 0)
                                    iRet = 3;
                                else
                                    iRet = 4;

                            }
                            else if (dTempRest > -0.0005)//第三层
                            {

                                if (originalValue > 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }
                            else// if (dTempRest < -0.0005)
                            {
                                if (originalValue > 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }

                            //第三次 完成

                        }

                    }
                    else// if (dTempRest < -0.0005) //第二层
                    {
                        if (originalValue > 0)
                            iRet = 2;
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
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }
                            else if (dTempRest > 0)//第三层
                            {
                                if (originalValue < 0)
                                    iRet = 3;
                                else
                                    iRet = 4;

                            }
                            else if (dTempRest > -0.0005)//第三层
                            {

                                if (originalValue > 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }
                            else// if (dTempRest < -0.0005)
                            {
                                if (originalValue > 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
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
        /// 
        /// </summary>
        /// <param name="fInputX"></param>
        /// <param name="fInputX2"></param>
        /// <param name="fInputX3"></param>
        /// <param name="fInputY"></param>
        /// <param name="fInputY2"></param>
        /// <param name="fInputY3"></param>
        /// <param name="dk"></param>
        /// <returns></returns>
        public static int FLastPointAcc(float fInputX, float fInputX2, float fInputX3, float fInputY, float fInputY2, float fInputY3, double[] dk)
        {
            double dTempRest = 0;
            int iRet = 0;


            dTempRest = 2 * dk[2] + 3 * 2 * dk[3] * fInputX + 4 * 3 * dk[4] * fInputX * fInputX + 5 * 4 * dk[5] * fInputX * fInputX * fInputX + 6 * 5 * dk[6] * fInputX * fInputX * fInputX * fInputX;

            double originalValue = fInputY;// dk[0] + dk[1] * fInputX + dk[2] * fInputX * fInputX + dk[3] * fInputX * fInputX * fInputX + dk[4] * fInputX * fInputX * fInputX * fInputX + dk[5] * fInputX * fInputX * fInputX * fInputX * fInputX + dk[6] * fInputX * fInputX * fInputX * fInputX * fInputX * fInputX;



            if (dTempRest > 0.0005)  //这个肯定是 >0
            {
                if (originalValue < 0)
                    iRet = 0;
                else
                {
                    //速度 > 0, 位移 > 0第一次了

                    //第二次
                    dTempRest = 2 * dk[2] + 3 * 2 * dk[3] * fInputX2 + 4 * 3 * dk[4] * fInputX2 * fInputX2 + 5 * 4 * dk[5] * fInputX2 * fInputX2 * fInputX2 + 6 * 5 * dk[6] * fInputX2 * fInputX2 * fInputX2 * fInputX2;
                    originalValue = fInputY2;// dk[0] + dk[1] * fInputX2 + dk[2] * fInputX2 * fInputX2 + dk[3] * fInputX2 * fInputX2 * fInputX2 + dk[4] * fInputX2 * fInputX2 * fInputX2 * fInputX2 + dk[5] * fInputX2 * fInputX2 * fInputX2 * fInputX2 * fInputX2 + dk[6] * fInputX2 * fInputX2 * fInputX2 * fInputX2 * fInputX2 * fInputX2;

                    if (dTempRest > 0.0005) //第二层
                    {
                        if (originalValue < 0)
                            iRet = 2;
                        else
                        { //第三次
                            dTempRest = 2 * dk[2] + 3 * 2 * dk[3] * fInputX3 + 4 * 3 * dk[4] * fInputX3 * fInputX3 + 5 * 4 * dk[5] * fInputX3 * fInputX3 * fInputX3 + 6 * 5 * dk[6] * fInputX3 * fInputX3 * fInputX3 * fInputX3;
                            originalValue = fInputY3;//  dk[0] + dk[1] * fInputX3 + dk[2] * fInputX3 * fInputX3 + dk[3] * fInputX3 * fInputX3 * fInputX3 + dk[4] * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[5] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[6] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3;

                            if (dTempRest > 0.0005) //第三层
                            {
                                if (originalValue < 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }
                            else if (dTempRest > 0)//第三层
                            {
                                if (originalValue < 0)
                                    iRet = 3;
                                else
                                    iRet = 4;

                            }
                            else if (dTempRest > -0.0005)//第三层
                            {

                                if (originalValue > 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }
                            else// if (dTempRest < -0.0005)
                            {
                                if (originalValue > 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }

                            //第三次 完成
                        }
                    }
                    else if (dTempRest > 0)//第二层  0~0.0005
                    {
                        if (originalValue < 0)
                            iRet = 2;
                        else
                        {
                            //下面是原来的处理
                            //加速度在-0.0005 ~ 0 之间，无论如何都是初步报警，因此看下一层
                            //第三次
                            dTempRest = 2 * dk[2] + 3 * 2 * dk[3] * fInputX3 + 4 * 3 * dk[4] * fInputX3 * fInputX3 + 5 * 4 * dk[5] * fInputX3 * fInputX3 * fInputX3 + 6 * 5 * dk[6] * fInputX3 * fInputX3 * fInputX3 * fInputX3;
                            originalValue = fInputY3;//  dk[0] + dk[1] * fInputX3 + dk[2] * fInputX3 * fInputX3 + dk[3] * fInputX3 * fInputX3 * fInputX3 + dk[4] * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[5] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[6] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3;

                            if (dTempRest > 0.0005) //第三层
                            {
                                if (originalValue < 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }
                            else if (dTempRest > 0)//第三层
                            {

                                if (originalValue < 0)
                                    iRet = 3;
                                else
                                    iRet = 4;

                            }
                            else if (dTempRest > -0.0005)//第三层
                            {

                                if (originalValue > 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }
                            else// if (dTempRest < -0.0005)
                            {
                                if (originalValue > 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }

                            //第三次 完成
                        }

                    }
                    else if (dTempRest > -0.0005)//第二层
                    {
                        if (originalValue > 0)
                            iRet = 2;
                        else
                        {

                            //第三次
                            dTempRest = 2 * dk[2] + 3 * 2 * dk[3] * fInputX3 + 4 * 3 * dk[4] * fInputX3 * fInputX3 + 5 * 4 * dk[5] * fInputX3 * fInputX3 * fInputX3 + 6 * 5 * dk[6] * fInputX3 * fInputX3 * fInputX3 * fInputX3;
                            originalValue = fInputY3;//  dk[0] + dk[1] * fInputX3 + dk[2] * fInputX3 * fInputX3 + dk[3] * fInputX3 * fInputX3 * fInputX3 + dk[4] * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[5] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[6] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3;

                            if (dTempRest > 0.0005) //第三层
                            {
                                if (originalValue < 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }
                            else if (dTempRest > 0)//第三层
                            {
                                if (originalValue < 0)
                                    iRet = 3;
                                else
                                    iRet = 4;

                            }
                            else if (dTempRest > -0.0005)//第三层
                            {

                                if (originalValue > 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }
                            else// if (dTempRest < -0.0005)
                            {
                                if (originalValue > 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }

                            //第三次 完成

                        }

                    }
                    else// if (dTempRest < -0.0005) //第二层
                    {
                        if (originalValue > 0)
                            iRet = 2;
                        else
                        {  //第三次
                            dTempRest = 2 * dk[2] + 3 * 2 * dk[3] * fInputX3 + 4 * 3 * dk[4] * fInputX3 * fInputX3 + 5 * 4 * dk[5] * fInputX3 * fInputX3 * fInputX3 + 6 * 5 * dk[6] * fInputX3 * fInputX3 * fInputX3 * fInputX3;
                            originalValue = fInputY3;//  dk[0] + dk[1] * fInputX3 + dk[2] * fInputX3 * fInputX3 + dk[3] * fInputX3 * fInputX3 * fInputX3 + dk[4] * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[5] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[6] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3;

                            if (dTempRest > 0.0005) //第三层
                            {
                                if (originalValue < 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }
                            else if (dTempRest > 0)//第三层
                            {
                                if (originalValue < 0)
                                    iRet = 3;
                                else
                                    iRet = 4;

                            }
                            else if (dTempRest > -0.0005)//第三层
                            {

                                if (originalValue > 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }
                            else// if (dTempRest < -0.0005)
                            {
                                if (originalValue > 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
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
                    dTempRest = 2 * dk[2] + 3 * 2 * dk[3] * fInputX2 + 4 * 3 * dk[4] * fInputX2 * fInputX2 + 5 * 4 * dk[5] * fInputX2 * fInputX2 * fInputX2 + 6 * 5 * dk[6] * fInputX2 * fInputX2 * fInputX2 * fInputX2;
                    originalValue = fInputY2;// dk[0] + dk[1] * fInputX2 + dk[2] * fInputX2 * fInputX2 + dk[3] * fInputX2 * fInputX2 * fInputX2 + dk[4] * fInputX2 * fInputX2 * fInputX2 * fInputX2 + dk[5] * fInputX2 * fInputX2 * fInputX2 * fInputX2 * fInputX2 + dk[6] * fInputX2 * fInputX2 * fInputX2 * fInputX2 * fInputX2 * fInputX2;

                    if (dTempRest > 0.0005) //第二层
                    {
                        if (originalValue < 0)
                            iRet = 2;
                        else
                        { //第三次
                            dTempRest = 2 * dk[2] + 3 * 2 * dk[3] * fInputX3 + 4 * 3 * dk[4] * fInputX3 * fInputX3 + 5 * 4 * dk[5] * fInputX3 * fInputX3 * fInputX3 + 6 * 5 * dk[6] * fInputX3 * fInputX3 * fInputX3 * fInputX3;
                            originalValue = fInputY3;//  dk[0] + dk[1] * fInputX3 + dk[2] * fInputX3 * fInputX3 + dk[3] * fInputX3 * fInputX3 * fInputX3 + dk[4] * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[5] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[6] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3;

                            if (dTempRest > 0.0005) //第三层
                            {
                                if (originalValue < 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }
                            else if (dTempRest > 0)//第三层
                            {
                                if (originalValue < 0)
                                    iRet = 3;
                                else
                                    iRet = 4;

                            }
                            else if (dTempRest > -0.0005)//第三层
                            {

                                if (originalValue > 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }
                            else// if (dTempRest < -0.0005)
                            {
                                if (originalValue > 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }

                            //第三次 完成
                        }
                    }
                    else if (dTempRest > 0)//第二层  0~0.0005
                    {
                        if (originalValue < 0)
                            iRet = 2;
                        else
                        {
                            //下面是原来的处理
                            //加速度在-0.0005 ~ 0 之间，无论如何都是初步报警，因此看下一层
                            //第三次
                            dTempRest = 2 * dk[2] + 3 * 2 * dk[3] * fInputX3 + 4 * 3 * dk[4] * fInputX3 * fInputX3 + 5 * 4 * dk[5] * fInputX3 * fInputX3 * fInputX3 + 6 * 5 * dk[6] * fInputX3 * fInputX3 * fInputX3 * fInputX3;
                            originalValue = fInputY3;//  dk[0] + dk[1] * fInputX3 + dk[2] * fInputX3 * fInputX3 + dk[3] * fInputX3 * fInputX3 * fInputX3 + dk[4] * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[5] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[6] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3;

                            if (dTempRest > 0.0005) //第三层
                            {
                                if (originalValue < 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }
                            else if (dTempRest > 0)//第三层
                            {

                                if (originalValue < 0)
                                    iRet = 3;
                                else
                                    iRet = 4;

                            }
                            else if (dTempRest > -0.0005)//第三层
                            {

                                if (originalValue > 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }
                            else// if (dTempRest < -0.0005)
                            {
                                if (originalValue > 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }

                            //第三次 完成
                        }

                    }
                    else if (dTempRest > -0.0005)//第二层
                    {
                        if (originalValue > 0)
                            iRet = 2;
                        else
                        {

                            //第三次
                            dTempRest = 2 * dk[2] + 3 * 2 * dk[3] * fInputX3 + 4 * 3 * dk[4] * fInputX3 * fInputX3 + 5 * 4 * dk[5] * fInputX3 * fInputX3 * fInputX3 + 6 * 5 * dk[6] * fInputX3 * fInputX3 * fInputX3 * fInputX3;
                            originalValue = fInputY3;//  dk[0] + dk[1] * fInputX3 + dk[2] * fInputX3 * fInputX3 + dk[3] * fInputX3 * fInputX3 * fInputX3 + dk[4] * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[5] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[6] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3;

                            if (dTempRest > 0.0005) //第三层
                            {
                                if (originalValue < 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }
                            else if (dTempRest > 0)//第三层
                            {
                                if (originalValue < 0)
                                    iRet = 3;
                                else
                                    iRet = 4;

                            }
                            else if (dTempRest > -0.0005)//第三层
                            {

                                if (originalValue > 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }
                            else// if (dTempRest < -0.0005)
                            {
                                if (originalValue > 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }

                            //第三次 完成

                        }

                    }
                    else// if (dTempRest < -0.0005) //第二层
                    {
                        if (originalValue > 0)
                            iRet = 2;
                        else
                        {  //第三次
                            dTempRest = 2 * dk[2] + 3 * 2 * dk[3] * fInputX3 + 4 * 3 * dk[4] * fInputX3 * fInputX3 + 5 * 4 * dk[5] * fInputX3 * fInputX3 * fInputX3 + 6 * 5 * dk[6] * fInputX3 * fInputX3 * fInputX3 * fInputX3;
                            originalValue = fInputY3;// dk[0] + dk[1] * fInputX3 + dk[2] * fInputX3 * fInputX3 + dk[3] * fInputX3 * fInputX3 * fInputX3 + dk[4] * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[5] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[6] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3;

                            if (dTempRest > 0.0005) //第三层
                            {
                                if (originalValue < 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }
                            else if (dTempRest > 0)//第三层
                            {
                                if (originalValue < 0)
                                    iRet = 3;
                                else
                                    iRet = 4;

                            }
                            else if (dTempRest > -0.0005)//第三层
                            {

                                if (originalValue > 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }
                            else// if (dTempRest < -0.0005)
                            {
                                if (originalValue > 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
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
                    dTempRest = 2 * dk[2] + 3 * 2 * dk[3] * fInputX2 + 4 * 3 * dk[4] * fInputX2 * fInputX2 + 5 * 4 * dk[5] * fInputX2 * fInputX2 * fInputX2 + 6 * 5 * dk[6] * fInputX2 * fInputX2 * fInputX2 * fInputX2;
                    originalValue = fInputY2;//  dk[0] + dk[1] * fInputX2 + dk[2] * fInputX2 * fInputX2 + dk[3] * fInputX2 * fInputX2 * fInputX2 + dk[4] * fInputX2 * fInputX2 * fInputX2 * fInputX2 + dk[5] * fInputX2 * fInputX2 * fInputX2 * fInputX2 * fInputX2 + dk[6] * fInputX2 * fInputX2 * fInputX2 * fInputX2 * fInputX2 * fInputX2;

                    if (dTempRest > 0.0005) //第二层
                    {
                        if (originalValue < 0)
                            iRet = 2;
                        else
                        { //第三次
                            dTempRest = 2 * dk[2] + 3 * 2 * dk[3] * fInputX3 + 4 * 3 * dk[4] * fInputX3 * fInputX3 + 5 * 4 * dk[5] * fInputX3 * fInputX3 * fInputX3 + 6 * 5 * dk[6] * fInputX3 * fInputX3 * fInputX3 * fInputX3;
                            originalValue = fInputY3;// dk[0] + dk[1] * fInputX3 + dk[2] * fInputX3 * fInputX3 + dk[3] * fInputX3 * fInputX3 * fInputX3 + dk[4] * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[5] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[6] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3;

                            if (dTempRest > 0.0005) //第三层
                            {
                                if (originalValue < 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }
                            else if (dTempRest > 0)//第三层
                            {
                                if (originalValue < 0)
                                    iRet = 3;
                                else
                                    iRet = 4;

                            }
                            else if (dTempRest > -0.0005)//第三层
                            {

                                if (originalValue > 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }
                            else// if (dTempRest < -0.0005)
                            {
                                if (originalValue > 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }

                            //第三次 完成
                        }
                    }
                    else if (dTempRest > 0)//第二层  0~0.0005
                    {
                        if (originalValue < 0)
                            iRet = 2;
                        else
                        {
                            //下面是原来的处理
                            //加速度在-0.0005 ~ 0 之间，无论如何都是初步报警，因此看下一层
                            //第三次
                            dTempRest = 2 * dk[2] + 3 * 2 * dk[3] * fInputX3 + 4 * 3 * dk[4] * fInputX3 * fInputX3 + 5 * 4 * dk[5] * fInputX3 * fInputX3 * fInputX3 + 6 * 5 * dk[6] * fInputX3 * fInputX3 * fInputX3 * fInputX3;
                            originalValue = fInputY3;// dk[0] + dk[1] * fInputX3 + dk[2] * fInputX3 * fInputX3 + dk[3] * fInputX3 * fInputX3 * fInputX3 + dk[4] * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[5] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[6] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3;

                            if (dTempRest > 0.0005) //第三层
                            {
                                if (originalValue < 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }
                            else if (dTempRest > 0)//第三层
                            {

                                if (originalValue < 0)
                                    iRet = 3;
                                else
                                    iRet = 4;

                            }
                            else if (dTempRest > -0.0005)//第三层
                            {

                                if (originalValue > 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }
                            else// if (dTempRest < -0.0005)
                            {
                                if (originalValue > 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }

                            //第三次 完成
                        }

                    }
                    else if (dTempRest > -0.0005)//第二层
                    {
                        if (originalValue > 0)
                            iRet = 2;
                        else
                        {

                            //第三次
                            dTempRest = 2 * dk[2] + 3 * 2 * dk[3] * fInputX3 + 4 * 3 * dk[4] * fInputX3 * fInputX3 + 5 * 4 * dk[5] * fInputX3 * fInputX3 * fInputX3 + 6 * 5 * dk[6] * fInputX3 * fInputX3 * fInputX3 * fInputX3;
                            originalValue = fInputY3;//  dk[0] + dk[1] * fInputX3 + dk[2] * fInputX3 * fInputX3 + dk[3] * fInputX3 * fInputX3 * fInputX3 + dk[4] * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[5] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[6] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3;

                            if (dTempRest > 0.0005) //第三层
                            {
                                if (originalValue < 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }
                            else if (dTempRest > 0)//第三层
                            {
                                if (originalValue < 0)
                                    iRet = 3;
                                else
                                    iRet = 4;

                            }
                            else if (dTempRest > -0.0005)//第三层
                            {

                                if (originalValue > 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }
                            else// if (dTempRest < -0.0005)
                            {
                                if (originalValue > 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }

                            //第三次 完成

                        }

                    }
                    else// if (dTempRest < -0.0005) //第二层
                    {
                        if (originalValue > 0)
                            iRet = 2;
                        else
                        {  //第三次
                            dTempRest = 2 * dk[2] + 3 * 2 * dk[3] * fInputX3 + 4 * 3 * dk[4] * fInputX3 * fInputX3 + 5 * 4 * dk[5] * fInputX3 * fInputX3 * fInputX3 + 6 * 5 * dk[6] * fInputX3 * fInputX3 * fInputX3 * fInputX3;
                            originalValue = fInputY3;//  dk[0] + dk[1] * fInputX3 + dk[2] * fInputX3 * fInputX3 + dk[3] * fInputX3 * fInputX3 * fInputX3 + dk[4] * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[5] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[6] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3;

                            if (dTempRest > 0.0005) //第三层
                            {
                                if (originalValue < 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }
                            else if (dTempRest > 0)//第三层
                            {
                                if (originalValue < 0)
                                    iRet = 3;
                                else
                                    iRet = 4;

                            }
                            else if (dTempRest > -0.0005)//第三层
                            {

                                if (originalValue > 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }
                            else// if (dTempRest < -0.0005)
                            {
                                if (originalValue > 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
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
                    dTempRest = 2 * dk[2] + 3 * 2 * dk[3] * fInputX2 + 4 * 3 * dk[4] * fInputX2 * fInputX2 + 5 * 4 * dk[5] * fInputX2 * fInputX2 * fInputX2 + 6 * 5 * dk[6] * fInputX2 * fInputX2 * fInputX2 * fInputX2;
                    originalValue = fInputY2;//  dk[0] + dk[1] * fInputX2 + dk[2] * fInputX2 * fInputX2 + dk[3] * fInputX2 * fInputX2 * fInputX2 + dk[4] * fInputX2 * fInputX2 * fInputX2 * fInputX2 + dk[5] * fInputX2 * fInputX2 * fInputX2 * fInputX2 * fInputX2 + dk[6] * fInputX2 * fInputX2 * fInputX2 * fInputX2 * fInputX2 * fInputX2;

                    if (dTempRest > 0.0005) //第二层
                    {
                        if (originalValue < 0)
                            iRet = 2;
                        else
                        { //第三次
                            dTempRest = 2 * dk[2] + 3 * 2 * dk[3] * fInputX3 + 4 * 3 * dk[4] * fInputX3 * fInputX3 + 5 * 4 * dk[5] * fInputX3 * fInputX3 * fInputX3 + 6 * 5 * dk[6] * fInputX3 * fInputX3 * fInputX3 * fInputX3;
                            originalValue = fInputY3;//  dk[0] + dk[1] * fInputX3 + dk[2] * fInputX3 * fInputX3 + dk[3] * fInputX3 * fInputX3 * fInputX3 + dk[4] * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[5] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[6] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3;

                            if (dTempRest > 0.0005) //第三层
                            {
                                if (originalValue < 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }
                            else if (dTempRest > 0)//第三层
                            {
                                if (originalValue < 0)
                                    iRet = 3;
                                else
                                    iRet = 4;

                            }
                            else if (dTempRest > -0.0005)//第三层
                            {

                                if (originalValue > 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }
                            else// if (dTempRest < -0.0005)
                            {
                                if (originalValue > 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }

                            //第三次 完成
                        }
                    }
                    else if (dTempRest > 0)//第二层  0~0.0005
                    {
                        if (originalValue < 0)
                            iRet = 2;
                        else
                        {
                            //下面是原来的处理
                            //加速度在-0.0005 ~ 0 之间，无论如何都是初步报警，因此看下一层
                            //第三次
                            dTempRest = 2 * dk[2] + 3 * 2 * dk[3] * fInputX3 + 4 * 3 * dk[4] * fInputX3 * fInputX3 + 5 * 4 * dk[5] * fInputX3 * fInputX3 * fInputX3 + 6 * 5 * dk[6] * fInputX3 * fInputX3 * fInputX3 * fInputX3;
                            originalValue = fInputY3;// dk[0] + dk[1] * fInputX3 + dk[2] * fInputX3 * fInputX3 + dk[3] * fInputX3 * fInputX3 * fInputX3 + dk[4] * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[5] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[6] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3;

                            if (dTempRest > 0.0005) //第三层
                            {
                                if (originalValue < 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }
                            else if (dTempRest > 0)//第三层
                            {

                                if (originalValue < 0)
                                    iRet = 3;
                                else
                                    iRet = 4;

                            }
                            else if (dTempRest > -0.0005)//第三层
                            {

                                if (originalValue > 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }
                            else// if (dTempRest < -0.0005)
                            {
                                if (originalValue > 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }

                            //第三次 完成
                        }

                    }
                    else if (dTempRest > -0.0005)//第二层
                    {
                        if (originalValue > 0)
                            iRet = 2;
                        else
                        {

                            //第三次
                            dTempRest = 2 * dk[2] + 3 * 2 * dk[3] * fInputX3 + 4 * 3 * dk[4] * fInputX3 * fInputX3 + 5 * 4 * dk[5] * fInputX3 * fInputX3 * fInputX3 + 6 * 5 * dk[6] * fInputX3 * fInputX3 * fInputX3 * fInputX3;
                            originalValue = fInputY3;//  dk[0] + dk[1] * fInputX3 + dk[2] * fInputX3 * fInputX3 + dk[3] * fInputX3 * fInputX3 * fInputX3 + dk[4] * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[5] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[6] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3;

                            if (dTempRest > 0.0005) //第三层
                            {
                                if (originalValue < 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }
                            else if (dTempRest > 0)//第三层
                            {
                                if (originalValue < 0)
                                    iRet = 3;
                                else
                                    iRet = 4;

                            }
                            else if (dTempRest > -0.0005)//第三层
                            {

                                if (originalValue > 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }
                            else// if (dTempRest < -0.0005)
                            {
                                if (originalValue > 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }

                            //第三次 完成

                        }

                    }
                    else// if (dTempRest < -0.0005) //第二层
                    {
                        if (originalValue > 0)
                            iRet = 2;
                        else
                        {  //第三次
                            dTempRest = 2 * dk[2] + 3 * 2 * dk[3] * fInputX3 + 4 * 3 * dk[4] * fInputX3 * fInputX3 + 5 * 4 * dk[5] * fInputX3 * fInputX3 * fInputX3 + 6 * 5 * dk[6] * fInputX3 * fInputX3 * fInputX3 * fInputX3;
                            originalValue = fInputY3;//  dk[0] + dk[1] * fInputX3 + dk[2] * fInputX3 * fInputX3 + dk[3] * fInputX3 * fInputX3 * fInputX3 + dk[4] * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[5] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 + dk[6] * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3 * fInputX3;

                            if (dTempRest > 0.0005) //第三层
                            {
                                if (originalValue < 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }
                            else if (dTempRest > 0)//第三层
                            {
                                if (originalValue < 0)
                                    iRet = 3;
                                else
                                    iRet = 4;

                            }
                            else if (dTempRest > -0.0005)//第三层
                            {

                                if (originalValue > 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
                            }
                            else// if (dTempRest < -0.0005)
                            {
                                if (originalValue > 0)
                                    iRet = 3;
                                else
                                    iRet = 4;
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
        /// 
        /// </summary>
        /// <param name="strInput"></param>
        /// <param name="fU"></param>
        /// <returns></returns>
        public static int ManDisplace(string strInput, float fU)
        {

            float fInput = (float)Convert.ToDouble(strInput);

            if (Math.Abs(fU) < fInput / 3.0)
                return 0;
            else if (Math.Abs(fU) <= 2.0 * fInput / 3.0)
                return 1;
            else
                return 2;
        }

    }
}
