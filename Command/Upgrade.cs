namespace VirtualTerminal.Command
{
    public class UpgradeCommand : VirtualTerminal.ICommand
    {
        public string? Execute(int argc, string[] argv, VirtualTerminal VT)
        {
            return null;
        }

        public string Description(bool detail)
        {
            if (detail)
            {
                return "upgrade - 서버 업그레이드\n";
            }

            return "upgrade - 서버 업그레이드";
        }
    }
}