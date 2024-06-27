namespace Domains.Boards.Stones
{
    public readonly record struct StoneNone : IStone
    {
        public IStone Flip() => new StoneNone();
        public string Symbol => "_";
    }
}