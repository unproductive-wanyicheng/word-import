using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;
using HtTool.Service.Contracts;

namespace HtTool.Service.Monitor
{
    public static class DataConvertService
    {
        public static List<SectionLine> ToList(List<Line> lines, Guid sId, string sectionName, string sectionType)
        {
            var sectionLineList = new List<SectionLine>();
            if (lines != null)
            {
                foreach (var line in lines)
                {
                    var dt = line.dataTable;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        var li = new SectionLine
                        {
                            Id = Guid.NewGuid(),
                            LineName = line.name,
                            SectionId = sId,
                            SectionName = sectionName,
                            SectionType = sectionType,
                            CreateTime = DateTime.Now
                        };
                        if (dt.Rows[i][0] != null && !string.IsNullOrEmpty(dt.Rows[i][0].ToString()))
                        {
                            li.RecordTime = DateTime.Parse(dt.Rows[i][0].ToString());
                        }
                        li.Result1 = dt.Rows[i][1] != null && !string.IsNullOrEmpty(dt.Rows[i][1].ToString()) ? double.Parse(dt.Rows[i][1].ToString()) : 0;
                        li.Result2 = dt.Rows[i][2] != null && !string.IsNullOrEmpty(dt.Rows[i][2].ToString()) ? double.Parse(dt.Rows[i][2].ToString()) : 0;
                        li.Result3 = dt.Rows[i][3] != null && !string.IsNullOrEmpty(dt.Rows[i][3].ToString()) ? double.Parse(dt.Rows[i][3].ToString()) : 0;
                        li.AvgResult = dt.Rows[i][4] != null && !string.IsNullOrEmpty(dt.Rows[i][4].ToString()) ? double.Parse(dt.Rows[i][4].ToString()) : 0;
                        li.EquipResult = dt.Rows[i][5] != null && !string.IsNullOrEmpty(dt.Rows[i][5].ToString()) ? double.Parse(dt.Rows[i][5].ToString()) : 0;
                        li.Temperature = dt.Rows[i][6] != null && !string.IsNullOrEmpty(dt.Rows[i][6].ToString()) ? double.Parse(dt.Rows[i][6].ToString()) : 0;
                        li.TempResult = dt.Rows[i][7] != null && !string.IsNullOrEmpty(dt.Rows[i][7].ToString()) ? double.Parse(dt.Rows[i][7].ToString()) : 0;
                        li.LastDiffResult = dt.Rows[i][8] != null && !string.IsNullOrEmpty(dt.Rows[i][8].ToString()) ? double.Parse(dt.Rows[i][8].ToString()) : 0;
                        li.DisSpeed = dt.Rows[i][9] != null && !string.IsNullOrEmpty(dt.Rows[i][9].ToString()) ? double.Parse(dt.Rows[i][9].ToString()) : 0;
                        li.CumSpeed = dt.Rows[i][10] != null && !string.IsNullOrEmpty(dt.Rows[i][10].ToString()) ? double.Parse(dt.Rows[i][10].ToString()) : 0;
                        sectionLineList.Add(li);
                    }
                }
            }
            return sectionLineList;
        }

        public static List<ReportSectionInfo> ToReportSectionInfo(List<Section> sections, Guid projectId, Guid tunnelId, string lrDesc, string tunnelDesc, string sectionType)
        {
            var reportSectionInfoList = new List<ReportSectionInfo>();
            if (sections != null)
            {
                foreach (var section in sections)
                {
                    var rse = new ReportSectionInfo
                    {
                        Id = Guid.NewGuid(),
                        SectionName = section.name,
                        ProjectId = projectId,
                        TunnelId = tunnelId,
                        LrDesc = lrDesc,
                        TunnelDesc = tunnelDesc,
                        GStrEquipConst = double.Parse(section.gStrEquipConst),
                        GStrU0 = double.Parse(section.gStrU0),
                        MaxDefSpeed = 0.2,
                        MaxDefAcc = 0,
                        LastTime = DateTime.Now,
                        Status = 1,
                        AnResult = ""
                    };
                    rse.Lines = ToList(section.lines, rse.Id, rse.SectionName, sectionType);
                    reportSectionInfoList.Add(rse);
                }
            }

            return reportSectionInfoList;
        }
    }
}
