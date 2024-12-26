using VirtualTerminal.FileSystem;
using VirtualTerminal.Tree.General;

namespace VirtualTerminal.Quest
{
    public class Quest3 : QuestManager.IQuest
    {
        public bool QuestClearCheck(VirtualTerminal VT)
        {
            Node<FileDataStruct>? file;
            file = VT.HomeNode;

            if (file == null)
            {
                return false;
            }

            if (file.Children.Any(tempFile => tempFile.Data.Name == "hello.txt"))
            {
                return false;
            }

            return true;
        }
    }
}