using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SoundManager : MonoBehaviour
{
    AudioClip[] _soundEffects;
    GameObject[] _speakers;

    string _effectPath = "Sound/Effect/";
    string _prefabPath = "Prefab/Speaker/";

    public static SoundManager Instance
    {
        get
        {
            if (!_instance)
            {
                var singleton = new GameObject("SoundManager", typeof(SoundManager));
                _instance = singleton.GetComponent<SoundManager>();
                DontDestroyOnLoad(singleton);
            }

            return _instance;
        }
    }
    static SoundManager _instance;

    void Awake()
    {
        if (!_instance)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _soundEffects = Resources.LoadAll<AudioClip>(_effectPath);

        _speakers = new GameObject[_soundEffects.Length];
        for(int i=0; i<_soundEffects.Length; ++i)
        {
            _speakers[i] = Instantiate(Resources.Load<GameObject>(_prefabPath), transform);
            _speakers[i].GetComponent<AudioSource>().clip = _soundEffects[i];
        }
    }

    public void PlaySound(ESoundType sound)
    {
        _speakers[(int)sound].GetComponent<AudioSource>().Play();
    }

    public void StopSound(ESoundType sound)
    {
        _speakers[(int)sound].GetComponent<AudioSource>().Stop();
    }

    public bool IsPlaying(ESoundType sound)
    {
        if (_speakers[(int)sound].GetComponent<AudioSource>().isPlaying)
        {
            return true;
        }
        return false;
    }
}