using System;
using System.Collections.Generic;

namespace Doer.Core;

class TaskList
{
  public List<Task> Tasks { get; } = [];

  public void Add(string name)
  {
    if (name.Trim().Length == 0)
    {
      throw new ArgumentException("Task name cannot be empty");
    }

    var chunks = name.Trim().Split(" ");
    var task = new Task() { Name = "" };

    foreach (var chunk in chunks)
    {
      switch (chunk)
      {
        case string s when s.StartsWith('@'):
          task.Assignees.Add(new Assignee { Name = chunk[1..] });
          break;
        case string s when s.StartsWith('#'):
          task.Labels.Add(new Label { Name = chunk[1..] });
          break;
        case { Length: > 0 }:
          task.Name += chunk + " ";
          break;
      }
    }

    task.Name = task.Name.Trim();
    Tasks.Add(task);
  }
}
