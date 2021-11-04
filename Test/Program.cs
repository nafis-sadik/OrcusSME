using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Breakdown(string RawData, int? parentNode = null)
        {
            int bracesCounter = 0;
            List<int> CollectionId = new List<int>();

            int startIndex = 0, endIndex = 0;
            List<string> objectList = new List<string>();
            for (int i = 0; i < RawData.Length; i++)
            {
                if (RawData[i] == '{')
                {
                    // Incrementing brace cout
                    bracesCounter++;
                    // Caching start index for getting substring
                    if(bracesCounter == 1)
                        startIndex = i;
                }
                else if(RawData[i] == '}')
                {
                    // Decrementing brace cout
                    bracesCounter--;
                    // When brace count is 0, we have found an unit from same level
                    if (bracesCounter == 0)
                    {
                        // Caching end index for getting substring
                        endIndex = i;
                        // Cutting substring from start index to end index
                        objectList.Add(RawData.Substring(startIndex, (endIndex-startIndex) + 1));
                    }
                }
            }
            // Order by length so that we deal with recursion at the end of the road when we have already dealt with the rest of  the ids 
            objectList = objectList.OrderBy(x => x.Length).ToList();
            foreach (string sentence in objectList)
            {
                string output = "";
                if (sentence.Contains("children"))
                {
                    // Find the first occurance of c
                    int elementBreakIndex = 0;
                    for (int i = 0; i < sentence.Length; i++)
                    {
                        if(sentence[i] == 'c')
                        {
                            elementBreakIndex = i;
                            break;
                        }
                    }

                    // Cut from index 0 till first occurance of 'c'
                    // Split it by ':' to get substring "id" and the id number seperated.
                    // The number will always come at the second index of the array due to the format of the string
                    // The result might have a ',' as a garbage. Simply replace that with empty space and trim to get the exact number 
                    output = sentence.Substring(0, elementBreakIndex).Split(':')[1].Replace(',', ' ').Trim();
                    if (output.Length == 1) { output = "0" + output; }
                    string parent = parentNode == null ? "None" : parentNode.ToString();
                    Console.WriteLine("Node: " + output + " Parent : " + parent);
                    Console.WriteLine("Children of : " + output);
                    Console.WriteLine("----------------------------");
                    Breakdown(sentence.Substring(1, sentence.Length - 1), int.Parse(output));
                }
                else
                {
                    output = sentence.Remove(sentence.Length - 1, 1).Remove(0, 1).Split(':')[1];
                    if (output.Length == 1) { output = "0" + output; }
                    string parent = parentNode == null ? "None" : parentNode.ToString();
                    Console.WriteLine("Node: " + output + " Parent : " + parent);
                }
            }
        }
        static void Main(string[] args)
        {
            //Console.WriteLine("Challange # 1");
            //Breakdown("[{ id :1},{ id :2, children :[{ id :3},{ id :4},{ id :5, children :[{ id :6},{ id :7}]},{ id :9},{ id :10}]},{ id :11},{ id :12}]");
            //Console.WriteLine();
            //Console.WriteLine("Challange # 2");
            //Breakdown("[{ id :13},{ id :14},{ id :15, children :[{ id :17},{ id :16},{ id :18}]}]");
            //Console.WriteLine();
            //Console.WriteLine("Challange # 3");
            //Breakdown("[{ id :1},{ id :2, children :[{ id :3},{ id :4},{ id :5, children :[{ id :6},{ id :7},{ id :8}]},{ id :9},{ id :10}]},{ id :11},{ id :12}]");
            //Console.WriteLine();
            //Console.WriteLine("Challange # 4");
            //Breakdown("[{id:1},{id:2,children:[{id:3},{id:4},{id:5,children:[{id:6},{id:7}]},{id:9},{id:10}]},{id:11},{id:12}]");
            //Console.WriteLine("Challange # 5");
            Breakdown("[{id:1,children:[{id:3}]}]");
            Console.ReadLine();
        }
    }
}
