using VirtualTerminal.FileSystem;
using VirtualTerminal.Tree.General;

namespace VirtualTerminal.Quest
{
    public class Quest1 : QuestManager.IQuest
    {
        public bool QuestClearCheck(VirtualTerminal VT)
        {
            Node<FileDataStruct>? file;
            file = VT.HomeNode;

            if (file == null)
            {
                return false;
            }

            foreach (Node<FileDataStruct> tempFile in file.Children)
            {
                if (tempFile.Data.Name != "hello.txt")
                {
                    continue;
                }

                file = tempFile;
                break;
            }

            if (file.Data.Name != "hello.txt")
            {
                return false;
            }

            if (file.Data.Content?.TrimEnd('\n').TrimEnd(' ') != "I love linux")
            {
                return false;
            }
            
            return true;
        }
    }
}