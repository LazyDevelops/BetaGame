using VirtualTerminal.Error;
using VirtualTerminal.FileSystem;
using VirtualTerminal.Tree.General;

// 수정 필요

namespace VirtualTerminal.Command
{
    public class LsCommand : VirtualTerminal.ICommand
    {
        public string? Execute(int argc, string[] argv, VirtualTerminal VT)
        {
            Node<FileDataStruct>? file;
            List<Node<FileDataStruct>> files = [];
            List<string> inputFilesArg = [];
            string? absolutePath;
            bool[] permission;

            Dictionary<string, bool> options = new() { { "l", false } };

            VirtualTerminal.OptionCheck(ref options, in argv);

            foreach (string arg in argv.Skip(1))
            {
                if (arg.Contains('-') || arg.Contains("--"))
                {
                    continue;
                }

                absolutePath = VT.FileSystem.GetAbsolutePath(arg, VT.HOME, VT.PWD);

                file = VT.FileSystem.FileFind(absolutePath, VT.Root);

                if (file == null)
                {
                    return ErrorMessage.NoSuchForD(argv[0], ErrorMessage.DefaultErrorComment(arg));
                }

                permission = VT.FileSystem.CheckPermission(VT.USER, file, VT.Root);

                if (!permission[0])
                {
                    return ErrorMessage.PermissionDenied(argv[0], ErrorMessage.DefaultErrorComment(arg));
                }

                if (file.Data.FileType != FileType.D)
                {
                    return arg;
                }

                files.Add(file);
                inputFilesArg.Add(arg);
            }

            if (files.Count == 0)
            {
                if (VT.PwdNode == null)
                {
                    return null;
                }

                files.Add(VT.PwdNode);
            }

            string? result = null;

            for (int i = 0; i < files.Count; i++)
            {
                if (files.Count > 1)
                {
                    result += $"{inputFilesArg[i]}:\n";
                }

                if (options["l"])
                {
                    result += $"total {files[i].Children.Count}\n";
                }

                foreach (Node<FileDataStruct> fileChild in files[i].Children)
                {
                    permission = VT.FileSystem.CheckPermission(VT.USER, fileChild, VT.Root);

                    if (options["l"])
                    {
                        string permissions = VT.FileSystem.PermissionsToString(fileChild.Data.Permission);
                        string time = string.Empty;
                        DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(fileChild.Data.LastTouchTime);

                        if (dateTimeOffset.Year != DateTime.Now.Year)
                        {
                            time += $"{dateTimeOffset.Year} ";
                        }

                        time += $"{dateTimeOffset.Month}월 {dateTimeOffset.Day} {dateTimeOffset.Hour}:{dateTimeOffset.Minute}";
                        result += $"{Convert.ToChar(fileChild.Data.FileType)}{permissions} {fileChild.Data.UID} {time} ";
                    }


                    if (fileChild.Data.FileType == FileType.D)
                    {
                        result += $"\u001b[34m{fileChild.Data.Name}\u001b[0m";
                    }
                    else if (permission[2])
                    {
                        result += $"\u001b[32m{fileChild.Data.Name}\u001b[0m";
                    }
                    else
                    {
                        result += fileChild.Data.Name;
                    }

                    result += "\n";
                }

                if (i != files.Count - 1)
                {
                    result += "\n";
                }
            }

            return result;
        }

        public string Description(bool detail)
        {
            if (detail)
            {
                return "\u001b[1m간략한 설명\x1b[22m\n" +
                       "   cd - 현제 디렉터리에 있는 디렉터리 및 파일 목록 출력\n\n" +
                       "\u001b[1m사용법\u001b[22m\n" +
                       "   ls [옵션] 폴더/파일명\n\n" +
                       "\u001b[1m설명\u001b[22m\n" +
                       "   위에 사용법을 이용하여 디렉터리 목록을 출력하며\n" +
                       "   파일같은 경우 단일 파일 하나의 대한 출력이 나옵니다.\n" +
                       "   (자세한 사용법은 예시 참조)\n\n" +
                       "\u001b[1m옵션\u001b[22m\n" +
                       "   -l\n" +
                       "       상세히 출력합니다.\n" +
                       "       (파일 종류, 권한, 소유자, 수정 시간, 파일명)\n\n" +
                       "\u001b[1m예시\u001b[22m\n" +
                       "   ls\n" +
                       "   ls -l ~\n" +
                       "   ls /\n" +
                       "   ls ../../etc\n";
            }

            return "ls - 현제 디렉터리에 있는 디렉터리 및 파일 목록 출력";
        }
    }
}