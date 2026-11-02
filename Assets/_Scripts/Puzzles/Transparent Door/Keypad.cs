using DG.Tweening;
using TMPro;
using UnityEngine;

public class KeypadUI : MonoBehaviour
{
    [SerializeField] private string correctPasscode = "1008";
    [SerializeField] private int maxDigits = 4;
    [SerializeField] private TMP_Text displayText;
    [SerializeField] private GameObject objectToUnlock;

    private string input = "";

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void Open()
    {
        input = "";
        UpdateDisplay();
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void Digit(int number)
    {
        if (input.Length >= maxDigits) return;

        input += number.ToString();
        UpdateDisplay();
    }

    public void Clear()
    {
        input = "";
        UpdateDisplay();
    }

    public void Submit()
    {
        if (input == correctPasscode)
        {
            Unlock();
        }
        else
        {
            WrongCode();
        }
    }

    private void Unlock()
    {
        if (objectToUnlock != null) objectToUnlock.SetActive(false);
        Close();
    }

    private void WrongCode()
    {
        input = "";
        UpdateDisplay();

        if (displayText != null)
        {
            displayText.transform.DOKill(true);
            displayText.transform.DOShakePosition(0.3f, 10f).SetLink(gameObject);
        }
    }

    private void UpdateDisplay()
    {
        if (displayText != null) displayText.text = input;
    }
}