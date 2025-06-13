using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

public enum RateType
{
    Perfect,
    Cool,
    Good,
    Miss
}
public class ScoreManager : BaseSingleton<ScoreManager>
{
    [SerializeField] TextMeshProUGUI _scoreText, _rateText, _comboText;
    [SerializeField] RectTransform _scoreRect;
    [SerializeField] Transform _rateTransform, _comboTransform;

    public Color perfectColor, coolColor, goodColor, missColor;
    
    private int score;
    private int combo;
    private Tween _scoreTween;
    private Sequence _rateSequence, _comboSequence;

    private void Start()
    {
        _rateTransform.localScale = Vector3.zero;
        _comboTransform.localScale = Vector3.zero;
    }

    public void ResetStat()
    {
        score = 0;
        combo = 0;
        _scoreText.text = score.ToString();
    }

    public RateType CheckPerfect(float startTime)
    {   
        float deltaTime = MathF.Abs(startTime - AudioManager.Instance.GetSource().time);
        if (deltaTime <= 0.05f)
            return RateType.Perfect;
        else if (deltaTime <= 0.1f)
            return RateType.Cool;
        else
            return RateType.Good;
    }

    public void AddScore(int score, RateType rate)
    {
        int multiple = rate switch
        {
            RateType.Perfect => 3,
            RateType.Cool => 2,
            RateType.Good => 1,
            _ => 0
        };
        this.score += score * multiple;
        _scoreText.text = this.score.ToString();
        AnimateScore();
    }
    public void PerformRate(RateType rate)
    {
        string rateTxt = "";
        switch (rate)
        {
            case RateType.Perfect:
                combo++;
                rateTxt = "Perfect";
                _rateText.color = perfectColor;
                _comboText.text = GetComboText(combo);
                BounceEffect(_comboTransform, _comboSequence);
                break;
            case RateType.Cool:
                combo++;
                rateTxt = "Cool";
                _rateText.color = coolColor;
                _comboText.text = GetComboText(combo);
                BounceEffect(_comboTransform, _comboSequence);
                break;
            case RateType.Good:
                combo = 0;
                rateTxt = "Good";
                _rateText.color = goodColor;
                _comboTransform.localScale = Vector3.zero;
                break;
            case RateType.Miss:
                combo = 0;
                rateTxt = "Miss";
                _rateText.color = missColor;
                _comboTransform.localScale = Vector3.zero;
                break;
        }

        _rateText.text = rateTxt;
        BounceEffect(_rateTransform, _rateSequence);
    }
    public void AnimateScore()
    {
        if (_scoreTween != null && _scoreTween.IsActive())
            _scoreTween.Kill();

        _scoreTween = _scoreRect.DOPunchPosition(
            new Vector2(0, 20), 0.2f, 5, 0.5f
        )
            .SetUpdate(true);
    }
    public void BounceEffect(Transform transform, Sequence sequence)
    {
        if (sequence != null && sequence.IsActive())
            sequence.Kill();

        transform.localScale = Vector3.zero;

        sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(1f, 0.5f).SetEase(Ease.OutQuad))
                     .AppendInterval(1f)
                     .Append(transform.DOScale(0f, 0.5f).SetEase(Ease.InQuad));
        sequence.SetUpdate(true);


    }
    private string GetComboText(int combo)
    {
        return "x" + combo;
    }
}
