//==================================================================
//=                                                                =    
//= File: Program.cs                                               =
//= Developer: Kenneth Bathgate                                    =
//= Details : Entry point to program that loads SSEM class         =
//=                                                                =
//==================================================================

using System;
using System.Collections.Generic;
using System.Text;


namespace test
{
    class Program
    {
      
        static void Main(string[] args)
        {
            Console.Title = "SSEM Simulator";
           
         SSEM ssem = new SSEM();
         ssem.Menu();
        
      
                
            
            }

        }
    }

