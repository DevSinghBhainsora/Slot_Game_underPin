using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SlotMachineManager : MonoBehaviour
{
    [Header("Reels & Symbols")]
    public Reel[] reels;                 // Drag your Reel GameObjects here
    public List<SymbolData> symbols;     // List of symbols (ScriptableObjects)

    [Header("Gameplay Settings")]
    public int credits = 1000;           // Starting credits
    public int betAmount = 100;          // Bet per spin
    public UIController uiController;    // Reference to your UIController

    private int finishedReels = 0;       // How many reels have finished spinning
    private bool isSpinning = false;     // Prevent multiple spins at once

    void Start()
    {
        uiController.UpdateCredits(credits);
    }

    /// <summary>
    /// Called when spin button is pressed
    /// </summary>
    public void OnSpinButtonPressed()
    {
        if (isSpinning)
            return;

        if (credits < betAmount)
        {
            uiController.ShowMessage("Not enough credits!");
            return;
        }

        // Deduct bet
        credits -= betAmount;
        uiController.UpdateCredits(credits);

        // UI update
        uiController.ShowMessage("Spinning...");
        isSpinning = true;
        finishedReels = 0;

        // Play sounds
        AudioManager.Instance.PlayButtonClick();
        AudioManager.Instance.PlayReelSpin();

        // Start spinning all reels
        StartCoroutine(StartSpinning());
    }

    /// <summary>
    /// Spins reels sequentially with a small delay
    /// </summary>
    IEnumerator StartSpinning()
    {
        for (int i = 0; i < reels.Length; i++)
        {
            reels[i].StartSpin(symbols, OnReelStopped);
            yield return new WaitForSeconds(0.2f); // stagger reel starts for effect
        }
    }

    /// <summary>
    /// Called by each reel when it stops
    /// </summary>
    void OnReelStopped()
    {
        finishedReels++;

        // Wait until all reels are stopped
        if (finishedReels == reels.Length)
        {
            CheckResult();
        }
    }

    /// <summary>
    /// Checks if all reels have the same symbol and updates UI/credits
    /// </summary>
    void CheckResult()
    {
        isSpinning = false;

        int index = reels[0].finalSymbolIndex;
        bool allSame = true;

        for (int i = 1; i < reels.Length; i++)
        {
            if (reels[i].finalSymbolIndex != index)
            {
                allSame = false;
                break;
            }
        }

        if (allSame)
        {
            SymbolData winSymbol = symbols[index];
            int payout = betAmount * winSymbol.multiplier;
            credits += payout;
            uiController.UpdateCredits(credits);

            if (winSymbol.isJackpot)
                uiController.ShowMessage($"🎉 JACKPOT! {winSymbol.symbolName} x{winSymbol.multiplier}!");
            else
                uiController.ShowMessage($"Win {payout} credits with {winSymbol.symbolName}!");

            AudioManager.Instance.PlayWin();
        }
        else
        {
            uiController.ShowMessage("Try Again...");
            AudioManager.Instance.PlayLose();
        }
    }
}
