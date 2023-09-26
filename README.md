INTRODUCTION
	This document focuses on the description of interfaces and classes that are exposed to the client application for using the Trimble FieldLink Project API and TFLX job library.

Steps to install the NuGet package (Internal to Trimble Development Teams)

Note:  Please share the Artifcatory ID to bobby_tait@trimble.com or karthikeyan_thangaraju@trimble.com or  chandru_patcheappane@trimble.com to get access to project APIs (bcfs-fieldlink-nuget)


The package is deployed in the Trimble Artifacts tool, So you have to add artifacts source as a NuGet source in order to consume the package, This can be done via Nuget CLI as detailed below
Run the following commands to add Artifacts to the existing list of NuGet source
NuGet sources Add -Name BcfsFieldlink -Source https://artifactory.trimble.tools/artifactory/api/nuget/bcfs-fieldlink-nuget -username <USERNAME> -password <APIKEY>
NuGet setapikey <USERNAME>:<APIKEY> -Source Artifactory
You can get the APIKEY from your profile page
After this setup you will be access and add package through the Nuget Package Manager screen									
				

	


Please make sure that you are connected through vpn-client when adding/restoring the packages.


Packages need to be installed 

#
Package Name
Description 
1
Trimble.FieldLink.Job
This will be used for FieldLink - TFLX file creation and modifications.

Note: All the dependent packages of Trimble.FieldLink.Job will be installed automatically from Articatory / nuget.Org
2
Trimble.FieldLink.Project
This will be used to create a project and open the existing project. TLFX and Models files and their folder structure will be maintained through this library 

Note: All the dependent packages of Trimble.FieldLink.Project will be installed automatically from Articatory / nuget.Org
3
Trimble.FieldLink.TrimbleConnect.Project
This package will be used to create and save the Field project to Trimble Connect 


4
Trimble.FieldLink.Project.Sample
Sample Project : Provides the sample code explains how to use the Trimble.FieldLink.Job & Trimble.FieldLink.Project


TFLX Job 
FieldLink - TFLX job will be created and modified using the Trimble.FieldLink.Job library. 
JobService is the class used to open an existing job OR create a new project. The below code will describe TFLX job open and creation. OpenJob method will return the IJob interface object 
		
Job - Open

var jobPath = @“C:\Projects\Sample1-Open.Tflx”
var jobservice = new JobService();
 return jobservice.OpenJob(jobPath);


	
Same OpenJob method of the JobServices will be used to create a project. JobService class will 
create the TFLX job in the path specified as the parameter in OpenJob method 

Job - Create

var jobPath = @“C:\Projects\Sample-2-Create.Tflx”
var jobservice = new JobService();
 return jobservice.OpenJob(jobPath);


DesignPoint, Arc, Line, etc. will be added to a TLFX job file using the Add method of the IJob interface. Below sample code describes the same 
	
IJob - Add Methods
Sample Code
Add(DesignPoint point)
var jobPath = @“C:\Projects\Sample1-Open.Tflx”
var jobservice = new JobService();
using var job =  jobservice.OpenJob(jobPath);\

var designPoint = DesignPoint.Create("100", 0, 0, 0, job.DefaultLayer);
 job.Add(designPoint);
job.Save();


Add(Line line)
var jobPath = @“C:\Projects\Sample1-Open.Tflx”
var jobservice = new JobService();
using var job =  jobservice.OpenJob(jobPath);

var startPoint = DesignPoint.Create("100", 0, 0, 0, job.DefaultLayer);
 var endPoint = DesignPoint.Create("101", 0, 0, 0, job.DefaultLayer);
 var line = Line.Create(startPoint, endPoint);

 job.Add(startPoint);
 job.Add(endPoint);
 job.Add(line);
 job.Save();
Add(Arc arc)
var jobPath = @“C:\Projects\Sample1-Open.Tflx”
var jobservice = new JobService();
using var job =  jobservice.OpenJob(jobPath);

var startPoint = DesignPoint.Create("100", 0, 0, 0, job.DefaultLayer);
 var endPoint = DesignPoint.Create("101", 0, 0, 0, job.DefaultLayer);
 var arcPoint = DesignPoint.Create("102", 0, 0, 0, job.DefaultLayer);

var arc = Arc.Create(startPoint, endPoint, arcPoint);

  job.Add(startPoint);
 job.Add(endPoint);
 job.Add(arcPoint);
 job.Add(arc);
 job.Save();
Add(Layer layer)
var jobPath = @“C:\Projects\Sample1-Open.Tflx”
var jobservice = new JobService();
using var job =  jobservice.OpenJob(jobPath);

  var layer = Layer.Create("test");
  job.Add(layer);
 job.Save();


DesignPoint, Arc, Line, etc. will be updated to a TLFX job file using the Update method of the IJob interface. Below sample code describes the same 
	
		
IJob - Update Methods
Sample Code
Update(DesignPoint point)
var jobPath = @“C:\Projects\Sample1-Open.Tflx”
var jobservice = new JobService();
using var job =  jobservice.OpenJob(jobPath);\

var dbDesignPoint = job.DesignPoints[designPoint.Id];
dbDesignPoint.Description = "Test Description";
job.Update(dbDesignPoint);

job.Save();



Update(Line line)
Similar to above sample code. 
Update(Arc arc)
Update(Layer layer)
Update(Line line)
var jobPath = @“C:\Projects\Sample1-Open.Tflx”
var jobservice = new JobService();
using var job =  jobservice.OpenJob(jobPath);

var id=”97b09e8d-5c3e-46a2-91ae-811aa1807c64”; 
var line = job.Lines[Id];

var newStartPoint = DesignPoint.Create("200", 270, 0, 0, job.DefaultLayer);
 var newEndPoint = DesignPoint.Create("300", 300, 0, 0, job.DefaultLayer);
 job.Add(newStartPoint );
 job.Add(newEndPoint );

line.StartPointId = newStartPoint.Id ;
line.EndPointId = newEndPoint.Id; 
job.Update(line );



job.Save();




Project SDK
Project SDK used to create FieldLink project in a specific folder structure 
New project can be created and existing project can be opened using the Project SDK 
Create Project: ProjectService class is used to create a new project and It will return IProject. Below is an example of creating a new project using ProjectService class 

	
Project - Create

//Create Project
           var  projectPath = @"C:\FieldLink\Project";
           var  projectName = @"SampleProject";
           var description = “Sample Description”
           var imagePath = @“C:\SampleData\Project.png” 
            var project = ProjectService.Create(projectPath , projectName, description , imagePath );
            //Add Job
            project.Jobs.Add(Path.GetFullPath(@”C:\SampleData\Jobs\Layout Job-1.tflx"));
            project.Jobs.Add(Path.GetFullPath(@”C:\SampleData\Jobs\Layout Job-2.tflx"));
            project.Jobs.Add(Path.GetFullPath(@”C:\SampleData\Jobs\Layout Job-3.tflx"));
            //Add Model
            project.Models.Add(Path.GetFullPath(@”C:\SampleData\Models\Site-1.dwg"));
            project.Models.Add(Path.GetFullPath(@”C:\SampleData\Models\sydney build.skp"));
            project.Models.Add(Path.GetFullPath(@”C:\SampleData\Models\Tekla Structures.ifc"));
            project.Models.Add(Path.GetFullPath@”C:\SampleData\Models\Trimble Headquarters.dwg"));




Open Project: ProjectService class is used to open an existing project and It will return  IProject. Below is the example of opening a project 


Project - Open

 //Open Project
 var openProjectPath = @"C:\FieldLink\Project";
    var project = ProjectService.Open(Path.GetFullPath(openProjectPath));

            //Iterate Tflx jobs after opening a project 
            foreach (var tflxJob in project.Jobs)
            {
                Console.WriteLine(tflxJob.Name);
            }

            //Iterate models after opening a project 
            foreach (var model in project.Models)
            {
                Console.WriteLine(model.Name);
            }





Save As Project : “Save As” method of the IProject interface is used to copy a project with a different name. 

Project - SaveAs

 var openProjectPath = @"C:\FieldLink\Project";
    var project = ProjectService.Open(Path.GetFullPath(openProjectPath));

           if (Directory.Exists(Path.GetFullPath(SampleSaveProjectName)))
                Directory.Delete(Path.GetFullPath(SampleSaveProjectName), true);
        
     var copyProjectName = @"C:\FieldLink\Project-Copy";
     var projectSaved = project.SaveAs(Path.GetFullPath(copyProjectName ));

 //Iterate Tflx jobs after opening a project 
            foreach (var tflxJob in projectSaved .Jobs)
            {
                Console.WriteLine(tflxJob.Name);
            }

            //Iterate models after opening a project 
            foreach (var model in projectSaved .Models)
            {
                Console.WriteLine(model.Name);
            }

Add modules: Add method of the IProject module collection interface is used to add the modules like Tflx, Models, etc from a project 


Project - Add Module

       var openProjectPath = @"C:\FieldLink\Project";
        var project = ProjectService.Open(Path.GetFullPath(openProjectPath));

          //Add Tflx
            project.Jobs.Add(Path.GetFullPath(@”C:\SampleData\Jobs\Layout Job-1.tflx"));

          //Add Models
           project.Models.Add(Path.GetFullPath(@”C:\SampleData\Models\Site-1.dwg"));





Remove modules: Remove method of the IProject module collection interface is used to remove/delete the modules like Tflx, Models, etc from a project 

Project - Remove Module

       var openProjectPath = @"C:\FieldLink\Project";
        var project = ProjectService.Open(Path.GetFullPath(openProjectPath));

          //Remove Tflx
            project .Jobs.Remove(projectSaved.Jobs.FirstOrDefault());

          //Remove Models
            project .Models.Remove(projectSaved.Models.FirstOrDefault());






