
Steps to install the NuGet package (Internal to Trimble Development Teams)

Note:  Please share the Artifcatory ID to bobby_tait@trimble.com or karthikeyan_thangaraju@trimble.com or  chandru_patcheappane@trimble.com to get access to project APIs (bcfs-fieldlink-nuget)

1. The package is deployed in the Trimble Artifacts tool, So you have to add artifacts source as a NuGet source in order to consume the package, This can be done via Nuget CLI as detailed below
	a. Run the following commands to add Artifacts to the existing list of NuGet source
	   1. NuGet sources Add -Name BcfsFieldlink -Source https://artifactory.trimble.tools/artifactory/api/nuget/bcfs-fieldlink-nuget -username <USERNAME> -password <APIKEY>
	   2. NuGet setapikey <USERNAME>:<APIKEY> -Source Artifactory
	   3. You can get the APIKEY from your profile page
	b. After this setup you will be access and add package through the Nuget Package Manager screen
	![image](https://github.com/karthikeyanthangaraju/FieldLinkProjectSDKSample/assets/126872854/bd014021-5c16-4d25-b462-ebf82ae8e127)
2. Please make sure that you are connected through vpn-client when adding/restoring the packages.

Packages need to be installed 
![image](https://github.com/karthikeyanthangaraju/FieldLinkProjectSDKSample/assets/126872854/23b4b8ff-0577-4f33-b937-5aa57543d277)

