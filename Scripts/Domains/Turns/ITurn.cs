using Domains.Boards.Stones;

namespace Domains.Turns
{
    public interface ITurn
    {
        IStone Stone { get; }
        ITurn Flip();
    }
}