using UnityEngine;
using UnityEngine.UI;

public class ModePreview : MonoBehaviour
{
    public Sprite[] modeSprites; // assign your 3 sprites in Inspector
    private Image display;

    void Awake() => display = GetComponent<Image>();

    public void showMode(int modeIndex)
    {
        if (modeIndex < 0 || modeIndex >= modeSprites.Length) return;
        display.sprite = modeSprites[modeIndex];
    }
}
