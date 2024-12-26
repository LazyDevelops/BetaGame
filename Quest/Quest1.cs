using VirtualTerminal.FileSystem;
using VirtualTerminal.Tree.General;

namespace VirtualTerminal.Quest
{
    public class Quest1 : QuestManager.IQuest
    {
        public bool QuestClearCheck(VirtualTerminal VT)
        {
            if (VT.PWD != "/etc")
            {
                return false;
            }
            
            return true;
        }
    }
}