using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Aspose.Words;
using Aspose.Words.Tables;

namespace Tunnel.Word.Table
{
    public class TunnelTableService : TableBase
    {
        public Document BuildTable(ITunnelTable tableData)
        {
            if (tableData is ScheduleDataModel)
            {
                return BuildTable((ScheduleDataModel) tableData);
            }
            if (tableData is BuriedSectionModel)
            {
                return BuildTable((BuriedSectionModel)tableData);
            }

            if (tableData is BurialSituationModel)
            {
                return BuildTable((BurialSituationModel)tableData);
            }
            if (tableData is DataAnalysisModel)
            {
                return BuildTable((DataAnalysisModel)tableData);
            }
            if (tableData is EquipModel)
            {
                return BuildTable((EquipModel)tableData);
            }
            // 初支参数表
            if (tableData is ChuzhiParamsModel)
            {
                return BuildTable((ChuzhiParamsModel)tableData);
            }
            // 初支厚度表
            if (tableData is ChuzhiThicknessModel)
            {
                return BuildTable((ChuzhiThicknessModel)tableData);
            }
            // 初支缺陷检查表
            if (tableData is ChuzhiQuexianModel)
            {
                return BuildTable((ChuzhiQuexianModel)tableData);
            }
            // 初支缺陷检查表
            if (tableData is ChuzhiGZCModel)
            {
                return BuildTable((ChuzhiGZCModel)tableData);
            }
            // 二衬参数表
            if (tableData is ErchenParamsModel)
            {
                return BuildTable((ErchenParamsModel)tableData);
            }
            // 二衬缺陷检查表
            if (tableData is ErchenDefectModel)
            {
                return BuildTable((ErchenDefectModel)tableData);
            }
            // 二衬间距表
            if (tableData is ErchenSpaceModel)
            {
                return BuildTable((ErchenSpaceModel)tableData);
            }
            return new Document();
        }

        private Document BuildTable(ChuzhiThicknessModel model)
        {
            var dc = new Aspose.Words.Document(GetSroce("Tunnel.Word.ChuzhiJiance.初支检测厚度表.doc"));
            var builder = new DocumentBuilder(dc);
            WordManage.SetModel(model, dc, builder);

            NodeCollection allTables = dc.GetChildNodes(NodeType.Table, true);
            Aspose.Words.Tables.Table table = allTables[0] as Aspose.Words.Tables.Table;
            if (table == null) return new Document();
            int j = 2;
            List<RowMergeData> dic = new List<RowMergeData>();
            // 初始化表格
            //if (model.ChuzhiThicknessDatas != null)
            //{
            //    for(var i=0;i< model.ChuzhiThicknessDatas.Count+1;i++)
            //    {
            //        var row = CreateRow(new[]
            //        {
            //            "",
            //            "",
            //            "",
            //            "",
            //            "",
            //            "",
            //            "",
            //            "",
            //            "",
            //            "",
            //            "",
            //            "",
            //            "",
            //            ""
            //        }, dc);
            //        table.AppendChild(row);
            //    }
            //}

            // 设置属性
            //if (model.ChuzhiThicknessDatas != null)
            //{
            //    int L = model.ChuzhiThicknessDatas.Count;
            //    for (int i = 0; i < L; i++)
            //    {
            //        var cellData = model.ChuzhiThicknessDatas[i];
            //        SetRowData(dc, table.Rows[i+2], new[]
            //        {
            //            cellData.Index,
            //            cellData.ParamsMileage,
            //            i == 0 ? model.ShotcreteThickness : "",
            //            cellData.RealThicknessG,
            //            cellData.RealThicknessE,
            //            cellData.RealThicknessC,
            //            cellData.RealThicknessA,
            //            cellData.RealThicknessB,
            //            cellData.RealThicknessD,
            //            cellData.RealThicknessF,
            //            i == 0 ? model.MaxThickness : "",
            //            i == 0 ? model.MinThickness : "",
            //            i == 0 ? model.AverageThickness : "",
            //            i == 0 ? model.GoodPercent : "",
            //        });
            //    }
            //    SetRowData(dc, table.Rows[L+2], new[]
            //    {
            //        model.BottomText,
            //        "",
            //        "",
            //        model.ChuzhiThicknessAveDatas[0].RealThicknessG,
            //        model.ChuzhiThicknessAveDatas[0].RealThicknessE,
            //        model.ChuzhiThicknessAveDatas[0].RealThicknessC,
            //        model.ChuzhiThicknessAveDatas[0].RealThicknessA,
            //        model.ChuzhiThicknessAveDatas[0].RealThicknessB,
            //        model.ChuzhiThicknessAveDatas[0].RealThicknessD,
            //        model.ChuzhiThicknessAveDatas[0].RealThicknessF,
            //        "",
            //        "",
            //        "",
            //        "",
            //    });
            //}
            //Cell cellStartRange = table.Rows[2].Cells[2];
            //Cell cellEndRange = table.Rows[4].Cells[2];
            //Console.WriteLine(cellStartRange.GetText());
            //MergeCells(cellStartRange, cellEndRange);
            //foreach (var dataTypeModel in model.DataTypeModels)
            //{
            //    if (dataTypeModel.DataList != null)
            //    {
            //        for (int i = 0; i < dataTypeModel.DataList.Count; i++)
            //        {

            //            SetRowData(dc, table.Rows[j], new[]
            //            {
            //                i == 0?dataTypeModel.Name:"",
            //                dataTypeModel.DataList[i].ConstructionProcess,
            //                dataTypeModel.DataList[i].StartDate,
            //                dataTypeModel.DataList[i].EndDate,
            //                dataTypeModel.DataList[i].StageFootage,
            //                dataTypeModel.DataList[i].AccumulativeFootage,
            //                i == 0?dataTypeModel.Remark:""
            //            });


            //            if (dataTypeModel.DataList.Count > 1 && i == dataTypeModel.DataList.Count - 1)
            //            {
            //                dic.Add(new RowMergeData { RowJ = j, RowCount = dataTypeModel.DataList.Count });
            //            }

            //            j++;
            //        }
            //    }
            //}
            //for (int i = dic.Count - 1; i >= 0; i--)
            //{
            //    Cell cellStartRange = table.Rows[dic[i].RowJ - dic[i].RowCount + 1].Cells[0];
            //    Cell cellEndRange = table.Rows[dic[i].RowJ].Cells[0];
            //    MergeCells(cellStartRange, cellEndRange);

            //    Cell cellStartRange1 = table.Rows[dic[i].RowJ - dic[i].RowCount + 1].Cells[6];
            //    Cell cellEndRange1 = table.Rows[dic[i].RowJ].Cells[6];
            //    MergeCells(cellStartRange1, cellEndRange1);
            //}

            //while (table.Rows.Count != j)
            //{
            //    table.Rows.Remove(table.LastRow);
            //}
            int dataL = model.ChuzhiThicknessDatas.Count + 1;
            for (var i = 2; i <= dataL; i++)
            {
                var row = table.Rows[i];
                for (var k = 0; k < 14; k++)
                {
                    string text = "";
                    if (k == 0 && i != dataL)
                    {
                        text = model.ChuzhiThicknessDatas[i - 2].Index;
                    }
                    if (k == 0 && i == dataL)
                    {
                        text = model.BottomText;
                    }
                    if (k == 1 && i != dataL)
                    {
                        text = model.ChuzhiThicknessDatas[i - 2].ParamsMileage;
                    }
                    if (k == 1 && i == dataL)
                    {
                        text = "";
                    }
                    if (k == 2 && i == 2)
                    {
                        text = model.ShotcreteThickness;
                    }
                    if (k == 2 && i != 2)
                    {
                        text = "";
                    }
                    if (k == 3 && i != dataL)
                    {
                        text = model.ChuzhiThicknessDatas[i - 2].RealThicknessG;
                    }
                    if (k == 3 && i == dataL)
                    {
                        text = model.ChuzhiThicknessAveDatas[0].RealThicknessG;
                    }
                    if (k == 4 && i != dataL)
                    {
                        text = model.ChuzhiThicknessDatas[i - 2].RealThicknessE;
                    }
                    if (k == 4 && i == dataL)
                    {
                        text = model.ChuzhiThicknessAveDatas[0].RealThicknessE;
                    }
                    if (k == 5 && i != dataL)
                    {
                        text = model.ChuzhiThicknessDatas[i - 2].RealThicknessC;
                    }
                    if (k == 5 && i == dataL)
                    {
                        text = model.ChuzhiThicknessAveDatas[0].RealThicknessC;
                    }
                    if (k == 6 && i != dataL)
                    {
                        text = model.ChuzhiThicknessDatas[i - 2].RealThicknessA;
                    }
                    if (k == 6 && i == dataL)
                    {
                        text = model.ChuzhiThicknessAveDatas[0].RealThicknessA;
                    }
                    if (k == 7 && i != dataL)
                    {
                        text = model.ChuzhiThicknessDatas[i - 2].RealThicknessB;
                    }
                    if (k == 7 && i == dataL)
                    {
                        text = model.ChuzhiThicknessAveDatas[0].RealThicknessB;
                    }
                    if (k == 8 && i != dataL)
                    {
                        text = model.ChuzhiThicknessDatas[i - 2].RealThicknessD;
                    }
                    if (k == 8 && i == dataL)
                    {
                        text = model.ChuzhiThicknessAveDatas[0].RealThicknessD;
                    }
                    if (k == 9 && i != dataL)
                    {
                        text = model.ChuzhiThicknessDatas[i - 2].RealThicknessF;
                    }
                    if (k == 9 && i == dataL)
                    {
                        text = model.ChuzhiThicknessAveDatas[0].RealThicknessF;
                    }
                    if (k == 10 && i != 2)
                    {
                        text = "";
                    }
                    if (k == 10 && i == 2)
                    {
                        text = model.MaxThickness;
                    }
                    if (k == 11 && i != 2)
                    {
                        text = "";
                    }
                    if (k == 11 && i == 2)
                    {
                        text = model.MinThickness;
                    }
                    if (k == 12 && i != 2)
                    {
                        text = "";
                    }
                    if (k == 12 && i == 2)
                    {
                        text = model.AverageThickness;
                    }
                    if (k == 13 && i != 2)
                    {
                        text = "";
                    }
                    if (k == 13 && i == 2)
                    {
                        text = model.GoodPercent;
                    }

                    builder.MoveToCell(0, i, k, 0);
                    Aspose.Words.Font font = builder.Font;
                    font.Size = 10;
                    builder.Bold = false;
                    builder.ParagraphFormat.LineSpacing = 10;
                    builder.Write(text);

                    row.Cells[k].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Center;
                    row.Cells[k].CellFormat.VerticalAlignment = CellVerticalAlignment.Center;
                }
                j++;
            }
            while (table.Rows.Count != j)
            {
                table.Rows.Remove(table.LastRow);
            }
            // 合并表格
            Cell cellStartRange = table.Rows[2].Cells[2];
            Cell cellEndRange = table.Rows[j-2].Cells[2];
            MergeCells(cellStartRange, cellEndRange);
            Cell cellStartRange1 = table.Rows[2].Cells[10];
            Cell cellEndRange1 = table.Rows[j-1].Cells[10];
            MergeCells(cellStartRange1, cellEndRange1);
            Cell cellStartRange2 = table.Rows[2].Cells[11];
            Cell cellEndRange2 = table.Rows[j-1].Cells[11];
            MergeCells(cellStartRange2, cellEndRange2);
            Cell cellStartRange3 = table.Rows[2].Cells[12];
            Cell cellEndRange3 = table.Rows[j-1].Cells[12];
            MergeCells(cellStartRange3, cellEndRange3);
            Cell cellStartRange4 = table.Rows[2].Cells[13];
            Cell cellEndRange4 = table.Rows[j-1].Cells[13];
            MergeCells(cellStartRange4, cellEndRange4);
            Cell cellStartRange5 = table.Rows[j - 1].Cells[0];
            Cell cellEndRange5 = table.Rows[j - 1].Cells[2];
            MergeCells(cellStartRange5, cellEndRange5);

            table.SetBorder(BorderType.Left, LineStyle.Single, 2, Color.Black, true);
            table.SetBorder(BorderType.Right, LineStyle.Single, 2, Color.Black, true);
            table.SetBorder(BorderType.Bottom, LineStyle.Single, 2, Color.Black, true);
            return dc;
        }

        private Document BuildTable(ErchenParamsModel model)
        {
            var dc = new Aspose.Words.Document(GetSroce("Tunnel.Word.ErchenFiles.二衬参数表.doc"));
            var builder = new DocumentBuilder(dc);
            NodeCollection allTables = dc.GetChildNodes(NodeType.Table, true);
            Aspose.Words.Tables.Table table = allTables[0] as Aspose.Words.Tables.Table;
            if (table == null) return new Document();

            foreach (var sectionData in model.ErchenParamsDatas)
            {
                var row = CreateRow(new[]
                {
                    "",
                    "",
                    "",
                    "",
                    "",
                    ""
                }, dc);
                table.AppendChild(row);
            }
            int rowNum = 0;

            foreach (Row row in table.Rows)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (rowNum != 0)
                    {
                        string text = "";
                        if (j == 0)
                        {
                            text = model.ErchenParamsDatas[rowNum - 1].ParamsMileage;
                        }
                        if (j == 1)
                        {
                            text = model.ErchenParamsDatas[rowNum - 1].SurroundingRockLevel;
                        }
                        if (j == 2)
                        {
                            text = model.ErchenParamsDatas[rowNum - 1].ProtectType;
                        }
                        if (j == 3)
                        {
                            text = model.ErchenParamsDatas[rowNum - 1].ErchenThickness;
                        }
                        if (j == 4)
                        {
                            text = model.ErchenParamsDatas[rowNum - 1].ErchenSpacingOfSteelSupport;
                        }
                        if (j == 5)
                        {
                            text = model.ErchenParamsDatas[rowNum - 1].Remark;
                        }
                        builder.MoveToCell(0, rowNum, j, 0);
                        Aspose.Words.Font font = builder.Font;
                        font.Size = 10;
                        builder.ParagraphFormat.LineSpacing = 10;
                        builder.Write(text);
                    }
                    row.Cells[j].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Center;
                    row.Cells[j].CellFormat.VerticalAlignment = CellVerticalAlignment.Center;
                }
                rowNum++;
            }
            table.SetBorder(BorderType.Bottom, LineStyle.Single, 2, Color.Black, true);
            table.SetBorder(BorderType.Left, LineStyle.Single, 2, Color.Black, true);
            table.SetBorder(BorderType.Right, LineStyle.Single, 2, Color.Black, true);
            return dc;
        }

        private Document BuildTable(ChuzhiParamsModel model)
        {
            var dc = new Aspose.Words.Document(GetSroce("Tunnel.Word.ChuzhiJiance.初支检测参数表.doc"));
            var builder = new DocumentBuilder(dc);
            NodeCollection allTables = dc.GetChildNodes(NodeType.Table, true);
            Aspose.Words.Tables.Table table = allTables[0] as Aspose.Words.Tables.Table;
            if (table == null) return new Document();

            foreach (var sectionData in model.ChuzhiParamsDatas)
            {
                //var row = CreateRow(new[]
                //{
                //    sectionData.ParamsMileage,
                //    sectionData.SurroundingRockLevel,
                //    sectionData.ProtectType,
                //    sectionData.ShotcreteThickness,
                //    sectionData.SpacingOfSteelSupport,
                //    sectionData.Remark
                //}, dc);
                var row = CreateRow(new[]
                {
                    "",
                    "",
                    "",
                    "",
                    "",
                    ""
                }, dc);
                table.AppendChild(row);
            }
            int rowNum = 0;

            foreach (Row row in table.Rows)
            {
                for(int j = 0; j < 6; j++)
                {
                    if (rowNum != 0)
                    {
                        string text = "";
                        if (j == 0)
                        {
                            text = model.ChuzhiParamsDatas[rowNum - 1].ParamsMileage;
                        }
                        if (j == 1)
                        {
                            text = model.ChuzhiParamsDatas[rowNum - 1].SurroundingRockLevel;
                        }
                        if (j == 2)
                        {
                            text = model.ChuzhiParamsDatas[rowNum - 1].ProtectType;
                        }
                        if (j == 3)
                        {
                            text = model.ChuzhiParamsDatas[rowNum - 1].ShotcreteThickness;
                        }
                        if (j == 4)
                        {
                            text = model.ChuzhiParamsDatas[rowNum - 1].SpacingOfSteelSupport;
                        }
                        if (j == 5)
                        {
                            text = model.ChuzhiParamsDatas[rowNum - 1].Remark;
                        }
                        builder.MoveToCell(0, rowNum, j, 0);
                        Aspose.Words.Font font = builder.Font;
                        font.Size = 10;
                        builder.ParagraphFormat.LineSpacing = 10;
                        builder.Write(text);
                    }
                    row.Cells[j].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Center;
                    row.Cells[j].CellFormat.VerticalAlignment = CellVerticalAlignment.Center;
                }
                rowNum++;
            }
            table.SetBorder(BorderType.Bottom, LineStyle.Single, 2, Color.Black, true);
            table.SetBorder(BorderType.Left, LineStyle.Single, 2, Color.Black, true);
            table.SetBorder(BorderType.Right, LineStyle.Single, 2, Color.Black, true);
            return dc;
        }

        private Document BuildTable(ErchenDefectModel model)
        {
            var dc = new Aspose.Words.Document(GetSroce("Tunnel.Word.ErchenFiles.二衬缺陷表.doc"));
            var builder = new DocumentBuilder(dc);
            NodeCollection allTables = dc.GetChildNodes(NodeType.Table, true);
            Aspose.Words.Tables.Table table = allTables[0] as Aspose.Words.Tables.Table;
            if (table == null) return new Document();

            foreach (var sectionData in model.ErchenDefectDatas)
            {
                var row = CreateRow(new[]
                {
                    "",
                    "",
                    "",
                    "",
                    "",
                    ""
                }, dc);
                table.AppendChild(row);
            }
            int rowNum = 0;

            foreach (Row row in table.Rows)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (rowNum != 0)
                    {
                        string text = "";
                        if (j == 0)
                        {
                            text = model.ErchenDefectDatas[rowNum - 1].Index;
                        }
                        if (j == 1)
                        {
                            text = model.ErchenDefectDatas[rowNum - 1].Position;
                        }
                        if (j == 2)
                        {
                            text = model.ErchenDefectDatas[rowNum - 1].ParamsMileage;
                        }
                        if (j == 3)
                        {
                            text = model.ErchenDefectDatas[rowNum - 1].BadLength;
                        }
                        if (j == 4)
                        {
                            text = model.ErchenDefectDatas[rowNum - 1].BadType;
                        }
                        if (j == 5)
                        {
                            text = model.ErchenDefectDatas[rowNum - 1].Remark;
                        }
                        builder.MoveToCell(0, rowNum, j, 0);
                        Aspose.Words.Font font = builder.Font;
                        font.Size = 10;
                        builder.ParagraphFormat.LineSpacing = 10;
                        builder.Write(text);
                    }
                    row.Cells[j].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Center;
                    row.Cells[j].CellFormat.VerticalAlignment = CellVerticalAlignment.Center;
                }
                rowNum++;
            }
            table.SetBorder(BorderType.Bottom, LineStyle.Single, 2, Color.Black, true);
            table.SetBorder(BorderType.Left, LineStyle.Single, 2, Color.Black, true);
            table.SetBorder(BorderType.Right, LineStyle.Single, 2, Color.Black, true);
            return dc;
        }

        private Document BuildTable(ChuzhiQuexianModel model)
        {
            var dc = new Aspose.Words.Document(GetSroce("Tunnel.Word.ChuzhiJiance.初支缺陷检测表.doc"));
            var builder = new DocumentBuilder(dc);
            NodeCollection allTables = dc.GetChildNodes(NodeType.Table, true);
            Aspose.Words.Tables.Table table = allTables[0] as Aspose.Words.Tables.Table;
            if (table == null) return new Document();

            foreach (var sectionData in model.ChuzhiQuexianDatas)
            {
                var row = CreateRow(new[]
                {
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    ""
                }, dc);
                table.AppendChild(row);
            }
            int rowNum = 0;

            foreach (Row row in table.Rows)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (rowNum != 0)
                    {
                        string text = "";
                        if (j == 0)
                        {
                            text = model.ChuzhiQuexianDatas[rowNum - 1].Index;
                        }
                        if (j == 1)
                        {
                            text = model.ChuzhiQuexianDatas[rowNum - 1].Position;
                        }
                        if (j == 2)
                        {
                            text = model.ChuzhiQuexianDatas[rowNum - 1].ParamsMileage;
                        }
                        if (j == 3)
                        {
                            text = model.ChuzhiQuexianDatas[rowNum - 1].BadLength;
                        }
                        if (j == 4)
                        {
                            text = model.ChuzhiQuexianDatas[rowNum - 1].BadType;
                        }
                        if (j == 5)
                        {
                            text = model.ChuzhiQuexianDatas[rowNum - 1].BadDeepth;
                        }
                        if (j == 6)
                        {
                            text = model.ChuzhiQuexianDatas[rowNum - 1].Remark;
                        }
                        builder.MoveToCell(0, rowNum, j, 0);
                        Aspose.Words.Font font = builder.Font;
                        font.Size = 10;
                        builder.ParagraphFormat.LineSpacing = 10;
                        builder.Write(text);
                    }
                    row.Cells[j].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Center;
                    row.Cells[j].CellFormat.VerticalAlignment = CellVerticalAlignment.Center;
                }
                rowNum++;
            }
            table.SetBorder(BorderType.Bottom, LineStyle.Single, 2, Color.Black, true);
            table.SetBorder(BorderType.Left, LineStyle.Single, 2, Color.Black, true);
            table.SetBorder(BorderType.Right, LineStyle.Single, 2, Color.Black, true);
            return dc;
        }

        private Document BuildTable(ChuzhiGZCModel model)
        {
            var dc = new Aspose.Words.Document(GetSroce("Tunnel.Word.ChuzhiJiance.初支钢支撑检测表.doc"));
            var builder = new DocumentBuilder(dc);
            NodeCollection allTables = dc.GetChildNodes(NodeType.Table, true);
            Aspose.Words.Tables.Table table = allTables[0] as Aspose.Words.Tables.Table;
            if (table == null) return new Document();

            foreach (var sectionData in model.ChuzhiGZCDatas)
            {
                var row = CreateRow(new[]
                {
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    ""
                }, dc);
                table.AppendChild(row);
            }
            int rowNum = 0;

            foreach (Row row in table.Rows)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (rowNum != 0)
                    {
                        string text = "";
                        if (j == 0)
                        {
                            text = model.ChuzhiGZCDatas[rowNum - 1].Index;
                        }
                        if (j == 1)
                        {
                            text = model.ChuzhiGZCDatas[rowNum - 1].ParamsMileage;
                        }
                        if (j == 2)
                        {
                            text = model.ChuzhiGZCDatas[rowNum - 1].SurroundingRockLevel;
                        }
                        if (j == 3)
                        {
                            text = model.ChuzhiGZCDatas[rowNum - 1].ProtectType;
                        }
                        if (j == 4)
                        {
                            text = model.ChuzhiGZCDatas[rowNum - 1].DesginNums;
                        }
                        if (j == 5)
                        {
                            text = model.ChuzhiGZCDatas[rowNum - 1].FactNums;
                        }
                        if (j == 6)
                        {
                            text = model.ChuzhiGZCDatas[rowNum - 1].DesginSpacing;
                        }
                        if (j == 7)
                        {
                            text = model.ChuzhiGZCDatas[rowNum - 1].FactSpacing;
                        }
                        builder.MoveToCell(0, rowNum, j, 0);
                        Aspose.Words.Font font = builder.Font;
                        font.Size = 10;
                        builder.ParagraphFormat.LineSpacing = 10;
                        builder.Write(text);
                    }
                    row.Cells[j].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Center;
                    row.Cells[j].CellFormat.VerticalAlignment = CellVerticalAlignment.Center;
                }
                rowNum++;
            }
            table.SetBorder(BorderType.Bottom, LineStyle.Single, 2, Color.Black, true);
            table.SetBorder(BorderType.Left, LineStyle.Single, 2, Color.Black, true);
            table.SetBorder(BorderType.Right, LineStyle.Single, 2, Color.Black, true);
            return dc;
        }

        private Document BuildTable(ErchenSpaceModel model)
        {
            var dc = new Aspose.Words.Document(GetSroce("Tunnel.Word.ErchenFiles.二衬间距表.doc"));
            var builder = new DocumentBuilder(dc);
            NodeCollection allTables = dc.GetChildNodes(NodeType.Table, true);
            Aspose.Words.Tables.Table table = allTables[0] as Aspose.Words.Tables.Table;
            if (table == null) return new Document();

            foreach (var sectionData in model.ErchenSpaceDatas)
            {
                var row = CreateRow(new[]
                {
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    ""
                }, dc);
                table.AppendChild(row);
            }
            int rowNum = 0;

            foreach (Row row in table.Rows)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (rowNum != 0)
                    {
                        string text = "";
                        if (j == 0)
                        {
                            text = model.ErchenSpaceDatas[rowNum - 1].Index;
                        }
                        if (j == 1)
                        {
                            text = model.ErchenSpaceDatas[rowNum - 1].ParamsMileage;
                        }
                        if (j == 2)
                        {
                            text = model.ErchenSpaceDatas[rowNum - 1].ProtectType;
                        }
                        if (j == 3)
                        {
                            text = model.ErchenSpaceDatas[rowNum - 1].DesginNums;
                        }
                        if (j == 4)
                        {
                            text = model.ErchenSpaceDatas[rowNum - 1].FactMax;
                        }
                        if (j == 5)
                        {
                            text = model.ErchenSpaceDatas[rowNum - 1].FactMin;
                        }
                        if (j == 6)
                        {
                            text = model.ErchenSpaceDatas[rowNum - 1].FactAve;
                        }
                        if (j == 7)
                        {
                            text = model.ErchenSpaceDatas[rowNum - 1].Remark;
                        }
                        builder.MoveToCell(0, rowNum, j, 0);
                        Aspose.Words.Font font = builder.Font;
                        font.Size = 10;
                        builder.ParagraphFormat.LineSpacing = 10;
                        builder.Write(text);
                    }
                    row.Cells[j].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Center;
                    row.Cells[j].CellFormat.VerticalAlignment = CellVerticalAlignment.Center;
                }
                rowNum++;
            }
            table.SetBorder(BorderType.Bottom, LineStyle.Single, 2, Color.Black, true);
            table.SetBorder(BorderType.Left, LineStyle.Single, 2, Color.Black, true);
            table.SetBorder(BorderType.Right, LineStyle.Single, 2, Color.Black, true);
            return dc;
        }

        private Document BuildTable(ScheduleDataModel model)
        {
            var dc = new Aspose.Words.Document(GetSroce("Tunnel.Word.TunnelMonitorFiles.进度表模型表.docx"));
            var builder = new DocumentBuilder(dc);
            WordManage.SetModel(model, dc, builder);

            NodeCollection allTables = dc.GetChildNodes(NodeType.Table, true);
            Aspose.Words.Tables.Table table = allTables[0] as Aspose.Words.Tables.Table;
            if (table==null)return new Document();
            int j = 1;
            List<RowMergeData> dic = new List<RowMergeData>();
            foreach (var dataTypeModel in model.DataTypeModels)
            {
                if (dataTypeModel.DataList != null)
                {
                    for (int i = 0; i < dataTypeModel.DataList.Count; i++)
                    {
                        
                        SetRowData(dc, table.Rows[j], new[]
                        {
                            i == 0?dataTypeModel.Name:"",
                            dataTypeModel.DataList[i].ConstructionProcess,
                            dataTypeModel.DataList[i].StartDate,
                            dataTypeModel.DataList[i].EndDate,
                            dataTypeModel.DataList[i].StageFootage,
                            dataTypeModel.DataList[i].AccumulativeFootage,
                            i == 0?dataTypeModel.Remark:""
                        });


                        if (dataTypeModel.DataList.Count>1&&i == dataTypeModel.DataList.Count - 1)
                        {
                            dic.Add(new RowMergeData{RowJ = j,RowCount = dataTypeModel.DataList.Count});
                        }

                        j++;
                    }
                }
            }
            for (int i = dic.Count-1; i >= 0; i--)
            {
                Cell cellStartRange = table.Rows[dic[i].RowJ - dic[i].RowCount+1].Cells[0];
                Cell cellEndRange = table.Rows[dic[i].RowJ].Cells[0];
                MergeCells(cellStartRange, cellEndRange);

                Cell cellStartRange1 = table.Rows[dic[i].RowJ - dic[i].RowCount + 1].Cells[6];
                Cell cellEndRange1 = table.Rows[dic[i].RowJ].Cells[6];
                MergeCells(cellStartRange1, cellEndRange1);
            }

            while (table.Rows.Count!=j)
            {
                table.Rows.Remove(table.LastRow);
            }
            foreach (Row row in table.Rows)
            {
                row.Cells[0].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Center;
                row.Cells[1].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Center;
                row.Cells[2].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Center;
                row.Cells[3].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Center;
                row.Cells[4].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Center;
                row.Cells[5].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Center;
            }
            table.SetBorder(BorderType.Bottom,LineStyle.Single,1.5,Color.Black, true);
            return dc;
        }

        private Document BuildTable(BuriedSectionModel model)
        {
            var dc = new Aspose.Words.Document(GetSroce("Tunnel.Word.TunnelMonitorFiles.埋设断面表.docx"));
            var builder = new DocumentBuilder(dc);
            NodeCollection allTables = dc.GetChildNodes(NodeType.Table, true);
            Aspose.Words.Tables.Table table = allTables[0] as Aspose.Words.Tables.Table;
            if (table == null) return new Document();

            foreach (var sectionData in model.BuriedSectionDatas)
            {
                var row = CreateRow(new[]
                {
                    sectionData.SerialNumber,
                    sectionData.SectionMileage,
                    //sectionData.SurroundingRockLevel,
                    sectionData.CrownSettlement,
                    sectionData.DisplacementAcc,
                    //sectionData.BurialTime,
                    sectionData.Remark
                }, dc);
                table.AppendChild(row);
            }
            foreach (Row row in table.Rows)
            {
                row.Cells[0].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Center;
                row.Cells[1].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Center;
                row.Cells[2].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Center;
                row.Cells[3].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Center;
                row.Cells[4].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Center;
            }
            table.SetBorder(BorderType.Bottom, LineStyle.Single, 1.5, Color.Black, true);
            table.SetBorder(BorderType.Left, LineStyle.Single, 1.5, Color.Black, true);
            table.SetBorder(BorderType.Right, LineStyle.Single, 1.5, Color.Black, true);
            return dc;
        }

        private Document BuildTable(BurialSituationModel model)
        {
            var dc = new Aspose.Words.Document(GetSroce("Tunnel.Word.TunnelMonitorFiles.埋设情况表.docx"));
            var builder = new DocumentBuilder(dc);
            NodeCollection allTables = dc.GetChildNodes(NodeType.Table, true);
            Aspose.Words.Tables.Table table = allTables[0] as Aspose.Words.Tables.Table;
            if (table == null) return new Document();

            foreach (var sectionData in model.SituationDataModels)
            {
                var row = CreateRow(new[]
                {
                    sectionData.Name,
                    sectionData.NumberSections,
                    sectionData.BuriedMileage,
                    sectionData.Remark
                }, dc);
                table.AppendChild(row);
            }
            foreach (Row row in table.Rows)
            {
                row.Cells[0].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Center;
                row.Cells[1].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Center;
                row.Cells[2].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Center;
                row.Cells[3].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Center;
            }
            table.SetBorder(BorderType.Bottom, LineStyle.Single, 1.5, Color.Black, true);
            table.SetBorder(BorderType.Left, LineStyle.Single, 1.5, Color.Black, true);
            table.SetBorder(BorderType.Right, LineStyle.Single, 1.5, Color.Black, true);
            return dc;
        }
        private Document BuildTable(DataAnalysisModel model)
        {
            var dc = new Aspose.Words.Document(GetSroce("Tunnel.Word.TunnelMonitorFiles.数据分析表.docx"));
            var builder = new DocumentBuilder(dc);
            WordManage.SetModel(model, dc, builder);

            NodeCollection allTables = dc.GetChildNodes(NodeType.Table, true);
            Aspose.Words.Tables.Table table = allTables[0] as Aspose.Words.Tables.Table;
            if (table == null) return new Document();
            int j = 1;
            List<RowMergeData> dic = new List<RowMergeData>();
            foreach (var dataTypeModel in model.AnalysisTypeModels)
            {
                if (dataTypeModel.DataList != null)
                {
                    for (int i = 0; i < dataTypeModel.DataList.Count; i++)
                    {

                        SetRowData(dc, table.Rows[j], new[]
                        {
                            i == 0?dataTypeModel.PileNumber:"",
                            dataTypeModel.DataList[i].MeasuringPoint,
                            dataTypeModel.DataList[i].MonitoringDays,
                            dataTypeModel.DataList[i].CumulativeValue,
                            dataTypeModel.DataList[i].AverageVelocity,
                            i == 0?dataTypeModel.DataAnalysis:""
                        });

                        table.Rows[j].RowFormat.Height = 25;
                        if (dataTypeModel.DataList.Count > 1 && i == dataTypeModel.DataList.Count - 1)
                        {
                            dic.Add(new RowMergeData { RowJ = j, RowCount = dataTypeModel.DataList.Count });
                        }

                        j++;
                    }
                }
            }
            for (int i = dic.Count - 1; i >= 0; i--)
            {
                Cell cellStartRange = table.Rows[dic[i].RowJ - dic[i].RowCount + 1].Cells[0];
                Cell cellEndRange = table.Rows[dic[i].RowJ].Cells[0];
                MergeCells(cellStartRange, cellEndRange);

                Cell cellStartRange1 = table.Rows[dic[i].RowJ - dic[i].RowCount + 1].Cells[5];
                Cell cellEndRange1 = table.Rows[dic[i].RowJ].Cells[5];
                MergeCells(cellStartRange1, cellEndRange1);
            }

            while (table.Rows.Count != j)
            {
                table.Rows.Remove(table.LastRow);
            }
            foreach (Row row in table.Rows)
            {
                row.Cells[0].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Center;
                row.Cells[1].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Center;
                row.Cells[2].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Center;
                row.Cells[3].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Center;
                row.Cells[4].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Center;
            }
            table.SetBorder(BorderType.Bottom, LineStyle.Single, 1.5, Color.Black, true);
            return dc;
        }

        private Document BuildTable(EquipModel model)
        {
            var dc = new Aspose.Words.Document(GetSroce("Tunnel.Word.TunnelMonitorFiles.仪器设备统计表.docx"));
            NodeCollection allTables = dc.GetChildNodes(NodeType.Table, true);
            Aspose.Words.Tables.Table table = allTables[0] as Aspose.Words.Tables.Table;
            if (table == null) return new Document();
            foreach (var dataTypeModel in model.EquipDatas)
            {
                var row = CreateRow(new[]
                {
                    dataTypeModel.Name,
                    dataTypeModel.EquipModel,
                    dataTypeModel.Vendor,
                    dataTypeModel.Accuracy,
                    dataTypeModel.Num,
                    dataTypeModel.Remark
                }, dc);
                table.AppendChild(row);
            }
            foreach (Row row in table.Rows)
            {
                row.Cells[0].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Center;
                row.Cells[1].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Center;
                row.Cells[2].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Center;
                row.Cells[3].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Center;
                row.Cells[4].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Center;
                row.Cells[5].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Center;
            }
            table.SetBorder(BorderType.Bottom, LineStyle.Single, 1.5, Color.Black, true);
            table.SetBorder(BorderType.Left, LineStyle.Single, 1.5, Color.Black, true);
            table.SetBorder(BorderType.Right, LineStyle.Single, 1.5, Color.Black, true);
            return dc;
        }
    }

    public class RowMergeData
    {
        public int RowJ { get; set; }
        public int RowCount { get; set; }
    }
}
