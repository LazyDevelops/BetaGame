using Tree;
using VirtualTerminal.Error;
using VirtualTerminal.FileSystem;

namespace VirtualTerminal.Command
{
    public class MkDirCommand : VirtualTerminal.ICommand
    {
        public void Execute(int argc, string[] argv, VirtualTerminal VT)
        {
            if (argc < 2)
            {
                Console.WriteLine(ErrorMessage.ArgLack(argv[0]));
                return;
            }

            Tree<FileNode>? parentFile;
            string? absolutePath;
            string? parentPath;
            string? fileName;
            bool[] permission;

            foreach (string arg in argv)
            {
                if (arg == argv[0] || arg.Contains('-') || arg.Contains("--"))
                {
                    continue;
                }

                absolutePath = VT.FileSystem.GetAbsolutePath(arg, VT.HOME, VT.PWD);
                fileName = absolutePath.Split('/')[^1];
                parentPath = absolutePath.Replace('/' + fileName, "");

                parentFile = VT.FileSystem.FindFile(parentPath, VT.Root);

                if (parentFile == null)
                {
                    Console.WriteLine(ErrorMessage.NoSuchForD(argv[0], ErrorMessage.DefaultErrorComment(arg)));
                    return;
                }

                permission = VT.FileSystem.CheckFilePermission(VT.USER, parentFile, VT.Root);

                if (!permission[0] || !permission[1] || !permission[2])
                {
                    Console.WriteLine(ErrorMessage.PermissionDenied(argv[0], ErrorMessage.DefaultErrorComment(arg)));
                    return;
                }

                if (parentFile.Data.FileType != FileType.D)
                {
                    Console.WriteLine(ErrorMessage.NotD(argv[0], ErrorMessage.DefaultErrorComment(arg)));
                    return;
                }

                if (VT.FileSystem.FindFile(absolutePath, VT.Root) != null)
                {
                    Console.WriteLine(ErrorMessage.FExists(argv[0], ErrorMessage.DefaultErrorComment(arg)));
                    return;
                }

                VT.FileSystem.CreateFile(parentPath, new FileNode(fileName, VT.USER, 0b111101, FileType.D), VT.Root);
            }
        }

        public string Description(bool detail)
        {
            return "mkdir - 디렉터리 생성";
        }
    }
}