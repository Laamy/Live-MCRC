using System;
using System.Windows.Forms;

namespace Live_MCRC
{
    internal class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.Run(new Preview()); // form for editing rendercontext uis live
        }
    }
}
