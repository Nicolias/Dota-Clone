using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GestureClick : MonoBehaviour, IPointerClickHandler
{
    public event UnityAction<Vector3> OnClick;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
            OnClick?.Invoke(eventData.pointerPressRaycast.worldPosition);
    }
}
