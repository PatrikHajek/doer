using System.Collections.Generic;

namespace Doer.Core;

class Task
{
  public string Name { get; set; } = "";
  public List<Assignee> Assignees { get; } = [];
  public List<Label> Labels { get; } = [];
}
