using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MiniGames.WolfAndEggs.Services
{
    public class UIController : MonoBehaviour
    {
        [Header("Иконки жизней")] 
        [SerializeField] private Image[] _lives;
        [SerializeField] private Image[] _livesFalse;
        
        [Header("Очки")]
        [SerializeField] private TextMeshProUGUI _pointText;
        
        [Header("Кнопки")]
        [SerializeField] private List<Button> _buttons;
        
        [Header("Конец игры")]
        [SerializeField] private GameObject _endGameScreen;
        
        public void PointsUpdate(int point)
        {
            _pointText.text = point switch
            {
                > 9999 => point.ToString(),
                > 999 => "0" + point,
                > 99 => "00" + point,
                > 9 => "000" + point,
                _ => "0000" + point
            };
        }

        public void LoseLife(int livesLeft)
        {
            _lives[livesLeft].gameObject.SetActive(false);
            _livesFalse[livesLeft].gameObject.SetActive(true);
        }

        public void SwitchPauseButton()
        {
            foreach (var button in _buttons)
                button.enabled = !button.enabled;
        }

        public void EndGame()
        {
            _endGameScreen.SetActive(!_endGameScreen.activeSelf);
        }

        public void ResetLives()
        {
            foreach (var live in _livesFalse)
                live.gameObject.SetActive(false);
            foreach (var live in _lives)
                live.gameObject.SetActive(true);
        }
    }
}