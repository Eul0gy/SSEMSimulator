//==================================================================
//=                                                                =    
//= File: Program.cs                                               =
//= Developer: Kenneth Bathgate                                    =
//= Details :  SSEM class, contains the needed methods for         =
//=            running SSEM Simulator                              =
//==================================================================


using System;
using System.IO;
using nsClearConsole;

namespace test
{
    class SSEM
    {
       
        private String[,] Store = new String[32, 3] ;  // THE STORE
        private int Accumulator=0 ; // THE ACCUMULATOR
        private int CI= 0 ;  // THE CI 
     
        
        // Method : Menu()
        // Details: Displays and menu and loops round till the your choice is = 4
        public void Menu()
        {
            
           ClearConsole ClearMyConsole = new ClearConsole();
          int choice= 0;

          while (choice != 4)
          {
             ClearMyConsole.Clear(); // Clear the screen
              Console.WriteLine("=============================================");
              Console.WriteLine("==      SSEM 'Manchester Baby' Simulator   ==");
              Console.WriteLine("==      --------------------------------   ==");
              Console.WriteLine("==                    by                   ==");
              Console.WriteLine("==              Kenneth Bathgate           ==");
              Console.WriteLine("=============================================");
              Console.WriteLine("==    Please Select:                       ==");
              Console.WriteLine("==                                         ==");
              Console.WriteLine("==    1) Load Instruction Set              ==");
              Console.WriteLine("==    2) Run Fetch and Execute             ==");
              Console.WriteLine("==    3) Make Changes To Instructions      ==");
              Console.WriteLine("==    4) Exit Program                      ==");
              Console.WriteLine("=============================================");
              Console.WriteLine(" ");
              Console.Write("Choice: ");
    
              string input = Console.ReadLine();
              choice = Convert.ToInt32(input);
              switch (choice)
              {
                  case 1:
                     
                      LoadInstructions();
                      break;
                  case 2:
                      
                      FetchExecute();
                      break;
                  case 3:
                      MakeChanges();
                      break;
                  case 4:
                      Console.WriteLine("Good Bye  ");
                      break;
                  
                 default:
                      Console.WriteLine("Opps Something went FUBAR!");
                     
                      break;
              }

          }




        }

        // Method: DisplayData()
        // Details: Used for debugging to display the store, accumulator and CI
        private void DisplayData()
        {
            String Input;
            Console.WriteLine("Memory Line   Instruction  Data    ");
            for (int a = 0; a <= 31; a++)
            {
                if (Store[a, 1] != "NULL")
                {
                    Console.Write("  " + Store[a, 0] + "           ");
                    Console.Write(Store[a, 1] + "         ");
                    Console.WriteLine(Store[a, 2] + " ");
                }


            }
            Console.Write("Accumulator: ");
            Console.WriteLine(Accumulator);
            Console.Write("CI: ");
            Console.WriteLine(CI);
            Console.WriteLine("Press any Key to continue");
            Input = Console.ReadLine();
            switch (Input)
            {
                default:
                    break;

            }
        }

        // Method: LoadInstructions
        // Details: loads from a file the instruction set from a txt file
        private void LoadInstructions()
        {
            StreamReader objReader = new StreamReader("Instructions.txt");

            String Input;
            String message;

            Console.WriteLine("Loading and Displaying Instruction Set");
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("Memory Line   Instruction  Data    ");
            for (int a = 0; a <= 31; a++)
            {

                message = objReader.ReadLine();

                String[] g = message.Split(' ');

                Store[a,0] = g[0];
                Store[a,1] = g[1];
                Store[a,2] = g[2];
                if (Store[a, 1] != "NULL")
                {
                    Console.Write("  " + Store[a, 0] + "           ");
                    Console.Write(Store[a, 1] + "         ");
                    Console.WriteLine(Store[a, 2] + " ");
                }
                
              
            }
            objReader.Close();
            
            Console.WriteLine("Press any Key to continue");
            Input = Console.ReadLine();
            switch(Input)
            {
                default:
                    break;
            }



        }

        //Method: MakeChanges()
        //Details: Allows the user to check each line of store and change if needed 
        private void MakeChanges()
        {
        String Input;
        Console.WriteLine("The Program will step through each line and ask you if you want");
        Console.WriteLine("edit that line");
         
            for (int row= 0; row <= 31; row++)
            {
                if (Store[row, 1] != "NULL"){

                    Console.Write("  " + Store[row, 0] + "           ");
                    Console.Write(Store[row, 1] + "         ");
                    Console.WriteLine(Store[row, 2] + " ");
                    Console.WriteLine("");
                    Console.WriteLine("do you want to change this line, Y/N ");
                    Input = Console.ReadLine();
                    char Input2;
                    Input2 = Convert.ToChar(Input);
                    switch (Input2)
                    {
                        case 'y':
                            String a; String b;
                            Console.WriteLine("please enter the Instruction");
                            a = Console.ReadLine();
                            Console.WriteLine("Please enter Data/Mem Line");
                            b = Console.ReadLine();
                            Store[row, 1] = a;
                            Store[row, 2] = b;
                            break;
                        case 'n':
                            break;

                        default:
                            break;
                    }
                    
                    
                    }
                 

            }  


        }
       

        //Method:FetchExecute()
        //details: runs through the store and does the fetch execute cycle
        private void FetchExecute()
        {
            String Input;
            
            Console.WriteLine("Running Fetch Execute Cycle");
          
            
           while (Store[CI, 1] != "HLT")
            {
                Console.WriteLine("Current Instruction: " + CI);
                Console.Write(Store[CI, 0] + " ");
                Console.Write(Store[CI, 1] + " ");
                Console.WriteLine(Store[CI, 2] + " ");
                Console.WriteLine("Accumulator:" + Accumulator);
               switch (Store[CI, 1])
                {
                    case "JMP":
                        int jump;
                        jump= Convert.ToInt32(Store[CI, 2]);
                        jump = jump +1;
                        CI = jump;
                       
                        break;
                    case "JPR":
                        int JPR;
                        JPR = Convert.ToInt32(Store[CI, 2]);
                        CI = JPR;
                        
                        break;
                    case "LDN":
                       int temp;//loads the address 
                        int temp2;// holds the actuall data thats being negitived 
                        temp = Convert.ToInt32(Store[CI, 2]);
                        temp2= Convert.ToInt32(Store[temp, 2]);
                        
                        temp2 = -temp2;
                        Accumulator = temp2;
                        CI++;
                        break;
                    case "STO":
                        int inst;
                        inst= Convert.ToInt32(Store[CI,2]);

                        string mem;
                        mem = Convert.ToString(Accumulator);
                        Store[inst,2]= mem;
                        CI++;
                        break;
                    case "SUB":
                        int var;
                        int var2; 
                        var=Convert.ToInt32(Store[CI, 2]);
                       var2 = Convert.ToInt32(Store[var,2]);
                        Accumulator = Accumulator- var2;
                        CI++;
                          break;
                    case "SKN":
                        int neg;
                        neg =Convert.ToInt32(Store[CI,2]);
                         if (neg < 0 )
                         {
                             CI++;
                         }
                         CI++;
                         break;
                    default:
                        CI++;
                        break;
                }

          
                
            }
            Console.WriteLine("=================================");
            Console.WriteLine("Final Current Instruction: " + CI);
            Console.WriteLine("Final Accumlator: " + Accumulator);
            Console.WriteLine("Press any Key to continue");
            Console.WriteLine("=================================");
            Input = Console.ReadLine();
                     switch (Input)
                       {
                           default:
                               break;
                       }




        }

   


    }
}
