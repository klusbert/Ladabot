using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KonjoBot.Util
{
  public static  class GlobalFunttions
    {
      public static bool WaitOrFinish(Func<bool> Method)
      {
          DateTime d = DateTime.Now.AddSeconds(5);

          while (Method() == false)
          {
             
              if (d <= DateTime.Now)
              {
                  return false;

              }
          }
          return true;
      }
    }
}
