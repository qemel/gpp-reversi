using System.Collections.Generic;
using Domains.Boards;
using R3;

namespace Applications
{
    public interface IBoardView
    {
        Observable<BoardPosition> OnPut { get; }
        void ShowPuttablePositions(IEnumerable<BoardPosition> puttablePositions);
        void ResetShowPuttablePositions();
        void Render(Board board);
    }
}