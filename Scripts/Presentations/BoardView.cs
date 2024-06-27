using System.Collections.Generic;
using System.Linq;
using Applications;
using Domains.Boards;
using R3;
using UnityEngine;

namespace Presentations
{
    public sealed class BoardView : MonoBehaviour, IBoardView
    {
        [SerializeField] private BoardTile _boardTilePrefab;

        private readonly Dictionary<BoardPosition, BoardTile> _boardTiles = new();
        public Observable<BoardPosition> OnPut => _onPut;
        private readonly Subject<BoardPosition> _onPut = new();

        private void Awake()
        {
            _onPut.AddTo(this);
        }

        public void Initialize(Board board)
        {
            foreach (var position in board.Positions)
            {
                var boardTile = Instantiate(_boardTilePrefab, transform);
                boardTile.Initialize(position);
                boardTile.OnPut.Subscribe(_onPut.OnNext).AddTo(this);
                _boardTiles.Add(position, boardTile);
            }
        }

        public void ShowPuttablePositions(IEnumerable<BoardPosition> puttablePositions)
        {
            var boardPositions = puttablePositions as BoardPosition[] ?? puttablePositions.ToArray();

            foreach (var position in _boardTiles.Keys)
            {
                if (boardPositions.Contains(position))
                {
                    _boardTiles[position].ShowPutIndicator();
                }
                else
                {
                    _boardTiles[position].HidePutIndicator();
                }
            }
        }

        public void ResetShowPuttablePositions()
        {
            foreach (var boardTile in _boardTiles.Values)
            {
                boardTile.HidePutIndicator();
            }
        }

        public void Render(Board board)
        {
            foreach (var position in board.Positions)
            {
                _boardTiles[position].Render(board.GetStone(position));
            }
        }
    }
}