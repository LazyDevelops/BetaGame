namespace VirtualTerminal.Command
{
    public class LevelCommand : VirtualTerminal.ICommand
    {
        public string? Execute(int argc, string[] argv, VirtualTerminal VT)
        {
            string? result = "레벨: " + VT.level +
                             ", Exp: " + VT.Exp + "\n" +
                             "0 [";

            int progress = CalculateProgress(VT.maxExp, VT.Exp, 20);

            for (int i = 0; i < progress; i++)
            {
                result += "=";
            }

            for (int i = progress; i < 20; i++)
            {
                result += "-";
            }

            result += "] " + VT.maxExp + "\n";

            return result;
        }

        public string Description(bool detail)
        {
            if (detail)
            {
                return "level - 현제 레벨 확인\n";
            }

            return "level - 현제 레벨 확인";
        }

        private static int CalculateProgress(long maxValue, long currentValue, int steps)
        {
            if (maxValue <= 0)
            {
                return 0;
            }

            if (steps <= 0)
            {
                return 0;
            }

            // 현재 값을 최대값 대비 0에서 steps까지의 단계로 변환
            double ratio = (double)currentValue / maxValue;
            int progress = (int)(ratio * steps);

            // 진행도는 0에서 steps 사이로 제한
            return Math.Clamp(progress, 0, steps);
        }
    }
}