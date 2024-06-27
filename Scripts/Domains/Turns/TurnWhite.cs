using Domains.Boards.Stones;

namespace Domains.Turns
{
    public readonly record struct TurnWhite : ITurn
    {
        public IStone Stone => new StoneWhite();

        public ITurn Flip() => new TurnBlack();
    }
}