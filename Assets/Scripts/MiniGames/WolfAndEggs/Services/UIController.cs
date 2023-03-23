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
        
        [Header("Очки")]
        [SerializeField] private TextMeshProUGUI _pointText;
        
        [Header("Кнопки")]
        [SerializeField] private List<Button> _buttons;
        
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
            _lives[livesLeft].enabled = false;
        }

        public void SwitchPauseButton()
        {
            foreach (var button in _buttons)
                button.enabled = !button.enabled;
        }
    }
}