using Domains.Boards;
using Domains.Boards.Stones;
using R3;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Presentations
{
    internal sealed class BoardTile : MonoBehaviour, IPointerClickHandler
    {
        private CanPutTileIndicator _canPutTileIndicator;
        private StoneView _stoneView;

        public Observable<BoardPosition> OnPut => _onPut;
        private readonly Subject<BoardPosition> _onPut = new();

        private BoardPosition _publishPosition;

        private void Awake()
        {
            _canPutTileIndicator = GetComponentInChildren<CanPutTileIndicator>();
            _stoneView = GetComponentInChildren<StoneView>();
            _onPut.AddTo(this);
        }

        public void Initialize(BoardPosition position)
        {
            transform.position = new Vector3(position.X, position.Y, 0);
            _publishPosition = position;
        }

        public void ShowPutIndicator()
        {
            _canPutTileIndicator.ShowPutIndicator();
        }

        public void HidePutIndicator()
        {
            _canPutTileIndicator.HidePutIndicator();
        }

        public void Render(IStone stone)
        {
            _stoneView.Render(stone);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("Clicked");
            _onPut.OnNext(_publishPosition);
        }
    }
}