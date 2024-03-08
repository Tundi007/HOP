using System.Net.Http.Headers;
using System.Security.Cryptography;

namespace HOP;

class HOP
{

    public static void Main(string[] args)
    {   

        dynamic? UserInput_Dynamic;

        GameClass game_GameClass;

        System.Console.WriteLine("What Multiplier To Play HOP On?\"exit\"");

        while(true){

            UserInput_Dynamic = System.Console.ReadLine();

            UserInput_Dynamic??="";

            if(UserInput_Dynamic.ToLower() == "exit")return;

            if(int.TryParse(UserInput_Dynamic, out int userNumber_Int))
            {

                game_GameClass = new(userNumber_Int);
                
                break;

            }
            
        }        

        bool ongoing_Bool = true;

        while(ongoing_Bool)
        {

            ongoing_Bool = game_GameClass.HOP_Function(InputClass.KeyToLine_Function().ToLower());            

        }
        
    }    

}

public class GameClass
{

    private int hop_Int;

    private int hopJump_Int;

    public GameClass(int number_Int)
    {

        hop_Int = 0;

        hopJump_Int = number_Int;

    }


    public bool HOP_Function(string userHop_String)
    {

        hop_Int++;

        System.Console.WriteLine("> "+hop_Int);

        _ = int.TryParse(userHop_String, out int userNumber_Int);

        if(userNumber_Int%hopJump_Int==0) return false; //or if(hop_Int % hopJump_Int == 0 & userHop_String!="hop") return false;

        if(userNumber_Int != hop_Int) return false;        

        return true;

    }

}

public class InputClass
{

    public static string KeyToLine_Function()
    {
        
        ConsoleKeyInfo key_ConsoleKeyInfo;

        string input_String = "";

        int stringIndex_Int = 0;

        string menu_String = "Enter Your Text, Paste Via \"Ctrl\" + \"Shift\" + \"V\", \"End\" To Proceed To Next Step And \"Escape\" To Abort Procedure.";

        string userInterfaceInput_String = input_String;

        while(true)
        {

            Console.Clear();

            System.Console.WriteLine(menu_String);

            System.Console.WriteLine(userInterfaceInput_String);

            key_ConsoleKeyInfo = Console.ReadKey(false); 

            switch (key_ConsoleKeyInfo.Key)
            {

                case ConsoleKey.Enter: Console.Clear(); return input_String;

                case ConsoleKey.End:return "Exit";

                case ConsoleKey.Escape:return "Exit";

                case ConsoleKey.LeftArrow:
                {
                 
                    if(stringIndex_Int < 1)break;

                    stringIndex_Int--;

                    userInterfaceInput_String = input_String[..stringIndex_Int] + "_" +
                        input_String[stringIndex_Int..];

                }break;

                case ConsoleKey.RightArrow:{
                 
                    if(stringIndex_Int >= input_String.Length)break;

                    stringIndex_Int++;

                    userInterfaceInput_String = input_String[..stringIndex_Int] + "_" +
                        input_String[stringIndex_Int..];

                }break;

                case ConsoleKey.Delete:
                {                    

                    if(stringIndex_Int >= input_String.Length)break;
                    
                    input_String = input_String.Remove(stringIndex_Int,1);

                    userInterfaceInput_String = input_String[..stringIndex_Int] + "_" +
                        input_String[stringIndex_Int..];
                
                }break;

                case ConsoleKey.Backspace:
                {

                    if (stringIndex_Int < 1)break;
                        
                    input_String = input_String.Remove(stringIndex_Int-1,1);

                    stringIndex_Int--;

                    userInterfaceInput_String = input_String[..stringIndex_Int] + "_" +
                        input_String[stringIndex_Int..];
                    
                }break;

                default:
                {
                
                    input_String = input_String[..stringIndex_Int] + key_ConsoleKeyInfo.KeyChar.ToString() +
                        input_String[stringIndex_Int..];

                    stringIndex_Int++;
                                    
                    userInterfaceInput_String = input_String[..stringIndex_Int] + "_" +
                        input_String[stringIndex_Int..];
                
                }break;

            }

        }

    }

}