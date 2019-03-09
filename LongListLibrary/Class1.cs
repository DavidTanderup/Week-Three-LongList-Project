using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace LongListLibrary
{
    public class ClassLists
    {



        /// <summary>
        /// Adds a new item to the list and then updates the Data File
        /// t.2 is set to false indicating the item has not been completed.
        /// </summary>
        public void AddThingsToMyList(string userInput, List<Tuple<string, bool>> MyList)
        {

            Files files = new Files();


            MyList.Add(new Tuple<string, bool>(userInput, false));

            files.SaveItem(MyList);

        }


        /// <summary>
        /// Removes the items from the specified location on the list.
        /// </summary>
        public void RemoveThingsFromMyList(List<Tuple<string, bool>> MyList, int indexNumber)
        {

            MyList.RemoveRange(0, indexNumber);
            

        }


        /// <summary>
        /// Marks the item in the list complete by creating a tuple with the altered bool value
        /// in the same position as the previous version. The list is then saved 
        /// to the data file.
        /// </summary>
        public void MarkedAsComplete(List<Tuple<string, bool>> MyList, int indexNumber)
        {
            Files files = new Files();
                       
            // Unpack the tuple
            var myTuple = MyList[indexNumber];
            var row = myTuple.Item1;  // value of 0
            var col = myTuple.Item2;  // value of 1

            // creates a new tuple in the index position of the old value
            MyList[indexNumber] = new Tuple<string, bool>(row, true);

            files.SaveItem(MyList);


        }




    }

    public class Files
    {
        // Ref: stackoverflow.com/questions/20823698/c-sharp-serialize-deserialize-or-save-
        //      load-a-list-of-tuples
        public string FileLocation()
        {
            string fileLocation = @"C:\Users\David Tanderup\Desktop\Data2.dat";
            return fileLocation;
        }

        /// <summary>
        /// Saves the changes made to the data file
        /// </summary>
        public void SaveItem(List<Tuple<string, bool>> MyList)
        {
            string fileLocation = FileLocation();


            FileStream stream = new FileStream(fileLocation, FileMode.OpenOrCreate);

            BinaryFormatter formatter = new BinaryFormatter();

            formatter.Serialize(stream, MyList);

            stream.Close();
        }


        /// <summary>
        /// Loads populates program list from the data save file
        /// </summary>
        public List<Tuple<string, bool>> LoadItem()
        {
            string fileLocation = FileLocation();
            List<Tuple<string, bool>> MyList = new List<Tuple<string, bool>>();

            /// Hardcode to the desired file location.
            FileStream inStr = new FileStream(fileLocation, FileMode.Open);

            BinaryFormatter bf = new BinaryFormatter();

            MyList = bf.Deserialize(inStr) as List<Tuple<string, bool>>;

            inStr.Close();

            return MyList;
        }



    }



    public class ProgramLaunch
    {
        public void Formalities()
        {
            
            ClassLists classLists = new ClassLists();
            Files files = new Files();
            List<Tuple<string, bool>> MyList = files.LoadItem();
            if (MyList.Count > 25)
            {
                
                int count = 0;
                /// Looks at the first 25 items on the list to see if they are complete. If all 25 are complete they are deleted.
                foreach (Tuple<string, bool> item in MyList.GetRange(0, 25))
                {
                    /// if a single item is uncomplete all 25 remain
                    if (item.Item2 == false)
                    {
                         
                    }
                    else
                    {
                        
                        count++;
                    }
                  
                }
                /// removes the 25 complete items and saves the list.
                if (count == 25)
                {
                    classLists.RemoveThingsFromMyList(MyList, 25);
                    
                    files.SaveItem(MyList);
                }
               
                
            }
        }
        /// <summary>
        /// Checks for a save file and if none is found creates one.
        /// </summary>
        public void CreateNewSaveFile()
        {
            Files files = new Files();

            List<Tuple<string, bool>> MyList = new List<Tuple<string, bool>>();

            string fileLocation = files.FileLocation();

            try
            {
                FileStream inStr = new FileStream(fileLocation, FileMode.Open);

                BinaryFormatter bf = new BinaryFormatter();

                MyList = bf.Deserialize(inStr) as List<Tuple<string, bool>>;

                inStr.Close();

            }
            catch (FileNotFoundException)
            {
                FileStream stream = new FileStream(fileLocation, FileMode.CreateNew);

                BinaryFormatter formatter = new BinaryFormatter();

                formatter.Serialize(stream, MyList);

                stream.Close();

            }
        }

        /// <summary>
        /// reviews the first 25 items to see if they have been completed. If they have been 
        /// completed they are deleted.
        /// </summary>



    }


    public class MainMenu
    {
        /// <summary>
        /// Clears the screen and sets white as default color
        /// </summary>
        public void MenuDisplaySettings()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Displays the text for the entry menu.
        /// </summary>
        public void WelcomeToYourList()
        {
            MenuDisplaySettings();

            Console.WriteLine("\n\n\tTo-Do List Main Menu");

            Console.WriteLine("-------------------------------------");

            Console.WriteLine("\t1) Add Tasks");

            Console.WriteLine("\n\t2) View Current List");

            Console.WriteLine("\n\t3) Quit");

            Console.Write("\nPlease Make Your Selection: ");
        }

        /// <summary>
        /// Takes the user's input and redirects to the correct method.
        /// </summary>
        /// <returns></returns>       
        public bool MainMenuUserInput()
        {

            var userInput = Console.ReadLine();
            // Add Tasks to the list
            if (userInput == "1")
            {
                AddToListMenu();
                return true;
            }
            // View Task List
            else if (userInput == "2")
            {
                ViewListMenu();
                return true;

            }
            // Quit
            else if (userInput == "3")
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        /// <summary>
        /// Adding Items to the user list by importing the list from file. Adding to the list and then saving the list.
        /// After enter the item the user is returned to the main menu
        /// </summary>
        public void AddToListMenu()
        {
            MenuDisplaySettings();

            ClassLists cL = new ClassLists();

            Files files = new Files();

            List<Tuple<string, bool>> MyList = files.LoadItem();

            Console.WriteLine("\n\tAdd Task Menu");

            // Change text
            Console.Write("Add Something: ");

            string userInput = Console.ReadLine();

            cL.AddThingsToMyList(userInput, MyList);

        }


        /// <summary>
        /// Displays Texts for the View Menu
        /// </summary>
        public void ViewList()
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("\n\tTo-Do List");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Complete");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Incomplete");
            Console.WriteLine("------------------");

        }


        /// <summary>
        /// View menu pulls from the load method > which opens the file location and assigns the list values to "MyList"
        /// </summary>
        public void ViewListMenu()
        {
            Files files = new Files();
            ClassLists classLists = new ClassLists();
            List<Tuple<string, bool>> MyList = files.LoadItem();

            /// The base that the item numbers in the console display calculate from.
            int i = 1;

            /// used to calulate list position
            int currentPage = 0;

            int totalPages = (int)Math.Ceiling(MyList.Count() / 25.0);

            /// To Do

            bool loopComplete = false;

            do
            {
                /// Menu display clears the console and resets the color to white.
                MenuDisplaySettings();
                /// view list is the text displayed for the user in the console window.
                ViewList();
                int lowerLimit = currentPage * 25;

                /// the count must consider if there are less than 25 items on the list. The else statement covers the
                /// range should there be more than 25 items. The code will not run without the first if statement.
                int count;
                if (MyList.Count < 25)
                {
                    count = MyList.Count;
                }
                else
                {             /// needs to start with 1  
                    count = (currentPage + 1 == totalPages) ? (MyList.Count - currentPage * 25) : 25;

                }
                /// Creates the list of the items currently on the list. Also evaluates if the task is complete.
                foreach (Tuple<string, bool> item in MyList.GetRange(lowerLimit, count))
                {
                    if (item.Item2 == true)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine($"{i++}) {item.Item1}");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine($"{i++}) {item.Item1}");
                    }
                }
                /// re-sets the counter to one so that the list only displays items 1 - 25 for each page.
                i = 1;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"\nCurrent Page #{currentPage + 1} of {totalPages}");
                Console.WriteLine("---------------------");
                // condition next && previous pages

                Console.WriteLine($"'n') Next Page");
                Console.WriteLine($"'p') Previous Page");


                Console.Write("\nEnter Your Selection: ");
                string userInput = Console.ReadLine().ToLower();

                try
                {


                    MenuDisplaySettings();

                    int intUserInput = Convert.ToInt32(userInput);

                    /// calculates list index position
                    int indexNumber = ((currentPage * 25) + intUserInput) - 1;


                    Console.WriteLine($"\nTask:\t{MyList[indexNumber].Item1}");

                    Console.WriteLine("\n\n1) Complete");

                    Console.WriteLine("\n2) Do It Later");

                    Console.ForegroundColor = ConsoleColor.Gray;

                    Console.WriteLine("   This Option will mark the item as\n" +
                                        "   complete and create a Incomplete\n" +
                                        "   at the end of the list. ");

                    Console.Write("\nPlease make your selection: ");
                    string completeOrDoItLater = Console.ReadLine();

                    string newTask = MyList[indexNumber].Item1;

                    if (completeOrDoItLater == "1")
                    {
                        classLists.MarkedAsComplete(MyList, indexNumber);
                        Console.WriteLine("\nYour task has been marked complete");
                        Console.ReadLine();
                    }
                    else if (completeOrDoItLater == "2")
                    {
                        classLists.MarkedAsComplete(MyList, indexNumber);
                        classLists.AddThingsToMyList(newTask, MyList);
                        Console.WriteLine("\nYour list has been updated");
                        Console.ReadLine();
                    }
                    else
                    {
                        continue;
                    }


                    Console.ReadLine();



                }
                catch (Exception)
                {
                    /// handles next page input
                    if (userInput == "n")
                    {
                        if (currentPage == totalPages)
                        {
                            currentPage = 0;
                            continue;
                        }
                        else
                        {
                            currentPage++;
                            continue;
                        }

                    }

                    /// handles previous page input
                    else if (userInput == "p")
                    {
                        if (currentPage == 0)
                        {
                            currentPage = (totalPages - 1);
                            continue;
                        }
                        else
                        {
                            currentPage--;
                            continue;
                        }
                    }
                    else
                    {
                        loopComplete = true;
                    }


                    //Console.ReadLine();




                }
                /// Previous


            } while (!loopComplete);
            Console.ReadLine();

 


        }





    }







}
