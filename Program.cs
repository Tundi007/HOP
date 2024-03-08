namespace Program;

class Program
{

    public static void Main(string[] args)
    {   

        dynamic? UserInput_Dynamic;

        GameClass game_GameClass;

        while(true){            

            UserInput_Dynamic = IInputInterface.KeyToLine_Function("What Multiplier To Play HOP On? Only Numbers Are Accepted (\"exit\" to abort)");

            UserInput_Dynamic??="";

            if(UserInput_Dynamic.ToLower() == "exit")return;

            if(int.TryParse(UserInput_Dynamic, out int userNumber_Int))
            {

                game_GameClass = new(userNumber_Int);
                
                break;

            }

            Console.Clear();
            
            System.Console.WriteLine("Please Enter A Number (type \"exit\" to abort):");
            
        }        

        while(game_GameClass.Game_Function());
        
    }    

}

public class GameClass
{

    private int gameJump_Int;

    private int gameHopNumber_Int;

    public GameClass(int number_Int)
    {

        gameHopNumber_Int = 0;

        gameJump_Int = number_Int;

    }
    
    private (bool,string) BotHop_Function()
    {

        string hop_String = BotCurrentNumber_Function().ToString();

        if(HumanCurrentNumber_Function() % gameJump_Int == 0)return(true,IInputInterface.KeyToLine_Function(hop_String).ToLower());
        
        if(BotCurrentNumber_Function() % gameJump_Int == 0)hop_String = "hop";

        return (false,IInputInterface.KeyToLine_Function(hop_String).ToLower());

    }

    private int HumanCurrentNumber_Function()
    {

        return GlobalCurrentNumber_Function()+2;

    }

    private int BotCurrentNumber_Function()
    {

        return GlobalCurrentNumber_Function()+1;

    }

    private void GlobalNextNumber_Function()
    {

        gameHopNumber_Int+=2;

    }

    private int GlobalCurrentNumber_Function()
    {

        return gameHopNumber_Int;

    }

    public bool Game_Function()
    {

        Console.Clear();

        (bool isHOP_Bool,string userHop_String) = BotHop_Function();

        bool userHop_Bool = userHop_String == "hop";

        _ = int.TryParse(userHop_String.Trim(), out int userNumber_Int);

        System.Console.WriteLine(userHop_String);

        if(isHOP_Bool & userNumber_Int == HumanCurrentNumber_Function())
        {
            
            System.Console.WriteLine($"Caught You! We Were On {GlobalCurrentNumber_Function()+2} With Hop Rate Of {gameJump_Int}, But You Missed The \"HOP\"!");

            return false;
            
        }

        if(userNumber_Int != HumanCurrentNumber_Function())
        {

            System.Console.WriteLine($"Wrong! Continue? (press \"y\" for yes and \"n\" for no)");

            while(true)
            {

                switch(Console.ReadKey(true).Key)
                {

                    case ConsoleKey.Y:
                        return true;

                    case ConsoleKey.N:
                    {

                        System.Console.WriteLine($"You Lost On {GlobalCurrentNumber_Function()}");

                        Thread.Sleep(500);
                     
                    }return false;

                    default:
                    {

                        Console.Clear();

                        System.Console.WriteLine("Please Press Either \"Y\" For Continuing Or \"N\" For Admitting Defeat!");

                    }break;

                }
                
            }
            
        }

        GlobalNextNumber_Function();

        return true;

    }

}

public interface IInputInterface
{

    public static string KeyToLine_Function(string menu_String)
    {
        
        ConsoleKeyInfo key_ConsoleKeyInfo;

        string input_String = "";

        int stringIndex_Int = 0;

        string userInterfaceInput_String = input_String;

        while(true)
        {

            Console.Clear();

            System.Console.WriteLine(menu_String);

            System.Console.WriteLine(userInterfaceInput_String);

            key_ConsoleKeyInfo = Console.ReadKey(false); 

            switch (key_ConsoleKeyInfo.Key)
            {

                case ConsoleKey.Enter: return input_String;

                case ConsoleKey.End:return "Exit";

                case ConsoleKey.Escape:return "Exit";

                case ConsoleKey.LeftArrow:
                {
                 
                    if(stringIndex_Int < 1)break;

                    stringIndex_Int--;

                    userInterfaceInput_String = input_String[..stringIndex_Int] + "\\/" +
                        input_String[stringIndex_Int..];

                }break;

                case ConsoleKey.RightArrow:{
                 
                    if(stringIndex_Int >= input_String.Length)break;

                    stringIndex_Int++;

                    userInterfaceInput_String = input_String[..stringIndex_Int] + "\\/" +
                        input_String[stringIndex_Int..];

                }break;

                case ConsoleKey.Delete:
                {                    

                    if(stringIndex_Int >= input_String.Length)break;
                    
                    input_String = input_String.Remove(stringIndex_Int,1);

                    userInterfaceInput_String = input_String[..stringIndex_Int] + "\\/" +
                        input_String[stringIndex_Int..];
                
                }break;

                case ConsoleKey.Backspace:
                {

                    if (stringIndex_Int < 1)break;
                        
                    input_String = input_String.Remove(stringIndex_Int-1,1);

                    stringIndex_Int--;

                    userInterfaceInput_String = input_String[..stringIndex_Int] + "\\/" +
                        input_String[stringIndex_Int..];
                    
                }break;

                default:
                {
                
                    input_String = input_String[..stringIndex_Int] + key_ConsoleKeyInfo.KeyChar.ToString() +
                        input_String[stringIndex_Int..];

                    stringIndex_Int++;
                                    
                    userInterfaceInput_String = input_String[..stringIndex_Int] + "\\/" +
                        input_String[stringIndex_Int..];
                
                }break;

            }

        }

    }

}