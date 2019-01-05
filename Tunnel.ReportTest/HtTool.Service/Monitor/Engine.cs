using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace HtTool.Service.Monitor
{
    public class Engine
    {
        FunctionEvaluator func = new FunctionEvaluator();
        public Engine()
        {
        }

        public PointF[] Run(string text, PointF[] npts)
        {
            func.Text = text;

            if (!func.Compile())
                return null;

            PointF[] data = (PointF[])Array.CreateInstance(typeof(PointF), npts.Length);
            //float dx = (xmax-xmin)/(npts-1);
            for (int i = 0; i < npts.Length; i++)
            {
                data[i].X = (float)npts[i].X;
                data[i].Y = (float)func.Invoke(npts[i].X);
            }

            return data;
        }

        public string[] Errors
        {
            get
            {
                return func.Errors;
            }
        }

        public bool Compile(string text)
        {
            func.Text = text;

            return func.Compile();
        }
    }
}
