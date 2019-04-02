using System;

namespace CSV_ScriptTest
{
    class Program
    {
        static void Main(string[] args)
        {
            CSV_Decoder decoder = new CSV_Decoder();

            decoder.Decode();
        }
    }
}
