using System;
using System.Threading;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using SDG.Unturned;

namespace FixLoading
{
    public class Hook
    {
        private static AppDomain mainDomain = AppDomain.CurrentDomain;

        public static void hook()
        {
            Debug.Log("Hooking...");
            try
            {
                mainDomain.AssemblyLoad += new AssemblyLoadEventHandler(onASMLoaded);
                Debug.Log("Hook complete!");
            }
            catch (Exception ex)
            {
                Debug.Log("Failed to hook!");
                Debug.LogException(ex);
            }
        }

        private static void onASMLoaded(object sender, AssemblyLoadEventArgs args)
        {
            Debug.Log("Loaded: " + args.LoadedAssembly.FullName);
            if (args.LoadedAssembly.FullName.ToLower().Contains("csharp") && !args.LoadedAssembly.FullName.ToLower().Contains("first"))
            {
                ASMExploit.jmp(typeof(Provider).GetMethod("send", BindingFlags.Public | BindingFlags.Static), typeof(Overridable).GetMethod("send", BindingFlags.Public | BindingFlags.Static));
            }
        }
    }
}
