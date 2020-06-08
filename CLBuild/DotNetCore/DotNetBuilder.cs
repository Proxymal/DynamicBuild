using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicBuild.CLBuild.DotNetCore
{
    public static class DotNetPlatform
    {
        #region Windows
        /// <summary>
        /// Windows
        /// </summary>
        public static object Windows = "win";
        /// <summary>
        /// Windows AOT
        /// </summary>
        public static object WindowsAOT = "win-aot";
        /// <summary>
        /// Windows x64
        /// </summary>
        public static object Windows64Bit = "win-x64";
        /// <summary>
        /// Windows x86
        /// </summary>
        public static object Windows32Bit = "win-x86";
        /// <summary>
        /// Windows arm
        /// </summary>
        public static object WindowsARM = "win-arm";
        /// <summary>
        /// Windows arm64
        /// </summary>
        public static object WindowsARM64Bit = "win-arm64";

        /// <summary>
        /// Windows 7 x64
        /// </summary>
        public static object Windows7_64Bit = "win7-x64";
        /// <summary>
        /// Windows 7 x86
        /// </summary>
        public static object Windows7_32Bit = "win7-x86";

        /// <summary>
        /// Windows 8.1 x64
        /// </summary>
        public static object Windows8_1_64Bit = "win81-x64";
        /// <summary>
        /// Windows 8.1 x86
        /// </summary>
        public static object Windows8_1_32Bit = "win81-x86";
        /// <summary>
        /// Windows 8.1 ARM
        /// </summary>
        public static object Windows8_1_ARM = "win81-arm";

        /// <summary>
        /// Windows 10 x64
        /// </summary>
        public static object Windows10_64Bit = "win10-x64";
        /// <summary>
        /// Windows 10 x86
        /// </summary>
        public static object Windows10_32Bit = "win10-x86";
        /// <summary>
        /// Windows 10 arm
        /// </summary>
        public static object Windows10_ARM = "win10-arm";
        /// <summary>
        /// Windows 10 arm64
        /// </summary>
        public static object Windows10_ARM64Bit = "win10-arm64";
        #endregion

        #region Linux OS
        /// <summary>
        /// Linux x64
        /// </summary>
        public static object Linux64Bit = "linux-x64";
        /// <summary>
        /// Linux musl x64
        /// </summary>
        public static object LinuxMusl64Bit = "linux-musl-x64";

        /// <summary>
        /// Linux arm
        /// </summary>
        public static object LinuxARM = "linux-arm";
        /// <summary>
        /// Linux arm x64
        /// </summary>
        public static object LinuxARM64Bit = "linux-arm64";

        public static object Ol = "ol";
        public static object Ol64Bit = "ol-x64";
        /// <summary>
        /// Red Hat Enterprise Linux x64
        /// </summary>
        public static object Rhel = "rhel";
        /// <summary>
        /// Red Hat Enterprise Linux x64
        /// </summary>
        public static object Rhel64Bit = "rhel-x64";
        /// <summary>
        /// Tizen
        /// </summary>
        public static object Tizen = "tizen";

        public static object FreeBSD = "freebsd";
        public static object FreeBSD64Bit = "freebsd-x64";

        public static object openSUSE = "opensuse";
        public static object CentOS = "centos";
        public static object Debian = "debian";
        public static object Fedora = "fedora";
        public static object Alpine64Bit = "alpine-x64";
        public static object Raspbian = LinuxARM;
        public static object Gentoo = "gentoo";
        public static object Gentoo64Bit = "gentoo-x64";
        public static object Arch = "arch";

        public static object Ubuntu = "ubuntu";
        public static object Ubuntu32Bit = "ubuntu-x86";
        public static object Ubuntu64Bit = "ubuntu-x64";
        public static object Ubuntu_ARM = "ubuntu-arm";
        public static object Ubuntu_ARM64Bit = "ubuntu-arm64";

        public static object tvOS = "tvos";
        public static object tvOS_ARM = "tvos-arm";
        public static object tvOS_ARM64Bit = "tvos-arm64";
        public static object tvOS64Bit = "tvos-x64";
        #endregion

        #region macOS
        /// <summary>
        /// MacOS x64
        /// </summary>
        public static object macOS = "osx";
        /// <summary>
        /// MacOS x64
        /// </summary>
        public static object macOS64Bit = "osx-x64";

        /// <summary>
        /// macOS Yosemite
        /// </summary>
        public static object macOS_Yosemite = "osx.10.10-x64";
        /// <summary>
        /// macOS El Capitan
        /// </summary>
        public static object macOS_ElCapitan = "osx.10.11-x64";
        /// <summary>
        /// macOS Sierra
        /// </summary>
        public static object macOS_Sierra = "osx.10.12-x64";
        /// <summary>
        /// macOS High Sierra
        /// </summary>
        public static object macOS_HighSierra = "osx.10.13-x64";
        /// <summary>
        /// macOS Mojave
        /// </summary>
        public static object macOS_Mojave = "osx.10.14-x64";
        #endregion

        #region Mono
        public static object Any = "any";
        public static object AOT = "aot";
        public static object Browser = "browser";
        public static object BrowserWASM = "browser-wasm";
        public static object Unix = "unix";
        #endregion

        #region Android
        public static object Android = "android";
        public static object Android64Bit = "android-x64";
        public static object Android32Bit = "android-x86";
        public static object Android_ARM = "android-arm";
        public static object Android_ARM64Bit = "android-arm64";
        #endregion

        #region IOS
        public static object IOS = "ios";
        public static object IOS_ARM = "ios-arm";
        public static object IOS_ARM64Bit = "ios-arm64";
        public static object IOS64Bit = "ios-x64";
        public static object IOS32Bit = "ios-x86";
        public static object IOS8 = "ios.8";
        public static object IOS9 = "ios.9";
        public static object IOS10 = "ios.10";
        public static object IOS11 = "ios.11";
        public static object IOS12 = "ios.12";
        public static object IOS13 = "ios.13";
        #endregion
    }

    public class DotNetBuilder
    {
        //Enums
        public enum BuildConfiguration { Release, Debug }
        public enum BuildVerbosity { Quiet, Minimal, Normal, Detailed, Diagnostic }
        public enum ProjectType
        {
            ConsoleApplication = 0,
            ClassLibrary = 1,
            WPFApp = 2,
            WinFormsApp = 3,
            SolutionFile = 4,
            BlazorServerSide = 5,
            AspNetCoreEmpty = 6,
            AspNetCoreMVC = 7,
            UnitTest = 8
        }
        public enum ProjectFramework
        {
            Default = 0,
            NetCoreApp3_0 = 1,
            NetCoreApp3_1 = 2,
            NetStandard2_1 = 3,
            NetStandard2_0 = 4,
        }
        public enum ProjectLanguage
        {
            CSharp = 0,
            VB = 1,
            FSharp = 2
        }

        //Command Caller
        private CmdCLCaller Caller { get; set; }

        public DotNetBuilder()
        {
            Caller = new CmdCLCaller();
        }

        /// <summary>
        /// Execute custom cmd.exe command
        /// </summary>
        /// <param name="cmd">command (without /C)</param>
        /// <returns></returns>
        public string ExecuteCustomCommand(string cmd)
        {
            return Caller.ExecuteCommand(cmd);
        }

        #region Project

        /// <summary>
        /// Create .net project
        /// </summary>
        /// <param name="type"></param>
        /// <param name="lang"></param>
        /// <param name="name"></param>
        /// <param name="dir"></param>
        /// <param name="framework"></param>
        public ResultLog CreateProject(ProjectType type,ProjectLanguage lang, 
            string name,string dir, ProjectFramework framework = ProjectFramework.Default)
        {
            string typeStr = ProjectTypeAsStr(type);
            string frameworkStr = String.Empty;
            string langStr = "--language " + LanguageAsStr(lang);
            string nameStr = "--name " + InQuotes(name);
            string outputStr = "--output " + InQuotes(dir);

            //framework arg
            if (framework != ProjectFramework.Default && (
                typeStr == "classlib" || typeStr == "web" ||
                typeStr == "wpf" || typeStr == "console"))
            {
                frameworkStr = "--framework "+FrameworkAsStr(framework);
            }

            string cmd = "dotnet new " + typeStr + " " + nameStr + " " + outputStr + " " + langStr;
            if (frameworkStr != String.Empty)
                cmd += " " + frameworkStr;

            //execute
            return new ResultLog(Caller.ExecuteCommand(cmd));
        }

        /// <summary>
        /// Build .net project
        /// </summary>
        /// <param name="projFile">Project/Soluton) file (.csproj/.sln)</param>
        /// <param name="outDir">bin directory</param>
        /// <param name="configuration">Build configuration</param>
        /// <param name="platform">DotNetPlatform value</param>
        /// <returns></returns>
        public ResultLog BuildProject(string projFile,string outDir,BuildConfiguration configuration,
            string platform,ProjectFramework framework = ProjectFramework.Default,
            bool withDependencies=true,BuildVerbosity verb = BuildVerbosity.Minimal)
        {
            string outputStr = "--output " + InQuotes(outDir);
            string configurationStr = "--configuration " + ConfigurationAsStr(configuration);
            string frameworkStr = "--framework " + FrameworkAsStr(framework);
            string runtimeStr = "--runtime " + platform;
            string verbosityStr = "--verbosity " + VerbosityAsStr(verb);
            string nodependStr = withDependencies ? "" : " --no-dependencies";


            string cmd = "dotnet build "+InQuotes(projFile)+" --nologo " + outputStr + " " + configurationStr + " " + runtimeStr + " " + verbosityStr + nodependStr;
            if (framework != ProjectFramework.Default)
                cmd += " "+frameworkStr;

            //execute
            return new ResultLog(Caller.ExecuteCommand(cmd));
        }

        /// <summary>
        /// Publish .net project
        /// </summary>
        /// <param name="projFile"></param>
        /// <param name="outDir"></param>
        /// <param name="selfContained"></param>
        /// <param name="framework"></param>
        /// <param name="withDependencies"></param>
        /// <param name="verb"></param>
        /// <returns></returns>
        public ResultLog PublishProject(string projFile,string outDir,
            BuildConfiguration configuration, string platform,bool selfContained=false,
            ProjectFramework framework = ProjectFramework.Default, bool withDependencies = true,
            BuildVerbosity verb= BuildVerbosity.Minimal)
        {
            string outputStr = "--output " + InQuotes(outDir);
            string configurationStr = "--configuration " + ConfigurationAsStr(configuration);
            string frameworkStr = "--framework " + FrameworkAsStr(framework);
            string runtimeStr = "--runtime " + platform;
            string verbosityStr = "--verbosity " + VerbosityAsStr(verb);
            string nodependStr = withDependencies ? "" : " --no-dependencies";
            string selfcontStr = "--self-contained "+BoolAsStr(selfContained);

            string cmd = "dotnet publish " + InQuotes(projFile) + " " + outputStr + " " + configurationStr + " " +verbosityStr + " " + runtimeStr + nodependStr + " " +selfcontStr;
            if (framework != ProjectFramework.Default)
                cmd += " " + frameworkStr;
            //execute
            return new ResultLog(Caller.ExecuteCommand(cmd));
        }

        /// <summary>
        /// Create solution
        /// </summary>
        /// <param name="name"></param>
        /// <param name="dir"></param>
        /// <returns></returns>
        public ResultLog CreateSolution(string name,string dir)
        {
            string typeStr = "sln";
            string nameStr = "--name " + InQuotes(name);
            string outputStr = "--output " + InQuotes(dir);

            string cmd = "dotnet new " + typeStr + " " + nameStr + " " + outputStr;

            //execute
            return new ResultLog(Caller.ExecuteCommand(cmd));
        }
        /// <summary>
        /// Get list of projects in solution
        /// </summary>
        /// <param name="slnFile">path to solution file</param>
        /// <returns></returns>
        public ResultLog GetSolutionProjects(string slnFile)
        {
            string cmd = "dotnet sln " + InQuotes(slnFile) + " list";
            return new ResultLog(Caller.ExecuteCommand(cmd));
        }
        /// <summary>
        /// Add project to solution
        /// </summary>
        /// <param name="slnFile">path to solution file</param>
        /// <param name="projFile">path to project file</param>
        /// <param name="inRoot">place project in root of the solution, rather than creating a solution folder</param>
        /// <returns></returns>
        public ResultLog AddProjectToSolution(string slnFile,string projFile,bool inRoot)
        {
            string cmd = "dotnet sln " + InQuotes(slnFile)+" add";
            cmd += inRoot ? " --in-root" : "";
            return new ResultLog(Caller.ExecuteCommand(cmd));
        }
        #endregion

        #region Utils
        //convertation & utils
        private string ProjectTypeAsStr(ProjectType type)
        {
            if (type == ProjectType.ConsoleApplication)
                return "console";
            else if (type == ProjectType.ClassLibrary)
                return "classlib";
            else if (type == ProjectType.WPFApp)
                return "wpf";
            else if (type == ProjectType.WinFormsApp)
                return "winforms";
            else if (type == ProjectType.SolutionFile)
                return "sln";
            else if (type == ProjectType.BlazorServerSide)
                return "blazorserverside";
            else if (type == ProjectType.AspNetCoreEmpty)
                return "web";
            else if (type == ProjectType.AspNetCoreMVC)
                return "mvc";
            else if (type == ProjectType.UnitTest)
                return "mstest";
            else
                throw new NotImplementedException("Unknown project type");
        }
        private string LanguageAsStr(ProjectLanguage lang)
        {
            if (lang == ProjectLanguage.CSharp)
                return "C#";
            else if (lang == ProjectLanguage.VB)
                return "VB";
            else if (lang == ProjectLanguage.FSharp)
                return "F#";
            else
                throw new NotImplementedException("Unknown language");
        }
        private string FrameworkAsStr(ProjectFramework framework)
        {
            if (framework == ProjectFramework.NetCoreApp3_0)
                return "netcoreapp3.0";
            else if (framework == ProjectFramework.NetCoreApp3_1)
                return "netcoreapp3.1";
            else if (framework == ProjectFramework.NetStandard2_1)
                return "netstandard2.1";
            else if (framework == ProjectFramework.NetStandard2_0)
                return "netstandard2.0";
            else
                throw new NotImplementedException("Unknown framework");
        }
        private string ConfigurationAsStr(BuildConfiguration configuration)
        {
            if (configuration == BuildConfiguration.Release)
                return "Release";
            else if (configuration == BuildConfiguration.Debug)
                return "Debug";
            else
                throw new NotImplementedException("Unknown build configuration");
        }
        private string VerbosityAsStr(BuildVerbosity verb)
        {
            if (verb == BuildVerbosity.Quiet)
                return "quiet";
            else if (verb == BuildVerbosity.Minimal)
                return "minimal";
            else if (verb == BuildVerbosity.Normal)
                return "normal";
            else if (verb == BuildVerbosity.Detailed)
                return "detailed";
            else if (verb == BuildVerbosity.Diagnostic)
                return "diagnostic";
            else
                throw new NotImplementedException("Unknown verbosity level");
        }
        private string BoolAsStr(bool val)
        {
            if (val == true)
                return "true";
            else
                return "false";
        }
        private string InQuotes(string val)
        {
            return '"' + val + '"';
        }
        #endregion
    }
}
