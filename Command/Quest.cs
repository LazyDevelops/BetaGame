using System.Text.Json;
using System.Text.Json.Nodes;
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
                    return ClearCheck(questNumber, VT);
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
                return ClearCheck(questNumber, VT);
            }

            return null;
        }

        private string ClearCheck(int questNumber, VirtualTerminal VT){
            string? error = null;
            bool result = _questManager.CheckQuest(questNumber, ref error, VT);

            if (error != null)
            {
                return error;
            }

            if(result)
            {
                List<KeyValuePair<string, string>>? rewards;
                rewards = GetQuestRewards(questNumber);

                if(rewards != null){
                    foreach(var reward in rewards)
                    {
                        if(reward.Key == "money")
                        {
                            VT.money += int.Parse(reward.Value);
                        }
                        else if(reward.Key == "exp")
                        {
                            VT.exp += int.Parse(reward.Value);
                        }
                    }
                }
                else
                {
                    return "error";
                }

                return "퀘스트 클리어 성공\n";
            }
            else
            {
                return "퀘스트 클리어 실패\n";
            }
        }

        private string? ReturnQuestContent(int questNumber)
        {
            string projectRoot = Path.Combine(AppContext.BaseDirectory, "..", "..", "..");
            string jsonFilePath = Path.Combine(projectRoot, "QuestList.json");
            
            if (!File.Exists(jsonFilePath))
            {
                return "JSON 파일이 존재하지 않습니다: " + jsonFilePath + "\n";
            }

            try
            {
                // JSON 파일 읽기
                string jsonString = File.ReadAllText(jsonFilePath);

                // JSON 데이터 파싱
                JsonObject? quests = JsonSerializer.Deserialize<JsonObject>(jsonString);

                if (quests != null && !quests.ContainsKey(questNumber.ToString()))
                {
                    return "해당 번호의 퀘스트가 존재하지 않습니다.\n";
                }

                if (quests[questNumber.ToString()] is not JsonObject quest)
                {
                    return "해당 퀘스트는 내용이 없습니다.\n";
                }

                if (quest == null || !quest.TryGetPropertyValue("questContent", out JsonNode? questContent))
                {
                    return "해당 퀘스트는 내용이 없습니다.\n";
                }

                if (string.IsNullOrEmpty(questContent?.ToString()))
                {
                    return "해당 퀘스트는 내용이 없습니다.\n";
                }

                return (questNumber < _questManager.CurrentQuest ? $"{questContent}\u001b[1;32m(성공)\u001b[0m" : questContent)+"\n";
            }
            catch (Exception ex)
            {
                return "JSON 파일 처리 중 오류가 발생했습니다: " + ex.Message + "\n";
            }
        }

        private List<KeyValuePair<string, string>>? GetQuestRewards(int questNumber)
        {
            string projectRoot = Path.Combine(AppContext.BaseDirectory, "..", "..", "..");
            string jsonFilePath = Path.Combine(projectRoot, "QuestList.json");

            if (!File.Exists(jsonFilePath))
            {
                Console.WriteLine("JSON 파일이 존재하지 않습니다.");
                return null;
            }

            try
            {
                // JSON 파일 읽기
                string jsonString = File.ReadAllText(jsonFilePath);

                // JSON 데이터 파싱
                JsonObject? quests = JsonSerializer.Deserialize<JsonObject>(jsonString);

                if (quests == null)
                {
                    Console.WriteLine("퀘스트 데이터가 없습니다.");
                    return null;
                }

                // 퀘스트 번호로 해당 퀘스트를 찾기
                if (!quests.TryGetPropertyValue(questNumber.ToString(), out JsonNode? questNode) || questNode is not JsonObject quest)
                {
                    Console.WriteLine($"퀘스트 {questNumber}가 없습니다.");
                    return null;
                }

                // "reward" 속성이 존재하는지 확인
                if (!quest.TryGetPropertyValue("reward", out JsonNode? rewardNode))
                {
                    Console.WriteLine("리워드 속성이 존재하지 않습니다.");
                    return null;
                }

                // rewardNode가 JsonObject인 경우에만 처리
                if (rewardNode is not JsonObject rewards)
                {
                    Console.WriteLine("리워드가 JsonObject가 아닙니다.");
                    return null;
                }

                // KeyValuePair 목록 생성
                List<KeyValuePair<string, string>> rewardList = new();

                foreach (var reward in rewards)
                {
                    rewardList.Add(new KeyValuePair<string, string>(reward.Key, reward.Value?.ToString() ?? "null"));
                }

                return rewardList;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"오류 발생: {ex.Message}");
                return null;
            }
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

            return "quest - 미션, 미션 클리어 여부 확인 및 미션 클리어 검사";
        }
    }
}