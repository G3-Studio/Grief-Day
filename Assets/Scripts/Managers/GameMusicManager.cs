using UnityEngine;

public class GameMusicManager : MonoBehaviour
{
    [SerializeField] public AudioClip intro;
    [SerializeField] public AudioClip loop;

    private AudioSource[] sources;
    
    void Awake()
    {
        sources = GetComponentsInChildren<AudioSource>();
        
        sources[0].clip = intro;
        sources[1].clip = loop;
        
        sources[0].loop = false;
        sources[1].loop = true;

        sources[0].Play();
        sources[1].PlayDelayed(sources[0].clip.length);
    }

    void Start()
    {
        
    }
}
