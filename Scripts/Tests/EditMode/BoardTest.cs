using System.Linq;
using Domains.Boards;
using Domains.Boards.Stones;
using Domains.Turns;
using NUnit.Framework;
using UnityEngine;

namespace Tests.EditMode
{
    internal sealed class BoardTest
    {
        [Test]
        public void A_8x8が作れる()
        {
            var board = Board.CreateBasicBoard();
            Assert.IsTrue(board.Contains(new BoardPosition(0, 0)));
            Assert.IsTrue(board.Contains(new BoardPosition(7, 7)));
            Assert.IsFalse(board.Contains(new BoardPosition(8, 0)));

            Assert.IsTrue(board.Height == 8);
            Assert.IsTrue(board.Width == 8);

            Assert.IsTrue(board.GetStone(new BoardPosition(0, 0)) is StoneNone);
            Assert.IsTrue(board.GetStone(new BoardPosition(7, 7)) is StoneNone);
        }

        [Test]
        public void A_イコール判定できる()
        {
            var board = Board.CreateBasicBoard();
            var board2 = Board.CreateBasicBoard();
            Assert.That(board.IsSameBoardAs(board2));

            var board3 = new Board(new[,]
            {
                { _, _, _, _, _, _, _, _ },
                { _, _, _, _, _, _, _, _ },
                { _, _, _, _, _, _, _, _ },
                { _, _, _, O, X, _, _, _ },
                { _, X, _, X, O, _, _, _ },
                { _, _, _, _, _, _, _, _ },
                { _, _, _, _, _, _, _, _ },
                { _, _, _, _, _, _, O, _ },
            });

            var board4 = new Board(new[,]
            {
                { _, _, _, _, _, _, _, _ },
                { _, _, _, _, _, _, _, _ },
                { _, _, _, _, _, _, _, _ },
                { _, _, _, O, X, _, _, _ },
                { _, X, _, X, O, _, _, _ },
                { _, _, _, _, _, _, _, _ },
                { _, _, _, _, _, _, _, _ },
                { _, _, _, _, _, _, O, _ },
            });

            Assert.That(board3.IsSameBoardAs(board4));
        }

        [Test]
        public void A_Xが右でYが下()
        {
            // ________
            // ________
            // ________
            // ___OX___
            // _X_XO___
            // ________
            // ________
            // ______O_

            var board = Board.CreateBasicBoard();
            var next = board.ChangeStoneAt(new BoardPosition(1, 4), new StoneBlack());
            var next2 = next.ChangeStoneAt(new BoardPosition(6, 7), new StoneWhite());

            Assert.IsTrue(next2.GetStone(new BoardPosition(1, 4)) is StoneBlack);
            Assert.IsTrue(next2.GetStone(new BoardPosition(6, 7)) is StoneWhite);

            var expected = new Board(new[,]
            {
                { _, _, _, _, _, _, _, _ },
                { _, _, _, _, _, _, _, _ },
                { _, _, _, _, _, _, _, _ },
                { _, _, _, O, X, _, _, _ },
                { _, X, _, X, O, _, _, _ },
                { _, _, _, _, _, _, _, _ },
                { _, _, _, _, _, _, _, _ },
                { _, _, _, _, _, _, O, _ },
            });

            Assert.That(next2.IsSameBoardAs(expected));
        }

        [Test]
        public void B_Stoneを置いてひっくり返せる1()
        {
            // ________
            // ________
            // ________
            // ___OX___
            // ___XO___
            // ________
            // ________
            // ________
            // 
            // ________
            // ________
            // ___X____
            // ___XX___
            // ___XO___
            // ________
            // ________
            // ________

            var board = Board.CreateBasicBoard();
            var turn = new TurnBlack();
            var nextBoard = board.PutReverseStone(new BoardPosition(3, 2), turn.Stone);
            Debug.Log(nextBoard);
            Assert.IsTrue(nextBoard.GetStone(new BoardPosition(3, 2)) is StoneBlack);
            Assert.IsTrue(nextBoard.GetStone(new BoardPosition(3, 3)) is StoneBlack);
            Assert.AreEqual(4, nextBoard.StoneCount(turn));
            Assert.AreEqual(1, nextBoard.StoneCount(new TurnWhite()));
        }

        [Test]
        public void B_Stoneを置いてひっくり返せる2()
        {
            // _O______
            // __O_____
            // ___X____
            // _OXXX___
            // __XXOO__
            // __OXXX__
            // _OO_XX__
            // __O_____
            // 
            // _O______
            // __O_____
            // ___X____
            // _OXXX___
            // __OXOO__
            // __OOXO__
            // _OO_OO__
            // __O__O__

            var board = new Board(new IStone[,]
            {
                { _, O, _, _, _, _, _, _ },
                { _, _, O, _, _, _, _, _ },
                { _, _, _, X, _, _, _, _ },
                { _, O, X, X, X, _, _, _ },
                { _, _, X, X, O, O, _, _ },
                { _, _, O, X, X, X, _, _ },
                { _, O, O, _, X, X, _, _ },
                { _, _, O, _, _, _, _, _ },
            });

            var expected = new Board(new IStone[,]
            {
                { _, O, _, _, _, _, _, _ },
                { _, _, O, _, _, _, _, _ },
                { _, _, _, X, _, _, _, _ },
                { _, O, X, X, X, _, _, _ },
                { _, _, O, X, O, O, _, _ },
                { _, _, O, O, X, O, _, _ },
                { _, O, O, _, O, O, _, _ },
                { _, _, O, _, _, O, _, _ },
            });

            var turn = new TurnWhite();
            var nextBoard = board.PutReverseStone(new BoardPosition(5, 7), turn.Stone);
            Debug.Log(nextBoard);
            Assert.That(nextBoard.IsSameBoardAs(expected));
        }

        [Test]
        public void B_Stoneを置いてひっくり返せる3()
        {
            // X_______
            // O_______
            // O_______
            // OXXXO___
            // O__XX___
            // OXX_____
            // OX______
            // _XX_____
            //
            // X_______
            // X_______
            // X_______
            // XXXXO___
            // X__XX___
            // XXX_____
            // XX______
            // XXX_____

            var board = new Board(new IStone[,]
            {
                { X, _, _, _, _, _, _, _ },
                { O, _, _, _, _, _, _, _ },
                { O, _, _, _, _, _, _, _ },
                { O, X, X, X, O, _, _, _ },
                { O, _, X, X, _, _, _, _ },
                { O, X, X, _, _, _, _, _ },
                { O, X, _, _, _, _, _, _ },
                { _, X, X, _, _, _, _, _ },
            });

            var expected = new Board(new IStone[,]
            {
                { X, _, _, _, _, _, _, _ },
                { X, _, _, _, _, _, _, _ },
                { X, _, _, _, _, _, _, _ },
                { X, X, X, X, O, _, _, _ },
                { X, _, X, X, _, _, _, _ },
                { X, X, X, _, _, _, _, _ },
                { X, X, _, _, _, _, _, _ },
                { X, X, X, _, _, _, _, _ },
            });

            var turn = new TurnBlack();
            var nextBoard = board.PutReverseStone(new BoardPosition(0, 7), turn.Stone);
            Debug.Log(nextBoard);
            Assert.That(nextBoard.IsSameBoardAs(expected));
        }

        [Test]
        public void B_Stoneを置いてひっくり返せる4()
        {
            // ________
            // ________
            // ___XO___
            // ___XO___
            // ___XO___
            // ________
            // ________
            // ________
            //
            // ________
            // ________
            // ___XO___
            // ___XXX__
            // ___XO___
            // ________
            // ________
            // ________

            var board = new Board(new[,]
            {
                { _, _, _, _, _, _, _, _ },
                { _, _, _, _, _, _, _, _ },
                { _, _, _, X, O, _, _, _ },
                { _, _, _, X, O, _, _, _ },
                { _, _, _, X, O, _, _, _ },
                { _, _, _, _, _, _, _, _ },
                { _, _, _, _, _, _, _, _ },
                { _, _, _, _, _, _, _, _ },
            });

            var expected = new Board(new[,]
            {
                { _, _, _, _, _, _, _, _ },
                { _, _, _, _, _, _, _, _ },
                { _, _, _, X, O, _, _, _ },
                { _, _, _, X, X, X, _, _ },
                { _, _, _, X, O, _, _, _ },
                { _, _, _, _, _, _, _, _ },
                { _, _, _, _, _, _, _, _ },
                { _, _, _, _, _, _, _, _ },
            });

            var turn = new TurnBlack();
            var nextBoard = board.PutReverseStone(new BoardPosition(5, 3), turn.Stone);
            Debug.Log(nextBoard);
            Assert.That(nextBoard.IsSameBoardAs(expected));
        }

        [Test]
        public void C_Stoneを置いてもひっくり返せないときはそのまま返す()
        {
            // ________
            // ________
            // ___#____
            // ___OX___
            // ___XO___
            // ________
            // ________
            // ________
            // 
            // ________
            // ________
            // ________
            // ___OX___
            // ___XO___
            // ________
            // ________
            // ________

            var board = Board.CreateBasicBoard();
            var turn = new TurnWhite();
            var nextBoard = board.PutReverseStone(new BoardPosition(3, 2), turn.Stone);
            Debug.Log(nextBoard);
            Assert.That(nextBoard.IsSameBoardAs(board));
        }

        [Test]
        public void C_入力候補を返せる1()
        {
            // ________
            // ________
            // ___#____
            // __#OX___
            // ___XO#__
            // ____#___
            // ________
            // ________

            var board = Board.CreateBasicBoard();
            var turn = new TurnBlack();
            var candidates = board.GetPuttablePositions(turn);

            // #の位置
            var expected = new[]
            {
                new BoardPosition(3, 2),
                new BoardPosition(2, 3),
                new BoardPosition(5, 4),
                new BoardPosition(4, 5),
            };

            Assert.AreEqual(expected.Length, candidates.Count());
            foreach (var expect in expected)
            {
                Assert.IsTrue(candidates.Contains(expect));
            }
        }

        [Test]
        public void C_入力候補を返せる2()
        {
            // _O______
            // __O_____
            // #__X____
            // #OXXX##_
            // __XXOO#_
            // _#OXXX#_
            // _OO_XX__
            // ##O_____

            var board = new Board(new[,]
            {
                { _, O, _, _, _, _, _, _ },
                { _, _, O, _, _, _, _, _ },
                { _, _, _, X, _, _, _, _ },
                { _, O, X, X, X, _, _, _ },
                { _, _, X, X, O, O, _, _ },
                { _, _, O, X, X, X, _, _ },
                { _, O, O, _, X, X, _, _ },
                { _, _, O, _, _, _, _, _ },
            });

            var turn = new TurnBlack();
            var candidates = board.GetPuttablePositions(turn);

            // #の位置
            var expected = new[]
            {
                new BoardPosition(0, 2),
                new BoardPosition(0, 3),
                new BoardPosition(0, 7),
                new BoardPosition(1, 5),
                new BoardPosition(1, 7),
                new BoardPosition(5, 3),
                new BoardPosition(6, 3),
                new BoardPosition(6, 4),
                new BoardPosition(6, 5),
            };

            Assert.AreEqual(expected.Length, candidates.Count());
            foreach (var expect in expected)
            {
                Assert.IsTrue(candidates.Contains(expect));
            }
        }

        [Test]
        public void C_入力候補を返せる3()
        {
            // X_______
            // O_______
            // O___##__
            // OXXXO#__
            // O__XX___
            // OXX_____
            // OX______
            // #XX_____

            var board = new Board(new[,]
            {
                { X, _, _, _, _, _, _, _ },
                { O, _, _, _, _, _, _, _ },
                { O, _, _, _, _, _, _, _ },
                { O, X, X, X, O, _, _, _ },
                { O, _, _, X, X, _, _, _ },
                { O, X, X, _, _, _, _, _ },
                { O, X, _, _, _, _, _, _ },
                { _, X, X, _, _, _, _, _ },
            });

            var turn = new TurnBlack();
            var candidates = board.GetPuttablePositions(turn);

            // #の位置
            var expected = new[]
            {
                new BoardPosition(0, 7),
                new BoardPosition(4, 2),
                new BoardPosition(5, 2),
                new BoardPosition(5, 3),
            };

            Assert.AreEqual(expected.Length, candidates.Count());
            foreach (var expect in expected)
            {
                Assert.IsTrue(candidates.Contains(expect));
            }
        }

        private static IStone _ => new StoneNone();
        private static IStone X => new StoneBlack();
        private static IStone O => new StoneWhite();
    }
}