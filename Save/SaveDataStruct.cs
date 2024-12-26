using VirtualTerminal.FileSystem;
using VirtualTerminal.Tree.General;

namespace VirtualTerminal.Save
{
    public struct SaveDataStruct
    {
        public long Money;
        public long Level;
        public long Exp;

        public string Home;
        public Node<FileDataStruct>? HomeNode;

        public string Pwd;
        public Node<FileDataStruct>? PwdNode;

        public Node<FileDataStruct> Root;

        public string User;

        public int CurrentQuest;
    }
}