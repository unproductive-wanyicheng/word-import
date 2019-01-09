using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Aspose.Words;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tunnel.Word.BuildWord;
using Tunnel.Word.BuildWord.Month;
using Tunnel.Word.Chart;
using Tunnel.Word.Enums;
using Tunnel.Word.Model;
using Tunnel.Word.Monitoring;
using Tunnel.Word.Table;

namespace Tunnel.Word.Test
{
    [TestClass]
    public class UnitTest2
    {

        [TestMethod]
        public void TestTunnelMonthTest()
        {
            string fileName = Path.Combine(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "TunnelMonthTest.docx");
            TunnelMonthTestService tunnelMonth = new TunnelMonthTestService();
            var odc = tunnelMonth.BuildWord();

            odc.Save(fileName, SaveFormat.Docx);
        }


        /// <summary>
        /// 测试目录中的页码改变后更新页码和总页数
        /// </summary>
        [TestMethod]
        public void TestItem()
        {
            string fileName = Path.Combine(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase,"TunnelMonthFiles", "TestItem.doc");
            var odc = new Document(fileName);
            var builder = new DocumentBuilder(odc);
            builder.MoveToDocumentEnd();
            builder.ParagraphFormat.StyleIdentifier = StyleIdentifier.Heading2;
            builder.Writeln("新建附件一");
            builder.Writeln("新建附件二");
            odc.UpdateFields();
            builder.MoveToHeaderFooter(HeaderFooterType.FooterPrimary);

            WordManage.BookMarkReplace(odc, builder, "totalPage",(odc.PageCount-6).ToString());

            odc.UpdateFields();
            odc.Save(fileName, SaveFormat.Doc);
        }


        [TestMethod]
        public void TestTunnelMonth()
        {
            ChuzhiService monthService = new ChuzhiService();
            String time = DateTime.Now.ToString("yyyy年MM月dd日");
            monthService.SetCoverModel(new CoverModel()
            {
                ReportNumber = "GJJCZX - BLGS - LYCZ - JKY - 001",
                ProjectName = "云南保山至泸水高速公路老营特长隧道",
                ContractSection = "      土建S1合同         ",
                TunnelName = "老营特长隧道右幅（进口端）",
                MonitoringRange1 = "      K1+400～K1+460      ",
                //MonitoringRange2 = " 右幅（K1+466～K1+556）   ",
                //MonitoringDateRange = "  2016.3.28～2016.4.28  "
                MonitoringDateRange = "    " + time + "     "
            });

            monthService.SetBodyModel(new BodyModel()
            {
                HeaderText = "云南保山至泸水高速公路老营特长隧道初期支护检测报告                  GJJCZX -BLGS-LYCZ-JKY-002",
                //TaskSource = "贵州省交通建设工程检测中心有限责任公司受保泸高速公路工程建设指挥部的委托，承担老营特长隧道进口施工期间的超前地质预报、质量检测及监控量测工作。按合同要求及时编写隧道施工期间监控量测阶段性报告。",
                EngineeringSurvey = "云南保山至泸水高速公路地处云南省西北部，路线走向总为由东向西北方向布设。路线起于保山市隆阳区老营，经过隆阳区瓦房乡、怒江州泸水县上江乡，止于怒江州泸水县六库镇。\r\n老营特长隧道垂直横穿怒江山脉，位于构造侵蚀高中山山地地貌单元内，高差起伏大。该段内地层岩性主要为寒武系、奥陶系、志留系、泥盆系粉砂岩、砂岩、页岩、灰岩为主。其为一座分离式隧道，左幅隧道起讫里程为ZK1+550～ZK12+980，长11430m，最大埋深约为1247m；右幅隧道起讫里程为K1+435～K12+955，长11520m，最大埋深约为1252m。",
                ConstructionSituation = "\r\n2016年04月21日，我公司运用地质雷达法对老营特长隧道右幅进口K1+484～K1+535段进行隧道初期支护施工质量检测。\r\n实际采集雷达数据357测线米，完成雷达检测工作量51延米，累计检测完成75m。",
                //ConstructionSituationList = Build113(),
                ChuzhiQuexianMileage = "老营特长隧道右幅进口K1+494～K1+545段",
                ChuzhiQuexianConclusion1 = "K1+494～K1+545段测线范围内发现明显缺陷",
                ChuzhiParamsTable = new List<IListModel>
                {
                    new ChuzhiParamsModel
                    {
                        TableName = "表2 老营特长隧道右幅进口复合初期支护参数表",
                        ChuzhiParamsDatas = new List<ChuzhiParamsDataModel>()
                        {
                            new ChuzhiParamsDataModel()
                            {
                                ParamsMileage = "K1+448-K1+460",
                                SurroundingRockLevel = "Ⅴ级",
                                ProtectType = "SF5c",
                                ShotcreteThickness = "27",
                                SpacingOfSteelSupport = "60",
                                Remark = "本段变更：支护类型SF5d；喷射混凝土厚度27cm；钢支撑间距50cm"
                            },
                            new ChuzhiParamsDataModel()
                            {
                                ParamsMileage = "K1+448-K1+460",
                                SurroundingRockLevel = "Ⅴ级",
                                ProtectType = "SF5c",
                                ShotcreteThickness = "27",
                                SpacingOfSteelSupport = "60",
                                Remark = "本段变更：支护类型SF5d；喷射混凝土厚度27cm；钢支撑间距50cm"
                            }
                        }
                    }
                },
                ChuzhisDefectTable = new List<IListModel>
                {
                    new ChuzhiQuexianModel
                    {
                        TableName = "表6  喷射混凝土支护施工质量缺陷分布统计表",
                        ChuzhiQuexianDatas = new List<ChuzhiQuexianDataModel>()
                        {
                            new ChuzhiQuexianDataModel()
                            {
                                Index = "1",
                                Position = "111",
                                ParamsMileage = "K1+448-K1+460",
                                BadLength = "122",
                                BadType = "SF5c",
                                BadDeepth = "27",
                                Remark = "本段变更：支护类型SF5d；喷射混凝土厚度27cm；钢支撑间距50cm"
                            },
                            new ChuzhiQuexianDataModel()
                            {
                                Index = "1",
                                Position = "111",
                                ParamsMileage = "K1+448-K1+460",
                                BadLength = "122",
                                BadType = "SF5c",
                                BadDeepth = "27",
                                Remark = "本段变更：支护类型SF5d；喷射混凝土厚度27cm；钢支撑间距50cm"
                            }
                        }
                    }
                },
                ChuzhisSteelTable = new List<IListModel>
                {
                    new ChuzhiGZCModel
                    {
                        TableName = "表6  喷射混凝土支护施工质量缺陷分布统计表",
                        ChuzhiGZCDatas = new List<ChuzhiGZCDataModel>()
                        {
                            new ChuzhiGZCDataModel()
                            {
                                Index = "1",
                                ParamsMileage = "K1+448-K1+460",
                                SurroundingRockLevel = "V级",
                                ProtectType = "SF5D",
                                DesginNums = "12",
                                FactNums = "12",
                                DesginSpacing = "50",
                                FactSpacing = "50"
                            },
                            new ChuzhiGZCDataModel()
                            {
                                Index = "2",
                                ParamsMileage = "K1+448-K1+460",
                                SurroundingRockLevel = "V级",
                                ProtectType = "SF5D",
                                DesginNums = "15",
                                FactNums = "15",
                                DesginSpacing = "60",
                                FactSpacing = "60"
                            }
                        }
                    }
                },
                ChuzhiThicknessTable = new List<IListModel>()
                {
                    new ChuzhiThicknessModel
                    {
                        TableName = "表5 喷射混凝土厚度检测结果统计表",
                        ShotcreteThickness = "30",
                        MaxThickness = "45",
                        MinThickness = "15",
                        AverageThickness = "30",
                        GoodPercent = "99.99%",
                        BottomText = "每条测线平均厚度",
                        ChuzhiThicknessDatas = new List<ChuzhiThicknessDataModel>
                        {
                            new ChuzhiThicknessDataModel
                            {
                                Index = "1",
                                ParamsMileage = "K11-K22",
                                RealThicknessG = "10",
                                RealThicknessE = "10",
                                RealThicknessC = "10",
                                RealThicknessA = "10",
                                RealThicknessB = "10",
                                RealThicknessD = "10",
                                RealThicknessF = "10",
                            },
                            new ChuzhiThicknessDataModel
                            {
                                Index = "2",
                                ParamsMileage = "K11-K22",
                                RealThicknessG = "10",
                                RealThicknessE = "10",
                                RealThicknessC = "10",
                                RealThicknessA = "10",
                                RealThicknessB = "10",
                                RealThicknessD = "10",
                                RealThicknessF = "10",
                            },
                            new ChuzhiThicknessDataModel
                            {
                                Index = "3",
                                ParamsMileage = "K11-K22",
                                RealThicknessG = "10",
                                RealThicknessE = "10",
                                RealThicknessC = "10",
                                RealThicknessA = "10",
                                RealThicknessB = "10",
                                RealThicknessD = "10",
                                RealThicknessF = "10",
                            },
                            new ChuzhiThicknessDataModel
                            {
                                Index = "4",
                                ParamsMileage = "K11-K22",
                                RealThicknessG = "10",
                                RealThicknessE = "10",
                                RealThicknessC = "10",
                                RealThicknessA = "10",
                                RealThicknessB = "10",
                                RealThicknessD = "10",
                                RealThicknessF = "10",
                            }
                        },
                        ChuzhiThicknessAveDatas = new List<ChuzhiThicknessAveDataModel>
                        {
                            new ChuzhiThicknessAveDataModel
                            {
                                RealThicknessG = "5",
                                RealThicknessE = "5",
                                RealThicknessC = "5",
                                RealThicknessA = "5",
                                RealThicknessB = "5",
                                RealThicknessD = "5",
                                RealThicknessF = "5",
                            }
                        }
                    }
                },
                //MustMonitoringResults = new List<IListModel>()
                //{
                //    new MustMonitoringResultModel
                //    {
                //        ResultName="6.1.1洞内观察",
                //        Describe="结合现场施工情况，本月老营特长隧道左幅ZK1+890～ZK1+960、右幅K1+870～K1+930段均未发现喷射混凝土开裂、脱落、掉块、渗水及其它特殊情况。"
                //    },
                //    new MustMonitoringResultModel
                //    {
                //        ResultName="6.1.2左幅进口拱顶下沉、周边位移监测数据分析",
                //        ResultDataList = Build113()
                //    }
                //},
                //MonitorSummery = "为叙述方便，在以下报告中对围岩与初期支护间压力、锚杆轴力、围岩内部位移、钢支撑轴力、二次衬砌混凝土应力数值的符号作了统一的规定：“+”表示围岩与初期支护受压，锚杆轴力受拉，围岩内部位移计受拉围岩向外位移，钢拱架钢筋受拉，二次衬砌混凝土应力受拉；“-”则反之。",
                //SelectionMonitoringResults = new List<IListModel>()
                //{
                //    new SelectionMonitoringResultModel
                //    {
                //        ItemName="6.2.1隧道左幅监测成果及数据分析",
                //        ResultListModel = new List<MonitoringResultModel>()
                //        {
                //            new MonitoringResultModel
                //            {
                //                ResultName="（1）钢支撑轴力",
                //                Describe="对监控量测所获得的轴力数据，归纳分析，ZK1+700初期支护中钢支撑目前最大受力数据见表29，时间曲线图见图39。",
                //                ResultDataList = Build113(),
                //                Summary = "结果分析：ZK1+700钢支撑轴力监测断面，钢筋计布设在钢拱架内外缘，围岩级别为Ⅴ级，隧道钢拱架型号为I20（长b:100mm，宽t:11.4mm，截面积1140mm2），强度设计值215MPa，钢拱架设计轴力为245kN从观测数据来看,在2016年04月13日安装监测后，测点A钢筋计处于受拉状态，最大拉力为5.51kN，占理论值的2%；测点B钢筋计处于受压状态，最大压力为-8.10kN，占理论值的3%；测点C钢筋计处于受压状态，最大压力为-14.96kN，占理论值的6%；测点D钢筋计处于受压状态，最大压力为-26.84kN，占理论值的11%；测点E钢筋计处于受压状态，最大压力为-17.25kN，占理论值的6%；综上所述，隧道钢拱架实际受力值小于强度设计值，钢支撑的尺寸、间距满足设计要求，如图39。"
                //            },
                //            new MonitoringResultModel
                //            {
                //                ResultName="（2）围岩内部位移",
                //                Describe="对监控量测所获得的围岩内部位移数据，归纳分析，ZK1+700初期支护中围岩内部位移目前最大位移数据见表30，时间曲线图见图40。",
                //                ResultDataList = Build113(),
                //                Summary = "结果分析：ZK1+700围岩级别为Ⅴ级，围岩内部位移布设在围岩中，从观测数据来看,在2016年04月13日安装监测后，左侧边墙测点1最大位移为-0.11mm；左侧边墙测2点最大位移为-0.37mm；左侧边墙测点3最大位移为0.27mm；右侧边墙测1最大位移为-0.02mm ；右侧边墙测2最大位移为-0.01mm；右侧边墙测3最大位移为0.03mm；围岩内部位移量测：当实测位移值为正值时，表明围岩受拉其围岩位移方向为向外位移；当实测位移值为负值时，表明围岩受压其围岩位移方向为向内位移；如图40。"
                //            }
                //            ,
                //            new MonitoringResultModel
                //            {
                //                ResultName="（3）围岩内部位移1",
                //                Describe="对监控量测所获得的围岩内部位移数据，归纳分析，ZK1+700初期支护中围岩内部位移目前最大位移数据见表30，时间曲线图见图40。",
                //                ResultDataList = Build113(),
                //                Summary = "结果分析：ZK1+700围岩级别为Ⅴ级，围岩内部位移布设在围岩中，从观测数据来看,在2016年04月13日安装监测后，左侧边墙测点1最大位移为-0.11mm；左侧边墙测2点最大位移为-0.37mm；左侧边墙测点3最大位移为0.27mm；右侧边墙测1最大位移为-0.02mm ；右侧边墙测2最大位移为-0.01mm；右侧边墙测3最大位移为0.03mm；围岩内部位移量测：当实测位移值为正值时，表明围岩受拉其围岩位移方向为向外位移；当实测位移值为负值时，表明围岩受压其围岩位移方向为向内位移；如图40。"
                //            }
                //        }
                //    }
                //},
                //Conclusion ="根据监控量测数据结果分析判断：\r\n"+
                //"  1)老营特长隧道左幅进口，ZK1+558～ZK1+594段各监测断面变形速度及累计变形值较小，累计变形值U0＜Un／3，各断面已基本稳定，可进行下道工序施工；ZK1+610、ZK1+623两个监测断面变形速度及累计变形值较小，累计变形值U0＜Un／3，由于时态曲线呈缓慢上升趋势，需继续监测。\r\n"+
                //"  2)老营特长隧道右幅进口, K1+466～K1+540段各监测断面变形速度及累计变形值较小，累计变形值U0＜Un／3，各断面已基本稳定，可下道工序施工； K1+556监测断面变形速度及累计变形值较小，累计变形值U0＜Un／3，由于时态曲线呈缓慢上升趋势，需继续监测。\r\n"+
                //"  3)老营特长隧道左幅进口地表下沉ZK1+550各监测点变形速度及累计变形值变化较小，各测点已趋于稳定。\r\n"+
                //"  4) 老营特长隧道右幅进口地表下沉K1+470各监测点变形速度及累计变形值变化较小，各测点已趋于稳定。",
                ChuzhiChuzhiConclusion = "对老营特长隧道右幅进口K1+484～K1+535段喷射混凝土检测：平均厚度30cm≥设计厚度27cm；检查点数的98.6%≥设计厚度；最大厚度38cm，最小厚度26cm≥0.5倍设计厚度，且≥50mm。检测结果满足设计和规范要求，评定为不合格。",
                ChuzhiQuexianConclusion = "老营特长隧道右幅进口K1+484～K1+535段测线范围内未发现明显缺陷，检测结果满足设计和规范要求，评定为不合格。",
                ChuzhiGangzhichengConclusion = "对老营特长隧道右幅进口K1+484～K1+490段钢支撑安装数量及间距检测：设计间距50cm，实测平均间距50cm；设计榀数12榀，实测榀数12榀；K1+490～K1+535段钢支撑安装数量及间距检测：设计间距60cm，实测平均间距60cm；设计榀数75榀，实测榀数75榀，检测结果满足设计和规范要求，评定为不合格。",
                //Proposal = "  1)建议施工时采用“短进尺、弱爆破、及时支护、勤量测、快循环”的施工原则，严格按照设计要求及施工规范施工，以确保隧道施工安全；\r\n"+
                //"  2)严格控制超前导管注浆质量，做到各施工工序衔接及时，严格控制系统锚杆及锁脚锚杆施工质量；\r\n"+
                //"  3) 施工过程中注意对监控测点的保护，确保监控量测数据的延续性，保证测量结果的有效性。"
            });

            monthService.SetEnclosureModel(new List<EnclosureModel>()
            {
                new EnclosureModel()
                {
                    HeaderName = "附图1 各测线实测厚度剖面图",
                    Type = EnclosureType.ChuzhiImage,
                    Images = new List<ImageModel>()
                    {
                        new ImageModel()
                        {
                            ImageName = "",
                            ImageUrl = Path.Combine(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase,"Files","侧线图.png")
                        }
                    }
                }
            });

            //monthService.SetEnclosureModel(new List<EnclosureModel>()
            //{
            //    new EnclosureModel()
            //    {
            //        HeaderName="附件1：老营特长隧道右幅进口支护观察记录1",
            //        Type = EnclosureType.Table,
            //        TunnelName = "老营特长隧道\r\n右幅进口",
            //        PileNumber="K1+935",
            //        ObservationTime =DateTime.Now.ToString("yyyy年M月d日"),
            //        Observant="罗毅",
            //        GeologicalSketchMap =Path.Combine(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase,"Files","地质素描图.png"),
            //        PhotoPalm = Path.Combine(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase,"Files","掌子面照片.png"),
            //        GeologicalDescription = "掌子面围岩为褐黄色灰岩，薄～中厚层状，呈强风化，受断层构造强烈影响，产生挤压破碎带，节理裂隙、溶蚀裂隙发育，有泥质物充填，岩体破碎，围岩受水浸泡易软化、掉块以致坍塌。\r\n"+
            //        "岩层产状：310°∠30°，主要发育节理有3组：J1：145°∠40°，7条/m；J2：350°∠70°，10条/m，J3：190°∠45°，8条/m。掌子面渗水，右侧岩体呈碎裂状，易掉块、坍塌，围岩完整性和稳定性差。",
            //        DescriptionSupportCondition = "喷射混凝土平整度较好，无开裂及渗水情况。",
            //        SurfaceCrackDescription = "边仰坡及地表无开裂及滑坡情况。"
            //    },
            //    new EnclosureModel()
            //    {
            //        HeaderName = "附件2：老营特长隧道右幅进口照片",
            //        Type = EnclosureType.Image,
            //        Images = new List<ImageModel>()
            //        {
            //            new ImageModel()
            //            {
            //                ImageName = "照片1",
            //                ImageUrl = Path.Combine(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase,"Files","图片1.png")
            //            },
            //            new ImageModel()
            //            {
            //                ImageName = "照片2",
            //                ImageUrl = Path.Combine(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase,"Files","图片1.png")
            //            }
            //        }
            //    },
            //    new EnclosureModel()
            //    {
            //        HeaderName="附件3：老营特长隧道右幅进口支护观察记录3",
            //        Type = EnclosureType.Table,
            //        TunnelName = "老营特长隧道\r\n右幅进口",
            //        PileNumber="K1+935",
            //        ObservationTime =DateTime.Now.ToString("yyyy年M月d日"),
            //        Observant="罗毅",
            //        GeologicalSketchMap =Path.Combine(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase,"Files","地质素描图.png"),
            //        PhotoPalm = Path.Combine(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase,"Files","掌子面照片.png"),
            //        GeologicalDescription = "掌子面围岩为褐黄色灰岩，薄～中厚层状，呈强风化，受断层构造强烈影响，产生挤压破碎带，节理裂隙、溶蚀裂隙发育，有泥质物充填，岩体破碎，围岩受水浸泡易软化、掉块以致坍塌。\r\n"+
            //                                "岩层产状：310°∠30°，主要发育节理有3组：J1：145°∠40°，7条/m；J2：350°∠70°，10条/m，J3：190°∠45°，8条/m。掌子面渗水，右侧岩体呈碎裂状，易掉块、坍塌，围岩完整性和稳定性差。",
            //        DescriptionSupportCondition = "喷射混凝土平整度较好，无开裂及渗水情况。",
            //        SurfaceCrackDescription = "边仰坡及地表无开裂及滑坡情况。"
            //    }
            //});

            string fileName = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "初支检测.docx");
            var odoc = monthService.BuildWord();
            odoc.Save(fileName, SaveFormat.Docx);
        }

        private List<IListModel> Build113()
        {
            return new List<IListModel>()
            {
                new ScheduleDataModel()
                {
                    TableName = "表1  老营特长隧道进口施工进度统计表",
                    StartDate = "2016.3.28",
                    EndDate = "2016.4.28",
                    DataTypeModels = new List<ScheduleDataTypeModel>()
                    {
                        new ScheduleDataTypeModel
                        {
                            Name = "左幅进口",
                            Remark = "",
                            DataList = new List<ScheduleTypeDataListModel>()
                            {
                                new ScheduleTypeDataListModel()
                                {
                                    ConstructionProcess = "掌子面里程",
                                    StartDate = "ZK1+948.8",
                                    EndDate = "ZK1+968.0",
                                    StageFootage = "19.2",
                                    AccumulativeFootage = "428.0"
                                },
                                new ScheduleTypeDataListModel()
                                {
                                    ConstructionProcess = "仰拱开挖里程",
                                    StartDate = "ZK1+902.8",
                                    EndDate = "ZK1+968.0",
                                    StageFootage = "3.2",
                                    AccumulativeFootage = "373.0"
                                },
                                new ScheduleTypeDataListModel()
                                {
                                    ConstructionProcess = "二次衬砌里程",
                                    StartDate = "ZK1+853.0",
                                    EndDate = "ZK1+853.0",
                                    StageFootage = "0.0",
                                    AccumulativeFootage = "313.0"
                                }
                            }
                        },
                        new ScheduleDataTypeModel
                        {
                            Name = "右幅进口",
                            Remark = "",
                            DataList = new List<ScheduleTypeDataListModel>()
                            {
                                new ScheduleTypeDataListModel()
                                {
                                    ConstructionProcess = "掌子面里程",
                                    StartDate = "ZK1+948.8",
                                    EndDate = "ZK1+968.0",
                                    StageFootage = "19.2",
                                    AccumulativeFootage = "428.0"
                                },
                                new ScheduleTypeDataListModel()
                                {
                                    ConstructionProcess = "仰拱开挖里程",
                                    StartDate = "ZK1+902.8",
                                    EndDate = "ZK1+968.0",
                                    StageFootage = "3.2",
                                    AccumulativeFootage = "373.0"
                                }
                            }
                        }
                    }
                },
                new ChartDataModel()
                {
                    PictureName = "图1",
                    SectionName = "TEST11",
                    EquipCost = "1",
                    U0 = "1",
                    LindDataModels = new List<ChartLineDataModel>()
                    {
                        new ChartLineDataModel()
                        {
                            LineName = "A1",
                            LineModels = new List<ChartLineModel>()
                            {
                                new ChartLineModel()
                                {
                                    DateMeasurement = "2017-3-1",
                                    FistData = "2883.0",
                                    SecondData = "2883.0",
                                    ThirdData = "2883.0",
                                    AverageValue = "2883",
                                    AfterConstant = "2884",
                                    Temperature = "22",
                                    TemperatureCorrection = "2884",
                                    ComparedLast = "0",
                                    DisplacementRate = "0",
                                    CumulativeDisplacement = "0"
                                },
                                new ChartLineModel()
                                {
                                    DateMeasurement = "2017-3-2",
                                    FistData = "2877.0",
                                    SecondData = "2877.0",
                                    ThirdData = "2877.0",
                                    AverageValue = "2877",
                                    AfterConstant = "2878",
                                    Temperature = "22",
                                    TemperatureCorrection = "2878",
                                    ComparedLast = "6",
                                    DisplacementRate = "6",
                                    CumulativeDisplacement = "6"
                                }
                            }
                        },
                        new ChartLineDataModel()
                        {
                            LineName = "A2",
                            LineModels = new List<ChartLineModel>()
                            {
                                new ChartLineModel()
                                {
                                    DateMeasurement = "2017-3-1",
                                    FistData = "2810.0",
                                    SecondData = "2810.0",
                                    ThirdData = "2810.0",
                                    AverageValue = "2810",
                                    AfterConstant = "2811",
                                    Temperature = "22",
                                    TemperatureCorrection = "2811",
                                    ComparedLast = "0",
                                    DisplacementRate = "0",
                                    CumulativeDisplacement = "0"
                                },
                                new ChartLineModel()
                                {
                                    DateMeasurement = "2017-3-2",
                                    FistData = "2804.0",
                                    SecondData = "2804.0",
                                    ThirdData = "2804.0",
                                    AverageValue = "2804",
                                    AfterConstant = "2805",
                                    Temperature = "23",
                                    TemperatureCorrection = "2805",
                                    ComparedLast = "5",
                                    DisplacementRate = "5",
                                    CumulativeDisplacement = "5"
                                }

                            }
                        },
                        new ChartLineDataModel()
                        {
                            LineName = "A3",
                            LineModels = new List<ChartLineModel>()
                            {
                                new ChartLineModel()
                                {
                                    DateMeasurement = "2017-3-1",
                                    FistData = "2810.0",
                                    SecondData = "2810.0",
                                    ThirdData = "2810.0",
                                    AverageValue = "2810",
                                    AfterConstant = "2811",
                                    Temperature = "24",
                                    TemperatureCorrection = "2812",
                                    ComparedLast = "0",
                                    DisplacementRate = "0",
                                    CumulativeDisplacement = "0"
                                },
                                new ChartLineModel()
                                {
                                    DateMeasurement = "2017-3-2",
                                    FistData = "2804.0",
                                    SecondData = "2804.0",
                                    ThirdData = "2804.0",
                                    AverageValue = "2804",
                                    AfterConstant = "2805",
                                    Temperature = "23",
                                    TemperatureCorrection = "2805",
                                    ComparedLast = "8",
                                    DisplacementRate = "8",
                                    CumulativeDisplacement = "8"
                                }

                            }
                        }
                    }
                },
                new BuriedSectionModel
                {
                    TableName = "表2 老营特长隧道左幅进口监测埋设断面表",
                    BuriedSectionDatas = new List<BuriedSectionDataModel>()
                    {
                        new BuriedSectionDataModel()
                        {
                            SerialNumber = "1",
                            SectionMileage = "ZK1+890",
                            SurroundingRockLevel = "V",
                            CrownSettlement = "26.0",
                            DisplacementAcc = "22.0",
                            BurialTime = "2016.8.07",
                            Remark = ""
                        },
                        new BuriedSectionDataModel()
                        {
                            SerialNumber = "2",
                            SectionMileage = "ZK1+900",
                            SurroundingRockLevel = "V",
                            CrownSettlement = "27.0",
                            DisplacementAcc = "25.0",
                            BurialTime = "2016.8.11",
                            Remark = ""
                        }
                    }
                },
                new BurialSituationModel
                {
                    TableName = "表4选测项目监测断面埋设情况",
                    SituationDataModels = new List<BurialSituationDataModel>()
                    {
                        new BurialSituationDataModel()
                        {
                            Name = "左幅进口",
                            NumberSections = "1",
                            BuriedMileage = "ZK1+700",
                            BurialTime = "2016年4月11日",
                            SurroundingRockLevel = "V",
                            Remark = ""
                        },
                        new BurialSituationDataModel()
                        {
                            Name = "右幅进口",
                            NumberSections = "1",
                            BuriedMileage = "K1+680",
                            BurialTime = "2016年4月13日",
                            SurroundingRockLevel = "V",
                            Remark = ""
                        }
                    }
                },
                new DataAnalysisModel
                {
                    TableName = "表14 老营特长隧道左幅进口断面ZK1+890拱顶下沉、周边位移监测数据分析",
                    AnalysisTypeModels = new List<DataAnalysisTypeModel>()
                    {
                        new DataAnalysisTypeModel
                        {
                            PileNumber = "ZK1+890",
                            DataAnalysis = "变形速度及累计变形值较小，累计变形值U0＜Un／3，曲线呈平缓趋势，趋于稳定。",
                            DataList = new List<DataAnalysisDataModel>()
                            {
                                new DataAnalysisDataModel()
                                {
                                    MeasuringPoint = "A",
                                    MonitoringDays = "22.0",
                                    CumulativeValue = "26.0",
                                    AverageVelocity = "1.18"
                                },
                                new DataAnalysisDataModel()
                                {
                                    MeasuringPoint = "AB",
                                    MonitoringDays = "22.0",
                                    CumulativeValue = "26.0",
                                    AverageVelocity = "1.18"
                                },
                                new DataAnalysisDataModel()
                                {
                                    MeasuringPoint = "AC",
                                    MonitoringDays = "22.0",
                                    CumulativeValue = "26.0",
                                    AverageVelocity = "1.18"
                                },
                                new DataAnalysisDataModel()
                                {
                                    MeasuringPoint = "BC",
                                    MonitoringDays = "22.0",
                                    CumulativeValue = "26.0",
                                    AverageVelocity = "1.18"
                                }
                            }
                        }
                    }
                }
            };
        }
    }
}
