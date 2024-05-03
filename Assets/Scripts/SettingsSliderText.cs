using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsSliderText : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _sliderText;
    public bool isCoins = false;
    void Start()
    {
        _slider.onValueChanged.AddListener((v) =>
        {
            string label = isCoins ? "монеты" : "враги";
            _sliderText.text = $"{label} {v}";

            if (isCoins)
            {
                DifficultySettings.coinsWinCondition = (int)v;
            }
            else
            {
                DifficultySettings.fragsWinCondition = (int)v;
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
