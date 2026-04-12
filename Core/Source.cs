using System;

namespace doer.Core;

class Source
{
  private static readonly string Path = System.IO.Path.Combine(
      Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
      "doer");

  public TaskList TaskList { get; } = new();

  public void Save()
  {
    Json.Write(Path, this);
  }

  public static Source Read()
  {
    return Json.Read<Source>(Path);
  }
}
