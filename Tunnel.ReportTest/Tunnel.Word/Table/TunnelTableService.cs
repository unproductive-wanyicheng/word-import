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
            return new Document();
        }

        private Document BuildTable(ChuzhiThicknessModel model)
        {
            var dc = new Aspose.Words.Document(GetSroce("Tunnel.Word.ChuzhiJiance.初支检测厚度表.docx"));
            var builder = new DocumentBuilder(dc);
            WordManage.SetModel(model, dc, builder);

            NodeCollection allTables = dc.GetChildNodes(NodeType.Table, true);
            Aspose.Words.Tables.Table table = allTables[0] as Aspose.Words.Tables.Table;
            if (table == null) return new Document();
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

                Cell cellStartRange1 = table.Rows[dic[i].RowJ - dic[i].RowCount + 1].Cells[6];
                Cell cellEndRange1 = table.Rows[dic[i].RowJ].Cells[6];
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
                row.Cells[5].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Center;
            }
            table.SetBorder(BorderType.Bottom, LineStyle.Single, 1.5, Color.Black, true);
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
