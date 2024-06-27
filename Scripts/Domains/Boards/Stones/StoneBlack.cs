namespace Domains.Boards.Stones
{
    public readonly record struct StoneBlack : IStone
    {
        public IStone Flip() => new StoneWhite();
        public string Symbol => "X";
    }
}