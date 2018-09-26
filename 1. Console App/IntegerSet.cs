using System.Text;

namespace ConsoleApp1
{
    /// <summary>
    /// Provides set which can store boolean value for integers from 0 to 100.
    /// </summary>
    public class IntegerSet
    {
        bool[] setOfInts = new bool[100];

        /// <summary>
        /// Default constructor
        /// </summary>
        public IntegerSet() { }

        /// <summary>
        /// Parameterized constructor to assign values to set with integer array
        /// </summary>
        /// <param name="intArray">Integer array used for assigning true</param>
        public IntegerSet(int[] intArray)
        {
            foreach (int value in intArray)
            {
                setElement(value, true);
            }
        }

        /// <summary>
        /// To return a string by appending all the elements in the 
        /// IntegerSet which are true.
        /// </summary>
        /// <returns>string: all the elements in the current set</returns>
        public override string ToString()
        {
            var builder = new StringBuilder();
            for (int i = 0; i < setOfInts.Length; i++)
            {
                if (setOfInts[i] == true)
                {
                    builder.Append(i);
                    builder.Append(" ");
                }
            }
            // check if string builder is still empty
            if (builder.ToString().Equals(""))
            {
                builder.Append("No elements");
            }
            return builder.ToString();
        }

        /// <summary>
        /// Assign boolean value to the set with the passed index.
        /// </summary>
        /// <param name="element">Index at which boolean value is to be placed.</param>
        /// <param name="isTrue">Boolean value to be placed in the set.</param>
        public void setElement(int element, bool isTrue)
        {
            //check if element is between 0-100 else do not insert
            if (0 <= element && element < 100)
            {
                setOfInts[element] = isTrue;
            }
        }

        /// <summary>
        /// Returns set with Union of two sets.
        /// </summary>
        /// <param name="set">Comparision with this set.</param>
        /// <returns></returns>
        public IntegerSet Union(IntegerSet set)
        {
            IntegerSet unionSet = new IntegerSet();
            //loop to check every 'i'th of two sets and create Union set
            for (int i = 0; i < set.setOfInts.Length; i++)
            {
                unionSet.setOfInts[i] = setOfInts[i] && set.setOfInts[i];
            }
            return unionSet;
        }

        /// <summary>
        /// Returns set with Intersection of two sets.
        /// </summary>
        /// <param name="set">Comparision with this set.</param>
        /// <returns></returns>
        public IntegerSet Intersection(IntegerSet set)
        {
            IntegerSet unionSet = new IntegerSet();
            //loop to check every 'i'th of two sets and create Intersection set
            for (int i = 0; i < set.setOfInts.Length; i++)
            {
                unionSet.setOfInts[i] = setOfInts[i] || set.setOfInts[i];
            }
            return unionSet;
        }

        /// <summary>
        /// Compare two sets are equal or not.
        /// </summary>
        /// <param name="set">Comparision with this set.</param>
        /// <returns>True if equal. False if unequal.</returns>
        public bool IsEqualTo(IntegerSet set)
        {
            IntegerSet unionSet = new IntegerSet();
            for (int i = 0; i < set.setOfInts.Length; i++)
            {
                if (setOfInts[i] != set.setOfInts[i])
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Insert a new element into the set.
        /// </summary>
        /// <param name="element">Element to insert in the set.</param>
        /// <returns>Returns instance of IntegerSet with inserted value.</returns>
        public IntegerSet InsertElement(int element)
        {
            setElement(element, true);
            return this;
        }

        /// <summary>
        /// Delete an existing element from the set.
        /// </summary>
        /// <param name="element">Element to delete from the set.</param>
        /// <returns>Returns instance of IntegerSet after deleting the element.</returns>
        public IntegerSet DeleteElement(int element)
        {
            setElement(element, false);
            return this;
        }
    }

}
