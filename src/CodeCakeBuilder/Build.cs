using Cake.Common.IO;
using Cake.Common.Solution;
using Cake.Core;
using Cake.Core.Diagnostics;
using SimpleGitVersion;
using System.Linq;
using Cake.Json;
using System;

namespace CodeCake
{
    /// <summary>
    /// Standard build "script".
    /// </summary>
    [AddPath( "%UserProfile%/.nuget/packages/**/tools*" )]
    public partial class Build : CodeCakeHost
    {
        public Build()
        {
            Cake.Log.Verbosity = Verbosity.Diagnostic;

            const string solutionName = "../CasinoSuperstars";
            const string solutionFileName = solutionName + ".sln";

            var releasesDir = Cake.Directory( "CodeCakeBuilder/Releases" );

            var projects = Cake.ParseSolution( solutionFileName )
                           .Projects
                           .Where( p => !(p is SolutionFolder)
                                        && p.Name != "CodeCakeBuilder" );
             
            // We do not publish Tests and Samples projects for this solution.
            var projectsToPublish = projects
                                        .Where(p => !p.Path.Segments.Contains("Tests"))
                                        .Where(p => !p.Path.Segments.Contains("Samples"));
        
            var webAppPath = projectsToPublish.FirstOrDefault(p => p.Name == "Superstars.WebApp").Path;
            if (webAppPath == null) throw new InvalidOperationException("WebApp is missing or not found"); 

            
            SimpleRepositoryInfo gitInfo = Cake.GetSimpleRepositoryInfo();

            // Configuration is either "Debug" or "Release".
            string configuration = "Debug";

            Task("Check-Repository")
                .Does(() =>
               {
                   configuration = StandardCheckRepository(projectsToPublish, gitInfo);
               });

            Task( "Clean" )
                .IsDependentOn( "Check-Repository" )
                .Does( () =>
                 {
                    
                     Cake.CleanDirectories( projects.Select( p => p.Path.GetDirectory().Combine( "bin" ) ) );
                     Cake.CleanDirectories( releasesDir );
                     Cake.DeleteFiles( "Tests/**/TestResult*.xml" );
                    
                 } );

         

            Task( "Build" )
                .IsDependentOn( "Clean" )
                .Does( () =>
                {
                    StandardSolutionBuild( solutionFileName, gitInfo, configuration );
                } );

            Task("Publish")
             .IsDependentOn("Build")   
             .Does(() =>
                {
                     PublishWebApp(webAppPath.ToString());
                 }
                 );

            Task( "Unit-Testing" )
                .IsDependentOn( "Publish" )
                .Does( () =>
                {
                    StandardUnitTests( configuration, projects.Where( p => p.Name.EndsWith( ".Tests" ) ) );
                } );

            Task( "Create-Zip" )
                .WithCriteria( () => gitInfo.IsValid )
                .IsDependentOn( "Unit-Testing" )
                .Does( () =>
                {
                    StandardCreateZip( releasesDir, projectsToPublish, gitInfo, configuration );
                } );
            Task("Deploy")
                .WithCriteria(() => gitInfo.IsValid)
                .IsDependentOn("Create-Zip")
                .Does(() =>
                {
                    StandardDeploy(releasesDir, projectsToPublish, gitInfo, configuration);
                });

            // The Default task for this script can be set here.
            Task( "Default" )
                .IsDependentOn("Deploy");

        }
    }
}
