using Domains.Boards;

namespace Applications
{
    public interface IBoardViewFactory
    {
        IBoardView Create(Board board);
    }
}