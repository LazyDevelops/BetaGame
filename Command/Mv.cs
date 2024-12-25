using VirtualTerminal.Error;
using VirtualTerminal.FileSystem;
using VirtualTerminal.Tree.General;

namespace VirtualTerminal.Command
{
    public class MvCommand : VirtualTerminal.ICommand
    {
        public string? Execute(int argc, string[] argv, VirtualTerminal VT)
        {
            if (argc < 3)
            {
                return ErrorMessage.ArgLack(argv[0]);
            }

            /*Tree<FileDataStruct>?[] file = new Tree<FileDataStruct>?[2];
            byte fileCounter = 0;
            string?[] absolutePath = new string?[2];
            string? fileName;
            bool[] permission;

            Dictionary<string, bool> options = new() { { "r", false }, { "f", false } };

            BetaGame.OptionCheck(ref options, in argv);*/

            /*foreach (string arg in argv.Skip(1))
            {
                if (arg.Contains('-') || arg.Contains("--"))
                {
                    continue;
                }

                if (fileCounter + 1 > file.Length)
                {
                    return ErrorMessage.ArgLack(argv[0]);
                }

                absolutePath[fileCounter] = VT.FileSystem.GetAbsolutePath(arg, VT.HOME, VT.PWD);

                if (fileCounter == 0)
                {
                }

                fileCounter++;
            }*/

            return null;
        }

        public string Description(bool detail)
        {
            if (detail)
            {
                return "mv - 파일이나 폴더 위치를 옮기거나 이름 제지정\n";
            }

            return "mv - 파일이나 폴더 위치를 옮기거나 이름 제지정";
        }
    }
}
/*
using VirtualTerminal.Error;
using VirtualTerminal.FileSystem;
using VirtualTerminal.Tree.General;

namespace VirtualTerminal.Command
{
    public class MvCommand : VirtualTerminal.ICommand
    {
        public string? Execute(int argc, string[] argv, VirtualTerminal VT)
        {
            if (argc < 3)
            {
                return ErrorMessage.ArgLack(argv[0]);
            }

            string sourcePath = VT.FileSystem.GetAbsolutePath(argv[1], VT.HOME, VT.PWD);
            string destinationPath = VT.FileSystem.GetAbsolutePath(argv[2], VT.HOME, VT.PWD);

            Node<FileDataStruct>? sourceFile = VT.FileSystem.FileFind(sourcePath, VT.Root);
            if (sourceFile == null)
            {
                return ErrorMessage.NoSuchForD(argv[0], ErrorMessage.DefaultErrorComment(argv[1]));
            }

            bool[] sourcePermission = VT.FileSystem.CheckPermission(VT.USER, sourceFile, VT.Root);
            if (!sourcePermission[0] || !sourcePermission[2])
            {
                return ErrorMessage.PermissionDenied(argv[0], ErrorMessage.DefaultErrorComment(argv[1]));
            }

            Node<FileDataStruct>? destinationFile = VT.FileSystem.FileFind(destinationPath, VT.Root);
            if (destinationFile != null && destinationFile.Data.FileType == FileType.D)
            {
                destinationPath = VT.FileSystem.CombinePaths(destinationPath, sourceFile.Data.Name);
            }

            string? parentPath = VT.FileSystem.GetParentPath(destinationPath);
            Node<FileDataStruct>? parentFile = VT.FileSystem.FileFind(parentPath, VT.Root);
            if (parentFile == null)
            {
                return ErrorMessage.NoSuchForD(argv[0], ErrorMessage.DefaultErrorComment(argv[2]));
            }

            bool[] destinationPermission = VT.FileSystem.CheckPermission(VT.USER, parentFile, VT.Root);
            if (!destinationPermission[0] || !destinationPermission[1])
            {
                return ErrorMessage.PermissionDenied(argv[0], ErrorMessage.DefaultErrorComment(argv[2]));
            }

            if (sourceFile.Data.FileType == FileType.D)
            {
                VT.FileSystem.MoveDirectory(sourceFile, destinationPath, VT.Root);
            }
            else
            {
                VT.FileSystem.MoveFile(sourceFile, destinationPath, VT.Root);
            }

            return null;
        }

        public string Description(bool detail)
        {
            if (detail)
            {
                return "mv - 파일이나 폴더 위치를 옮기거나 이름 제지정\n";
            }

            return "mv - 파일이나 폴더 위치를 옮기거나 이름 제지정";
        }
    }
}
*/