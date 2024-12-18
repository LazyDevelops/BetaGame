namespace VirtualTerminal.Command
{
    public class MoneyCommand : VirtualTerminal.ICommand
    {
        public string? Execute(int argc, string[] argv, VirtualTerminal VT)
        {
            return "잔고: " + VT.money.ToString() + "\n";
        }

        public string Description(bool detail)
        {
            if (detail)
            {
                return "money - 보유중인 돈 확인\n";
            }

            return "money - 보유중인 돈 확인";
        }
    }
}