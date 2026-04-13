using System;
using System.IO;
using System.Text.Json;

namespace doer.Core;

class Json
{
  public static T Read<T>(string path)
  {
    string text = File.ReadAllText(path);
    var ret = JsonSerializer.Deserialize<T>(text);
    ArgumentNullException.ThrowIfNull(ret);
    return ret;
  }

  public static void Write<T>(string path, T data)
  {
    var text = JsonSerializer.Serialize(data);
    File.WriteAllText(path, text);
  }
}
