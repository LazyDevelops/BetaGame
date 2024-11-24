using VirtualTerminal.FileSystem;
using VirtualTerminal.Tree.General;

namespace VirtualTerminal.Quest
{
    public class Quest1 : QuestManager.IQuest
    {
        public string QuestClearCheck(VirtualTerminal VT)
        {
            Node<FileDataStruct>? file;
            file = VT.HomeNode;

            if (file == null)
            {
                return "실패\n";
            }

            foreach (Node<FileDataStruct> tempFile in file.Children)
            {
                if (tempFile.Data.Name != "eminai.txt")
                {
                    continue;
                }

                file = tempFile;
                break;
            }

            if (file.Data.Name != "eminai.txt")
            {
                return "실패\n";
            }

            if (file.Data.Content?.TrimEnd('\n').TrimEnd(' ') != "I love linux")
            {
                return "실패\n";
            }
            
            return "성공\n";
        }
    }
}