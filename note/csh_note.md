rwxr-x code bin

drwxr-x code bin  
-rw-r-- code hello.txt

￦- : 일반 파일  
d : 디렉터일 파일(폴더)

2자리 8진수  
앞자리는 소유자  
뒷자리는 기타

rwxr-x  
111101

r = 4, w = 2, x = 1

ls -1  
자세히 표시  
ls -a  
숨김 파일 표시

```cs
private struct FileSystemEntry{
    public string name { get; }
    public byte permission { get; }
    public string UID { get; }
    public enum FileType { 
        F, D 
    } public FileType fileType { get; }
    public string? content { get; }

    public FileSystemEntry(string name, string UID, byte permission, FileType fileType, string? content = null){
        this.name = name;
        this.UID = UID;
        this.permission = permission;
        this.fileType = fileType;
        this.content = content;
    }
}
```

```cs
Tree<FileSystemNode> root = new Tree<FileSystemNode>(new FileSystemEntry("/", "root", 0b111101, 1));

CreateFile(root, "/", new FileSystemEntry("root", "root", 0b111000, 1));
CreateFile(root, "/", new FileSystemEntry("home", "root", 0b111101, 1));
Tree<FileSystemNode> HOME = CreateFile(root, "/home", new FileSystemEntry($"{USER}", USER, 0b111101, 1));
CreateFile(root, $"/home/{USER}", new FileSystemEntry($"Hello_{USER}.txt", "root", 0b111111, 0, $"Hello, {USER}!"));
```

```cs
bool RemoveFile("상대 주소 or 절대 주소")

Tree<FileSystemNode> FindFile("상대 주소 or 절대 주소")
```


상대 주소 예시  
./ : 현제 디렉터리  
예)  
"pwd는 /home/user라고 가정"  
./Hello.sh = Hello.sh = /home/user/Hello.sh


../ : 이전 디렉터리  
예)  
"pwd는 /home/user라고 가정"  
../ = /home  
../../ = /


~/ = home 디렉터리  
예)  
"pwd는 /home라고 가정"  
./user = /home/user = ~

## TODO
- 경로 스택으로 관리하게 전부 변경하기