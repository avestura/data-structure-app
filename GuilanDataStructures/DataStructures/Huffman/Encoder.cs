using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuilanDataStructures.DataStructures.Huffman
{
    public  class Encoder
    {

        public  PriorityQueue PQueue { get; set; }
        public  BinaryTree Root { get; set; }

        private  readonly byte[] MetaAndDataSeparatorKey = Encoding.Unicode.GetBytes("@db:");
        private  readonly byte[] CharMapCharacterSeparatorKey = Encoding.Unicode.GetBytes(",;");

        private  readonly bool[] MetaAndDataSeparatorBool = { false, false, false, false, false, false, true, false, false, false, false, false, false, false, false, false, false, false, true, false, false, true, true, false, false, false, false, false, false, false, false, false, false, true, false, false, false, true, true, false, false, false, false, false, false, false, false, false, false, true, false, true, true, true, false, false, false, false, false, false, false, false, false, false };
        private  readonly bool[] CharMapCharacterSeparatorBool = { false, false, true, true, false, true, false, false, false, false, false, false, false, false, false, false, true, true, false, true, true, true, false, false, false, false, false, false, false, false, false, false };

        public void Encode(string text, string url)
        {

            ResetEncoder();

            PQueue = new PriorityQueue();

            var charRepaetDictionary = text.CharacterRepeat();

            PQueue.FillQueue(charRepaetDictionary);

            Root = PQueue.BuildTree(charRepaetDictionary);

            var codedCharactersDictionary = new Dictionary<char, bool[]>();
            codedCharactersDictionary.FillDictionaryCode(text, Root);



            var bitDataToWriteOnHardDisk = new List<bool>();

            bitDataToWriteOnHardDisk.AddRange(codedCharactersDictionary.EmbededCharMap(CharMapCharacterSeparatorBool));
            bitDataToWriteOnHardDisk.AddRange(MetaAndDataSeparatorBool);
            bitDataToWriteOnHardDisk.AddRange(text.ToHuffmanData(codedCharactersDictionary));

            File.WriteAllBytes(url, bitDataToWriteOnHardDisk.ToByteArray());

        }

        public void EncodeFile(string filePath, string url) {
            Encode(File.ReadAllText(filePath), url);
        }


        public void ResetEncoder()
        {
            PQueue = null;
            Root = null;
        }



    }
}
