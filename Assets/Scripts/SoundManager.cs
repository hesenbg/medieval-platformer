using UnityEngine;
public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource Music;
    [SerializeField] AudioSource PlayerSFXSource;
    [SerializeField] AudioSource GuardSFXSource;
    [SerializeField] AudioClip BackGroundMusic;
    [SerializeField] AudioClip FootSteps;
    [SerializeField] AudioClip HurtKnight;
    [SerializeField] AudioClip SwordHit;
    [SerializeField] AudioClip SwordSlash;
    Player player;
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        Music.clip = BackGroundMusic;
        Music.Play();
    }
    public void PlaySlash()
    {
        PlayerSFXSource.clip= SwordSlash; 
        PlayerSFXSource.Play();
    }
    public void PlayHurt()
    {
        GuardSFXSource.clip = HurtKnight;
        GuardSFXSource.Play();
    }
    public void PlayFootStep()
    {
        PlayerSFXSource.clip = FootSteps;
        PlayerSFXSource.Play();
    }
    public void PlayHit()
    {
        PlayerSFXSource.clip = SwordHit;
        PlayerSFXSource.Play();
    }
    void Update()
    {
        PlayerSFX();
    }
    void PlayerSFX()
    {
        if(player.RunFloat==1 && player.IsOnGround)
        {
            if (!PlayerSFXSource.isPlaying)
            {
                PlayFootStep();
            }
        }
    }
}
