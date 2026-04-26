using Doer.Core;

namespace Doer.Tests.Core;

public class TaskListTests
{
  readonly TaskList List = new();

  [Fact]
  public void Add_AddsTask()
  {
    List.Add("hello @me #app #backend");

    Assert.Single(List.Tasks);

    var task = new Doer.Core.Task("hello @me #app #backend");
    Assert.Equivalent(task, List.Tasks[0], true);
  }

  [Fact]
  public void Search_Is_CaseInsensitiveForTaskName()
  {
    List.Add("build it ASAP #project");
    List.Add("talk with @peter asap");

    var expected = new TaskList();

    expected.Tasks.Clear();
    expected.Add("build it ASAP #project");
    expected.Add("talk with @peter asap");
    Assert.Equivalent(expected.Tasks, List.Search("aSaP"), true);

    expected.Tasks.Clear();
    Assert.Equivalent(expected.Tasks, List.Search("#PROJECT"), true);

    expected.Tasks.Clear();
    Assert.Equivalent(expected.Tasks, List.Search("@Peter"), true);
  }

  [Fact]
  public void Search_Is_Pure()
  {
    List.Add("this #project needs to be done by @peter asap");
    List.Add("make design for #app @john!");

    // none match
    List.Search("hello");
    // first matches
    List.Search("asap");
    // second matches
    List.Search("design");
    // both match
    List.Search("o");

    var expected = new TaskList();
    expected.Add("this #project needs to be done by @peter asap");
    expected.Add("make design for #app @john!");

    Assert.Equivalent(expected, List, true);
  }

  [Fact]
  public void Search_FiltersTasks_By_FuzzyMatching()
  {
    List.Add("implement backend #app #dev @me");
    List.Add("brainstorm new features #app @me @john");
    List.Add("design new list #app #design @john");
    List.Add("consult with client #app");
    List.Add("buy new groceries");
    List.Add("clean up @me");

    var expected = new TaskList();

    // by name
    expected.Tasks.Clear();
    expected.Add("buy new groceries");
    expected.Add("design new list #app #design @john");
    expected.Add("brainstorm new features #app @me @john");
    Assert.Equivalent(expected.Tasks, List.Search("new"), true);

    // by label
    expected.Tasks.Clear();
    expected.Add("implement backend #app #dev @me");
    expected.Add("brainstorm new features #app @me @john");
    expected.Add("design new list #app #design @john");
    expected.Add("consult with client #app");
    Assert.Equivalent(expected.Tasks, List.Search("#app"), true);

    // by assignee
    expected.Tasks.Clear();
    expected.Add("implement backend #app #dev @me");
    expected.Add("brainstorm new features #app @me @john");
    expected.Add("clean up @me");
    Assert.Equivalent(expected.Tasks, List.Search("@me"), true);

    // by name, label and assignee
    expected.Tasks.Clear();
    expected.Add("brainstorm new features #app @me @john");
    Assert.Equivalent(expected.Tasks, List.Search("@me new #app"), true);

    // empty list
    expected.Tasks.Clear();
    Assert.Equivalent(expected.Tasks, List.Search("nonexistent #label @assignee"), true);
  }

  [Fact]
  public void Search_ReturnsEmptyList_When_QueryIsLongerThanAnyTask()
  {
    List.Add("do the #work");
    List.Add("do the #other #work");
    List.Add("@me should check inbox");

    var expected = new TaskList();
    Assert.Equivalent(expected.Tasks, List.Search("some very long search query that will find nothing"), true);
  }
}
