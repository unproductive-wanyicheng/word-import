using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Aspose.Words;

namespace HtTool.Service.Word
{
    public static class WordHelper
    {
        /// <summary>
        /// 设置字体大小
        /// </summary>
        /// <param name="nSize">字体大小</param>
        /// <param name="builder"></param>
        public static double SetFontSize(double nSize, DocumentBuilder builder)
        {
            double oldFont = builder.Font.Size;
            builder.Font.Size = nSize;
            return oldFont;
        }

        /// <summary>
        /// 跳转到文档末尾
        /// </summary>
        /// <param name="builder"></param>
        public static void GoToTheEnd(DocumentBuilder builder)
        {
            builder.MoveToDocumentEnd();
        }

        /// <summary>
        /// 插入表格
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="oWordApplic"></param>
        /// <param name="haveBorder"></param>
        public static void InsertTable(DataTable dt, DocumentBuilder oWordApplic, bool haveBorder)
        {
            Aspose.Words.Tables.Table table = oWordApplic.StartTable();//开始画Table 
            ParagraphAlignment paragraphAlignmentValue = oWordApplic.ParagraphFormat.Alignment;
            oWordApplic.ParagraphFormat.Alignment = ParagraphAlignment.Center;
            //列头
            foreach (DataColumn col in dt.Columns)
            {
                oWordApplic.InsertCell();
                oWordApplic.Font.Size = 8;
                oWordApplic.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐 
                if (haveBorder == true)
                {
                    //设置外框样式    
                    oWordApplic.CellFormat.Borders.LineStyle = LineStyle.Single;
                    //样式设置结束    
                }
                oWordApplic.Write(col.ColumnName);
            }

            oWordApplic.EndRow();

            //添加Word表格 
            for (int row = 0; row < dt.Rows.Count; row++)
            {
                //oWordApplic.RowFormat.Height = 25;
                for (int col = 0; col < dt.Columns.Count; col++)
                {
                    oWordApplic.InsertCell();
                    oWordApplic.Font.Size = 8;
                    //oWordApplic.Font.Name = "宋体";
                    //oWordApplic.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐 
                    oWordApplic.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐 
                    //oWordApplic.CellFormat.Width = 50.0;
                    //oWordApplic.CellFormat.PreferredWidth = Aspose.Words.Tables.PreferredWidth.FromPoints(50);
                    if (haveBorder == true)
                    {
                        //设置外框样式    
                        oWordApplic.CellFormat.Borders.LineStyle = LineStyle.Single;
                        //样式设置结束    
                    }

                    oWordApplic.Write(dt.Rows[row][col].ToString());
                }

                oWordApplic.EndRow();

            }
            oWordApplic.EndTable();
            oWordApplic.ParagraphFormat.Alignment = paragraphAlignmentValue;
            //table.Alignment = Aspose.Words.Tables.TableAlignment.Center;
            //table.PreferredWidth = Aspose.Words.Tables.PreferredWidth.Auto;

        }
    }
}
