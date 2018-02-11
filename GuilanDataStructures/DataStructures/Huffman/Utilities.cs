using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuilanDataStructures.DataStructures.Huffman
{
    public static class Utilities
    {

        public static byte[] ToByteArray(this BitArray bArr)
        {
            byte[] bytes = new byte[bArr.Length / 8 + (bArr.Length % 8 == 0 ? 0 : 1)];
            bArr.CopyTo(bytes, 0);
            return bytes;
        }

        public static string ToNativeOneZeroString(this BitArray bArr)
        {
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < bArr.Length; i++) sBuilder.Append((bArr[i]) ? "1" : "0");
            return sBuilder.ToString();
        }

        public static Dictionary<char, int> CharacterRepeat(this string text)
        {
            var returnValue = new Dictionary<char, int>();
            foreach (char @char in text)
            {
                if (returnValue.ContainsKey(@char)) returnValue[@char]++;
                else returnValue.Add(@char, 1);
            }
            return returnValue;
        }

        public static BinaryTree BuildTree(this PriorityQueue pQ, Dictionary<char, int> dic)
        {
            for (int i = 0; i < dic.Count - 1; i++)
            {
                var firstDequeue = pQ.DequeueData();
                var secondDequeue = pQ.DequeueData();
                var tree = new Huffman.BinaryTree(firstDequeue.Repeat + secondDequeue.Repeat, null);
                tree.Left = firstDequeue;
                tree.Right = secondDequeue;

                pQ.Enqueue(tree);

            }

            return pQ.DequeueData();

        }

        public static void FillQueue(this PriorityQueue pQ, Dictionary<char, int> repeats)
        {
            foreach (var element in repeats)
                pQ.EnqueueNew(element.Value, element.Key);
        }

        public static void FillDictionaryCode(this Dictionary<char, bool[]> dic, string text, BinaryTree root)
        {
            for (int i = 0; i < text.Length; i++)
            {
                // If character already exists in dictionary, no need to calculate it again
                if (dic.ContainsKey(text[i])) continue;

                bool[] codedCharacter = root.Traverse(text[i], new List<bool>()).ToArray();

                dic.Add(text[i], codedCharacter);

            }
        }

        public static List<bool> EmbededCharMap(this Dictionary<char, bool[]> dic, bool[] charSeparator)
        {

            var list = new List<bool>();

            foreach (var element in dic)
            {

                bool[] standardCharBits = new bool[16];
                new BitArray(Encoding.Unicode.GetBytes(element.Key.ToString())).CopyTo(standardCharBits, 0);
                list.AddRange(standardCharBits);
                list.AddRange(element.Value);
                list.AddRange(charSeparator);

            }

            return list;
        }

        public static List<bool> ToHuffmanData(this string text, Dictionary<char, bool[]> codedChars)
        {
            var retuenValue = new List<bool>();

            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                retuenValue.AddRange(codedChars[c]);
            }

            return retuenValue;
        }

        public static byte[] ToByteArray(this List<bool> bools)
        {
            return new BitArray(bools.ToArray()).ToByteArray();
        }

        public static void FillModel(this List<Decoder.FrequencyModel> dic, string metaData, string splitor)
        {
            string[] token = metaData.Split(new string[] { splitor }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var c in token)
            {
                string bitsOfStandardChar = c.Substring(0, 16);
                string key = c.Substring(16);

                bool[] bools = new bool[16];

                for (int i = 0; i < 16; i++) bools[i] = (bitsOfStandardChar[i] == '1') ? true : false;

                var characterBitArray = new BitArray(bools);

                byte[] byteOfBools = new byte[2];
                characterBitArray.CopyTo(byteOfBools, 0);

                char character = Convert.ToChar(byteOfBools[0]);

                dic.Add(new Decoder.FrequencyModel(character, key));

            }
        }

        public static string DecompressDataUsingKeys(this string data, List<Decoder.FrequencyModel> fModel)
        {
            var charsOfText = new List<char>();

            for (int i = 1; i < data.Length; i++)
            {
                string part = data.Substring(0, i);

                for (int j = 0; j < fModel.Count; j++)
                {
                    if (part == fModel[j].Replacement)
                    {
                        charsOfText.Add(fModel[j].Character);
                        data = data.Substring(i);
                        i = 0;
                        break;
                    }
                }

            }

            return new string(charsOfText.ToArray());
        }

    }
}
