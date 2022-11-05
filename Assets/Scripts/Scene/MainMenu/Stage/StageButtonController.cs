using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StageButtonController : MonoBehaviour
{
    [SerializeField] private bool _isSoon;

    [SerializeField] private TextMeshProUGUI _stageText;
    [SerializeField] private Transform _partStarParent;
    [SerializeField] private GameObject _lock;

    private Stage _myStage;
    private Button _button;

    private int _partMax;


    public void Setup(int stageNumber)
    {
        if (_isSoon)
        {
            SetSoon();
            return;
        }

        _myStage = PlayerData.Instance.StageData.GetStageByNumber(stageNumber);

        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClickButtonDo);

        _partMax = _partStarParent.childCount + 1;

        SetProperty();
    }

    private void OnClickButtonDo()
    {
        if (_myStage.Part >= _partMax || _myStage.Islocked)
            return;

        MoveSceneRequest.Instance.SetStage(_myStage.StageNumber);
        MoveSceneRequest.Instance.MoveTo("Gameplay");
    }

    private void SetProperty()
    {
        int currentPart = _myStage.Part;

        for (int i = 1; i < currentPart; i++)
            _partStarParent.GetChild(i - 1).gameObject.SetActive(true);

        _stageText.text = "" + _myStage.StageNumber;

        if (_myStage.Islocked)
            _lock.SetActive(true);

    }

    private void SetSoon()
    {
        GetComponent<Button>().interactable = false;
        _stageText.fontSize = 38;
        _stageText.text = "Soon";
    }
}