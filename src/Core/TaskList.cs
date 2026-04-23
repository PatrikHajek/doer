using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace Doer.Core;

public class TaskList
{
  public ObservableCollection<Task> Tasks { get; init; } = [];

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
        case string s when Regex.IsMatch(s, Task.AssigneeRegex.Pattern):
          task.Assignees.Add(new Assignee(chunk[1..]));
          break;
        case string s when Regex.IsMatch(s, Task.LabelRegex.Pattern):
          task.Labels.Add(new Label(chunk[1..]));
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
