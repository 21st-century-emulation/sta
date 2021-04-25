public class CpuState
{
    public CpuFlags Flags { get; set; }

    public byte A { get; set; }
    public byte B { get; set; }
    public byte C { get; set; }
    public byte D { get; set; }
    public byte E { get; set; }
    public byte H { get; set; }
    public byte L { get; set; }

    public ushort StackPointer { get; set; }

    public ushort ProgramCounter { get; set; }

    public ulong Cycles { get; set; }

    public bool InterruptsEnabled { get; set; }
}