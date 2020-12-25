using System;
using App.Scripts.Interfaces;
using DefaultNamespace;

namespace App.Scripts.Ui.Controller
{
    public delegate void ScoreHandler(int score);
    
    public class ClickerGameController : IController
    {
        public event Action Click;
        public event ScoreHandler ChangeScore;
        
        private ClickerGameView _clickerGameView;

        private int _currentScore;

        public ClickerGameController(ClickerGameView clickerGameView)
        {
            _clickerGameView = clickerGameView;
            ResetScore();
        }
        
        public void Activate()
        {
            _clickerGameView.Click += OnClicked;
        }

        public void Deactivate()
        {
            _clickerGameView.Click -= OnClicked;
        }
        
        public void ResetScore()
        {
            _currentScore = 0;
        }
        
        private void OnClicked()
        {
            _currentScore++;
            _clickerGameView.ShowScore(_currentScore);
            
            Click?.Invoke();
            ChangeScore?.Invoke(_currentScore);
        }
        
    }
}