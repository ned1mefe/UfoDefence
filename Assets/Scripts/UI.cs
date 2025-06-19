using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    private TowerPlacer _towerPlacer;
    private TMP_Text[] _counterTexts;
    private Button[] _towerButtons;
    private Button _actionButton;
    private TMP_Text _levelIndicator;
    private TMP_Text _postLevelText;
    private void Awake()
    {
        _towerPlacer = GetComponentInChildren<TowerPlacer>();
        _towerButtons = _towerPlacer.GetComponentsInChildren<Button>();
        _actionButton = transform.GetChild(1).GetComponent<Button>();
        _levelIndicator = transform.GetChild(2).GetComponentInChildren<TMP_Text>();
        _postLevelText = transform.GetChild(3).GetComponentInChildren<TMP_Text>();
        
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

    public void OnLevelLose()
    {
        _postLevelText.text = $"You Lost, Try Again";

        StartCoroutine(ShowPostLevelPanel());
    }
    public void OnLevelEnd(int newLevel)
    {
        if(newLevel < 4) _levelIndicator.text = $"Level: {newLevel}";
        _postLevelText.text = $"Level {newLevel - 1} Done";

        StartCoroutine(ShowPostLevelPanel());
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

    public void ActionButtonPressed()
    {
        GameManager.Instance.ToggleGamePhase();
    }

    public IEnumerator DisableAndToggleActionButton()
    {
        _actionButton.interactable = false;
        
        var child1 = _actionButton.transform.GetChild(0).gameObject;
        var child2 = _actionButton.transform.GetChild(1).gameObject;
        
        child1.SetActive(!child1.activeSelf);
        child2.SetActive(!child2.activeSelf);

        yield return new WaitForSeconds(0.5f);
        
        _actionButton.interactable = true;
    }
    
    private IEnumerator ShowPostLevelPanel()
    {
        var parent = _postLevelText.transform.parent.gameObject;
        parent.SetActive(true);
        yield return new WaitForSeconds(2f);
        parent.SetActive(false);
    }
    
}
