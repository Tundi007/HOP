namespace HOP;

class HOP
{

    public static void Main(string[] args)
    {   

        dynamic? UserInput_Dynamic;

        GameClass game_GameClass;

        System.Console.WriteLine("What Multiplier To Play HOP On?(\"exit\" to abort)");

        while(true){            

            UserInput_Dynamic = IInputInterface.KeyToLine_Function("What Multiplier To Play HOP On?(\"exit\" to abort)");

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

        while(game_GameClass.HOP_Function());
        
    }    

}

public class GameClass
{

    private int hop_Int = 1;

    private int hopJump_Int;

    public GameClass(int number_Int)
    {

        hopJump_Int = number_Int;

    }
    
    private (bool,string) CurrentHop_Function()
    {

        string hop_String = hop_Int.ToString();

        if(hop_Int+1 % hopJump_Int == 0)return (true,IInputInterface.KeyToLine_Function(hop_Int.ToString()).ToLower());
        
        if(hop_Int % hopJump_Int == 0)hop_String = "hop";

        return (false,IInputInterface.KeyToLine_Function(hop_String).ToLower());

    }

    private void NextNumber_Function()
    {

        hop_Int++;

    }

    private int CurrentNumber_Function()
    {

        return hop_Int;

    }

    public bool HOP_Function()
    {

        Console.Clear();

        string userHop_String = "";

        ((bool isHOP_Bool, userHop_String), bool userHop_Bool,_) =
            (CurrentHop_Function(), userHop_String == "hop",int.TryParse(userHop_String, out int userNumber_Int));

        if(isHOP_Bool & !userHop_Bool)
        {
            
            System.Console.WriteLine($"Caught You! We Were On {CurrentNumber_Function()} But You Missed The \"HOP\"!");

            return false;
            
        }

        if(userNumber_Int != CurrentNumber_Function()+1)
        {

            System.Console.WriteLine($"Wrong! Continue? (press \"y\" for yes and \"n\" for no)");

            ConsoleKey userKey_ConsoleKey;

            while((userKey_ConsoleKey = Console.ReadKey().Key) != ConsoleKey.Y || userKey_ConsoleKey != ConsoleKey.N)
            {

                Console.Clear();
                
                System.Console.WriteLine("Please Press Either \"Y\" For Continuing Or \"N\" For Admitting Defeat!");
                
            }

            if(userKey_ConsoleKey == ConsoleKey.N)
            {

                System.Console.WriteLine($"You Lost On {CurrentNumber_Function()}");

                Thread.Sleep(500);

                return false;
                
            }
            
            return true;
            
        }

        NextNumber_Function();

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

                    userInterfaceInput_String = input_String[..stringIndex_Int] + "->" +
                        input_String[stringIndex_Int..];

                }break;

                case ConsoleKey.RightArrow:{
                 
                    if(stringIndex_Int >= input_String.Length)break;

                    stringIndex_Int++;

                    userInterfaceInput_String = input_String[..stringIndex_Int] + "->" +
                        input_String[stringIndex_Int..];

                }break;

                case ConsoleKey.Delete:
                {                    

                    if(stringIndex_Int >= input_String.Length)break;
                    
                    input_String = input_String.Remove(stringIndex_Int,1);

                    userInterfaceInput_String = input_String[..stringIndex_Int] + "->" +
                        input_String[stringIndex_Int..];
                
                }break;

                case ConsoleKey.Backspace:
                {

                    if (stringIndex_Int < 1)break;
                        
                    input_String = input_String.Remove(stringIndex_Int-1,1);

                    stringIndex_Int--;

                    userInterfaceInput_String = input_String[..stringIndex_Int] + "->" +
                        input_String[stringIndex_Int..];
                    
                }break;

                default:
                {
                
                    input_String = input_String[..stringIndex_Int] + key_ConsoleKeyInfo.KeyChar.ToString() +
                        input_String[stringIndex_Int..];

                    stringIndex_Int++;
                                    
                    userInterfaceInput_String = input_String[..stringIndex_Int] + "->" +
                        input_String[stringIndex_Int..];
                
                }break;

            }

        }

    }

}