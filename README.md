INTRODUCTION
	This document focuses on the description of interfaces and classes that are exposed to the client application for using the Trimble FieldLink Project API and TFLX job library.

Steps to install the NuGet package (Internal to Trimble Development Teams)

Note:  Please share the Artifcatory ID to bobby_tait@trimble.com or karthikeyan_thangaraju@trimble.com or  chandru_patcheappane@trimble.com to get access to project APIs (bcfs-fieldlink-nuget)

The package is deployed in the Trimble Artifacts tool, So you have to add artifacts source as a NuGet source in order to consume the package, This can be done via Nuget CLI as detailed below
a. Run the following commands to add Artifacts to the existing list of NuGet source
   1. NuGet sources Add -Name BcfsFieldlink -Source https://artifactory.trimble.tools/artifactory/api/nuget/bcfs-fieldlink-nuget -username <USERNAME> -password <APIKEY>
   2. NuGet setapikey <USERNAME>:<APIKEY> -Source Artifactory
   3. You can get the APIKEY from your profile page
b. After this setup you will be access and add package through the Nuget Package Manager screen
