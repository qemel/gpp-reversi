namespace Domains.Boards.Stones
{
    public interface IStone
    {
        IStone Flip();
        string Symbol { get; }
    }
}