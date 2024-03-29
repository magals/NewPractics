﻿using System.ComponentModel;
using System.Reflection;
using System.Runtime.InteropServices;

namespace WebLoadEmbeddedDllFromResources
{
  public class EmbeddedDllClass
  {
    private static string tempFolder = "";

    /// <summary>
    /// Extract DLLs from resources to temporary folder
    /// </summary>
    /// <param name="dllName">name of DLL file to create (including dll suffix)</param>
    /// <param name="resourceBytes">The resource name (fully qualified)</param>
    public static void ExtractEmbeddedDlls(string dllName, byte[] resourceBytes)
    {
      Assembly assem = Assembly.GetExecutingAssembly();
      string[] names = assem.GetManifestResourceNames();
      AssemblyName an = assem.GetName();

      // The temporary folder holds one or more of the temporary DLLs
      // It is made "unique" to avoid different versions of the DLL or architectures.
      tempFolder = String.Format("{0}.{1}.{2}", an.Name, an.ProcessorArchitecture, an.Version);

      string dirName = Path.Combine(Path.GetTempPath(), tempFolder);
      if (!Directory.Exists(dirName))
      {
        Directory.CreateDirectory(dirName);
      }

      // Add the temporary dirName to the PATH environment variable (at the head!)
      string path = Environment.GetEnvironmentVariable("PATH");
      string[] pathPieces = path.Split(';');
      bool found = false;
      foreach (string pathPiece in pathPieces)
      {
        if (pathPiece == dirName)
        {
          found = true;
          break;
        }
      }
      if (!found)
      {
        Environment.SetEnvironmentVariable("PATH", dirName + ";" + path);
      }

      // See if the file exists, avoid rewriting it if not necessary
      string dllPath = Path.Combine(dirName, dllName);
      bool rewrite = true;
      if (File.Exists(dllPath))
      {
        byte[] existing = File.ReadAllBytes(dllPath);
        if (resourceBytes.SequenceEqual(existing))
        {
          rewrite = false;
        }
      }
      if (rewrite)
      {
        File.WriteAllBytes(dllPath, resourceBytes);
      }
    }

    [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Unicode)]
    static extern IntPtr LoadLibrary(string lpFileName);

    /// <summary>
    /// managed wrapper around LoadLibrary
    /// </summary>
    /// <param name="dllName"></param>
    static public void LoadDll(string dllName)
    {
      if (tempFolder == "")
      {
        throw new Exception("Please call ExtractEmbeddedDlls before LoadDll");
      }
      IntPtr h = LoadLibrary(dllName);
      if (h == IntPtr.Zero)
      {
        Exception e = new Win32Exception();
        throw new DllNotFoundException("Unable to load library: " + dllName + " from " + tempFolder, e);
      }
    }

  }
}
