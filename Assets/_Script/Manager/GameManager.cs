using UnityEngine;

public class GameManager : BaseSingleton<GameManager>
{
    [SerializeField] NormalTilePool _tilePool;
    [SerializeField] LongTilePool _longTilePool;
    [SerializeField] GameObject _playButton;
    
    public void StartGame()
    {
        AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.TileClip);
        Time.timeScale = 1;
        Toggle(false);
        AudioManager.Instance.GetSource().Play();
        ScoreManager.Instance.ResetStat();
        TileSpawner.Instance.ResetSpawner();
        _tilePool.ResetPool();
        _longTilePool.ResetPool();
    }
    public void Toggle(bool toggle)
    {
        _playButton.SetActive(toggle);
    }
}
