using Domains.Boards;
using Presentations;
using UnityEngine;

namespace Applications
{
    public sealed class BoardViewFactory : IBoardViewFactory
    {
        private readonly ReversiAssets _reversiAssets;

        public BoardViewFactory(ReversiAssets reversiAssets)
        {
            _reversiAssets = reversiAssets;
        }

        public IBoardView Create(Board board)
        {
            var view = Object.Instantiate(_reversiAssets.BoardViewPrefab);
            view.Initialize(board);
            return view;
        }
    }
}