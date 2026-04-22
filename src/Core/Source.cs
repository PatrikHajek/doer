using System;

namespace Doer.Core;

public class Source
{
  private static readonly string Path = System.IO.Path.Combine(
      Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
      "doer");

  public TaskList TaskList { get; init; } = new();

  public static Source Init()
  {
    var source = new Source();
    Json.Write(Path, source);
    return source;
  }

  public void Save()
  {
    Json.Write(Path, this);
  }

  public static Source Read()
  {
    return Json.Read<Source>(Path);
  }
}
