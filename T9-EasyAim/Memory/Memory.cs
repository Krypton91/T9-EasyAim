using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace T9_EasyAim.Memory
{
    public class Memory
    {
        public IntPtr ProcessHandle;
        public Int64 BaseAddress;

        [DllImport("kernelbase.dll")]
        public static extern bool ReadProcessMemory(IntPtr hProcess, Int64 lpBaseAddress, [Out] byte[] lpBuffer, uint nSize, out uint lpNumberOfBytesRead);

        [DllImport("kernelbase.dll")]
        public static extern bool WriteProcessMemory(IntPtr hProcess, Int64 lpBaseAddress, byte[] lpBuffer, uint nSize, out uint lpNumberOfBytesRead);

        [DllImport("kernelbase.dll")]
        public static extern void Beep(int r3, int r4);

        public bool IsProcRunning(string procname)
        {
            try
            {
                Process processes = Process.GetProcessesByName(procname)[0];

                if (processes.Handle.ToInt64() != 0)
                {
                    BaseAddress = processes.MainModule.BaseAddress.ToInt64();
                    ProcessHandle = processes.Handle;
                    return true;
                }
            }
            catch {}
            return false;
        }

        public void Beep()
        {
            Beep(200, 200);
        }

        public void WriteFloat(Int64 pAddress, float value)
        {
            uint outSize = 0;
            WriteProcessMemory(ProcessHandle, pAddress, BitConverter.GetBytes(value), 4, out outSize);
        }

        public void WriteInt(Int64 pAddress, int value)
        {
            uint outSize = 0;
            WriteProcessMemory(ProcessHandle, pAddress, BitConverter.GetBytes(value), 4, out outSize);
        }

        public void WriteUInt(Int64 pAddress, uint value)
        {
            uint outSize = 0;
            WriteProcessMemory(ProcessHandle, pAddress, BitConverter.GetBytes(value), value, out outSize);
        }

        public void WriteBytes(Int64 pAddress, byte[] value)
        {
            uint outSize = 0;
            WriteProcessMemory(ProcessHandle, pAddress, value, (uint)value.Length, out outSize);
        }

        public float ReadFloat(Int64 pAddress)
        {
            uint outSize = 0;
            byte[] bytes = new byte[4];
            ReadProcessMemory(ProcessHandle, pAddress, bytes, 4, out outSize);
            return BitConverter.ToSingle(bytes, 0);
        }

        public int ReadInt(Int64 pAddress)
        {
            uint outSize = 0;
            byte[] bytes = new byte[4];
            ReadProcessMemory(ProcessHandle, pAddress, bytes, 4, out outSize);
            return BitConverter.ToInt32(bytes, 0);
        }

        public uint ReadUInt(Int64 pAddress)
        {
            uint outSize = 0;
            byte[] bytes = new byte[4];
            ReadProcessMemory(ProcessHandle, pAddress, bytes, 4, out outSize);
            return BitConverter.ToUInt32(bytes, 0);
        }

        public Int64 ReadPointer(Int64 pAddress)
        {
            uint outSize = 0;
            byte[] bytes = new byte[8];
            ReadProcessMemory(ProcessHandle, pAddress, bytes, 8, out outSize);
            return BitConverter.ToInt64(bytes, 0);
        }

        public byte[] ReadBytes(Int64 pAddress, uint length)
        {
            uint outSize = 0;
            byte[] bytes = new byte[length];
            ReadProcessMemory(ProcessHandle, pAddress, bytes, length, out outSize);
            return bytes;
        }
    }
}
