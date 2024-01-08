using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

    public class AeroSnapTitleBarControls : NativeWindow
    {
        private Form form;
        private Size formSize;

        protected override void WndProc(ref Message m)
        {
            const int WM_NCCALCSIZE = 0x0083;
            const int WM_NCHITTEST = 0x0084;
            const int HTCLIENT = 1;
            const int HTTOP = 12;

            //cursor status title bar edge change
            if (m.Msg == WM_NCHITTEST)
            {
                base.WndProc(ref m);

                if (form.WindowState == FormWindowState.Normal)
                {
                    if ((int)m.Result == HTCLIENT)
                    {
                        m.Result = (IntPtr)HTTOP;
                    }
                }
                return;
            }

            //cursor status borders change
            if (m.Msg == WM_NCCALCSIZE )
            {
                NCCALCSIZE_PARAMS nccsp = (NCCALCSIZE_PARAMS)Marshal.PtrToStructure(m.LParam, typeof(NCCALCSIZE_PARAMS));
                nccsp.rect0.Left += 8;
                nccsp.rect0.Right -= 8;
                nccsp.rect0.Bottom -= 8;

                Marshal.StructureToPtr(nccsp, m.LParam, false);
                return;
            }
            base.WndProc(ref m);
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct NCCALCSIZE_PARAMS
        {
            public RECT rect0;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        public AeroSnapTitleBarControls(Form form)
        {
            this.form = form;
            form.Padding = new Padding(form.Padding.Left, +2, form.Padding.Right, form.Padding.Bottom);
            formSize = form.ClientSize;
            AssignHandle(form.Handle);
        }

        public void MiniControl()
        {
            formSize = form.ClientSize;
            form.WindowState = FormWindowState.Minimized;
        }

        public void MaxiNormControl()
        {
            if (form.WindowState == FormWindowState.Normal)
            {
                formSize = form.ClientSize;
                form.WindowState = FormWindowState.Maximized;
            }
            else
            {
                form.WindowState = FormWindowState.Normal;
                form.Size = formSize;
            }
        }

        // 2 click titleBar --> full screan form
        private DateTime lastClickTime = DateTime.MinValue;
        private const int doubleClickInterval = 350;
        private int clickCounter = 0;

        public void AeroSnapTitleBar()
        {
            ReleaseCapture();
            SendMessage(form.Handle, 0x112, 0xf012, 0);
            if ((DateTime.Now - lastClickTime).TotalMilliseconds < doubleClickInterval)
            {
                if (clickCounter % 2 == 1)
                {
                    MaxiNormControl();
                }
                clickCounter++;
            }
            else
            {
                clickCounter = 1;
            }
            lastClickTime = DateTime.Now;
        }
    }
