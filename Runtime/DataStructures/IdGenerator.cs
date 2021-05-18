namespace InfenixTools.DataStructures
{
    public class IdGenerator
    {
        uint currentValue;
        public uint Current { get { return currentValue; } }
        public uint Next
        {
            get
            {
                currentValue++;
                return currentValue;
            }
        }

        public IdGenerator(uint initialValue = uint.MinValue)
        {
            currentValue = initialValue;
        }
    }
}
