using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    private TowerPlacer _towerPlacer;
    private Button[] _towerButtons;
    private TMP_Text[] _counterTexts;
    private void Awake()
    {
        _towerPlacer = GetComponentInChildren<TowerPlacer>();
        _towerButtons = _towerPlacer.GetComponentsInChildren<Button>();
        
        _counterTexts = new TMP_Text[_towerButtons.Length];
        for (int i = 0; i < _towerButtons.Length; i++)
        {
            var counterObj = _towerButtons[i].transform.Find("Counter");
            if (counterObj != null)
                _counterTexts[i] = counterObj.GetComponent<TMP_Text>();
        }
    }

    private void Start()
    {
        UpdateTowerButtons();
    }

    public void TowerSelectButtonPressed(int towerNumber)
    {
        _towerPlacer.StartPlacing(towerNumber);
    }

    public void UpdateTowerButtons()
    {
        _counterTexts[0].text = $"x{_towerPlacer.availableTower1}";
        _towerButtons[0].interactable = _towerPlacer.availableTower1 > 0;
        
        _counterTexts[1].text = $"x{_towerPlacer.availableTower2}";
        _towerButtons[1].interactable = _towerPlacer.availableTower2 > 0;

        _counterTexts[2].text = $"x{_towerPlacer.availableTower3}";
        _towerButtons[2].interactable = _towerPlacer.availableTower3 > 0;
    }
}
