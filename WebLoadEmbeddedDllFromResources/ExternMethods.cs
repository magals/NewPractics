using System.Runtime.InteropServices;

namespace WebLoadEmbeddedDllFromResources
{
  internal static class ExternMethods
  {
    [DllImport("plcommpro.dll")]
    public static extern IntPtr Connect(string Parameters);
  }
}
