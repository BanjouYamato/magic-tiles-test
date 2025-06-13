using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiBackGround : MonoBehaviour
{   
    [SerializeField] RectTransform _canvas;
    [SerializeField] RectTransform _lineContainer, _laneContainer;
    private void Start()
    {
        float width = _canvas.rect.width;
        float posXLeft = -width / 2;
        float lineDistance = width / 4;

        foreach (RectTransform child in _lineContainer)
        {
            posXLeft += lineDistance;
            child.anchoredPosition = new Vector2(posXLeft, child.anchoredPosition.y);
        }
        posXLeft = -width / 2 + width / 8;
        foreach (RectTransform child in _laneContainer)
        {
            child.sizeDelta = new Vector2(lineDistance, child.sizeDelta.y);
            child.anchoredPosition = new Vector2(posXLeft, child.anchoredPosition.y);
            posXLeft += lineDistance;
        }
    }
}
