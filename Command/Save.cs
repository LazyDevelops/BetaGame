namespace VirtualTerminal.Command
{
    public class SaveCommand : VirtualTerminal.ICommand
    {
        public string? Execute(int argc, string[] argv, VirtualTerminal VT)
        {
            return null;
        }

        public string Description(bool detail)
        {
            if (detail)
            {
                return "save - 진행상황 저장";
            }

            return "save - 진행상황 저장";
        }
    }
}