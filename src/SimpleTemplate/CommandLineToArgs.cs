﻿
namespace SimpleTemplate
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Command line parser
    /// </summary>
    /// <remarks>// http://stackoverflow.com/questions/298830/split-string-containing-command-line-parameters-into-string-in-</remarks>
    public static class CommandLine
    {
        [DllImport("shell32.dll", SetLastError = true)]
        static extern IntPtr CommandLineToArgvW([MarshalAs(UnmanagedType.LPWStr)] string lpCmdLine, out int pNumArgs);

        public static string[] ToArgs(string commandLine)
        {
            int argc;
            var argv = CommandLineToArgvW(commandLine, out argc);

            if (argv == IntPtr.Zero)
            {
                throw new System.ComponentModel.Win32Exception();
            }

            try
            {
                var args = new string[argc];

                for (var i = 0; i < args.Length; i++)
                {
                    var p = Marshal.ReadIntPtr(argv, i * IntPtr.Size);
                    args[i] = Marshal.PtrToStringUni(p);
                }

                return args;
            }
            finally
            {
                Marshal.FreeHGlobal(argv);
            }
        }
    }
}
