using Domains.Boards;

namespace Domains
{
    public sealed class Game
    {
        public Board Board { get; }

        private Game(Board board)
        {
            Board = board;
        }

        /// <summary>
        /// 普通の初期状態のGameを作成する
        /// </summary>
        /// <returns></returns>
        public static Game CreateBasicGame()
        {
            var board = Board.CreateBasicBoard();
            return new Game(board);
        }
    }
}