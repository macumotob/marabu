#if _USE_CISCO
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marabu
{

  public class TAPIParameter
  {
    public string Id { get; set; }
    public string Value { get; set; }
    public override string ToString()
    {
      return "{\"id\":\"" + Id + "\",\"value\":\"" + Value + "\"}";
    }
  }
  public class TAPICommand
  {
    public string cmd { get; set; }
    public List<TAPIParameter> p { get; set; }
  }
}
#endif