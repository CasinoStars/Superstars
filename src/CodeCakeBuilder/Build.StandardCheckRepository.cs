using System.Collections.Generic;
using System.Linq;
using Cake.Common.Build;
using Cake.Common.Diagnostics;
using Cake.Common.Solution;
using CK.Text;
using SimpleGitVersion;

namespace CodeCake
{
    public partial class Build
    {
        private string StandardCheckRepository(IEnumerable<SolutionProject> projectsToPublish,
            SimpleRepositoryInfo gitInfo)
        {
            var configuration = "Debug";
            if (!gitInfo.IsValid)
            {
                if (Cake.InteractiveMode() == InteractiveMode.Interactive
                    && Cake.ReadInteractiveOption("PublishDirtyRepo",
                        "Repository is not ready to be published. Proceed anyway?", 'Y', 'N') == 'Y')
                {
                    Cake.Warning("GitInfo is not valid, but you choose to continue...");
                }
                else
                {
                    // On Appveyor, we let the build be done: this gracefully handles Pull Requests.
                    if (!Cake.AppVeyor().IsRunningOnAppVeyor)
                        Cake.TerminateWithError("Repository is not ready to be published.");
                }
            }

            if (gitInfo.IsValidRelease
                && (gitInfo.PreReleaseName.Length == 0 || gitInfo.PreReleaseName == "rc"))
                configuration = "Release";

            Cake.Information("Publishing {0} projects with version={1} and configuration={2}: {3}",
                projectsToPublish.Count(),
                gitInfo.SafeSemVersion,
                configuration,
                projectsToPublish.Select(p => p.Name).Concatenate());
            return configuration;
        }
    }
}