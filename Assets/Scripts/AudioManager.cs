using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    
    public Sound[] musicSounds, fairyMusicSounds, foodMusicSounds, undergroundMusicSounds, sfxSounds;
    public AudioSource musicSource, fairyMusicSource, foodMusicSource, undergroundMusicSource, sfxSource;

    [SerializeField] private float minPitch = 0.5f;
    [SerializeField] private float maxPitch = 1.5f;
    
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

    public void PlayHitSound()
    {
        if (GameManager.Instance.GetAmountOfItemsOfPlayer(BodyPartSO.Type.FairyTale) > GameManager.Instance.GetAmountOfItemsOfPlayer(BodyPartSO.Type.Food) && GameManager.Instance.GetAmountOfItemsOfPlayer(BodyPartSO.Type.FairyTale) > GameManager.Instance.GetAmountOfItemsOfPlayer(BodyPartSO.Type.Underground))
        {
            PlaySFX("FairyHit");
        }
        else if (GameManager.Instance.GetAmountOfItemsOfPlayer(BodyPartSO.Type.Food) > GameManager.Instance.GetAmountOfItemsOfPlayer(BodyPartSO.Type.FairyTale) && GameManager.Instance.GetAmountOfItemsOfPlayer(BodyPartSO.Type.Food) > GameManager.Instance.GetAmountOfItemsOfPlayer(BodyPartSO.Type.Underground))
        {
            PlaySFX("FoodHit");
        }
        else if (GameManager.Instance.GetAmountOfItemsOfPlayer(BodyPartSO.Type.Underground) > GameManager.Instance.GetAmountOfItemsOfPlayer(BodyPartSO.Type.Food) && GameManager.Instance.GetAmountOfItemsOfPlayer(BodyPartSO.Type.Underground) > GameManager.Instance.GetAmountOfItemsOfPlayer(BodyPartSO.Type.FairyTale))
        {
            PlaySFX("UndergroundHit");
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
            float randomPitch = Random.Range(minPitch, maxPitch);

            sfxSource.pitch = randomPitch;
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
            PlayFoodMusic("FoodMusicLow");
        }
        else if (GameManager.Instance.GetAmountOfItemsOfPlayer(BodyPartSO.Type.Food) >= 3)
        {
            PlayFoodMusic("FoodMusicHigh");
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
