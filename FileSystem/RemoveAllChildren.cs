using VirtualTerminal.Tree.General;

namespace VirtualTerminal.FileSystem
{
    public partial class FileSystem
    {
        private void RemoveAllChildren(Node<FileDataStruct> node)
        {
            for (int i = node.Children.Count - 1; i >= 0; i--)
            {
                Node<FileDataStruct> child = node.Children[i];
                RemoveAllChildren(child); // 재귀적으로 하위 노드 삭제
                node.RemoveChildWithNode(child);
            }
        }
    }
}