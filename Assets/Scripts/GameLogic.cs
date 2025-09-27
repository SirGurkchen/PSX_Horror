using System.Collections;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private BorderLogic[] _borderLogic;
    [SerializeField] private const float returnTextTimer = 2f;

    private void Start()
    {
        foreach (var border in _borderLogic)
        {
            border.OnBorderHit += Border_OnBorderHit;
        }
    }

    private void Border_OnBorderHit()
    {
        StartCoroutine(ShowReturnPrompt());
    }

    public IEnumerator ShowReturnPrompt()
    {
        _uiManager.ShowReturnText();

        yield return new WaitForSeconds(returnTextTimer);

        _uiManager.DisableReturnText();
    }
}