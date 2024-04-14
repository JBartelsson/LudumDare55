using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    
    public Sound[] musicSounds, fairyMusicSounds, foodMusicSounds, undergroundMusicSounds, sfxSounds;
    public AudioSource musicSource, fairyMusicSource, foodMusicSource, undergroundMusicSource, sfxSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }

        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }
    public void PlayFightMusic()
    {
        if (GameManager.Instance.GetAmountOfItemsOfPlayer(BodyPartSO.Type.FairyTale) >= 1)
        {
            PlayFairyMusic("FairyMusicLow");
        }
        else if (GameManager.Instance.GetAmountOfItemsOfPlayer(BodyPartSO.Type.FairyTale) >= 3)
        {
            PlayFairyMusic("FairyMusicHigh");
        }
          
        if (GameManager.Instance.GetAmountOfItemsOfPlayer(BodyPartSO.Type.Food) >= 1)
        {
            PlayFairyMusic("FoodMusicLow");
        }
        else if (GameManager.Instance.GetAmountOfItemsOfPlayer(BodyPartSO.Type.Food) >= 3)
        {
            PlayFairyMusic("FoodMusicHigh");
        }
        
        if (GameManager.Instance.GetAmountOfItemsOfPlayer(BodyPartSO.Type.Food) >= 1)
        {
            PlayFairyMusic("UndergroundMusicLow");
        }
        else if (GameManager.Instance.GetAmountOfItemsOfPlayer(BodyPartSO.Type.Food) >= 3)
        {
            PlayUndergroundMusic("UndergroundMusicHigh");
        }
    }
    
    public void PlayFairyMusic(string name)
    {
        Sound s = Array.Find(fairyMusicSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Music Not Found");
        }

        else
        {
            fairyMusicSource.clip = s.clip;
            fairyMusicSource.Play();
        }
    }
    public void PlayFoodMusic(string name)
    {
        Sound s = Array.Find(foodMusicSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Music Not Found");
        }

        else
        {
            foodMusicSource.clip = s.clip;
            foodMusicSource.Play();
        }
    }    
    public void PlayUndergroundMusic(string name)
    {
        Sound s = Array.Find(undergroundMusicSounds, x => x.name == name);
        
        if (s == null)
        {
            Debug.Log("Music Not Found");
        }

        else
        {
            undergroundMusicSource.clip = s.clip;
            undergroundMusicSource.Play();
        }
    }    

    
    
    /*public void StopSFX()
    {
        sfxSource.DOFade(0, .3f).OnComplete(() =>
        {
            sfxSource.Stop();
            sfxSource.volume = 1f;
        });
    }*/
}
