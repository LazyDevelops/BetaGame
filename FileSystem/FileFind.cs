using VirtualTerminal.Tree.General;

namespace VirtualTerminal.FileSystem
{
    public partial class FileSystem
    {
        public Node<FileDataStruct>? FileFind(string? path, Node<FileDataStruct> root)
        {
            if (string.IsNullOrEmpty(path))
            {
                return null;
            }

            Node<FileDataStruct> currentNode = root;
            List<string> files = [];

            // 경로 정리
            path = path.TrimStart('/');
            files.AddRange(path.Split('/'));

            if (files.Count == 0 || files[0] == "")
            {
                return root;
            }

            // 파일 트리 탐색
            foreach (string file in files)
            {
                Node<FileDataStruct>? nextNode = currentNode.Children.FirstOrDefault(child => child.Data.Name == file);

                if (nextNode == null)
                {
                    // 경로가 존재하지 않으면 null 반환
                    return null;
                }

                currentNode = nextNode;
            }

            return currentNode;
        }
    }
}