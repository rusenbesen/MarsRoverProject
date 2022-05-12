namespace MarsRoverProject.Data.Enumerations
{
    public class DirectionEnum
    {
        private DirectionEnum(string value) { Value = value; }

        public DirectionEnum()
        {
        }

        public string Value { get; private set; }

        public static DirectionEnum North { get { return new DirectionEnum("N"); } }
        public static DirectionEnum East { get { return new DirectionEnum("E"); } }
        public static DirectionEnum South { get { return new DirectionEnum("S"); } }
        public static DirectionEnum West { get { return new DirectionEnum("W"); } }
        public DirectionEnum GetValue(string value)
        {
            if (value == DirectionEnum.North.Value)
            {
                return DirectionEnum.North;
            }
            else if (value == DirectionEnum.East.Value)
            {
                return DirectionEnum.East;
            }
            else if (value == DirectionEnum.South.Value)
            {
                return DirectionEnum.South;
            }
            else if (value == DirectionEnum.West.Value)
            {
                return DirectionEnum.West;
            }
            else
            {
                return null;
            }
        }
    }
}
