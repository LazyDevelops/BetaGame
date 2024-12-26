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
            
            file = file.Children.Find(x => x.Data.Name == "test.txt");

            if (file == null)
            {
                return false;
            }
            
            if(VT.FileSystem.PermissionsToString(file.Data.Permission) != "rw-r--")
            {
                return false;
            }
            
            return true;
        }
    }
}