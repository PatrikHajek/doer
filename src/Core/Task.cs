using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Doer.Core;

public class Task
{
  public string Name { get; set; }
  public HashSet<Assignee> Assignees { get; init; } = [];
  public HashSet<Label> Labels { get; init; } = [];

  public static GeneratedRegexAttribute AssigneeRegex { get; } = new(@"^@[^@]+$");
  public static GeneratedRegexAttribute LabelRegex { get; } = new(@"^#[^#]+$");

  public Task()
  {
    Name = "";
  }

  public Task(string name)
  {
    if (name.Trim().Length == 0)
    {
      throw new ArgumentException("Task name cannot be empty");
    }

    Name = "";
    var chunks = name.Trim().Split(" ");

    foreach (var chunk in chunks)
    {
      switch (chunk)
      {
        case string s when Regex.IsMatch(s, AssigneeRegex.Pattern):
          Assignees.Add(new Assignee(chunk[1..]));
          break;
        case string s when Regex.IsMatch(s, LabelRegex.Pattern):
          Labels.Add(new Label(chunk[1..]));
          break;
        case { Length: > 0 }:
          Name += chunk + " ";
          break;
      }
    }

    Name = Name.Trim();
  }
}
