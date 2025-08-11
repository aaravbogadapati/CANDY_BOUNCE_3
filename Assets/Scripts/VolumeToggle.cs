using UnityEngine;
using UnityEngine.UI;

public class VolumeToggleButton : MonoBehaviour
{
    public Sprite spriteOn;
    public Sprite spriteOff;
    public string saveKey = "VolumeMuted";

    private Image buttonImage;
    private bool isMuted;

    void Start()
    {
        buttonImage = GetComponent<Image>();

        // Load saved mute state, default is unmuted (0)
        isMuted = PlayerPrefs.GetInt(saveKey, 0) == 1;

        ApplyVolumeSetting();
        UpdateButtonSprite();
    }

    public void ToggleVolume()
    {
        isMuted = !isMuted;
        PlayerPrefs.SetInt(saveKey, isMuted ? 1 : 0);
        PlayerPrefs.Save();

        ApplyVolumeSetting();
        UpdateButtonSprite();
    }

    void ApplyVolumeSetting()
    {
        AudioListener.volume = isMuted ? 0f : 1f;
    }

    void UpdateButtonSprite()
    {
        if (buttonImage != null)
            buttonImage.sprite = isMuted ? spriteOff : spriteOn;
    }
}
