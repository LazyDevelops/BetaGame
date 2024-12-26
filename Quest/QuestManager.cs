namespace VirtualTerminal.Quest
{
    public class QuestManager
    {
        private readonly List<IQuest> _quests = [
            new Quest1(), new Quest2(), new Quest3()
        ];

        public int CurrentQuest = 1;

        public bool CheckQuest(int questNumber, ref string? errorMessage, VirtualTerminal VT)
        {
            questNumber--;
            
            if (questNumber < 0 || questNumber >= _quests.Count)
            {
                errorMessage = "해당 번호의 퀘스트를 찾을 수 없습니다.";
                return false;
            }
            
            if (questNumber+1 > CurrentQuest)
            {
                errorMessage = "이전 퀘스트를 클리어한 뒤 시도해주세요.";
                return false;
            }

            if (questNumber + 1 < CurrentQuest)
            {
                errorMessage = "이미 클리어한 퀘스트입니다.";
                return false;
            }

            bool result = _quests[questNumber].QuestClearCheck(VT);

            if (result)
            {
                CurrentQuest++;
            }

            return result;
        }
        
        internal interface IQuest
        {
            bool QuestClearCheck(VirtualTerminal VT);
        }
    }
}