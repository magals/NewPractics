using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRSendFilesCheckHash.Models;

public static class HelpersMethods
{
  public static byte[] Xor(byte[] left, byte[] right)
  {
    byte[] val = new byte[left.Length];
    for (int i = 0; i < left.Length; i++)
      val[i] = (byte)(left[i] ^ right[i]);
    return val;
  }
}
