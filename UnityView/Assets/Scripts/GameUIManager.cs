using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    [Header("Полоски здоровья")]
    [SerializeField] private Slider _playerSlider;
    [SerializeField] private Slider _bossSlider;

    public void SetupHUD(int maxPlayerHP, int maxBossHP)
    {
        if (_playerSlider != null)
        {
            _playerSlider.maxValue = maxPlayerHP;
            _playerSlider.value = maxPlayerHP;
        }

        if (_bossSlider != null)
        {
            _bossSlider.maxValue = maxBossHP;
            _bossSlider.value = maxBossHP;
        }
    }

    public void UpdatePlayerHP(int currentHP)
    {
        if (_playerSlider != null)
        {
            _playerSlider.value = currentHP;
        }
    }

    public void UpdateBossHP(int currentHP)
    {
        if (_bossSlider != null)
        {
            _bossSlider.value = currentHP;
        }
    }
}