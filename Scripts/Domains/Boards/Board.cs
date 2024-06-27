using System;
using System.Collections.Generic;
using System.Text;
using Domains.Boards.Stones;
using Domains.Turns;

namespace Domains.Boards
{
    public sealed class Board
    {
        private readonly IStone[,] _stones;

        private static readonly List<(int y, int x)> Directions = new()
        {
            (1, 0), (1, 1), (0, 1), (-1, 1), (-1, 0), (-1, -1), (0, -1), (1, -1),
        };

        internal Board(IStone[,] stones)
        {
            _stones = stones ?? throw new ArgumentNullException(nameof(stones));
        }

        public int Width => _stones.GetLength(1);
        public int Height => _stones.GetLength(0);
        
        /// <summary>
        /// 指定位置のStoneを変更した新しいBoardを作成する
        /// </summary>
        /// <param name="boardPosition"></param>
        /// <param name="stone"></param>
        /// <returns></returns>
        internal Board ChangeStoneAt(BoardPosition boardPosition, IStone stone)
        {
            var newStones = (IStone[,])_stones.Clone();
            newStones[boardPosition.Y, boardPosition.X] = stone;
            return new Board(newStones);
        }

        private Board FlipStoneAt(BoardPosition boardPosition)
        {
            var stone = GetStone(boardPosition);
            return ChangeStoneAt(boardPosition, stone.Flip());
        }

        /// <summary>
        /// 石を置いてひっくり返す
        /// </summary>
        /// <param name="boardPosition"></param>
        /// <param name="stone"></param>
        /// <returns></returns>
        public Board PutReverseStone(BoardPosition boardPosition, IStone stone)
        {
            if (!Contains(boardPosition)) return this;
            if (GetStone(boardPosition) is not StoneNone) return this;
            if (!IsPuttable(boardPosition, stone)) return this;

            var resBoard = ChangeStoneAt(boardPosition, stone);
            foreach (var (dy, dx) in Directions)
            {
                var (y, x) = (boardPosition.Y, boardPosition.X);
                var reversePositions = new List<BoardPosition>();
                while (true)
                {
                    y += dy;
                    x += dx;
                    var targetPosition = new BoardPosition(x, y);
                    if (!Contains(targetPosition)) break;
                    var targetStone = GetStone(targetPosition);
                    if (targetStone is StoneNone) break;
                    if (targetStone.Equals(stone))
                    {
                        foreach (var reversePosition in reversePositions)
                        {
                            resBoard = resBoard.FlipStoneAt(reversePosition);
                        }

                        break;
                    }

                    reversePositions.Add(targetPosition);
                }
            }

            return resBoard;
        }

        public IEnumerable<BoardPosition> GetPuttablePositions(ITurn turn)
        {
            for (var y = 0; y < Height; y++)
            for (var x = 0; x < Width; x++)
            {
                var boardPosition = new BoardPosition(x, y);
                if (IsPuttable(boardPosition, turn.Stone)) yield return boardPosition;
            }
        }

        private bool IsPuttable(BoardPosition boardPosition, IStone stone)
        {
            if (!Contains(boardPosition)) return false;
            if (GetStone(boardPosition) is not StoneNone) return false;

            foreach (var (dy, dx) in Directions)
            {
                var (y, x) = (boardPosition.Y, boardPosition.X);
                var passedOpponent = false;
                while (true)
                {
                    y += dy;
                    x += dx;
                    var targetPosition = new BoardPosition(x, y);
                    if (!Contains(targetPosition)) break;
                    var targetStone = GetStone(targetPosition);
                    if (targetStone is StoneNone) break;
                    if (targetStone.Equals(stone))
                    {
                        if (passedOpponent) return true;
                        break;
                    }

                    passedOpponent = true;
                }
            }

            return false;
        }

        public IStone GetStone(BoardPosition boardPosition)
        {
            if (!Contains(boardPosition)) throw new ArgumentOutOfRangeException(nameof(boardPosition));
            return _stones[boardPosition.Y, boardPosition.X];
        }

        public bool Contains(BoardPosition boardPosition) => boardPosition.Y >= 0 && boardPosition.Y < Height &&
                                                             boardPosition.X >= 0 && boardPosition.X < Width;


        /// <summary>
        /// 普通の初期状態のBoardを作成する
        /// </summary>
        /// <returns></returns>
        public static Board CreateBasicBoard()
        {
            var stones = new IStone[8, 8];
            for (var y = 0; y < 8; y++)
            for (var x = 0; x < 8; x++)
            {
                stones[x, y] = new StoneNone();
            }

            stones[3, 3] = new StoneWhite();
            stones[4, 4] = new StoneWhite();
            stones[3, 4] = new StoneBlack();
            stones[4, 3] = new StoneBlack();

            return new Board(stones);
        }

        public int StoneCount(ITurn turn)
        {
            var count = 0;
            for (var y = 0; y < Height; y++)
            for (var x = 0; x < Width; x++)
            {
                var stone = GetStone(new BoardPosition(x, y));
                if (stone.Equals(turn.Stone)) count++;
            }

            return count;
        }

        public IEnumerable<BoardPosition> Positions
        {
            get
            {
                for (var y = 0; y < Height; y++)
                for (var x = 0; x < Width; x++)
                {
                    yield return new BoardPosition(x, y);
                }
            }
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    var stone = GetStone(new BoardPosition(x, y));
                    var symbol = stone.Symbol;
                    builder.Append(symbol);
                }

                builder.AppendLine();
            }


            return builder.ToString();
        }

        /// <summary>
        /// Equalsの代わりに使う
        /// 参照型のinterfaceが含まれているのでEqualsが使えない
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool IsSameBoardAs(Board other)
        {
            if (other.Width != Width || other.Height != Height) return false;

            for (var y = 0; y < Height; y++)
            for (var x = 0; x < Width; x++)
            {
                if (GetStone(new BoardPosition(x, y)).GetType() != other.GetStone(new BoardPosition(x, y)).GetType())
                    return false;
            }

            return true;
        }

        public override bool Equals(object obj) =>
            throw new NotImplementedException("Equals(object obj) is not allowed.");

        private bool Equals(Board other) => throw new NotImplementedException("Equals(Board other) is not allowed.");
        public override int GetHashCode() => throw new NotImplementedException("GetHashCode() is not allowed.");

        public static bool operator ==(Board left, Board right) =>
            throw new NotImplementedException("== is not allowed.");

        public static bool operator !=(Board left, Board right) =>
            throw new NotImplementedException("!= is not allowed.");
    }
}