namespace Domains.Boards.Stones
{
    public readonly record struct StoneWhite : IStone
    {
        public IStone Flip() => new StoneBlack();
        public string Symbol => "O";
    }
}