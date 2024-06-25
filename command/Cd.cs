﻿using Tree;
using static FileSystem.FileSystem;

namespace VirtualTerminal.Commands
{
    public class CdCommand : VirtualTerminal.ICommand
    {
        public void Execute(string[] args, VirtualTerminal VT)
        {
            Tree<FileNode>? file;
            string? absolutePath;

            foreach (string arg in args)
            {
                if (arg != args[0] && !arg.Contains('-') && !arg.Contains("--"))
                {
                    absolutePath = VT.fileSystem.GetAbsolutePath(arg, VT.HOME, VT.PWD);
                    file = VT.fileSystem.FindFile(absolutePath, VT.root);

                    if (file == null)
                    {
                        Console.WriteLine($"bash: {args[0]}: {arg}: No such file or directory");
                        return;
                    }

                    if (file.Data.FileType != FileType.D)
                    {
                        Console.WriteLine($"bash: {args[0]}: {arg}: Not a directory");
                        return;
                    }

                    // if(file.Data.Permission )
                    if(false)
                    {
                        Console.WriteLine($"bash: {args[0]}: {arg}: Permission denied");
                        return;
                    }

                    VT.pwdNode = file;
                    VT.PWD = absolutePath;
                }
            }
        }
    }
}