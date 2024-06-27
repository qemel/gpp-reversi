using Domains.Boards.Stones;

namespace Domains.Turns
{
    public readonly record struct TurnBlack : ITurn
    {
        public IStone Stone => new StoneBlack();

        public ITurn Flip() => new TurnWhite();
    }
}