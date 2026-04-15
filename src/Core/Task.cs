using System.Collections.Generic;

namespace Doer.Core;

class Task
{
  public required string Name { get; set; }
  public HashSet<Assignee> Assignees { get; } = [];
  public HashSet<Label> Labels { get; } = [];
}
