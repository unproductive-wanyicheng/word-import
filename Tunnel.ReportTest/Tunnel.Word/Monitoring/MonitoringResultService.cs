using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aspose.Words;
using Tunnel.Word.Table;

namespace Tunnel.Word.Monitoring
{
    public class MonitoringResultService : TableBase
    {
        public Document BuildMustMonitoring(MustMonitoringResultModel model)
        {
            var dc = new Aspose.Words.Document(GetSroce("Tunnel.Word.TunnelMonthFiles.项目监测标题.docx"));
            var builder = new DocumentBuilder(dc);
            var bm = dc.Range.Bookmarks["ItemName"];
            if (bm != null)
            {
                builder.MoveToBookmark("ItemName");
                bm.Text = model.ResultName;
            }
            builder.MoveToDocumentEnd();
            if (!string.IsNullOrEmpty(model.Describe))
            {
                builder.Writeln("    "+model.Describe);
            }
            if (model.ResultDataList != null && model.ResultDataList.Count > 0)
            {
                WordManage.BuildData(dc, builder, "", model.ResultDataList);
            }
            if (!string.IsNullOrEmpty(model.Summary))
            {
                builder.Writeln("    " + model.Summary);
            }
            return dc;
        }

        public Document BuildSelectionMonitoring(SelectionMonitoringResultModel model)
        {
            var dc = new Aspose.Words.Document(GetSroce("Tunnel.Word.TunnelMonthFiles.项目监测标题.docx"));
            var builder = new DocumentBuilder(dc);
            var bm = dc.Range.Bookmarks["ItemName"];
            if (bm != null)
            {
                builder.MoveToBookmark("ItemName");
                bm.Text = model.ItemName;
            }
            builder.MoveToDocumentEnd();
            foreach (var resultModel in model.ResultListModel)
            {
                if (!string.IsNullOrEmpty(resultModel.ResultName))
                {
                    builder.Writeln(resultModel.ResultName);
                }
                if (!string.IsNullOrEmpty(resultModel.Describe))
                {
                    builder.Writeln("    " + resultModel.Describe);
                }
                if (resultModel.ResultDataList != null && resultModel.ResultDataList.Count > 0)
                {
                    WordManage.BuildData(dc, builder, "", resultModel.ResultDataList);
                }
                if (!string.IsNullOrEmpty(resultModel.Summary))
                {
                    builder.Writeln("    " + resultModel.Summary);
                }
            }
            return dc;
        }
    }
}
