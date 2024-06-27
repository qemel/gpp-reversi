namespace Domains.Boards
{
    public sealed record BoardPosition(int X, int Y)
    {
        public int X { get; } = X;
        public int Y { get; } = Y;

        public static BoardPosition operator +(BoardPosition a, BoardPosition b) => new(a.X + b.X, a.Y + b.Y);
        public static BoardPosition operator -(BoardPosition a, BoardPosition b) => new(a.X - b.X, a.Y - b.Y);
    }
}