namespace VirtualTerminal.Command
{
    public class LevelCommand : VirtualTerminal.ICommand
    {
        public string? Execute(int argc, string[] argv, VirtualTerminal VT)
        {
            return "레벨: " + VT.level.ToString() + "\n";
        }

        public string Description(bool detail)
        {
            if (detail)
            {
                return "level - 현제 레벨 확인\n";
            }

            return "level - 현제 레벨 확인";
        }
    }
}