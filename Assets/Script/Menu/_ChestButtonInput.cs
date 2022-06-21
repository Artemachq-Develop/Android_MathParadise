using UnityEngine;
using UnityEngine.EventSystems;
public class _ChestButtonInput : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private ChestSystem chestSystem;
    
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        chestSystem._pressed = true;
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        chestSystem._pressed = false;
        chestSystem.saveLoad.SaveChestProgress();
    }
}
