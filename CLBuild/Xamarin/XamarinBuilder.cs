using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace DynamicBuild.CLBuild.Xamarin
{
    public class XamarinBuilder
    {
        private CLCaller Caller { get; set; }

        public string ApkName { get; set; }
        public string PackageName { get; set; }
        /// <summary>
        /// Default: 10
        /// </summary>
        public int VersionCode { get; set; }
        /// <summary>
        /// Default: 1.2.3
        /// </summary>
        public string VersionName { get; set; }
        /// <summary>
        /// Default: "armeabi", "armeabi-v7a", "x86", "arm64-v8a", "x86_64"
        /// </summary>
        public string[] Abis { get; set; }
        /// <summary>
        /// Default: Release
        /// </summary>
        public string Configuration { get; set; }

        /// <summary>
        /// Default: "Properties/AndroidManifest.xml"
        /// </summary>
        public string BuildManifest { get; set; }
        /// <summary>
        /// Example: C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\msbuild.exe
        /// </summary>
        public string MSBuildLocation { get; set; }
        /// <summary>
        /// Example: C:\Android\sdk\build-tools\27.0.3\zipalign.exe
        /// </summary>
        public string ZipAlignLocation { get; set; }
        /// <summary>
        /// Example: C:\Program Files\Java\jdk1.8.0_161\bin\jarsigner.exe
        /// </summary>
        public string JarSignerLocation { get; set; }

        /// <summary>
        /// Output dir
        /// </summary>
        public string OutputPath { get; set; }

        /// <summary>
        /// Example: C:\Git\AndroidApp.Droid
        /// </summary>
        public string AndroidProjectFolder { get; set; }
        /// <summary>
        /// Example: C:\Git\AndroidApp.Droid\\AndroidApp.Mobile.Droid.csproj
        /// </summary>
        public string AndroidProjectFile { get; set; }
        /// <summary>
        /// Example: Keystore.keystore
        /// </summary>
        public string KeystoreFilename { get; set; }
        public string KeystorePassword { get; set; }
        public string KeystoreKey { get; set; }

        /// <summary>
        /// Default: true
        /// </summary>
        public bool DoAlign { get; set; }
        public XamarinBuilder(string packageName,string apkName,string outputPath)
        {
            Caller = new CLCaller();

            ApkName = apkName;
            PackageName = packageName;
            OutputPath = outputPath;
            Configuration = "Release";

            VersionCode = 10;
            VersionName = "1.2.3";
            Abis = new string[] { "armeabi", "armeabi-v7a", "x86", "arm64-v8a", "x86_64" };

            BuildManifest = "Properties/AndroidManifest.xml";
            MSBuildLocation = @"C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\msbuild.exe";
            //MSBuildLocation = @"C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin\msbuild.exe";
            ZipAlignLocation = @"C:\Android\sdk\build-tools\27.0.3\zipalign.exe";
            JarSignerLocation = @"C:\Program Files\Java\jdk1.8.0_161\bin\jarsigner.exe";
            KeystoreFilename = "Keystore.keystore";
            AndroidProjectFolder = @"C:\Git\AndroidApp.Droid";
            AndroidProjectFile = $"{AndroidProjectFolder}\\AndroidApp.Mobile.Droid.csproj";
            DoAlign = true;
        }

        public void BuildApk()
        {
            bool doSign = true;
            if (KeystoreKey == null || KeystorePassword == null)
                doSign = false;

            for (int i = 0; i < Abis.Length; i++)
            {
                var abi = Abis[i];


                var specificManifest = $"{BuildManifest}.{abi}_{VersionCode}.xml";
                var binPath = $"{OutputPath}/{abi}/bin";
                var objPath = $"{OutputPath}/{abi}/obj";

                var keystorePath = $"\"{AndroidProjectFolder}/{KeystoreFilename}\"";

                var xmlFile = XDocument.Load($"{AndroidProjectFolder}/{BuildManifest}");
                var mnfst = xmlFile.Elements("manifest").First();
                var androidNamespace = mnfst.GetNamespaceOfPrefix("android");
                mnfst.Attribute("package").Value = PackageName;
                mnfst.Attribute(androidNamespace + "versionName").Value = VersionName;
                mnfst.Attribute(androidNamespace + "versionCode").Value = ((i + 1) * 100000 + VersionCode).ToString();
                xmlFile.Save($"{AndroidProjectFolder}/{BuildManifest}");

                var unsignedApkPath = $"\"{binPath}/{ApkName}.apk\"";
                var signedApkPath = $"\"{binPath}/{ApkName}_signed.apk\"";
                var alignedApkPath = $"{binPath}/{ApkName}_signed_aligned.{abi}.apk";

                var mbuildArgs = $"{AndroidProjectFile} /t:PackageForAndroid /t:restore /p:AndroidSupportedAbis={abi} /p:Configuration={Configuration} /p:IntermediateOutputPath={objPath}/ /p:OutputPath={binPath}";
                var jarsignerArgs = $"-verbose -sigalg SHA1withRSA -digestalg SHA1 -keystore {keystorePath} -storepass {KeystorePassword} -signedjar \"{signedApkPath}\" {unsignedApkPath} {KeystoreKey}";
                var zipalignArgs = $"-f -v 4 {signedApkPath} {alignedApkPath}";

                Console.WriteLine("MS Building...");
                CallBuild(MSBuildLocation, mbuildArgs);
                Console.WriteLine("MS Build is done");

                if (doSign)
                {
                    Console.WriteLine("Jar signing...");
                    CallBuild(JarSignerLocation, jarsignerArgs);
                    Console.WriteLine("Jar sign is done");
                }
                else
                    Console.WriteLine("Jar signing skipped (Keystore was null)");

                if (doSign)
                {
                    //This is should be the last step otherwise Google Play Store will not accept the APK
                    Console.WriteLine("Zip aligning...");
                    CallBuild(ZipAlignLocation, zipalignArgs);
                    Console.WriteLine("Zip align is done");

                    File.Copy($"{alignedApkPath}", $"{OutputPath}/{Path.GetFileName(alignedApkPath)}", true);
                }
                else
                {
                    Console.WriteLine("Zip aligning skipped (Apk was not signed)");

                    File.Copy($"{unsignedApkPath}", $"{OutputPath}/{Path.GetFileName(unsignedApkPath)}", true);
                }
            }

            Console.WriteLine("Built and signed");
        }

        private void CallBuild(string filename,string args)
        {
            Caller.CurrentCall.StartInfo.FileName = filename;
            Caller.CurrentCall.StartInfo.Arguments = args;
            Caller.BeginCall();
            Caller.WaitForExit();
        }
    }
}
