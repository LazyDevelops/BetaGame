using VirtualTerminal.Error;
using VirtualTerminal.FileSystem;
using VirtualTerminal.Tree.General;

namespace VirtualTerminal.Command
{
    public class RmDirCommand : VirtualTerminal.ICommand
    {
        public string? Execute(int argc, string[] argv, VirtualTerminal VT)
        {
            if (argc < 2)
            {
                return ErrorMessage.ArgLack(argv[0]);
            }

            foreach (string arg in argv.Skip(1))
            {
                if (arg.Contains('-') || arg.Contains("--"))
                {
                    continue;
                }

                // 절대 경로 계산
                string absolutePath = VT.FileSystem.GetAbsolutePath(arg, VT.HOME, VT.PWD);

                // 파일 찾기
                Node<FileDataStruct>? file = VT.FileSystem.FileFind(absolutePath, VT.Root);

                if (file?.Parent == null)
                {
                    return ErrorMessage.NoSuchForD(argv[0], ErrorMessage.DefaultErrorComment(arg));
                }

                // 권한 검사
                bool[] permission = VT.FileSystem.CheckPermission(VT.USER, file.Parent, VT.Root);
                if (permission[0] || !permission[1] || !permission[2]) // 쓰기 및 실행 권한 확인
                {
                    return ErrorMessage.PermissionDenied(argv[0], ErrorMessage.DefaultErrorComment(arg));
                }

                // 디렉터리인지 확인
                if (file.Data.FileType != FileType.D)
                {
                    return ErrorMessage.NotD(argv[0], ErrorMessage.DefaultErrorComment(arg));
                }

                // 디렉터리 삭제
                int removeResult = VT.FileSystem.FileRemove(absolutePath, VT.Root, null);
                if (removeResult != 0)
                {
                    return ErrorMessage.DNotEmpty(argv[0], ErrorMessage.DefaultErrorComment(arg));
                }
            }

            return null;
        }

        public string Description(bool detail)
        {
            if (detail)
            {
                return "\u001b[1m간략한 설명\x1b[22m\n" +
                       "   rmdir - 디렉터리 삭제\n\n" +
                       "\u001b[1m사용법\u001b[22m\n" +
                       "   rmdir [옵션] 폴더명\n\n" +
                       "\u001b[1m설명\u001b[22m\n" +
                       "   위에 사용법을 이용하여 빈 디렉터리를 삭제할 수 있습니다.\n" +
                       "   -f 없이는 빈 디렉터리가 아닐 경우 삭제가 불가능합니다.\n" +
                       "   (자세한 사용법은 예시 참조)\n\n" +
                       "\u001b[1m옵션\u001b[22m\n" +
                       "   -f\n" +
                       "       안에 파일이 있어서 삭제합니다.\n" +
                       "\u001b[1m예시\u001b[22m\n" +
                       "   rmdir test\n" +
                       "   rmdir test/helloworld\n";
            }

            return "rmdir - 비어있는 디렉터리를 삭제";
        }
    }
}