using System.Globalization;
using VirtualTerminal.FileSystem;
using VirtualTerminal.Tree.General;

namespace VirtualTerminal.Quest
{
    public class Quest5 : QuestManager.IQuest
    {
        public bool QuestClearCheck(VirtualTerminal VT)
        {
            Node<FileDataStruct>? file;
            file = VT.HomeNode;

            if (file == null)
            {
                return false;
            }

            file = file.Children.Find(x => x.Data.Name == "date.txt");

            if (file == null)
            {
                return false;
            }

            string? content = file.Data.Content?.TrimEnd('\n').TrimEnd(' ');
            const string format = "yyyy. MM. dd. (ddd) HH:mm:ss";

            bool isValid = DateTime.TryParseExact(content, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateTime);

            if (isValid)
            {
                return false;
            }

            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(file.Data.LastTouchTime);
            int count = 0;

            count += dateTime.Year == dateTimeOffset.Year ? 1 : 0;
            count += dateTime.Month == dateTimeOffset.Month ? 1 : 0;
            count += dateTime.Day == dateTimeOffset.Day ? 1 : 0;
            count += dateTime.Hour == dateTimeOffset.Hour ? 1 : 0;
            count += dateTime.Minute == dateTimeOffset.Minute ? 1 : 0;
            count += dateTime.Second == dateTimeOffset.Second ? 1 : 0;

            if (count > 0)
            {
                return false;
            }

            return true;
        }
    }
}