using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    
    public Sound[] musicSounds, fairyMusicSounds, foodMusicSounds, undergroundMusicSounds, loopedSFXSounds, sfxSounds, sfxDefenseSounds, sfxLayerSounds;
    public AudioSource musicSource, fairyMusicSource, foodMusicSource, undergroundMusicSource, loopedSFXSource, sfxSource, sfxDefenseSource, sfxLayerSource;

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

    public void StopMusic()
    {
        musicSource.Stop();
        fairyMusicSource.Stop();
        foodMusicSource.Stop();
        undergroundMusicSource.Stop();
    }
    
    public void PlayHitSound()
    {
        Debug.Log("Try to Hit");

        if (GameManager.Instance.GetAmountOfItemsOfPlayer(BodyPartSO.Type.FairyTale) > GameManager.Instance.GetAmountOfItemsOfPlayer(BodyPartSO.Type.Food) && GameManager.Instance.GetAmountOfItemsOfPlayer(BodyPartSO.Type.FairyTale) > GameManager.Instance.GetAmountOfItemsOfPlayer(BodyPartSO.Type.Underground))
        {
            Debug.Log("FairyHit");
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

    public void PlayDodgeSound()
    {
        PlaySFXDefense("Dodge");
    }
    
    public void PlayShieldSound()
    {
        PlaySFXDefense("Shield");
    }
    
    public void PlayCritSound()
    {
        PlayHitSound();
        PlaySFXLayer("Crit");
    }

    /*public void PlayLowLifeLoop()
    {
        PlayLoopedSFX("LowLifeLoop");
    }*/
    
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
    
    public void PlaySFXDefense(string name)
    {
        Sound s = Array.Find(sfxDefenseSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }

        else
        {
            float randomPitch = Random.Range(minPitch, maxPitch);

            sfxDefenseSource.pitch = randomPitch;
            sfxDefenseSource.PlayOneShot(s.clip);
        }
    }
    
    public void PlaySFXLayer(string name)
    {
        Sound s = Array.Find(sfxLayerSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }

        else
        {
            float randomPitch = Random.Range(minPitch, maxPitch);

            sfxLayerSource.pitch = randomPitch;
            sfxLayerSource.PlayOneShot(s.clip);
        }
    }
    
    /*public void PlayLoopedSFX(string name)
    {
        Sound s = Array.Find(loopedSFXSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("SFX-Loop Not Found");
        }

        else
        {
            loopedSFXSource.clip = s.clip;
            loopedSFXSource.Play();
        }
    }*/
    
    public void StopLoopedSFX()
    {
        loopedSFXSource.Stop();
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
}
