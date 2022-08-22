using SolidworksAddinTemplate.States;
using System.Windows.Forms;

namespace SolidworksAddinTemplate.Graphic;

[ComVisible(true)]
[ProgId(UiProgId.TASKPANE_UI)]
public partial class TaskpaneUI : UserControl
{
    public TaskpaneUI()
    {
        InitializeComponent();
    }

    private void testButton_Click(object sender, EventArgs e)
    {
        SwAddin.SwApplication!.SendMsgToUser("Example...");
    }
}
