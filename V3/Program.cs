using System;
using System.IO;

namespace V3
{
    class Program
    {

		public static string filepath;

        static void Main(string[] args)
        {

			filepath = args[0];

			var Exists = (File.Exists(filepath));

			if (!Exists)
			{

				Console.Clear();
				Record("ID", "First Name", "Last Name", "Age", filepath);
				Console.WriteLine("\nThe file of records has been created!\n");
			
			}

			while (true)
            {

                Menu();

            }
		}

        public static void Menu()
        {

            Console.WriteLine("\n 	DataRegister App! v2.0.0\n========================================");
            Console.WriteLine("\n1. Registry a record");
            Console.WriteLine("2. View the file of records");
            Console.WriteLine("3. Record Finder");
            Console.WriteLine("4. Record Remover");
            Console.WriteLine("5. Record Editer");
            Console.WriteLine("6. Exit\n");

            string rightselec = Console.ReadLine();

            switch (rightselec)
            {

                case "1":

                    Console.Clear();
                    Procedure();

                    break;

                case "2":

                    Console.Clear();
                    ToList();

                    break;

                case "3":

                    Console.Clear();
                    Console.WriteLine("\nEnter the ID of the record you want:");
                    string id = Convert.ToString(Console.ReadLine());
                    Finder(id);

                    break;

                case "4":

                    Console.Clear();
                    Console.WriteLine("\nEnter the ID of the record you want:");
                    string remove = Console.ReadLine();
                    Delete(remove);

                    break;

                case "5":

                    Console.Clear();
                    Console.WriteLine("\nEnter the ID of the record you want:");
                    string change = Console.ReadLine();
                    Edit(change);

                    break;

                case "6":

                    Console.Clear();
                    Environment.Exit(0);

                    break;

                default:

                    Console.Clear();
					Console.WriteLine("\nSomething went wrong, try again.\n");
                    Menu();

                    break;

            }

        }

		public static void Procedure()
		{

            while (true)
            {

                int ev = 0;
                bool isNum;

                Console.WriteLine("\nEnter the ID: ");
                var id = Console.ReadLine();

                isNum = Int32.TryParse(id, out ev);

                if (!isNum)
                {

                    Console.WriteLine("\nThe ID must be a serie of numbers!");
                    break;

                }

                if (UniqueID(id))
                {

                    Console.WriteLine("\nThis ID has already been recorded.");
                    break;

                }

                Console.WriteLine("\nEnter the First Name: ");
                string fname = Convert.ToString(Console.ReadLine());
                Console.WriteLine("\nEnter the Last Name: ");
                string lname = Convert.ToString(Console.ReadLine());
                Console.WriteLine("\nEnter the age: ");
                var age = Console.ReadLine();

                isNum = Int32.TryParse(age, out ev);

                if (!isNum)
                {

                    Console.WriteLine("\nThe Age must be a number!");
                    break;

                }
                
                Console.WriteLine("\nSave [S]; Discart[D]; Exit[E]");
                string Selection = Console.ReadLine();

                switch (Selection.ToLower())
                {

                    case "s":

                        Record(id, fname, lname, age, filepath);
                        Console.Clear();
                        Console.WriteLine("\nRecord registered correctly!\n");

                        //Procedure();

                        break;

                    case "d":

                        Console.Clear();
                        Procedure();

                        break;

                    case "e":

                        Console.Clear();
                        //Environment.Exit(0);
                        break;

                    default:

                        Console.Clear();
                        Console.WriteLine("\nSomething went wrong, try again.");

                        break;

                }

                break;

            }


		}

		public static void Record(string ID, string FirstName, string LastName, string Age, string filepath)
		{

			try
			{

				using (System.IO.StreamWriter file = new System.IO.StreamWriter(@filepath, true))
				{

					file.WriteLine(ID + "," + FirstName + "," + LastName + "," + Age);

				}

			}

			catch(Exception exc)
			{

				throw new ApplicationException("This program failed to run correctly: ", exc);

			}

		}

        public static void ToList()
        {

            string[] data = File.ReadAllLines(filepath);
            Console.WriteLine("");
            
            foreach (var lines in data)
            {

                Console.WriteLine(lines);

            }

        }

        public static void Finder(string id)
        {       

            var lines = File.ReadLines(filepath);
            int counter = 0;

            foreach(var line in lines)
            {
               
                var identity = line.Split(new char[] {','});

                if (identity[0] == id)
                {

                    Console.Clear();
                    Console.WriteLine("Record found!\n" + line);
                    counter = 1;

                }

            }

            if (counter == 0)
            {
                
                Console.Clear();
                Console.WriteLine("That record doesn't exist.");

            }

        }

        public static bool UniqueID(string id)
        {

            var content = File.ReadLines(filepath);
            bool verify = false;

            foreach (var item in content)
            {

                var exists = item.Split(new char[] {','});

                if (exists[0] == id)
                {

                    return !verify;

                }
                
            }

            return verify;

        }

        public static void Delete(string id)
        {

            bool counter = false;
            var content = File.ReadAllLines(filepath);
            System.IO.StreamWriter file = new System.IO.StreamWriter(filepath, false);

            foreach (var item in content)
            {

                var element = item.Split(",");

                if (element[0] == id)
                {

                    Console.Clear();
                    Console.WriteLine("The record has been deleted.");
                    counter = !counter;
                    continue;

                }

                file.WriteLine(item);

            }

            if (counter == false)
            {

                Console.Clear();
                Console.WriteLine("That record is not in the Registry File.");

            }

            file.Close();

        }

        public static void Edit(string id)
        {

            bool counter = false;
            var content = File.ReadAllLines(filepath);
            System.IO.StreamWriter file = new System.IO.StreamWriter(filepath, false);

            foreach (var item in content)
            {

                var element = item.Split(",");

                if (element[0] == id)
                {

                    int ev = 0;
                    bool isNum;

                    Console.Clear();
                    Console.WriteLine("Proceed to make the changes!");
                    Console.WriteLine("\nEnter the new First Name: ");
                    string fname = Console.ReadLine();
                    Console.WriteLine("\nEnter the new Last Name: ");
                    string lname = Console.ReadLine();
                    Console.WriteLine("\nEnter the new Age: ");
                    var age = Console.ReadLine();

                    isNum = Int32.TryParse(age, out ev);

                    while (!isNum)
                    {

                        Console.WriteLine("\nThe Age must be a number, try again.");
                        Console.WriteLine("\nEnter the new Age: ");
                        age = Console.ReadLine();

                        isNum = Int32.TryParse(age, out ev);

                    }

                    if (element[1] != fname)
                    {

                        Console.WriteLine("\nChanges in the First Name made successfully!");

                    }

                    if (element[2] != lname)
                    {

                        Console.WriteLine("\nChanges in the Last Name made successfully!");

                    }

                    if (element[3] != age)
                    {

                        Console.WriteLine("\nChanges in the Age made successfully!");

                    }

                    if ((element[1] == fname) && (element[2] == lname) && (element[3] == age))
                    {

                        Console.WriteLine("\nIt appears no changes has been made.");

                    }

                    file.WriteLine(id + "," + fname + "," + lname + "," + age);
                    counter = !counter;
                    continue;

                }

                file.WriteLine(item);

            }

            if (counter == false)
            {

                Console.Clear();
                Console.WriteLine("That record is not in the Registry File.");

            }

            file.Close();

        }

    }
}
