using System;
using System.Collections.Generic;
using System.IO;

namespace CSV_ScriptTest
{
    public class CSV_Decoder
    {
        StreamReader reader = new StreamReader(File.OpenRead(@"C:\Users\mrhus\source\repos\MVLabs\TESTS\CSV_ScriptTest\CSV_ScriptTest\csv.csv"));
        //StreamReader reader = new StreamReader(File.OpenRead(@"C:\Users\mrhus\source\repos\MVLabs\TESTS\CSV_ScriptTest\CSV_ScriptTest\Test.csv"));
        List<string[]> listRows = new List<string[]>();
        static List<string> text = new List<string>();
        static int wordCounter = 0;
        static int quotationMarkCounter = 0;
        static string word = "";
        static string old_word = "";


        static int csvString;
        static bool startPhrase = false;

        public List<string[]> Decode()
        {
            while (!reader.EndOfStream)
            {

                while (!(csvString = reader.Read()).Equals(-1))
                {
                    switch (csvString)
                    {
                        case 10: //case "\n"
                            word += (char)csvString;
                            if (!startPhrase)
                            {
                                word = AddWord(text, word);
                            }
                            break;

                        case 34: //case '"'
                            word = AddPhrase(word);
                            break;

                        case 44: //case ","
                            if (!startPhrase)
                            {
                                word = AddWord(text, word);
                            }
                            else
                            {
                                word += (char)csvString;
                            }
                            break;

                        default:
                            word += (char)csvString;
                            break;
                    }

                    if (old_word.Contains("Note"))
                    {
                        text.Clear();
                    }
                }
                text.ForEach(p => Console.Write(p));
                Console.ReadLine();
            }


            return listRows;
        }

        public static bool WordEmptyControl(string word)
        {
            return word.Equals("")
                ? true
                : false;
        }

        public static string AddWord(List<string> list, string word)
        {
            if (word.Equals("") || word.Equals("\0") || word.Equals("\r\n"))
            {
                return "";
            }
            else
            {
                list.Add(word);
                wordCounter++;
                old_word = word;
                return word = "";
            }
        }

        public static string AddPhrase(string word)
        {
            quotationMarkCounter++;
            if (quotationMarkCounter == 1)
            {
                startPhrase = true;
                word += (char)csvString;
            }
            else if (quotationMarkCounter == 2)
            {
                if (!WordEmptyControl(word))
                {
                    startPhrase = false;
                    quotationMarkCounter = 0;
                    return AddWord(text, word);
                }
            }
            return "";
        }
    }
}