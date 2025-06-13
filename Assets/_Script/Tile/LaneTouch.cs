using UnityEngine;
using UnityEngine.EventSystems;

public class LaneTouch : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        ScoreManager.Instance.PerformRate(RateType.Miss);
    }
}
