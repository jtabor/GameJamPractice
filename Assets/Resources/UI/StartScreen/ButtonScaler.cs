using System.Collections.Generic;
using Unity.Multiplayer.Center.Common;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonScaler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Vector3 normalScale = Vector3.one;
    public Vector3 hoveredScale = Vector3.one * 1.1f;
    public ModePreview modeUI;
    public List<GameObject> modeTanks;
    public int direction = 1;
    public static int currentSelection = 0;
    public Tank tankScript;
    void Start()
    {
        transform.localScale = normalScale;

    }

    public void OnPointerEnter(PointerEventData e)
        => transform.localScale = hoveredScale;

    public void OnPointerExit(PointerEventData e)
        => transform.localScale = normalScale;

    public void OnPointerClick(PointerEventData e)
    {
        transform.localScale = hoveredScale; // Keeps it scaled when clicked
        currentSelection = (currentSelection + direction) % modeTanks.Count;
        if (currentSelection < 0)
        {
            currentSelection = 0;
        }

        tankScript.aimAtTarget(modeTanks[currentSelection].transform, 1f);
        modeUI.showMode(currentSelection);
    }
}
