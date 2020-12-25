using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class ClickerGameView : MonoBehaviour
    {
        [SerializeField] private Button _buttonClickMe;
        [SerializeField] private TextMeshProUGUI _textScore;
        
        public event Action Click;

        public void ShowScore(int score)
        {
            _textScore.text = score.ToString();
        }

        private void OnEnable()
        {
            _buttonClickMe.onClick.AddListener(OnClicked);
        }

        private void OnDisable()
        {
            _buttonClickMe.onClick.RemoveListener(OnClicked);
        }

        private void OnClicked()
        {
            Click?.Invoke();
        }
    }
}