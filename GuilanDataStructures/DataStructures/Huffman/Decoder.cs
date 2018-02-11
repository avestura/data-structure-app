using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuilanDataStructures.DataStructures.Huffman
{
    public class Decoder
    {

        
        private const string MetaAndDataSeparatorString = "0000001000000000001001100000000001000110000000000101110000000000";
        private const string CharMapCharacterSeparatorString = "00110100000000001101110000000000";


        public void Decode(string fileUrl, string saveUrl)
        {

            var fileBits = new BitArray(File.ReadAllBytes(fileUrl)).ToNativeOneZeroString();

            string[] splitedData = fileBits.Split(new string[] { MetaAndDataSeparatorString }, StringSplitOptions.None);

            string CharMapData = splitedData[0];
            string HuffmanData = splitedData[1];

            var frequencyModelList = new List<FrequencyModel>();

            frequencyModelList.FillModel(CharMapData, CharMapCharacterSeparatorString);

            var charactersOfDecompressedFile = new List<char>();

            File.WriteAllText(saveUrl, HuffmanData.DecompressDataUsingKeys(frequencyModelList));


        }

        public class FrequencyModel
        {
            public char Character { get; set; }
            public string Replacement { get; set; }

            public FrequencyModel(char c, string k)
            {
                Character = c;
                Replacement = k;
            }
        }


    }
}
