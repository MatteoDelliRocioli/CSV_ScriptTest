using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace CSV_ScriptTest
{
    public class CSV_Decoder
    {
        StreamReader reader = new StreamReader(File.OpenRead(@"Path"));
        List<string[]> listRows = new List<string[]>();
        static List<string> text = new List<string>();
        static int wordCounter = 0;
        static int quotationMarkCounter = 0;
        static string word = "";
        static string old_word = "";


        static int csvString;
        static bool startPhrase = false;
        static bool endPhrase = false;

        public List<string[]> Decode()
        {
            while (!reader.EndOfStream)
            {
                //var csvString = reader.ReadToEnd();
                //string[] splittedFile = Regex.Split(csvString, @",("")(?#matches the lines after ,"")");


                while (!(csvString = reader.Read()).Equals(-1))
                {
                    //Console.WriteLine(csvString);
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
                        //quotationMarkCounter++;
                        //startPhrase = true;
                        //if (startPhrase)
                        //{
                        //    word += (char)csvString;
                        //}
                        //else
                        //{
                        //    if (!WordEmptyControl(word))
                        //    {
                        //        word = AddWord(text, word);
                        //    }
                        //}
                        //break;

                        case 44: //case ","
                            //Console.WriteLine("egolo");
                            csvString = 0;
                            //Console.Write((char)csvString);
                            word += (char)csvString; //this adds a \0 at the end of a word
                            if (!startPhrase)
                            {
                                word = AddWord(text, word);
                            }
                            break;

                        default:
                            //Console.Write((char)csvString);
                            word += (char)csvString;
                            break;
                    }

                    if (old_word.Contains("Note"))
                    {
                        text.Clear();
                    }
                }
                ////Console.WriteLine(text);
                //foreach (string term in text)
                //{
                //    //Console.Write(term);
                //    if (term.Contains("Note"))
                //    {
                //        text.Clear();
                //        break;
                //    }
                //}

                //foreach (string term in text)
                //{
                //    Console.Write(term);
                //}
                text.ForEach(p => Console.Write(p));
                Console.ReadLine();



                /*4th try*/
                //foreach (string element in splittedFile)
                //{
                //    element.Replace(@"""", "");
                //    element.Replace("\"", "");
                //    element.Trim('/');
                //}
                //for (int i = 0; i < splittedFile.Length; i++)
                //{
                //    splittedFile[i].Replace(",", "");
                //    Console.WriteLine(splittedFile[i]);
                //}


                /*3rd try*/
                //foreach (string element in splittedFile)
                //{
                //    switch (element)
                //    {
                //        case ("\""):
                //            break;

                //        default:
                //            string[] splittedFile2 = Regex.Split(element, @"(""),");
                //            listRows.Add(splittedFile2);
                //            Console.WriteLine();
                //            break;
                //    }
                //}


                /*2nd try*/
                //foreach (string element in splittedFile)
                //{
                //    if (element.Equals("\""))
                //    { }
                //    else
                //    {
                //        string[] splittedFile2 = Regex.Split(element, @"(""),");
                //        listRows.Add(splittedFile2);
                //    }
                //}

                /*1st try*/
                //foreach (string element in splittedFile)
                //{
                //    listRows.Add(element);
                //    if (element.Contains("Note"))
                //    {
                //        listRows.Clear();
                //    }
                //}
                //listRows.Add(splittedFile);
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
                return null;
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