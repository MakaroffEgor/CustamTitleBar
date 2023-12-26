# CustamTitleBar
Custam Title Bar c# winForm.
dd this class to your project to create your own title bar for using commands in your form, here is a usage example:

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private AeroSnapTitleBarControls aeroSnapTitleBarControls;

        public Form1()
        {
            //be sure to write on top of InitializeComponent();
            aeroSnapTitleBarControls = new AeroSnapTitleBarControls(this);
            InitializeComponent();
        }
        
    private void btnMaxiNormSizeForm_Click(object sender, EventArgs e)
    {
        aeroSnapTitleBarControls.MaxiNormControl();
    }

    private void btnMiniSizeForm_Click(object sender, EventArgs e)
    {
        aeroSnapTitleBarControls.MiniControl();
    }

    private void pnlTitleBarForm_MouseDown(object sender, MouseEventArgs e)
    {
        aeroSnapTitleBarControls.AeroSnapTitleBar();
    }
  } 
}
