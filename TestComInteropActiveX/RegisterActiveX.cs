using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace TestComInteropActiveX
{
  public partial class MagalsProdaction
  {
    ///	<summary>
		///	Register the class as a	control	and	set	it's CodeBase entry
		///	</summary>
		///	<param name="key">The registry key of the control</param>
		[ComRegisterFunction()]
    public static void RegisterClass(string key)
    {
      // Strip off HKEY_CLASSES_ROOT\ from the passed key as I don't need it
      StringBuilder sb = new StringBuilder(key);

      sb.Replace(@"HKEY_CLASSES_ROOT\", "");
      // Open the CLSID\{guid} key for write access
      RegistryKey k = Registry.ClassesRoot.OpenSubKey(sb.ToString(), true);

      // And create	the	'Control' key -	this allows	it to show up in
      // the ActiveX control container
      RegistryKey ctrl = k.CreateSubKey("Control");
      ctrl.Close();

      // Next create the CodeBase entry	- needed if	not	string named and GACced.
      RegistryKey inprocServer32 = k.OpenSubKey("InprocServer32", true);
      inprocServer32.SetValue("CodeBase", Assembly.GetExecutingAssembly().CodeBase);
      inprocServer32.Close();
      // Finally close the main	key
      k.Close();
      MessageBox.Show("Registered");
    }

    ///	<summary>
    ///	Called to unregister the control
    ///	</summary>
    ///	<param name="key">Tke registry key</param>
    [ComUnregisterFunction()]
    public static void UnregisterClass(string key)
    {
      StringBuilder sb = new StringBuilder(key);
      sb.Replace(@"HKEY_CLASSES_ROOT\", "");

      // Open	HKCR\CLSID\{guid} for write	access
      RegistryKey k = Registry.ClassesRoot.OpenSubKey(sb.ToString(), true);

      // Delete the 'Control'	key, but don't throw an	exception if it	does not exist
      k.DeleteSubKey("Control", false);

      // Next	open up	InprocServer32
      //RegistryKey	inprocServer32 = 
      k.OpenSubKey("InprocServer32", true);

      // And delete the CodeBase key,	again not throwing if missing
      k.DeleteSubKey("CodeBase", false);

      // Finally close the main key
      k.Close();
      MessageBox.Show("UnRegistered");
    }
  }
}
