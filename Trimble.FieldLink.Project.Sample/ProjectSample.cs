using System.IO;
using System.Linq;
using Trimble.FieldTech.Project;
using Trimble.FieldTech.Connect.Project;
using Trimble.FieldTech.Connect.Project.Interface;

namespace Trimble.FieldLink.ProjectAPI.Sample
{
    public class ProjectSample
    {
        private readonly string SampleSaveProjectName = "ProjectSaveSample";
        private readonly string SampleOpenProjectName = "OpenProjectSample";
        private readonly string OpenProjectPath = @"..\..\..\SampleData\OpenProjectSample";
        private readonly string ProjectName = "SampleProject";
        private readonly string Description = "Sample Description";
        private readonly string ProjectPath = @"C:\FieldLink\Project";

        public ProjectSample()
        {
            if (!Directory.Exists(this.ProjectPath))
                Directory.CreateDirectory(this.ProjectPath);
        }

        public void CreateProject()
        {
            this.CleanTheProject();
            //Create Project
            var project = ProjectService.Create(this.ProjectPath, this.ProjectName, this.Description, "");
            Program.CompletionMessage($"Project created in the path : {ProjectPath}");
        }

        public void AddTflx()
        {
            this.CleanTheProject();
            //Create Project
            var project = ProjectService.Create(this.ProjectPath, this.ProjectName, this.Description, "");
            //Add Tflx
            project.Jobs.Add(Path.GetFullPath(@"..\..\..\SampleData\Jobs\Layout Job-1.tflx"));
            project.Jobs.Add(Path.GetFullPath(@"..\..\..\SampleData\Jobs\Layout Job-2.tflx"));
            project.Jobs.Add(Path.GetFullPath(@"..\..\..\SampleData\Jobs\Layout Job-3.tflx"));
            Program.CompletionMessage($"Project created and Tflx added in the path : {ProjectPath}");
        }

        public void AddModels()
        {
            this.CleanTheProject();
            //Create Project
            var project = ProjectService.Create(this.ProjectPath, this.ProjectName, this.Description, "");
            //Add Model
            project.Models.Add(Path.GetFullPath(@"..\..\..\SampleData\Models\Site-1.dwg"));
            project.Models.Add(Path.GetFullPath(@"..\..\..\SampleData\Models\sydney build.skp"));
            project.Models.Add(Path.GetFullPath(@"..\..\..\SampleData\Models\Tekla Structures.ifc"));
            project.Models.Add(Path.GetFullPath(@"..\..\..\SampleData\Models\Trimble Headquarters.dwg"));
            Program.CompletionMessage($"Project created and models added in the path : {ProjectPath}");
        }

        public void CreateProjectWithJobAndModel()
        {
            this.CleanTheProject();
            //Create Project
            var project = ProjectService.Create(this.ProjectPath, this.ProjectName, this.Description, "");
            //Add Job
            project.Jobs.Add(Path.GetFullPath(@"..\..\..\SampleData\Jobs\Layout Job-1.tflx"));
            project.Jobs.Add(Path.GetFullPath(@"..\..\..\SampleData\Jobs\Layout Job-2.tflx"));
            project.Jobs.Add(Path.GetFullPath(@"..\..\..\SampleData\Jobs\Layout Job-3.tflx"));
            //Add Model
            project.Models.Add(Path.GetFullPath(@"..\..\..\SampleData\Models\Site-1.dwg"));
            project.Models.Add(Path.GetFullPath(@"..\..\..\SampleData\Models\sydney build.skp"));
            project.Models.Add(Path.GetFullPath(@"..\..\..\SampleData\Models\Tekla Structures.ifc"));
            project.Models.Add(Path.GetFullPath(@"..\..\..\SampleData\Models\Trimble Headquarters.dwg"));

            Program.CompletionMessage($"Project created and Tflx & models added in the path : {ProjectPath}");
        }

        public void OpenProject()
        {
            //Open Project
            var project = ProjectService.Open(Path.GetFullPath(OpenProjectPath));
            Program.CompletionMessage($"Project opened successfully from the path : {Path.GetFullPath(OpenProjectPath)}");
        }

        public void SaveAsProject()
        {
            var project = ProjectService.Open(Path.GetFullPath(OpenProjectPath));

            if (Directory.Exists(Path.GetFullPath(SampleSaveProjectName)))
                Directory.Delete(Path.GetFullPath(SampleSaveProjectName), true);

            var projectSaved = project.SaveAs(Path.GetFullPath(SampleSaveProjectName));

            Program.CompletionMessage($"Project saved successfully to the path : {Path.GetFullPath(SampleSaveProjectName)}");
        }

        public async void SaveAsTrimbleConnect(string projectName, string region, string accessToken)
        {
            var project = ProjectService.Open(Path.GetFullPath(OpenProjectPath));
            var serviceURI = "https://app.connect.trimble.com/tc/api/2.0/"; //Please check with Trimble Connect team for the application service URI
            var connectProjectService = new ConnectProjectService(serviceURI, accessToken);
   

            var connectProject = await connectProjectService.Create(project, region, projectName,"");
            connectProject.Description = "Trimble Connect Sample Description";
            await connectProject.SaveAsync();

            Program.CompletionMessage($"Project saved successfully in Trimble Connect");
        }

        public void RemoveTflx()
        {
            //Open the project 
            var project = ProjectService.Open(Path.GetFullPath(OpenProjectPath));

            if (Directory.Exists(Path.GetFullPath(SampleSaveProjectName)))
                Directory.Delete(Path.GetFullPath(SampleSaveProjectName), true);
      
            var projectSaved = project.SaveAs(Path.GetFullPath(SampleSaveProjectName));
            //Remove Tflx
            projectSaved.Jobs.Remove(projectSaved.Jobs.FirstOrDefault());

            Program.CompletionMessage($"Tflx removed from the project path : {Path.GetFullPath(SampleSaveProjectName)}");
        }

        public void RemoveModel()
        {
            //Open the project 
            var project = ProjectService.Open(Path.GetFullPath(OpenProjectPath));

            if (Directory.Exists(Path.GetFullPath(SampleSaveProjectName)))
                Directory.Delete(Path.GetFullPath(SampleSaveProjectName), true);

            var projectSaved = project.SaveAs(Path.GetFullPath(SampleSaveProjectName));

            //Remove Models
            projectSaved.Models.Remove(projectSaved.Models.FirstOrDefault());

            Program.CompletionMessage($"Model removed from the project path : {Path.GetFullPath(SampleSaveProjectName)}");
        }

        private void CleanTheProject()
        {
            if (Directory.Exists(Path.Combine(this.ProjectPath, this.ProjectName)))
                Directory.Delete(Path.Combine(this.ProjectPath, this.ProjectName), true);
        }
    }
}
