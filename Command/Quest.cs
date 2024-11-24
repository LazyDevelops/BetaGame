using System.Text.Json;
using VirtualTerminal.Error;
using VirtualTerminal.Quest;

namespace VirtualTerminal.Command
{
    public class QuestCommand : VirtualTerminal.ICommand
    {
        private QuestManager _questManager = new();
        
        public string? Execute(int argc, string[] argv, VirtualTerminal VT)
        {
            int questNumber = _questManager.CurrentQuest;

            if (argc >= 4)
            {
                return ErrorMessage.ArgLack(argv[0]);
            }
            
            if (argc == 1)
            {
                // 현제 퀘스트 확인
                return ReturnQuestContent(questNumber);
            }

            if (argc == 2)
            {
                if (!int.TryParse(argv[1], out int intArgv))
                {
                    // 현제 퀘스트 클리어 여부 확인
                    return _questManager.CheckQuest(questNumber, VT);
                }

                // 입력으로 받은 번째의 퀘스트 확인
                questNumber = intArgv;
                return ReturnQuestContent(questNumber);
            }

            if (argc == 3)
            {
                if (!int.TryParse(argv[2], out int intArgv))
                {
                    // 에러 띄우기  
                    return null;
                }

                // 입력으로 받은 번째의 클리어 여부 확인
                questNumber = intArgv;
                return _questManager.CheckQuest(questNumber, VT);
            }

            return null;
        }

        private string? ReturnQuestContent(int questNumber)
        {
            string projectRoot = Path.Combine(AppContext.BaseDirectory, "..", "..", "..");
            string jsonFilePath = Path.Combine(projectRoot, "QuestList.json");
            
            string jsonString = File.ReadAllText(jsonFilePath);
            var quests = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(jsonString);

            if (quests == null || !quests.TryGetValue(questNumber.ToString(), out var quest))
            {
                return "해당 번호의 퀘스트를 찾을 수 없습니다.\n";
            }

            if (!quest.TryGetValue("questContent", out var questContent))
            {
                return null;
            }

            return (questNumber < _questManager.CurrentQuest ? $"{questContent}\u001b[1;32m(성공)\u001b[0m" : questContent)+"\n";
        }

        public string Description(bool detail)
        {
            if (detail)
            {
                return "\u001b[1m간략한 설명\x1b[22m\n" +
                       "   quest - 미션, 미션 클리어 여부 확인 및 미션 클리어 검사\n\n" +
                       "\u001b[1m사용법\u001b[22m\n" +
                       "   quest clear/숫자 숫자 \n\n" +
                       "\u001b[1m설명\u001b[22m\n" +
                       "   위에 사용법을 이용하여 현제 퀘스트 및 원하는 번호의 퀘스트 및 클리어 여부를 확인할 수 있으며\n" +
                       "   현제 퀘스트 및 원하는 번호의 퀘스트 클리어 검사를 받을 수 있습니다.\n" +
                       "   (자세한 사용법은 예시 참조)\n\n" +
                       "\u001b[1m옵션\u001b[22m\n" +
                       "   (없음)\n\n" +
                       "\u001b[1m예시\u001b[22m\n" +
                       "   현제 퀘스트 출력\n" +
                       "       quest\n" +
                       "   내가 원하는 번호의 퀘스트 출력\n" +
                       "       quest 1\n" +
                       "       quest 2\n" +
                       "   현제 퀘스트 클리어 검사 받기\n" +
                       "       quest clear\n" + 
                       "   내가 원하는 퀘스트의 클리어 검사 받기\n"+
                       "       quest clear 3\n"+
                       "       quest clear 4\n";
            }

            return "quest - 미션, 미션 클리어 여부 확인 및 미션 클리어 검사\n";
        }
    }
}