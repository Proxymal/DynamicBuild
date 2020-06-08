using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DynamicBuild.CLBuild
{
    public class CLCaller
    {
        public Process CurrentCall { get; set; }
        public string CLPath { get; set; }

        public CLCaller(string clPath="cmd.exe",string workingDir=@"C:\",bool visible=false)
        {
            CurrentCall = new Process();
            CLPath = clPath;

            CurrentCall.StartInfo = new ProcessStartInfo(clPath);
            CurrentCall.StartInfo.CreateNoWindow = !visible;
            CurrentCall.StartInfo.UseShellExecute = false;
            CurrentCall.StartInfo.RedirectStandardInput = true;
            CurrentCall.StartInfo.RedirectStandardOutput = true;
            CurrentCall.StartInfo.RedirectStandardError = true;
            CurrentCall.StartInfo.WorkingDirectory = workingDir;
            CurrentCall.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
        }

        public void SetCallArguments(string args)
        {
            CurrentCall.StartInfo.Arguments = args;
        }

        public void BeginCall()
        {
            CurrentCall.Start();
        }
        public void BeginCall(string args)
        {
            SetCallArguments(args);
            CurrentCall.Start();
        }

        public void CallInputWrite(string toWrite)
        {
            if(!CurrentCall.HasExited)
            {
                CurrentCall.StandardInput.Write(toWrite);
            }
        }
        public void CallInputWriteLine(string toWrite)
        {
            if (!CurrentCall.HasExited)
            {
                CurrentCall.StandardInput.WriteLine(toWrite);
            }
        }
        public void CallInputFlush()
        {
            if (!CurrentCall.HasExited)
            {
                CurrentCall.StandardInput.Flush();
            }
        }
        public void CallInputClose()
        {
            if (!CurrentCall.HasExited)
            {
                CurrentCall.StandardInput.Close();
            }
        }

        public string CallOutputReadAll()
        {
            if (!CurrentCall.HasExited)
            {
                return CurrentCall.StandardOutput.ReadToEnd();
            }
            return "";
        }
        public string CallOutputReadLine()
        {
            if (!CurrentCall.HasExited)
            {
                return CurrentCall.StandardOutput.ReadLine();
            }
            return "";
        }
        public int CallOutputRead()
        {
            if (!CurrentCall.HasExited)
            {
                return CurrentCall.StandardOutput.Read();
            }
            return 0;
        }
        public int CallOutputReadBlock(char[] buffer,int index,int count)
        {
            if (!CurrentCall.HasExited)
            {
                return CurrentCall.StandardOutput.ReadBlock(buffer,index,count);
            }
            return 0;
        }

        public void WaitForExit()
        {
            CurrentCall.WaitForExit();
        }
        public void EndCall()
        {
            CurrentCall.Kill();
        }
    }
}
