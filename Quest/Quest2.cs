using VirtualTerminal.FileSystem;
using VirtualTerminal.Tree.General;

namespace VirtualTerminal.Quest
{
    public class Quest2 : QuestManager.IQuest
    {
        public string QuestClearCheck(VirtualTerminal VT)
        {
            Node<FileDataStruct>? file;
            file = VT.HomeNode;
            
            if (file == null)
            {
                return "실패\n";
            }
            
            if (file.Children.Any(tempFile => tempFile.Data.Name == "eminai.txt"))
            {
                return "실패\n";
            }
            
            return "성공\n";
        }
    }
}