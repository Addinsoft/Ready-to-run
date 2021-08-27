
using Xunit;

namespace ReadyToRun.test
{
    public class Workspace_test
    {
        [Fact]
        public void Test1()
        {
            string path = XLSTAT.Models.Workspace.GetWorkspacePath();

            Assert.False(path == "", "The workspace should not be empty");
            XLSTAT.Models.Workspace.ClearWorkspace();
        }
    }
}
