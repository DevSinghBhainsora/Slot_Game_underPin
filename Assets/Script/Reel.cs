using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Reel : MonoBehaviour
{
    [Header("UI")]
    public Image symbolDisplay;

    [Header("Spin Settings")]
    public float spinDuration = 2f; // Fixed spin duration in seconds

    [HideInInspector]
    public int finalSymbolIndex; // set at runtime

    private List<SymbolData> symbols;
    private bool spinning = false;
    private System.Action onStop;

    /// <summary>
    /// Starts spinning the reel
    /// </summary>
    public void StartSpin(List<SymbolData> availableSymbols, System.Action onReelStop)
    {
        if (spinning) return;

        symbols = availableSymbols;
        onStop = onReelStop;

        StartCoroutine(SpinRoutine());
    }

    IEnumerator SpinRoutine()
    {
        spinning = true;

        float timer = 0f;
        int index = 0;

        while (timer < spinDuration)
        {
            index = Random.Range(0, symbols.Count);
            symbolDisplay.sprite = symbols[index].sprite;

            timer += Time.deltaTime;
            yield return null; // spin every frame for smoothness
        }

        // Reel stops here
        finalSymbolIndex = index;
        symbolDisplay.sprite = symbols[finalSymbolIndex].sprite;

        spinning = false;

        // Notify manager that this reel has stopped
        onStop?.Invoke();
    }
}
