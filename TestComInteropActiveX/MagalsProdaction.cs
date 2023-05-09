using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace TestComInteropActiveX
{
  [ProgId("Magals.TestComInteropActiveX")]
  [Guid("E4506C69-EAF1-4073-A102-C40B0F28C812")]
  public partial class MagalsProdaction
  {
    private string myParam = "Hi Max";

    public MagalsProdaction()
    {

    }

    [ComVisible(true)]
    public void Open()
    {
      //TODO: Replace the try catch in aspx with try catch below. The problem is that js OnClose does not register.
      try
      {

        MessageBox.Show(myParam); //Show param that was passed from JS
        Thread.Sleep(2000); //Wait a little before closing. This is just to show the gap between calling OnClose event.

      }
      catch (Exception e)
      {
        //ExceptionHandling.AppException(e);
        throw e;
      }
    }
  }
}
