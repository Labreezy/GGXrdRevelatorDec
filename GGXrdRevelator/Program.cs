using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GGXrdRevelator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("GGXrdRevelator v0.2 by gdkchan/Labryz");
            Console.ResetColor();

            Console.WriteLine("Guilty Gear XRD -REVELATOR- UPK/Steam AH3 PAC decrypter");

            Console.WriteLine(string.Empty);

            uint OutVal = 0;
            bool encrypt = false;
            if (args.Length > 0)
            {
                encrypt = args[0].Equals("-e");
                if(!encrypt){
                    switch (args[0].ToLower())
                    {
                        case "-revel": OutVal = 0x72642a6f; break;
                        case "-sign": OutVal = 0x43415046; break;
                    }
                } else {
                    switch (args[1].ToLower())
                    {
                        case "-revel": OutVal = 0x72642a6f; break;
                        case "-sign": OutVal = 0x43415046; break;
                    }
                }
            }

            if (args.Length < 2 || OutVal == 0)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Usage:");
                Console.ResetColor();

                Console.WriteLine("GGXrdRevelator [-e] (-revel|-sign) infile");
                Console.WriteLine("-revel  Decrypts GG Xrd REVELATOR files");
                Console.WriteLine("-sign  Decrypts GG Xrd SIGN files");

                Console.WriteLine("-e, when added, encrypts the input file. Otherwise, the program decrypts the file.");

                Console.WriteLine(string.Empty);

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Examples:");
                Console.ResetColor();

                Console.WriteLine("GGXrdRevelator -revel ELP_VOICE_JPN_A_SF.upk");
                Console.WriteLine("GGXrdRevelator -e -sign chara_split_28.pac");

                Console.WriteLine(string.Empty);

                Console.WriteLine("The output file name is the input name with .dec or .enc extension\ndepending on if you used the -e flag.");

                return;
            }
            string FileName = encrypt ? args[2] : args[1];
            string Name = Path.GetFileName(FileName).ToUpper();
            if (Name.EndsWith(".dec") || Name.EndsWith(".enc")){
                Name = Name.Substring(0,Name.Length-4);
            }

            uint Seed = 0;

            foreach (char Chr in Name)
            {
                Seed *= 137;
                Seed += Chr;
            }

            MersenneTwister MT = new MersenneTwister(Seed);

            using (FileStream Input = new FileStream(FileName, FileMode.Open))
            {
                string ext = encrypt ? ".enc" : ".dec"; 
                using (FileStream Output = new FileStream(FileName + ext, FileMode.Create))
                {
                    BinaryReader Reader = new BinaryReader(Input);
                    BinaryWriter Writer = new BinaryWriter(Output);
                    if (!encrypt){
                        while (Input.Position + 4 <= Input.Length)
                        {
                            Writer.Write(OutVal ^= Reader.ReadUInt32() ^ MT.GenRandomNumber());
                        }
                    } else {
                        uint OldOut = OutVal;
                        while(Input.Position + 4 <= Input.Length){
                            uint NewOut = Reader.ReadUInt32();
                            Writer.Write(OldOut ^ NewOut ^ MT.GenRandomNumber());
                            OldOut = NewOut;
                        
                        }                       
                    }      
                }
            }
            Console.WriteLine("Finished!");
        }
    }
}
