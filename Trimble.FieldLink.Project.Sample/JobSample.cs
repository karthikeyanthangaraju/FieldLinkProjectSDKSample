using System;
using System.IO;
using System.Windows.Forms;
using Trimble.FieldLink.Data;
using Trimble.FieldLink.Job;
using Trimble.FieldLink.Job.Interface;

namespace Trimble.FieldLink.ProjectAPI.Sample
{
    public class JobSample
    {
        private const string JobPath = "Sample.tflx";

        public JobSample()
        {
            if(File.Exists(JobPath))
                File.Delete(JobPath);
        }

        private IJob OpenJob()
        {
            var jobservice = new JobService();
            return jobservice.OpenJob(JobPath);
        }

        #region Create
        public void AddDesignPoint()
        {
            using (var job = this.OpenJob())
            {
                var designPoint = DesignPoint.Create("100", 0, 0, 0, job.DefaultLayer);
                job.Add(designPoint);
                job.Save();
            }

            Program.CompletionMessage($"Design point added to the tflx file in the path: {Path.GetFullPath(JobPath)}");
        }

        public void AddLine()
        {
            using (var job = this.OpenJob())
            {
                var startPoint = DesignPoint.Create("100", 0, 0, 0, job.DefaultLayer);
                var endPoint = DesignPoint.Create("101", 0, 0, 0, job.DefaultLayer);
                var line = Line.Create(startPoint, endPoint);

                job.Add(startPoint);
                job.Add(endPoint);
                job.Add(line);
                job.Save();
            }

            Program.CompletionMessage($"Line added to the tflx file in the path: {Path.GetFullPath(JobPath)}");
        }

        public void AddArc()
        {
            using (var job = this.OpenJob())
            {
                var startPoint = DesignPoint.Create("100", 0, 0, 0, job.DefaultLayer);
                var endPoint = DesignPoint.Create("101", 0, 0, 0, job.DefaultLayer);
                var arcPoint = DesignPoint.Create("102", 0, 0, 0, job.DefaultLayer);

                var arc = Arc.Create(startPoint, endPoint, arcPoint);

                job.Add(startPoint);
                job.Add(endPoint);
                job.Add(arcPoint);
                job.Add(arc);
                job.Save();
            }
            Program.CompletionMessage($"Arc added to the tflx file in the path: {Path.GetFullPath(JobPath)}");
        }

        public void AddLayer()
        {
            using (var job = this.OpenJob())
            {
                var layer = Layer.Create("test");
                job.Add(layer);
                job.Save();
            }

            Program.CompletionMessage($"Layer added to the tflx file in the path: {Path.GetFullPath(JobPath)}");
        }
        #endregion

        #region Update
        public void UpdateDesignPoint()
        {
            using (var job = this.OpenJob())
            {
                var designPoint = DesignPoint.Create("100", 0, 0, 0, job.DefaultLayer);
                job.Add(designPoint);
                job.Save();

                var dbDesignPoint = job.DesignPoints[designPoint.Id];
                dbDesignPoint.Description = "Test Description";
                job.Update(dbDesignPoint);

                job.Save();
            }

            Program.CompletionMessage($"DesingPoint updated successfully in the tflx file path: {Path.GetFullPath(JobPath)}");
        }

        public void UpdateLayer()
        {
            using (var job = this.OpenJob())
            {
                var layer = Layer.Create("test");
                job.Add(layer);
                job.Save();

                var dbLayer = job.Layers[layer.Id];
                dbLayer.IsVisible = false;
                job.Update(dbLayer);
                job.Save();
            }

            Program.CompletionMessage($"Layer updated successfully in the tflx file path: {Path.GetFullPath(JobPath)}");
        }
        #endregion

        #region Delete
        public void RemoveDesignPoint()
        {
            using (var job = this.OpenJob())
            {
                var designPoint1 = DesignPoint.Create("100", 0, 0, 0, job.DefaultLayer);
                var designPoint2 = DesignPoint.Create("101", 0, 0, 0, job.DefaultLayer);
                job.Add(designPoint1);
                job.Add(designPoint2);
                job.Save();

                //Remove Design Point
                job.Remove(designPoint1);
                job.Save();
            }

            Program.CompletionMessage($"DesignPoint removed from the tflx file path: {Path.GetFullPath(JobPath)}");
        }

        public void RemoveLine()
        {
            using (var job = this.OpenJob())
            {
                //Create Start and End Point  - Line-1         
                var startPoint = DesignPoint.Create("100", 0, 0, 0, job.DefaultLayer);
                var endPoint = DesignPoint.Create("101", 0, 0, 0, job.DefaultLayer);
                // Add Line
                var line = Line.Create(startPoint, endPoint);
                job.Add(startPoint);
                job.Add(endPoint);
                job.Add(line);
                //Save
                job.Save();

                //Create Start and End Point  - Line-2
                var startPoint2 = DesignPoint.Create("103", 0, 0, 0, job.DefaultLayer);
                var endPoint2 = DesignPoint.Create("104", 0, 0, 0, job.DefaultLayer);
                // Add Line
                var line2 = Line.Create(startPoint2, endPoint2);
                job.Add(startPoint2);
                job.Add(endPoint2);
                job.Add(line2);
                //Save
                job.Save();

                // Remove Line-1
                job.Remove(job.Lines[line.Id]);
                job.Save();
            }
            Program.CompletionMessage($"Line removed from the tflx file path: {Path.GetFullPath(JobPath)}");
        }

        public void RemoveArc()
        {
            using (var job = this.OpenJob())
            {
                // adding 1st arc
                var startPoint = DesignPoint.Create("100", 0, 0, 0, job.DefaultLayer);
                var endPoint = DesignPoint.Create("101", 0, 0, 0, job.DefaultLayer);
                var arcPoint = DesignPoint.Create("102", 0, 0, 0, job.DefaultLayer);
                var arc = Arc.Create(startPoint, endPoint, arcPoint);
                job.Add(startPoint);
                job.Add(endPoint);
                job.Add(arcPoint);
                job.Add(arc);
                job.Save();

                // adding 2nd arc
                var startPoint2 = DesignPoint.Create("103", 10, 0, 0, job.DefaultLayer);
                var endPoint2 = DesignPoint.Create("104", 10, 0, 0, job.DefaultLayer);
                var arcPoint2 = DesignPoint.Create("105", 10, 0, 0, job.DefaultLayer);

                var arc2 = Arc.Create(startPoint2, endPoint2, arcPoint2);
                job.Add(startPoint2);
                job.Add(endPoint2);
                job.Add(arcPoint2);
                job.Add(arc2);
                job.Save();

                // remove arc
                job.Remove(job.Arcs[arc.Id]);
                job.Save();
            }
            Program.CompletionMessage($"Arc removed from the tflx file path: {Path.GetFullPath(JobPath)}");
        }
        #endregion

        #region Retrieve
        public void GetDesignPoints()
        {
            using (var job = this.OpenJob())
            {
                var designPoint1 = DesignPoint.Create("100", 0, 0, 0, job.DefaultLayer);
                job.Add(designPoint1);

                var designPoint2 = DesignPoint.Create("101", 0, 0, 0, job.DefaultLayer);
                job.Add(designPoint2);

                job.Save();

                //Retrieve a Designpoint
                var dbDesignPoint = job.DesignPoints[designPoint1.Id];

                //Retrieve a all the Designpoints
                var dbDesignPoints = job.DesignPoints;
            }
            Program.CompletionMessage($"DesignPoints retrieved from the tflx under the path: {JobPath}");

        }

        public void GetLines()
        {
            using (var job = this.OpenJob())
            {
                // adding 1st line
                var startPoint = DesignPoint.Create("100", 0, 0, 0, job.DefaultLayer);
                var endPoint = DesignPoint.Create("101", 0, 0, 0, job.DefaultLayer);

                var line1 = Line.Create(startPoint, endPoint);
                job.Add(startPoint);
                job.Add(endPoint);
                job.Add(line1);
                job.Save();

                // adding 2nd line
                var startPoint2 = DesignPoint.Create("103", 0, 0, 0, job.DefaultLayer);
                var endPoint2 = DesignPoint.Create("104", 0, 0, 0, job.DefaultLayer);

                var line2 = Line.Create(startPoint2, endPoint2);
                job.Add(startPoint2);
                job.Add(endPoint2);
                job.Add(line2);
                job.Save();

                //Retrieve a line
                var line = job.Lines.Find(l => l.Id == line1.Id);

                //Retrieve all the lines
                var lines = job.Lines;
            }
            Program.CompletionMessage($"Lines retrieved from the tflx under the path: {JobPath}");
        }
        #endregion

        #region AprilTag
        public void AddAprilTagWithPoints()
        {
            // Below 3 step process helps to add a AprilTag points to a tflx database.
            using (var job = this.OpenJob())
            {
                // Step-1 : Add April Tag
                var aprilTag = AprilTag.Create("11");
                job.Add(aprilTag);
                job.Save();

                // Step-2 : Add DesignPoint as AprilTag Measurement Point
                var topLeftPoint = DesignPoint.Create("101", 0, 10, 0, job.DefaultLayer,null,PointType.AprilTag);
                job.Add(topLeftPoint);

                var topRightPoint = DesignPoint.Create("102", 10, 10, 0, job.DefaultLayer, null, PointType.AprilTag);
                job.Add(topRightPoint);

                var bottomRightPoint = DesignPoint.Create("103", 10, 0, 0, job.DefaultLayer, null, PointType.AprilTag);
                job.Add(bottomRightPoint);

                //Special Note : BottomLeft Point is visible to user and it has to added as DesignPoint point type  
                //               and all other above points (TopLeft, TopRight, BottomRight) are added as AprilTag point type is the key difference 
                var bottomLeftPoint = DesignPoint.Create("104", 0, 0, 0, job.DefaultLayer, null, PointType.Design);
                job.Add(bottomLeftPoint);
                job.Save();

                // Step-3 : Link the DesignPoint and AprilTag using AprilTagMeasurement 
                var topLeftAprilTagMeasurement = AprilTagMeasurement.Create(aprilTag.Id, topLeftPoint.Id, LocationType.TopLeft);
                job.Add(topLeftAprilTagMeasurement);

                var topRightAprilTagMeasurement = AprilTagMeasurement.Create(aprilTag.Id, topRightPoint.Id, LocationType.TopRight);
                job.Add(topRightAprilTagMeasurement);

                var bottomRightAprilTagMeasurement = AprilTagMeasurement.Create(aprilTag.Id, bottomRightPoint.Id, LocationType.BottomRight);
                job.Add(bottomRightAprilTagMeasurement);

                var bottomLeftAprilTagMeasurement = AprilTagMeasurement.Create(aprilTag.Id, bottomLeftPoint.Id, LocationType.BottomLeft);
                job.Add(bottomLeftAprilTagMeasurement);
                job.Save();
            }

            Program.CompletionMessage($"AprilTags added to the tflx file in the path: {Path.GetFullPath(JobPath)}");
        }


        public void RetrieveAprilTagPoints()
        {
            using (var job = this.OpenJob())
            {
                // Step-1 : Add April Tag
                var aprilTag = AprilTag.Create("11");
                job.Add(aprilTag);
                job.Save();

                // Step-2 : Add DesignPoint as AprilTag Measurement Point
                var topLeftPoint = DesignPoint.Create("101", 0, 10, 0, job.DefaultLayer, null, PointType.AprilTag);
                job.Add(topLeftPoint);

                var topRightPoint = DesignPoint.Create("102", 10, 10, 0, job.DefaultLayer, null, PointType.AprilTag);
                job.Add(topRightPoint);

                var bottomRightPoint = DesignPoint.Create("103", 10, 0, 0, job.DefaultLayer, null, PointType.AprilTag);
                job.Add(bottomRightPoint);

                //Special Note : BottomLeft Point is visible to user and it has to added as DesignPoint point type  
                //               and all other above points (TopLeft, TopRight, BottomRight) are added as AprilTag point type is the key difference 
                var bottomLeftPoint = DesignPoint.Create("104", 0, 0, 0, job.DefaultLayer, null, PointType.Design);
                job.Add(bottomLeftPoint);
                job.Save();

                // Step-3 : Link the DesignPoint and AprilTag using AprilTagMeasurement 
                var topLeftAprilTagMeasurement = AprilTagMeasurement.Create(aprilTag.Id, topLeftPoint.Id, LocationType.TopLeft);
                job.Add(topLeftAprilTagMeasurement);

                var topRightAprilTagMeasurement = AprilTagMeasurement.Create(aprilTag.Id, topRightPoint.Id, LocationType.TopRight);
                job.Add(topRightAprilTagMeasurement);

                var bottomRightAprilTagMeasurement = AprilTagMeasurement.Create(aprilTag.Id, bottomRightPoint.Id, LocationType.BottomRight);
                job.Add(bottomRightAprilTagMeasurement);

                var bottomLeftAprilTagMeasurement = AprilTagMeasurement.Create(aprilTag.Id, bottomLeftPoint.Id, LocationType.BottomLeft);
                job.Add(bottomLeftAprilTagMeasurement);
                job.Save();

                // Below are the steps to retrieve the  
                // Retrieve the AprilTags
                var aprilTagsFromDB = job.AprilTags;
                foreach (var aprilTagFromDB in aprilTagsFromDB)
                {
                    Console.WriteLine(aprilTagFromDB.TagId);
                }

                //  Retrieve the AprilTag Measurement, Location Type and Location
                var specificAprilTag = job.AprilTags.FindEntity(s => s.TagId == "11");
                foreach (var aprilTagMeasurement in specificAprilTag.AprilTagMeasurements)
                {
                    Console.WriteLine($"AprilTag:{specificAprilTag.TagId}, " +
                                      $"Location Type:{aprilTagMeasurement.LocationType}, " +
                                      $"Location:({aprilTagMeasurement.DesignPoint.X},{aprilTagMeasurement.DesignPoint.Y},{aprilTagMeasurement.DesignPoint.Z})" );
                }

            }

            Program.CompletionMessage($"AprilTag added and retrieved from the tflx file in the path: {Path.GetFullPath(JobPath)}");
        }
        #endregion
    }
}
