using Cake.Common.Diagnostics;
using Cake.Common.IO;
using Cake.Common.Solution;
using Cake.Common.Tools.DotNetCore;
using Cake.Common.Tools.DotNetCore.Pack;
using Cake.Core;
using Cake.Core.IO;
using Cake.Npm;
using Cake.Npm.Install;
using Cake.Npm.RunScript;
using SimpleGitVersion;
using System.Collections.Generic;
using System.Reflection;
using Cake.Common.Tools.OctopusDeploy;


namespace CodeCake
{
    public partial class Build
    {
        void StandardDeploy( DirectoryPath releasesDir, IEnumerable<SolutionProject> projectsToPublish, SimpleRepositoryInfo gitInfo, string configuration )
        {
            List<FilePath> filePaths = new List<FilePath>();
            foreach (SolutionProject p in projectsToPublish)
            {
                if (p.Name == "Superstars.WebApp")
                {
                    filePaths.Add(new FilePath("WebApp." + gitInfo.SafeSemVersion + ".zip"));
                }
                if (p.Name == "Superstars.DB")
                {
                    filePaths.Add(new FilePath("DB." + gitInfo.SafeSemVersion + ".zip"));
                }
            }

            OctopusDeployAliases.OctoPush(Cake, "http://octo.francecentral.cloudapp.azure.com", "API-SQSHNGIU7ACDBGGWDWQQ7S9RZOQ", filePaths, new OctopusPushSettings());
            CreateReleaseSettings releaseSettings = new CreateReleaseSettings();
            releaseSettings.ReleaseNumber = gitInfo.SafeSemVersion;
            releaseSettings.ApiKey = "API-SQSHNGIU7ACDBGGWDWQQ7S9RZOQ";
            releaseSettings.Server = "http://octo.francecentral.cloudapp.azure.com";
            releaseSettings.DefaultPackageVersion = gitInfo.SafeSemVersion;
            OctopusDeployAliases.OctoCreateRelease(Cake, "All'in", releaseSettings);
        }
    }
}
