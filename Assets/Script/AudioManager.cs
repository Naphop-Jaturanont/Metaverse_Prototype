using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{   
    [SerializeField] Slider volumeSlider;
    private float beforeMute;

    void Start() {
        if(!PlayerPrefs.HasKey("musicVolume")) {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        } else {
            Load();
        }
    } 

    public void ChangeVolume() {
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    public void Load() {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    public void Save() {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }

    public void MuteToggle(bool muted) {
        if(muted) {
            beforeMute = volumeSlider.value;
            volumeSlider.value = 0;
            ChangeVolume();
        } else {
            volumeSlider.value = beforeMute;
            ChangeVolume();
        }
    }
}
