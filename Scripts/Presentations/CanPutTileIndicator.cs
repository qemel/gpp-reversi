using UnityEngine;

namespace Presentations
{
    public sealed class CanPutTileIndicator : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;

        [SerializeField] private Color _puttableColor;


        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void ShowPutIndicator()
        {
            _spriteRenderer.color = _puttableColor;
        }

        public void HidePutIndicator()
        {
            _spriteRenderer.color = new Color(0, 0, 0, 0);
        }
    }
}