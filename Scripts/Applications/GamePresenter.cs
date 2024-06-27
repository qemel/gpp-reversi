using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Domains.Boards;
using Domains.Turns;
using R3;
using UnityEngine;
using VContainer.Unity;

namespace Applications
{
    internal sealed class GamePresenter : IAsyncStartable
    {
        private readonly IBoardViewFactory _boardViewFactory;
        private readonly IResultView _resultView;

        public GamePresenter(IBoardViewFactory boardViewFactory, IResultView resultView)
        {
            _boardViewFactory = boardViewFactory;
            _resultView = resultView;
        }

        /// <summary>
        /// ゲームループ
        /// </summary>
        /// <param name="cancellation"></param>
        public async UniTask StartAsync(CancellationToken cancellation)
        {
            // Prepare
            var board = Board.CreateBasicBoard();
            var boardView = _boardViewFactory.Create(board);
            ITurn turn = new TurnWhite();

            var turnChangedDueToNoPut = false;

            // InGame
            while (true)
            {
                turn = turn.Flip();
                boardView.Render(board);

                await UniTask.WaitForSeconds(0.2f, cancellationToken: cancellation);

                var puttablePositions = board.GetPuttablePositions(turn);
                boardView.ShowPuttablePositions(puttablePositions);
                Debug.Log($"{puttablePositions.Count()} positions are puttable.");

                // 置ける場所がない場合はターンを変更
                if (!puttablePositions.Any())
                {
                    // 2回連続で置ける場所がない場合はゲーム終了
                    if (turnChangedDueToNoPut) break;
                    turnChangedDueToNoPut = true;
                    continue;
                }

                var putPos =
                    await boardView.OnPut.Where(x => puttablePositions.Contains(x)).FirstAsync(cancellation);

                board = board.PutReverseStone(putPos, turn.Stone);
                boardView.ResetShowPuttablePositions();
            }

            // Game Over
            Debug.Log("Game Over");
            _resultView.ShowWithResult(board.StoneCount(new TurnBlack()), board.StoneCount(new TurnWhite()));
            await _resultView.RetryButton.OnClickAsync(cancellation);
            _resultView.Hide();
            SceneLoader.LoadGameScene();
        }
    }
}