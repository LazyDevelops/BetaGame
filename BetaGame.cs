using System.Text.RegularExpressions;

internal class BetaGame
{
    private static void Main()
    {   
        string? name = Input("이름을 입력해 주세요(영어, 숫자 만).\n> ",
                             "다시 이름을 입력해 주세요(영어, 숫자 만).\n> ",
                             @"^[a-zA-Z0-9]+$");

        if(name == "root" || name == "nobody"){
            Console.Clear();
            Console.WriteLine("\u001b[31m사용할 수 없는 이름입니다.\u001b[0m");
            Thread.Sleep(3000);
            Environment.Exit(0);
        }

        string check = "n";

        while(check != "y" && check != "Y")
        {
            check = Input("이름이 \"" + name + "\" 맞습니까? (y/n)\n> ",
                          "이름이 \"" + name + "\" 맞습니까? (y/n)\n> ",
                          @"^[yYnN]$");

            if(check == "n" || check == "N")
            {
                Console.Clear();
                name = Input("이름을 입력해 주세요(영어, 숫자 만).\n> ",
                             "다시 이름을 입력해 주세요(영어, 숫자 만).\n> ",
                             @"^[a-zA-Z0-9]+$");
            }
        }

        Console.Clear();
        
        VirtualTerminal.VirtualTerminal terminal = new(name);
        terminal.Run();
    }

    static string Input(string message, string retryMessage, string pattern){
        Console.Write(message);

        string? input = Console.ReadLine();

        while (string.IsNullOrEmpty(input) || !CustomRegular(input, pattern))
        {
            Console.Clear();
            Console.Write(retryMessage);
            input = Console.ReadLine();
        }

        return input;
    }

    static bool CustomRegular(string? input, string pattern)
    {
        if (input == null)
        {
            return false;
        }
        
        return Regex.IsMatch(input, pattern);
    }
}
