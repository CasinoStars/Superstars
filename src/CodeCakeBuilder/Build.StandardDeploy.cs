using System.Collections.Generic;
using Cake.Common.Solution;
using Cake.Common.Tools.OctopusDeploy;
using Cake.Core.IO;
using SimpleGitVersion;

namespace CodeCake
{
    public partial class Build
    {
        private void StandardDeploy(DirectoryPath releasesDir, IEnumerable<SolutionProject> projectsToPublish,
            SimpleRepositoryInfo gitInfo, string configuration)
        {
            var filePaths = new List<FilePath>();
            foreach (var p in projectsToPublish)
            {
                if (p.Name == "Superstars.WebApp")
                    filePaths.Add(new FilePath("WebApp." + gitInfo.SafeSemVersion + ".zip"));

                if (p.Name == "Superstars.DB") filePaths.Add(new FilePath("DB." + gitInfo.SafeSemVersion + ".zip"));
            }

            Cake.OctoPush("http://octo.francecentral.cloudapp.azure.com",
                "API-SQSHNGIU7ACDBGGWDWQQ7S9RZOQ", filePaths, new OctopusPushSettings());
        }
    }
}