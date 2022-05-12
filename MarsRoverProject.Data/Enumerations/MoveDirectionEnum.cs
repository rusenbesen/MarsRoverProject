namespace MarsRoverProject.Data.Enumerations
{
    public class MoveDirectionEnum
    {
        private MoveDirectionEnum(char value) { Value = value; }

        public MoveDirectionEnum()
        {
        }

        public char Value { get; private set; }

        public static MoveDirectionEnum Right { get { return new MoveDirectionEnum('R'); } }
        public static MoveDirectionEnum Left { get { return new MoveDirectionEnum('L'); } }
        public static MoveDirectionEnum MoveStraight { get { return new MoveDirectionEnum('M'); } }
        public MoveDirectionEnum GetValue(char value)
        {
            if (value == MoveDirectionEnum.Right.Value)
            {
                return MoveDirectionEnum.Right;
            }
            else if (value == MoveDirectionEnum.Left.Value)
            {
                return MoveDirectionEnum.Left;
            }
            else if (value == MoveDirectionEnum.MoveStraight.Value)
            {
                return MoveDirectionEnum.MoveStraight;
            }
            else
            {
                return null;
            }
        }
    }
}
