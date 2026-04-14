using System.Collections.Generic;

namespace Doer.Core;

class Task
{
  public required string Name { get; set; }
  public List<Assignee> Assignees { get; } = [];
  public List<Label> Labels { get; } = [];
}
