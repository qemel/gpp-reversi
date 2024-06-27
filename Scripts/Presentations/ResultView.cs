using Applications;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Presentations
{
    public sealed class ResultView : MonoBehaviour, IResultView
    {
        [SerializeField] private TextMeshProUGUI _blackCountText;
        [SerializeField] private TextMeshProUGUI _whiteCountText;
        [SerializeField] private TextMeshProUGUI _winText;
        [SerializeField] private Button _retryButton;

        public Button RetryButton => _retryButton;

        public void ShowWithResult(int blackCount, int whiteCount)
        {
            gameObject.SetActive(true);

            _blackCountText.text = blackCount.ToString();
            _whiteCountText.text = whiteCount.ToString();

            if (blackCount > whiteCount)
            {
                _winText.text = "Black Win!";
            }
            else if (blackCount < whiteCount)
            {
                _winText.text = "White Win!";
            }
            else
            {
                _winText.text = "Draw!";
            }
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}