using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public TMP_Text creditsText;
    public TMP_Text messageText;
    public TMP_Text betText;
    public Button spinButton;
    public SlotMachineManager manager;

    void Start()
    {
        spinButton.onClick.AddListener(OnSpinClicked);
        messageText.text = "";
        if (betText != null) betText.text = manager?.betAmount.ToString();
        UpdateCredits(manager?.credits ?? 0);
    }

    public void UpdateCredits(int c)
    {
        if (creditsText != null) creditsText.text = $"Credits: {c}";
    }

    public void ShowMessage(string msg)
    {
        if (messageText != null) messageText.text = msg;
    }

    void OnSpinClicked()
    {
        AudioManager.Instance.PlayButtonClick();
        manager?.OnSpinButtonPressed();
    }
}
