using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicBuild.CLBuild
{
    public class CmdCLCaller
    {
        public CLCaller Caller { get; set; }

        public CmdCLCaller()
        {
            Caller = new CLCaller();
        }

        public string ExecuteCommand(string cmd)
        {
            Caller.BeginCall("/C " + cmd);
            return Caller.CallOutputReadAll();
        }
        public string[] ExecuteCommands(string[] cmds)
        {
            Caller.BeginCall();

            List<string> outputs = new List<string>();
            foreach (string cmd in cmds)
            {
                Caller.CurrentCall.StandardInput.Write(cmd);
                outputs.Add(Caller.CallOutputReadAll());
            }

            return outputs.ToArray();
        }
    }
}
