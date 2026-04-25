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
    var task = new Task(name);
    Tasks.Add(task);
  }
}
