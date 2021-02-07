using System;
using System.IO;
using System.Linq;

namespace ByteSequenceInverter
{
    class Program
    {
        static void Main(string[] args)
        {
            //byte[] B = File.ReadAllBytes(Environment.CurrentDirectory + "\\T" + "\\Test.txt");
            Console.WriteLine("Enter \"+\" to start.");
            if (Console.ReadLine() == "+")
            {
                //string StrPath = File.ReadAllText(Environment.CurrentDirectory + "\\Path.txt", Encoding.UTF8);
                Console.WriteLine("Enter the path to the folder [D:\\].");
                string StrPath = Console.ReadLine();
                string[] AllFiles = Directory.GetFiles(StrPath, "*.*", SearchOption.AllDirectories);
                int Counter = 0;
                string StrLogsPath = Environment.CurrentDirectory + "\\Logs.txt";

                using (StreamWriter SW = new StreamWriter(File.Create(StrLogsPath)))
                {
                    foreach (var Str in AllFiles)
                    {
                        byte[] B;
                        try
                        {
                            if (new FileInfo(Str).Length > 500 * 1024 * 1024)
                            {
                                SW.WriteLine(Str + " | The file is too long.");
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(Str + " | The file is too long.");
                                Console.ForegroundColor = ConsoleColor.Gray;
                                continue;
                            }
                            B = File.ReadAllBytes(Str);
                        }
                        catch (Exception Ex)
                        {
                            SW.WriteLine(Str);
                            SW.WriteLine(Ex.Message);
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(Str);
                            Console.WriteLine(Ex.Message);
                            Console.ForegroundColor = ConsoleColor.Gray;
                            continue;
                        }
                        try
                        {
                            B = B.Reverse().ToArray();
                            Console.WriteLine(Str);
                            SW.WriteLine(Str);
                            File.WriteAllBytes(Str, B);
                            Counter++;
                        }
                        catch (UnauthorizedAccessException Ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            SW.WriteLine(Ex.Message);
                            Console.WriteLine(Ex.Message);
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }
                    }
                    SW.WriteLine("Processed files: " + Counter + " out of " + AllFiles.Length + ".");
                    SW.Close();
                }
                Console.WriteLine("Processed files: {0} out of {1}.", Counter, AllFiles.Length);
                Console.ReadKey();
            }
            else
            {
                return;
            }
        }
    }
}
