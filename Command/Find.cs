namespace VirtualTerminal.Command
{
    public class FindCommand : VirtualTerminal.ICommand
    {
        public string? Execute(int argc, string[] argv, VirtualTerminal VT)
        {
            return null;
        }

        public string Description(bool detail)
        {
            if (detail)
            {
                return "find - ";
            }

            return "find - ";
        }
    }
}