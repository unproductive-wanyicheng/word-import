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
    public class TableBase
    {

        protected Stream GetSroce(string url)
        {
            Assembly assm = Assembly.GetExecutingAssembly();
            return assm.GetManifestResourceStream(url);
        }
        protected Aspose.Words.Tables.Cell CreateCell(string value, Document doc)
        {
            Aspose.Words.Tables.Cell c1 = new Aspose.Words.Tables.Cell(doc);
            c1.CellFormat.HorizontalMerge = Aspose.Words.Tables.CellMerge.None;
            c1.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
            Aspose.Words.Paragraph p = new Paragraph(doc);
            p.AppendChild(new Run(doc, value));
            c1.AppendChild(p);
            return c1;
        }
        protected Aspose.Words.Tables.Row CreateRow(string[] columnValues, Document doc)
        {
            Aspose.Words.Tables.Row r2 = new Aspose.Words.Tables.Row(doc);
            for (int i = 0; i < columnValues.Length; i++)
            {
                if (columnValues.Length > i)
                {
                    var cell = CreateCell(columnValues[i], doc);
                    r2.Cells.Add(cell);
                }
                else
                {
                    var cell = CreateCell("", doc);
                    r2.Cells.Add(cell);
                }

            }
            return r2;

        }

        protected void SetRowData(Document doc,Row row,string[] columnValues)
        {
            for (int i = 0; i < columnValues.Length; i++)
            {
                if (row.Cells.Count > i)
                {
                    Aspose.Words.Paragraph p = new Paragraph(doc);
                    p.AppendChild(new Run(doc, columnValues[i]));
                    row.Cells[i].RemoveAllChildren();
                    row.Cells[i].AppendChild(p);
                }

            }
        }

        protected void MergeCells(Cell startCell, Cell endCell)
        {
            Aspose.Words.Tables.Table parentTable = startCell.ParentRow.ParentTable;

            // Find the row and cell indices for the start and end cell.
            Point startCellPos = new Point(startCell.ParentRow.IndexOf(startCell), parentTable.IndexOf(startCell.ParentRow));
            Point endCellPos = new Point(endCell.ParentRow.IndexOf(endCell), parentTable.IndexOf(endCell.ParentRow));
            // Create the range of cells to be merged based off these indices. Inverse each index if the end cell if before the start cell.
            Rectangle mergeRange = new Rectangle(Math.Min(startCellPos.X, endCellPos.X), Math.Min(startCellPos.Y, endCellPos.Y), Math.Abs(endCellPos.X - startCellPos.X) + 1,
            Math.Abs(endCellPos.Y - startCellPos.Y) + 1);

            foreach (Row row in parentTable.Rows)
            {
                foreach (Cell cell in row.Cells)
                {
                    Point currentPos = new Point(row.IndexOf(cell), parentTable.IndexOf(row));

                    // Check if the current cell is inside our merge range then merge it.
                    if (mergeRange.Contains(currentPos))
                    {
                        if (mergeRange.Width > 1)
                        {
                            if (currentPos.X == mergeRange.X)
                                cell.CellFormat.HorizontalMerge = CellMerge.First;
                            else
                                cell.CellFormat.HorizontalMerge = CellMerge.Previous;
                        }
                        if (mergeRange.Height > 1)
                        {
                            if (currentPos.Y == mergeRange.Y)
                                cell.CellFormat.VerticalMerge = CellMerge.First;
                            else
                                cell.CellFormat.VerticalMerge = CellMerge.Previous;
                        }
                    }
                }
            }
        }
    }
}
