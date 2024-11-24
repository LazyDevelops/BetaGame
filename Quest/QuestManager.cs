namespace VirtualTerminal.Quest
{
    public class QuestManager
    {
        private List<IQuest> _quests = [
            new Quest1(), new Quest2()
        ];

        public int CurrentQuest = 1;

        public string CheckQuest(int questNumber, VirtualTerminal VT)
        {
            questNumber--;
            
            if (questNumber < 0 || questNumber >= _quests.Count)
            {
                return "해당 번호의 퀘스트를 찾을 수 없습니다.\n";
            }
            
            if (questNumber+1 > CurrentQuest)
            {
                return "이전 퀘스트를 클리어한 뒤 시도해주세요.\n";
            }

            if (questNumber + 1 < CurrentQuest)
            {
                return "이미 클리어한 퀘스트입니다.\n";
            }

            string result = _quests[questNumber].QuestClearCheck(VT);

            if (result == "성공\n")
            {
                CurrentQuest++;
            }

            return result;
        }
        
        internal interface IQuest
        {
            string QuestClearCheck(VirtualTerminal VT);
        }
    }
}