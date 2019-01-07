using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Aspose.Words;
using Aspose.Words.Drawing;
using Aspose.Words.Fields;
using Aspose.Words.Tables;
using Tunnel.Word.Chart;
using Tunnel.Word.Monitoring;
using Tunnel.Word.Table;

namespace Tunnel.Word
{
    public class WordManage
    {
        protected int Docversion = 2007;
        protected DocumentBuilder oWordApplic;
        protected Document oDoc;

        public WordManage(int version = 2007)
        {
            this.Docversion = version;
        }

        public void Open(string fileName = "")
        {
            if(string.IsNullOrEmpty(fileName))
            {
                Assembly assm = Assembly.GetExecutingAssembly();
                Stream istr = assm.GetManifestResourceStream("Tunnel.Word.temp.docx");
                oDoc = new Document(istr);
            }
            else
            {

                oDoc = new Document(fileName);
            }
            oWordApplic = new DocumentBuilder(oDoc);
        }
        public void Open(Stream stream)
        {
            oDoc = new Document(stream);
            oWordApplic = new DocumentBuilder(oDoc);
        }

        public void SaveAs(string strFileName)
        {
            oDoc.Save(strFileName, this.Docversion == 2007 ? SaveFormat.Docx : SaveFormat.Doc);
        }

        public void Writeln()
        {
            oWordApplic.Underline = Underline.None;
            oWordApplic.Writeln();
        }
        public void Writeln(int nline)
        {
            oWordApplic.Underline = Underline.None;
            for (int i = 0; i < nline; i++)
                oWordApplic.Writeln();
        }
        public void Writeln(string strText, double conSize = 12, bool nBold = false,
            ParagraphAlignment pa = ParagraphAlignment.Left, bool underline = false)
        {
            oWordApplic.Bold = nBold;
            oWordApplic.Font.Size = conSize;
            oWordApplic.ParagraphFormat.Alignment = pa;
            
            oWordApplic.Underline = underline ? Underline.Single : Underline.None;
            oWordApplic.Writeln(strText);
            oWordApplic.ParagraphFormat.Alignment = ParagraphAlignment.Left;
            oWordApplic.Underline = Underline.None;
        }

        public void Write(string strText, double conSize = 12, bool nBold = false,
            ParagraphAlignment pa = ParagraphAlignment.Left,bool underline = false)
        {
            oWordApplic.Bold = nBold;
            oWordApplic.Font.Size = conSize;
            oWordApplic.ParagraphFormat.Alignment = pa;
            oWordApplic.Underline = underline ? Underline.Single : Underline.None;
            oWordApplic.Write(strText);
        }

        #region 设置纸张

        public void SetPaperSize(string papersize = "A4")
        {

            switch (papersize)
            {
                case "A4":
                    foreach (Aspose.Words.Section section in oDoc)
                    {
                        section.PageSetup.PaperSize = PaperSize.A4;
                        section.PageSetup.Orientation = Orientation.Portrait;
                        section.PageSetup.VerticalAlignment = Aspose.Words.PageVerticalAlignment.Top;
                    }
                    break;
                case "A4H": //A4横向 
                    foreach (Aspose.Words.Section section in oDoc)
                    {
                        section.PageSetup.PaperSize = PaperSize.A4;
                        section.PageSetup.Orientation = Orientation.Landscape;
                        section.PageSetup.TextColumns.SetCount(2);
                        section.PageSetup.TextColumns.EvenlySpaced = true;
                        section.PageSetup.TextColumns.LineBetween = true;
                        //section.PageSetup.LeftMargin = double.Parse("3.35"); 
                        //section.PageSetup.RightMargin =double.Parse("0.99"); 
                    }
                    break;
                case "A3":
                    foreach (Aspose.Words.Section section in oDoc)
                    {
                        section.PageSetup.PaperSize = PaperSize.A3;
                        section.PageSetup.Orientation = Orientation.Portrait;

                    }

                    break;
                case "A3H": //A3横向 

                    foreach (Aspose.Words.Section section in oDoc)
                    {
                        section.PageSetup.PaperSize = PaperSize.A3;
                        section.PageSetup.Orientation = Orientation.Landscape;
                        section.PageSetup.TextColumns.SetCount(2);
                        section.PageSetup.TextColumns.EvenlySpaced = true;
                        section.PageSetup.TextColumns.LineBetween = true;

                    }

                    break;

                case "16K":

                    foreach (Aspose.Words.Section section in oDoc)
                    {
                        section.PageSetup.PaperSize = PaperSize.B5;
                        section.PageSetup.Orientation = Orientation.Portrait;

                    }

                    break;

                case "8KH":

                    foreach (Aspose.Words.Section section in oDoc)
                    {

                        section.PageSetup.PageWidth = double.Parse("36.4 "); //纸张宽度 
                        section.PageSetup.PageHeight = double.Parse("25.7"); //纸张高度 
                        section.PageSetup.Orientation = Orientation.Landscape;
                        section.PageSetup.TextColumns.SetCount(2);
                        section.PageSetup.TextColumns.EvenlySpaced = true;
                        section.PageSetup.TextColumns.LineBetween = true;
                        //section.PageSetup.LeftMargin = double.Parse("3.35"); 
                        //section.PageSetup.RightMargin = double.Parse("0.99"); 
                    }



                    break;
            }
        }

        #endregion

        /// <summary>
        /// 设置页码开始
        /// </summary>
        public void SetPageHeaderFooter(int total=0,double conSize = 12, bool nBold = false,
            ParagraphAlignment pa = ParagraphAlignment.Center)
        {

            oWordApplic.MoveToHeaderFooter(HeaderFooterType.FooterPrimary);
            oWordApplic.Bold = nBold;
            oWordApplic.Font.Size = conSize;
            oWordApplic.ParagraphFormat.Alignment = pa;
            oWordApplic.Write("第");
            oWordApplic.InsertField(FieldType.FieldPage,true);

            oWordApplic.Write("页 共");
            if (total==0)
            oWordApplic.InsertField(FieldType.FieldNumPages, true);
            else
            {
                oWordApplic.Write(total.ToString());
            }
            oWordApplic.Write("页");

            oWordApplic.MoveToDocumentEnd();
        }

        public void SetPageHeaderHeader(string headerstr,double conSize = 12,bool nBold = false, ParagraphAlignment pa = ParagraphAlignment.Center, int header = 30,bool underline=true)
        {
            if (string.IsNullOrEmpty(headerstr)) return;
            oWordApplic.MoveToHeaderFooter(HeaderFooterType.HeaderPrimary);

            oWordApplic.Bold = nBold;
            oWordApplic.Font.Size = conSize;
            oWordApplic.ParagraphFormat.Alignment = pa;
            oWordApplic.CurrentSection.PageSetup.HeaderDistance = header;

            if (underline)
            {
                var table = oWordApplic.StartTable();
                //添加Word表格 
                oWordApplic.InsertCell();
                oWordApplic.CellFormat.Borders.LineStyle = LineStyle.None;
                oWordApplic.CellFormat.Borders.Bottom.LineStyle = LineStyle.Single;

                oWordApplic.Write(headerstr);
                oWordApplic.EndTable();

                table.TextWrapping = TextWrapping.Around;
            }
            else
            {
                oWordApplic.Write(headerstr);


            }
            oWordApplic.MoveToDocumentEnd(); 

        }

        public void NewPage()
        {
            oWordApplic.InsertBreak(BreakType.PageBreak);
        }
        /// <summary> 
        /// 换行 
        /// </summary> 
        public void InsertLineBreak()
        {
            oWordApplic.InsertBreak(BreakType.LineBreak);
        }
        /// <summary> 
        /// 换多行 
        /// </summary> 
        /// <param name="nline"></param> 
        public void InsertLineBreak(int nline)
        {
            for (int i = 0; i < nline; i++)
                oWordApplic.InsertBreak(BreakType.LineBreak);
        }
        /// <summary>
        /// 重新开始页码
        /// </summary>
        public Document Document
        {
            get { return oDoc; }
        }

        public DocumentBuilder Builder
        {
            get { return oWordApplic; }
        }

        public void WriteFixedWidth(string text, float width, bool underline=true)
        {
            if(underline)
            oWordApplic.Underline = Underline.Single;

            float w = GetStrWidth(text, oWordApplic.Font);
            if (w >= width) oWordApplic.Writeln(text);

            float spacew = GetStrWidth(" ", oWordApplic.Font);
            float sw = ((width - w) / spacew) / 2;
            for (int i = 0; i <= sw; i++)
            {
                if (1 < sw - i && sw - i < 0.5)
                {
                    text = " " + text;
                }
                else
                {

                    text = " " + text + " ";
                }
            }

            oWordApplic.Writeln(text);
        }


        public float GetStrWidth(string text, Aspose.Words.Font font)
        {
            Graphics g = Graphics.FromImage(new Bitmap(1, 1));
            return g.MeasureString(text, new System.Drawing.Font(font.Name, (float)font.Size)).Width;
        }

        public void InsertFile(string vfilename)
        {
            Aspose.Words.Document srcDoc = new Aspose.Words.Document(vfilename);
            Node insertAfterNode = oWordApplic.CurrentParagraph.PreviousSibling;
            InsertDocument(insertAfterNode, oDoc, srcDoc);

        }

        public void AppendFile(string vfilename)
        {
            oDoc.AppendDocument(new Aspose.Words.Document(vfilename), ImportFormatMode.KeepSourceFormatting);
        }

        public void SetModel<T>(T model)
        {
            WordManage.SetModel<T>(model, oDoc, oWordApplic);
        }
        public static void InsertDocument(Node insertAfterNode, Aspose.Words.Document srcDoc)
        {
            // Make sure that the node is either a paragraph or table. 
            if ((!insertAfterNode.NodeType.Equals(NodeType.Paragraph)) &
                (!insertAfterNode.NodeType.Equals(NodeType.Table)))
                throw new ArgumentException("The destination node should be either a paragraph or table.");

            // We will be inserting into the parent of the destination paragraph. 
            CompositeNode dstStory = insertAfterNode.ParentNode;

            // This object will be translating styles and lists during the import. 
            NodeImporter importer = new NodeImporter(srcDoc, insertAfterNode.Document, ImportFormatMode.KeepSourceFormatting);

            // Loop through all sections in the source document. 
            foreach (Aspose.Words.Section srcSection in srcDoc.Sections)
            {
                // Loop through all block level nodes (paragraphs and tables) in the body of the section. 
                foreach (Node srcNode in srcSection.Body)
                {
                    // Let's skip the node if it is a last empty paragraph in a section. 
                    if (srcNode.NodeType.Equals(NodeType.Paragraph))
                    {
                        Aspose.Words.Paragraph para = (Aspose.Words.Paragraph)srcNode;
                        if (para.IsEndOfSection && !para.HasChildNodes)
                            continue;
                    }

                    // This creates a clone of the node, suitable for insertion into the destination document. 
                    Node newNode = importer.ImportNode(srcNode, true);

                    // Insert new node after the reference node. 
                    dstStory.InsertAfter(newNode, insertAfterNode);
                    insertAfterNode = newNode;
                }
            }
        }
        /// <summary> 
        /// 插入word内容 
        /// </summary> 
        /// <param name="insertAfterNode"></param> 
        /// <param name="mainDoc"></param> 
        /// <param name="srcDoc"></param> 
        public static void InsertDocument(Node insertAfterNode, Aspose.Words.Document mainDoc, Aspose.Words.Document srcDoc)
        {
            // Make sure that the node is either a pargraph or table. 
            if ((insertAfterNode.NodeType != NodeType.Paragraph)
                & (insertAfterNode.NodeType != NodeType.Table))
                throw new Exception("The destination node should be either a paragraph or table.");

            //We will be inserting into the parent of the destination paragraph. 

            CompositeNode dstStory = insertAfterNode.ParentNode;

            //Remove empty paragraphs from the end of document 

            while (null != srcDoc.LastSection.Body.LastParagraph && !srcDoc.LastSection.Body.LastParagraph.HasChildNodes)
            {
                srcDoc.LastSection.Body.LastParagraph.Remove();
            }
            NodeImporter importer = new NodeImporter(srcDoc, mainDoc, ImportFormatMode.KeepSourceFormatting);

            //Loop through all sections in the source document. 

            int sectCount = srcDoc.Sections.Count;

            for (int sectIndex = 0; sectIndex < sectCount; sectIndex++)
            {
                Aspose.Words.Section srcSection = srcDoc.Sections[sectIndex];
                //Loop through all block level nodes (paragraphs and tables) in the body of the section. 
                int nodeCount = srcSection.Body.ChildNodes.Count;
                for (int nodeIndex = 0; nodeIndex < nodeCount; nodeIndex++)
                {
                    Node srcNode = srcSection.Body.ChildNodes[nodeIndex];
                    Node newNode = importer.ImportNode(srcNode, true);
                    dstStory.InsertAfter(newNode, insertAfterNode);
                    insertAfterNode = newNode;
                }
            }


        }

        public static void BookMarkReplace(Document wordDoc, DocumentBuilder builder, string bookMark,string value)
        {
            var bm = wordDoc.Range.Bookmarks[bookMark];
            if (bm == null)
            {
                return;
            }
            builder.MoveToBookmark(bookMark);
            bm.Text = value;  
        }


        public static void SetModel<T>(T model,Document wordDoc, DocumentBuilder builder)
        {
            foreach (var sp in typeof(T).GetProperties())
            {
                var attribute = sp.GetCustomAttributes(typeof(WordMarkAttribute), false).FirstOrDefault();
                if (attribute != null)
                {
                    var enumValueAttr = (WordMarkAttribute)attribute;
                    var marknames = enumValueAttr.MarkName.Split(',');

                    var value = sp.GetValue(model, null);
                    switch (enumValueAttr.Alignment)
                    {
                        case MarkAlignment.Header:
                            builder.MoveToHeaderFooter(HeaderFooterType.HeaderPrimary);
                            break;
                        case MarkAlignment.Footer:
                            builder.MoveToHeaderFooter(HeaderFooterType.FooterPrimary);
                            break;
                        default:
                            builder.MoveToDocumentEnd();
                            break;
                    }

                    if (enumValueAttr.MarkType==MarkType.String)
                    {
                        foreach (var markname in marknames)
                        {
                            BookMarkReplace(wordDoc, builder, markname, value==null?"":value.ToString());
                        }
                    }
                    if (enumValueAttr.MarkType == MarkType.Image)
                    {
                        if (value == null)
                        {
                            foreach (var markname in marknames)
                            {
                                BookMarkReplace(wordDoc, builder, markname, "");
                            }

                        }
                        else
                        {
                            Shape shape=null;
                            


                            foreach (var markname in marknames)
                            {
                                var bm = wordDoc.Range.Bookmarks[markname];
                                if (bm == null)
                                {
                                    continue;
                                }
                                builder.MoveToBookmark(markname);
                                bm.Text = "";
                                if (value is string)
                                {
                                    shape =builder.InsertImage(value.ToString());
                                }
                                else if (value is Stream)
                                {
                                    shape = builder.InsertImage(value as Stream);
                                }
                                else if (value is Image)
                                {
                                    shape = builder.InsertImage(value as Image);
                                }
                                else if (value is byte[])
                                {
                                    shape = builder.InsertImage((byte[])value);
                                }

                                if (shape != null)
                                {
                                    if (enumValueAttr.Width != 0)
                                        shape.Width = enumValueAttr.Width;
                                    if (enumValueAttr.Height != 0)
                                        shape.Height = enumValueAttr.Height;
                                }
                            }
                        }
                        
                        
                    }

                    if (enumValueAttr.MarkType == MarkType.BuildData)
                    {
                        foreach (var markname in marknames)
                        {
                            BuildData(wordDoc, builder, markname, value as List<IListModel>);
                        }
                    }
                }
            }
        }

        public static void BuildData(Document wordDoc, DocumentBuilder builder, string bookMark, List<IListModel> listModels)
        {
            if(listModels==null||listModels.Count==0)return;
            if (!string.IsNullOrEmpty(bookMark))
            {
                var bm = wordDoc.Range.Bookmarks[bookMark];
                if (bm != null)
                {
                    builder.MoveToBookmark(bookMark);
                    bm.Text = "";
                }
            }
            for (int i = 0; i < listModels.Count; i++)
            {
                if (listModels[i] is ITunnelTable)
                {
                    BuildTable(wordDoc,builder,listModels[i] as ITunnelTable);
                }
                if (listModels[i] is ITunnelChart)
                {
                    BuildChart(wordDoc, builder,  listModels[i] as ITunnelChart);
                }
                if (listModels[i] is IMonitoringResult)
                {
                    BuildMonitoring(wordDoc, builder,  listModels[i] as IMonitoringResult);
                }
            }
        }

        public static void BuildTable(Document wordDoc, DocumentBuilder builder, ITunnelTable tableData)
        {
            var doc = new TunnelTableService().BuildTable(tableData);
            builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;
            //Aspose.Words.Font font = builder.Font;
            //font.Size = 8;
            builder.Writeln(((TableDataModel) tableData).TableName);
            builder.Bold = true;
            builder.ParagraphFormat.Alignment = ParagraphAlignment.Left;
            builder.Bold = false;
            
            builder.InsertDocument(doc, ImportFormatMode.KeepSourceFormatting);
        }

        public static void BuildChart(Document wordDoc, DocumentBuilder builder,  ITunnelChart chartData)
        {
            if (chartData is ChartDataModel)
            {
                var chartDataModel = (ChartDataModel) chartData;
                
                TunnelChartService tunnelChartService = new TunnelChartService();
                tunnelChartService.InitChart(chartDataModel);
                tunnelChartService.BuildChart();
                builder.InsertImage(tunnelChartService.GetImage(ImageFormat.Png, new Size(chartDataModel.Width, chartDataModel.Height)));


                if (!string.IsNullOrEmpty(((ChartDataModel) chartData).PictureName))
                {
                    builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;
                    builder.Writeln(((ChartDataModel) chartData).PictureName);
                    builder.ParagraphFormat.Alignment = ParagraphAlignment.Left;
                }
            }
        }
        public static void BuildMonitoring(Document wordDoc, DocumentBuilder builder, IMonitoringResult monitoringData)
        {
            var resultService = new MonitoringResultService();
            if (monitoringData is MustMonitoringResultModel)
            {
                var monitoringDataModel = (MustMonitoringResultModel)monitoringData;
                var doc = resultService.BuildMustMonitoring(monitoringDataModel);
                builder.InsertDocument(doc, ImportFormatMode.KeepSourceFormatting);

            }
            else if (monitoringData is SelectionMonitoringResultModel)
            {
                var monitoringDataModel = (SelectionMonitoringResultModel)monitoringData;
                var doc = resultService.BuildSelectionMonitoring(monitoringDataModel);
                builder.InsertDocument(doc, ImportFormatMode.KeepSourceFormatting);
            }
        }
    }
}
