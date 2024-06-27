using UnityEngine;

namespace Presentations
{
    [CreateAssetMenu(fileName = "ReversiAssets", menuName = "Create ReversiAssets")]
    public sealed class ReversiAssets : ScriptableObject
    {
        public BoardView BoardViewPrefab => _boardViewPrefab;
        [SerializeField] private BoardView _boardViewPrefab;
    }
}