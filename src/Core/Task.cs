using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Doer.Core;

public class Task
{
  public required string Name { get; set; }
  public HashSet<Assignee> Assignees { get; init; } = [];
  public HashSet<Label> Labels { get; init; } = [];

  public static GeneratedRegexAttribute AssigneeRegex { get; } = new(@"^@[^@]+$");
  public static GeneratedRegexAttribute LabelRegex { get; } = new(@"^#[^#]+$");
}
