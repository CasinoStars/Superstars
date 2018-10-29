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


namespace CodeCake
{
    public partial class Build
    {
        void StandardCreateZip( DirectoryPath releasesDir, IEnumerable<SolutionProject> projectsToPublish, SimpleRepositoryInfo gitInfo, string configuration )
        {
            var settings = new DotNetCorePackSettings().AddVersionArguments( gitInfo, c =>
            {
                // Waiting for netcore 2.1 (https://github.com/dotnet/cli/issues/5331).
                // Setting nobuild and BuildProjectReferences=false
                // IsPackable=true is required for Tests package. Without this Pack on Tests projects does not generate nupkg.
                c.ArgumentCustomization += args => args.Append( "/p:IsPackable=true" )
                                                       .Append( "/p:BuildProjectReferences=false" );
                c.NoBuild = true;
                c.IncludeSymbols = true;
                c.Configuration = configuration;
                c.OutputDirectory = releasesDir;
            } );

             foreach( SolutionProject p in projectsToPublish )
            {
                if(p.Name == "Superstars.WebApp")
                {
                    Cake.NpmInstall(new NpmInstallSettings()
                    {
                        WorkingDirectory = p.Path.GetDirectory() + "/App/all-in/"
                    });
                    Cake.NpmRunScript(new NpmRunScriptSettings()
                    {
                        ScriptName = "prod",
                        WorkingDirectory = p.Path.GetDirectory() + "/App/all-in/"
                    });
                   
                    
                    
                    Cake.Zip(p.Path.GetDirectory() + "/bin/Debug/netcoreapp2.1/publish", "WebApp."+ gitInfo.SafeSemVersion + ".zip");
                }
                if(p.Name == "Superstars.DB")
                {
                    Cake.Zip(p.Path.GetDirectory() + "/bin/Debug", "DB."+ gitInfo.SafeSemVersion + ".zip");
                }

                //     C: \Users\Albin\DEV\Superstars\src\Superstars.WebApp\bin\Debug\netcoreapp2.1 + "/bin/Debug/netcorapp2.1"
                //    Cake.Information( p.Path.GetDirectory().FullPath );
                //   Cake.Zip(p.Path.GetDirectory(), "Test.zip");
                //    if(p.Name == 
                //Cake.Zip(System.IO.Directory.GetParent(System.IO.Directory.GetParent(p.Path.FullPath).FullName) + "\\Superstars.DB\\bin\\Debug\\net461", "Test.zip" );

            }
        }
    }
}
