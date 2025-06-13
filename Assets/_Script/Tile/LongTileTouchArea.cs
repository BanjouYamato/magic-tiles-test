using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LongTileTouchArea : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] RectTransform _rect;   
    [SerializeField] LongTileController _controler;
    [SerializeField] Image _image;
    [SerializeField] TileMover _mover;

    private void Start()
    {
        float width = _mover.GetWidth();
        _rect.sizeDelta = new Vector2(width, width * 1.6f);
    }

    private void OnEnable()
    {
        _image.raycastTarget = true;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        _image.raycastTarget = false;

        _controler.TouchingArea();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _controler.TouchingOut();
    }
}
