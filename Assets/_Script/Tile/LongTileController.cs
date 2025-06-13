using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LongTileController : MonoBehaviour
{
    [SerializeField] private Image _fillImage, _loseImage;
    private TileMover _mover;
    private bool _isTouch;
    private Vector3 _originalScale;
    private float _duration;
    private float _holdStartTime;
    private RateType _rateType;
    private int _score;

    private void Awake()
    {
        _originalScale = transform.localScale;
        _mover = GetComponent<TileMover>();
    }

    private void OnEnable()
    {
        _isTouch = false;
        _fillImage.fillAmount = 0;
        Color color = _loseImage.color;
        color.a = 0;
        _loseImage.color = color;
        transform.localScale = _originalScale;
    }
    private void Update()
    {
        if (!_isTouch) return;
        float currentTime = AudioManager.Instance.GetSource().time;
        float timeHeld = currentTime - _holdStartTime;
        float delta = timeHeld / _duration;
        delta = Mathf.Clamp(delta, 0, 1);
        _fillImage.fillAmount = delta;

        if (delta >= 1f)
        {
            ScoreManager.Instance.AddScore(1 + _score, _rateType);
            CompleteTile();
        }
    }

    private void CompleteTile()
    {
        _isTouch = false;

        var sequence = DOTween.Sequence();

        sequence.Append(_loseImage.DOFade(1, 0.05f));
        sequence.Append(transform.DOScale(0, 0.5f));
        sequence.AppendCallback(_mover.ReturnToPool);
    }

    public void TouchingOut()
    {
        _isTouch = false;
    }
    public void TouchingArea()
    {
        _rateType = ScoreManager.Instance.CheckPerfect(_mover.StartTime);
        AudioManager audio = AudioManager.Instance;
        audio.PlaySoundEffect(audio.TileClip);
        ScoreManager.Instance.PerformRate( _rateType );
        _holdStartTime = AudioManager.Instance.GetSource().time;
        _duration = _mover.EndTime - _mover.StartTime;
        _score = (int)MathF.Floor(_duration / 0.2f);
        _mover.SetTouch();
        _isTouch = true;

    }
}