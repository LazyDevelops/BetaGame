using VirtualTerminal.Error;
using VirtualTerminal.FileSystem;
using VirtualTerminal.Tree.General;

namespace VirtualTerminal.Command
{
    public class CpCommand : VirtualTerminal.ICommand
    {
        public string? Execute(int argc, string[] argv, VirtualTerminal VT)
        {
            if (argc < 3)
            {
                return ErrorMessage.ArgLack(argv[0]);
            }

            /*List<Node<FileDataStruct>?> file;
            Node<FileDataStruct>? parentFile;
            string? fileName;
            string? absolutePath;
            string? parentPath;
            bool[] permission;


            Dictionary<string, bool> options = new() { { "r", false }, { "f", false } };

            VirtualTerminal.OptionCheck(ref options, in argv);*/

            /*foreach (string arg in argv.Skip(1))
            {
                if (file.Count == 0)
                {
                    absolutePath = VT.FileSystem.GetAbsolutePath(arg, VT.HOME, VT.PWD);
                    file.Add(VT.FileSystem.FindFile(absolutePath, VT.Root));

                    if (file[0] == null)
                    {
                        Console.WriteLine(ErrorMessage.NoSuchForD(argv[0], ErrorMessage.DefaultErrorComment(arg)));
                        return;
                    }

                    permission = VT.FileSystem.CheckPermission(VT.USER, file[0], VT.Root);

                    if (!permission[0] || !permission[2])
                    {
                        Console.WriteLine(ErrorMessage.PermissionDenied(argv[0], ErrorMessage.DefaultErrorComment(arg)));
                        return;
                    }
                }

                absolutePath = VT.FileSystem.GetAbsolutePath(arg, VT.HOME, VT.PWD);
                fileName = absolutePath.Split('/')[^1];
                file[1] = VT.FileSystem.FindFile(absolutePath, VT.Root);

                parentPath = absolutePath.Replace('/' + fileName, "");
                parentFile = VT.FileSystem.FindFile(parentPath, VT.Root);

                if (parentFile == null)
                {
                    Console.WriteLine(ErrorMessage.NoSuchForD(argv[0], ErrorMessage.DefaultErrorComment(arg)));
                    return;
                }

                permission = VT.FileSystem.CheckPermission(VT.USER, parentFile, VT.Root);

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

                VT.FileSystem.CreateFile(parentPath, new FileDataStruct(fileName, file[0].Data.UID, file[0].Data.Permission, file[0].Data.FileType, file[0].Data.Content), VT.Root);
            }*/

            return null;
        }

        public string Description(bool detail)
        {
            if (detail)
            {
                return "cp - 파일과 디렉터리 복사\n";
            }

            return "cp - 파일과 디렉터리 복사";
        }
    }
}
/*
using VirtualTerminal.Error;
using VirtualTerminal.FileSystem;
using VirtualTerminal.Tree.General;

namespace VirtualTerminal.Command
{
    public class CpCommand : VirtualTerminal.ICommand
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
                VT.FileSystem.CopyDirectory(sourceFile, destinationPath, VT.Root);
            }
            else
            {
                VT.FileSystem.CopyFile(sourceFile, destinationPath, VT.Root);
            }

            return null;
        }

        public string Description(bool detail)
        {
            if (detail)
            {
                return "cp - 파일과 디렉터리 복사\n";
            }

            return "cp - 파일과 디렉터리 복사";
        }
    }
}
*/