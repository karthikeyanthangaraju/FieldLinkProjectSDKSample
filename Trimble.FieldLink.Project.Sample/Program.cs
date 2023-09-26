using ConsoleTools;
using System;

namespace Trimble.FieldLink.ProjectAPI.Sample
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            ShowMenuOptions(args);
        }

        private static void ShowMenuOptions(string[] args)
        {
            var mainMenu = new ConsoleMenu(args, 0)
            .Add("Add-DesignPoint", () => ExecuteAction(() => new JobSample().AddDesignPoint(), "AddDesignPoint"))
            .Add("Add-Line", () => ExecuteAction(() => new JobSample().AddLine(), "AddLine"))
            .Add("Add-Arc", () => ExecuteAction(() => new JobSample().AddArc(), "AddArc"))
            .Add("Add-Layer", () => ExecuteAction(() => new JobSample().AddLayer(), "AddLayer"))
            .Add("Update-DesignPoint", () => ExecuteAction(() => new JobSample().UpdateDesignPoint(), "UpdateDesignPoint"))
            .Add("Update-Layer", () => ExecuteAction(() => new JobSample().UpdateLayer(), "UpdateLayer"))
            .Add("Remove-DesignPoint", () => ExecuteAction(() => new JobSample().RemoveDesignPoint(), "RemoveDesignPoint"))
            .Add("Remove-Line", () => ExecuteAction(() => new JobSample().RemoveLine(), "RemoveLine"))
            .Add("Remove-Arc", () => ExecuteAction(() => new JobSample().RemoveArc(), "RemoveArc"))
            .Add("Get-DesignPoint", () => ExecuteAction(() => new JobSample().GetDesignPoints(), "GetDesignPoints"))
            .Add("Get-Line", () => ExecuteAction(() => new JobSample().GetLines(), "GetLines"))
            .Add("AddAprilTags", () => ExecuteAction(() => new JobSample().AddAprilTagWithPoints(), "AddAprilTags"))
            .Add("RetrieveAprilTags", () => ExecuteAction(() => new JobSample().RetrieveAprilTagPoints(), "RetrieveAprilTags"))
            .Add("Project-Create", () => ExecuteAction(() => new ProjectSample().CreateProject(), "CreateProject"))
            .Add("Project-AddTflx", () => ExecuteAction(() => new ProjectSample().AddTflx(), "AddTflx"))
            .Add("Project-AddModels", () => ExecuteAction(() => new ProjectSample().AddModels(), "AddModels"))
            .Add("Project-CreateWithJobAndModel", () => ExecuteAction(() => new ProjectSample().CreateProjectWithJobAndModel(), "CreateProjectWithJobAndModel"))
            .Add("Project-Open", () => ExecuteAction(() => new ProjectSample().OpenProject(), "OpenProject"))
            .Add("Project-SaveAs", () => ExecuteAction(() => new ProjectSample().SaveAsProject(), "SaveAsProject"))
            .Add("Project-SaveAsTrimbleConnect", () => {
                Console.WriteLine("Enter Project Name :");
                var projectName = Console.ReadLine();

                Console.WriteLine("Enter Region (North America \\ Europe \\ Asia) :");
                var region = Console.ReadLine();

                Console.WriteLine("Enter accessToken :");
                var accessToken = Console.ReadLine();

                ExecuteAction(() => new ProjectSample().SaveAsTrimbleConnect(projectName,
                                                                             region,
                                                                             accessToken), 
                                                                             "SaveAsTrimbleConnect");
            })
            .Add("Project-RemoveTflx", () => ExecuteAction(() => new ProjectSample().RemoveTflx(), "RemoveTflx"))
            .Add("Project-RemoveModel", () => ExecuteAction(() => new ProjectSample().RemoveModel(), "RemoveModel"))
            .Configure(config =>
            {
                config.Selector = "-->";
                config.EnableFilter = true;
                config.Title = "Choose operation to execute";
                config.EnableBreadcrumb = true;
                config.SelectedItemBackgroundColor = ConsoleColor.Yellow;
            });

            mainMenu.Show();
        }

        private static void ExecuteAction(Action action,string methodName)
        {
            Console.WriteLine($"Executing method : {methodName}");
            action.Invoke();
            Console.ReadKey();
            Console.WriteLine($"Execution completed for the method : {methodName}");
        }

        internal static void CompletionMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
