using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;


public class NormalTileControler : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] Image _image;
    [SerializeField] CanvasGroup _fillCG;
    private Vector3 _originalScale;
    private TileMover _mover;

    private void Awake()
    {
        _originalScale = transform.localScale;
        _mover = GetComponent<TileMover>();
    }
    private void OnEnable()
    {
        _image.raycastTarget = true;
        _fillCG.alpha = 0;
        transform.localScale = _originalScale;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        RateType rate = ScoreManager.Instance.CheckPerfect(_mover.StartTime);
        ScoreManager.Instance.PerformRate(rate);
        ScoreManager.Instance.AddScore(1, rate);
        AudioManager audio = AudioManager.Instance;
        audio.PlaySoundEffect(audio.TileClip);
        _mover.SetTouch();
        _image.raycastTarget = false;
        var sequence = DOTween.Sequence();

        sequence.Append(_fillCG.DOFade(1, 0.05f));
        sequence.Append(transform.DOScale(0, 0.5f));
        sequence.AppendCallback(_mover.ReturnToPool);


    }
}
