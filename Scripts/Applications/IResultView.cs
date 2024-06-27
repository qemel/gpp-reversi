using UnityEngine.UI;

namespace Applications
{
    public interface IResultView
    {
        Button RetryButton { get; }
        void ShowWithResult(int blackCount, int whiteCount);
        void Hide();
    }
}