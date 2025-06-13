using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TileMover : MonoBehaviour
{
    public RectTransform canvas;
    public Image warningImage;

    private float speed;
    private BaseTilePool<TileMover> pool;
    private TileSpawner spawner;
    private float startTime;
    private float endTime;
    private float _originalWidth;
    private float _botLimit;
    private bool _isTouched;
    public float EndTime => endTime;
    public float StartTime => startTime;

    public void SetPool(BaseTilePool<TileMover> poolRef, Vector2 size, float heigh)
    {
        pool = poolRef;
        canvas.sizeDelta = size;
        _originalWidth = size.x;
        _botLimit = -heigh - (size.x * 1.6f);

    }
    public void Init(float speed, TileSpawner spawner, TileData data)
    {
        this.speed = speed;
        this.spawner = spawner;
        this.startTime = data.startTime;
        _isTouched = false;

        if (data.tileType != TileType.Long) return;
        endTime = data.endTime;
        float heigh = canvas.sizeDelta.x * 1.6f;
        float extraHeigh = (data.endTime - data.startTime) * speed * 190;
        heigh += extraHeigh;
        canvas.sizeDelta = new Vector2(canvas.sizeDelta.x, heigh);
    }

    void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
        if (canvas.anchoredPosition.y <= _botLimit && !_isTouched)
        {
            AudioManager.Instance.GetSource().Stop();
            AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.LoseClip);
            LoseGame();
            Time.timeScale = 0;
        }
    }
    private void LoseGame()
    {   
        warningImage.DOFade(1, 0.3f)
            .SetLoops(10, LoopType.Yoyo)
            .SetEase(Ease.InOutSine)
            .OnComplete(() => GameManager.Instance.Toggle(true))
            .SetUpdate(true);
        Vector2 ancorPos = new Vector2(canvas.anchoredPosition.x, canvas.anchoredPosition.y + _originalWidth * 1.6f);
        canvas.DOAnchorPos(ancorPos, 3f)
            .SetEase(Ease.OutCubic)
            .SetUpdate(true);
    }
    public void ReturnToPool()
    {
        spawner.RemoveTile();
        pool.ReturnToPool(this);
    }
    public float GetWidth()
    {
        return _originalWidth;
    }
    public void SetTouch()
    {
        _isTouched = true;
    }
}
