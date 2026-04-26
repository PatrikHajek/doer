using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Doer.Core;

public class TaskList
{
  public ObservableCollection<Task> Tasks { get; init; } = [];

  public void Add(string name)
  {
    var task = new Task(name);
    Tasks.Add(task);
  }

  public List<Task> Search(string name)
  {
    var task = new Task(name);

    var tasks = new List<Task>(Tasks).Where(t => t.Rank(task) >= 0).ToList();
    tasks.Sort((a, b) => a.Rank(task) - b.Rank(task));

    return tasks;
  }
}
