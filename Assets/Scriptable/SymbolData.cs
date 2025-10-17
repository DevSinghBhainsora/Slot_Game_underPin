using UnityEngine;

[CreateAssetMenu(fileName = "SymbolData", menuName = "SlotMachine/SymbolData")]
public class SymbolData : ScriptableObject
{
    public string symbolName;
    public Sprite sprite;
    public int multiplier = 2;
    public bool isWild = false;
    public bool isJackpot = false;
}
