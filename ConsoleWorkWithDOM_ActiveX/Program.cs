using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ConsoleWorkWithDOM_ActiveX
{
  internal class Program
  {
    static void Main(string[] args)
    {
      Assembly.Load("TestComInteropActiveX");
    }
  }
}
