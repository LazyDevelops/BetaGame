using VirtualTerminal.Error;
using VirtualTerminal.FileSystem;
using VirtualTerminal.Tree.General;

namespace VirtualTerminal.Command
{
    public class GrepCommand : VirtualTerminal.ICommand
    {
        public string? Execute(int argc, string[] argv, VirtualTerminal VT)
        {
            return null;
        }

        public string Description(bool detail)
        {
            if (detail)
            {
                return "grap - ";
            }

            return "grap - ";
        }
    }
}