using System;
using System.Drawing;
using System.IO;
using Aspose.Words;

namespace Tunnel.Word.BuildWord.Month
{
    public class TunnelMonthTestService : IBuild
    {
        private WordManage _word;
        private readonly string _basePath;

        public TunnelMonthTestService()
        {
            _basePath = Path.Combine(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
                "TunnelMonthFiles");
        }
        public Document BuildWord()
        {
            _word = new WordManage();
            _word.Open();

            Page1();
            Zuyisixiang();
            CreateItem();

            return _word.Document;
        }

        private void Page1()
        {
            _word.Builder.Font.Name = "仿宋_GB2312";
            _word.Write("报告编号：", 14, true, ParagraphAlignment.Right);
            _word.Builder.Underline = Underline.Single;
            _word.Builder.Font.Name = "宋体";
            _word.Write("GJJCZX-BLGS-LYJKLC-JK-001", 14, false, ParagraphAlignment.Right);
            _word.Builder.Underline = Underline.None;

            _word.Writeln(2);

            _word.Writeln("云南保山至泸水高速公路老营特长隧道", 22, false, ParagraphAlignment.Center);
            _word.Writeln();

            _word.Builder.Font.Name = "黑体";
            _word.Writeln("监 控 量 测 报 告", 36, true, ParagraphAlignment.Center);
            _word.Writeln(2);

            _word.Builder.Font.Name = "仿宋_GB2312";
            _word.Builder.Font.Size = 16;
            //段落左缩进55
            _word.Builder.ParagraphFormat.LeftIndent = 55;
            _word.Write("合 同 段： ", 16, true);
            _word.Write("      土建S1合同         ", 16, false, ParagraphAlignment.Left, true);
            _word.Writeln();

            _word.Write("隧道名称： ", 16, true);
            _word.Write("  老营特长隧道（进口端）  ", 16, false, ParagraphAlignment.Left, true);
            _word.Writeln();

            _word.Write("监测范围： ", 16, true);
            _word.Write(" 左幅（ZK1+558～ZK1+623） ", 16, false, ParagraphAlignment.Left, true);
            _word.Writeln();

            _word.Builder.ParagraphFormat.LeftIndent = 143;
            _word.Write(" 右幅（K1+466～K1+556）   ", 16, false, ParagraphAlignment.Left, true);
            _word.Writeln();

            _word.Builder.ParagraphFormat.LeftIndent = 55;
            _word.Write("监测日期： ", 16, true);
            _word.Write("  2016.3.28～2016.4.28    ", 16, false, ParagraphAlignment.Left, true);
            _word.Writeln(6);

            _word.Builder.ParagraphFormat.LeftIndent = 0;
            _word.Builder.Font.Name = "宋体";

            _word.Builder.InsertImage(Path.Combine(_basePath, "log.png"));
            _word.Write("贵州省交通建设工程检测中心有限责任公司", 18, true, ParagraphAlignment.Center);
            _word.Writeln();
            _word.Writeln("云南保泸高速公路隧道检测第一合同项目经理部", 18, true, ParagraphAlignment.Center);
        }

        /// <summary>
        /// 注意事项
        /// </summary>
        private void Zuyisixiang()
        {
            var zysx = new Aspose.Words.Document(Path.Combine(_basePath, "注意事项.docx"));
            var builder = new DocumentBuilder(zysx);

            WordManage.BookMarkReplace(zysx, builder, "公司名", "云南保山至泸水高速公路老营特长隧道");
            WordManage.BookMarkReplace(zysx, builder, "检测单位1", "贵州省交通建设工程检测中心有限责任公司");
            WordManage.BookMarkReplace(zysx, builder, "检测单位2", "云南保泸高速公路隧道检测第一合同项目经理部");
            WordManage.BookMarkReplace(zysx, builder, "日期", DateTime.Now.ToString("yyyy年M月d日"));

            _word.Document.AppendDocument(zysx, ImportFormatMode.KeepSourceFormatting);
            _word.NewPage();
        }

        private void CreateItem()
        {
            _word.Builder.MoveToDocumentEnd();

            _word.Builder.InsertBreak(BreakType.SectionBreakNewPage);
            _word.Builder.InsertTableOfContents("\"1-3\"");
            _word.Builder.InsertBreak(BreakType.SectionBreakNewPage);
            _word.Builder.ParagraphFormat.Alignment = ParagraphAlignment.Left;
            _word.Builder.Font.Color = Color.Black;
            _word.Builder.Bold = true;

            _word.Builder.ParagraphFormat.StyleIdentifier = StyleIdentifier.Heading1;
            _word.Builder.Font.Size = 15;

            _word.Builder.Writeln("1、前言");

            _word.Builder.ParagraphFormat.StyleIdentifier = StyleIdentifier.Heading2;
            _word.Builder.Font.Size = 14;

            _word.Builder.Writeln("1.1任务来源");

            _word.Builder.Writeln("1.2隧道工程概况");

            _word.Builder.Writeln("1.3隧道施工情况及断面埋设");

            _word.Builder.ParagraphFormat.StyleIdentifier = StyleIdentifier.Heading1;
            _word.Builder.Font.Size = 15;

            _word.Builder.Writeln("2、隧道施工监控量测");
            _word.Builder.ParagraphFormat.StyleIdentifier = StyleIdentifier.Heading2;
            _word.Builder.Font.Size = 14;

            _word.Builder.Writeln("2.1监控量测参考依据");

            _word.Builder.Writeln("2.2监控量测的目的");

            _word.Builder.Writeln("2.3监控量测内容");
            _word.Builder.Writeln("2.4监控量测频率");


            _word.Builder.ParagraphFormat.StyleIdentifier = StyleIdentifier.BodyText;

            // Call the method below to update the TOC.
            _word.SetPageHeaderHeader("云南保山至泸水高速公路老营特长隧道", 10);

            _word.SetPageHeaderFooter(14);

            _word.Document.UpdateFields();

            _word.Builder.InsertBreak(BreakType.SectionBreakNewPage);


            foreach (Bookmark bm in _word.Document.Range.Bookmarks)
            {
                if (bm.Name.StartsWith("_Toc"))
                {
                    _word.Builder.MoveToBookmark(bm.Name, false, false);
                    WordManage nword1 = new WordManage();
                    nword1.Open();
                    nword1.Writeln(bm.Name);
                    nword1.NewPage();
                    WordManage.InsertDocument(bm.BookmarkStart.ParentNode, nword1.Document);
                }
            }
            _word.Document.UpdateFields();
        }
    }
}
