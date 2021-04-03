using System;
using System.Runtime.InteropServices;

namespace T9_EasyAim.Memory
{
    internal class ImportProtection
    {
        [DllImport("kernelbase.dll")]
        public static extern bool ReadProcessMemory(IntPtr hProcess, Int64 lpBaseAddress, [In, Out] byte[] buffer, UInt32 size, out int lpNumberOfBytesWritten);

        [DllImport("kernelbase.dll")]
        public static extern Int64 GetProcAddress(Int64 hModule, string lpProcName);

        [DllImport("kernelbase.dll")]
        public static extern IntPtr GetCurrentProcess();

        [DllImport("kernelbase.dll")]
        public static extern Int64 GetModuleHandleA(string lpModuleName);

        public struct Adresses 
        {
            public const UInt64 GodMode = 0x666;
            public const UInt64 Ammo = 0x666;

            //Usage: ImportProtection prrotection = new ImportProtection();
            //protection.Adresses.GodMode;
        }
        public bool IsMemorySafe()
        {
            //7FFAB1A5C4F0
            try
            {
                byte[] WPC_Bytes = new byte[16];
                byte[] RPC_Bytes = new byte[16];
                byte[] NormalShitWPM = { 0x48, 0x8B, 0xC4, 0x48, 0x89, 0x58, 0x08, 0x4C, 0x89, 0x48, 0x20, 0x4C, 0x89, 0x40, 0x18, 0x48 };
                byte[] NormalShitRPM = { 0x48, 0x83, 0xEC, 0x48, 0x48, 0x8D, 0x44, 0x24, 0x30, 0x48, 0x89, 0x44, 0x24, 0x20, 0x48, 0xFF };
                
                int outint;
                Int64 pWriteProcKernelBase = GetProcAddress(GetModuleHandleA("kernelbase.dll"), "WriteProcessMemory");
                Int64 pReadProcKernelBase = GetProcAddress(GetModuleHandleA("kernelbase.dll"), "ReadProcessMemory");
                System.Windows.Forms.Clipboard.SetText($"{pReadProcKernelBase:X}");

                //48 8B C4 48 89 58 08 4C 89 48 20 4C 89 40 18 48
                if (ReadProcessMemory(GetCurrentProcess(), pWriteProcKernelBase, WPC_Bytes, 16, out outint) && ReadProcessMemory(GetCurrentProcess(), pReadProcKernelBase, RPC_Bytes, 16, out outint))
                {
                    for (int i = 0; i < WPC_Bytes.Length; i++)
                    {
                        if (WPC_Bytes[i] != NormalShitWPM[i])
                            return false;
                    }

                    for (int i = 0; i < RPC_Bytes.Length; i++)
                    {
                        if (RPC_Bytes[i] != NormalShitRPM[i])
                            return false;
                    }

                    return true;
                }
                System.Windows.Forms.Clipboard.SetText($"{pWriteProcKernelBase:X}");
                Console.WriteLine($"{pWriteProcKernelBase:X}");
                System.Console.WriteLine($"{pWriteProcKernelBase:X}");
                return false;
            }
            catch
            {

            }
            return false;
        }
    }
}
