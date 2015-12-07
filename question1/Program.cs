using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace question1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Convert.ToString(findLIS(new int[] { 1,4,3,1})));
            Console.WriteLine(Convert.ToString(findLIS(new int[] { 1,4,5,2,6,4})));
            Console.ReadKey();
        }
        /*
 * Complete the function below.
 */
        static int findLIS(int[] s)
        {
            //s =  new int[]{ 6,2,5,4,1,3};
            
            return LargestSequence(s,s.Length-1,s.Last()) +1;
        }
        static int LargestSequence(int[] sequence, int pos,int cMax)
        {
            int longestSequence = 0;
            for (int i = pos - 1; i>=0; i--)
            {
                int sequenceLength = 0;
                if (sequence[i] < cMax)
                {
                    sequenceLength++;
                }
                sequenceLength += LargestSequence(sequence, i, sequence[i]);
                if (sequenceLength > longestSequence)
                {
                    longestSequence = sequenceLength;
                }
            }
            return longestSequence;
        }
    }
}
