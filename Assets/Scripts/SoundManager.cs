using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public AudioSource SFX;

    public AudioClip backgroundMusic;
    public AudioClip car;

    public static SoundManager Instance;

    void Awake()
    {
        Instance = this;
    }

    public void StopSound()
    {
        SFX.Stop();
    }

    /*
    public bool SlowCar()
    {
        if (SFX.isPlaying == true)
        {
            if (MediumCar() == true || FastCar() == true)
            {
                SoundManager.Instance.SFX.Stop();
                return false;
            }
            else
            {
                return true;
            }
        }
        else if (SFX.isPlaying == false)
        {
            SFX.pitch = 1.0f;
            SFX.PlayOneShot(carSlow);
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool MediumCar()
    {
        if (SFX.isPlaying == true)
        {
            if (SlowCar() == true || FastCar() == true)
            {
                SoundManager.Instance.SFX.Stop();
                return false;
            }
            else
            {
                return true;
            }
        }
        else if (SFX.isPlaying == false)
        {
            SFX.pitch = 1.3f;
            SFX.PlayOneShot(carMedium);
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool FastCar()
    {
        if (SFX.isPlaying == true)
        {
            if (MediumCar() == true || SlowCar() == true)
            {
                SoundManager.Instance.SFX.Stop();
                return false;
            }
            else
            {
                return true;
            }
        }
        else if (SFX.isPlaying == false)
        {
            SFX.pitch = 1.5f;
            SFX.PlayOneShot(carFast);
            return true;
        }
        else
        {
            return false;
        }
    }
    */
}

