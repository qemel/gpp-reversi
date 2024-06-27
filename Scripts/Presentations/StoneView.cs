using System;
using UnityEngine;

namespace Presentations
{
    public sealed class StoneView : MonoBehaviour
    {
        [SerializeField] private Sprite _white;
        [SerializeField] private Sprite _black;

        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Render(Domains.Boards.Stones.IStone stone)
        {
            _spriteRenderer.sprite = stone switch
            {
                Domains.Boards.Stones.StoneBlack _ => _black,
                Domains.Boards.Stones.StoneWhite _ => _white,
                Domains.Boards.Stones.StoneNone _ => null,
                _ => throw new ArgumentOutOfRangeException(nameof(stone), stone, null)
            };
        }
    }
}