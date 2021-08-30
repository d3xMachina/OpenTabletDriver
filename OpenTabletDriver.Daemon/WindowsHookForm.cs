using System;
using System.Windows.Forms;

namespace OpenTabletDriver.Daemon
{
    public class WindowsHookForm : Form
    {
        public static WindowsHookForm Instance { protected set; get; } = new WindowsHookForm();

        private readonly int message;

        public event EventHandler<IntPtr> WindowActivatedEvent;

        private WindowsHookForm()
        {
            message = Native.Windows.Windows.RegisterWindowMessage("SHELLHOOK");
            Native.Windows.Windows.RegisterShellHookWindow(this.Handle);
        }

        protected override void WndProc(ref Message message)
        {
            if (message.Msg == this.message)
            {
                Native.Windows.Windows.ShellEvents shellEvent = (Native.Windows.Windows.ShellEvents) message.WParam.ToInt32();
                IntPtr windowHandle = message.LParam;

                switch (shellEvent)
                {
                    case Native.Windows.Windows.ShellEvents.HSHELL_RUDEAPPACTIVATED:
                    case Native.Windows.Windows.ShellEvents.HSHELL_WINDOWACTIVATED:
                        if(windowHandle != IntPtr.Zero)
                        {
                            WindowActivatedEvent?.Invoke(this, windowHandle);
                        }
                        break;
                }
            }
            base.WndProc(ref message);
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                Native.Windows.Windows.DeregisterShellHookWindow(this.Handle); 
            }
            catch { }
        }
    }
}
