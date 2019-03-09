using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using LongListLibrary;

namespace LongList
{
    class Program
    {
        static void Main(string[] args)
        {
            ProgramLaunch programLaunch = new ProgramLaunch();
            programLaunch.CreateNewSaveFile();
            programLaunch.Formalities();
            MainMenu mM = new MainMenu();

            bool displayMenu = true;
            while (displayMenu)
            {
                mM.WelcomeToYourList();
                displayMenu = mM.MainMenuUserInput();

            }

            Console.Clear();
            Console.WriteLine("Press any key to exit....");
            Console.ReadLine();








        }







    }







}
