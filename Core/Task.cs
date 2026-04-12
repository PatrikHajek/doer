using System.Collections.Generic;

namespace doer.Core;

class Task
{
  public string Name { get; set; } = "";
  public List<Assignee> Assignees { get; } = [];
  public List<Label> Labels { get; } = [];
}
