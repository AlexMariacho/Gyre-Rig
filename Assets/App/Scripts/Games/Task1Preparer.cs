using System;
using App.Scripts.Ui.Controller;
using DefaultNamespace;
using UnityEngine;

namespace App.Scripts.Games
{
    public class Task1Preparer : MonoBehaviour
    {
        [SerializeField] private ClickerGameView _clickerGameView;
        private ClickerGameController _clickerGameController;

        private void Awake()
        {
            _clickerGameController = new ClickerGameController(_clickerGameView);
        }

        private void Start()
        {
            _clickerGameController.Activate();
        }
    }
}