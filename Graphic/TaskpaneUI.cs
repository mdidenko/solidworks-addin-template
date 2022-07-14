using NLog;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using CAPP.States;

namespace CAPP.Graphic
{
    [ComVisible(true)]
    [ProgId(PROG_ID)]
    public partial class TaskpaneUI : UserControl
    {
        public const string PROG_ID = ConfigData.Graphic.UIProgId.TASKPANE_UI;
        private static readonly Lazy<Logger> s_logger = new Lazy<Logger>(() => LogManager.GetCurrentClassLogger());

        public TaskpaneUI()
        {
            InitializeComponent();
        }

        private void DocButton_Click(object sender, EventArgs e)
        {
            SwAddin.SWApplication.SendMsgToUser("Document"); // hardcode - lang!!!
        }
    }
}
