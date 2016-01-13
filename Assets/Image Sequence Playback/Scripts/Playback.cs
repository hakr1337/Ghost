using UnityEngine;
using UnityEngine.UI;

public class Playback : MonoBehaviour 
{
    public string sequenceFolder;
    public float FPS = 25;
    public bool playOnAwake = true;
    public bool loop;
    public PlaybackMode mode;
    public Material playbackMat;
    public RawImage rawImage;
    public float startDelay;
    Texture2D[] sequence;
    Texture2D current;
    
    int index = 0;
    
    bool isPlaying;
    bool isPaused;
    public bool IsPlaying
    {
        get
        {
            return isPlaying;
        }
    }
    public bool IsPaused
    {
        get
        {
            return isPaused;
        }
    }

    public bool playAudio;
    public AudioSource source;
    public AudioClip clip;
    public float clipBaseFps = 25;

    /// <summary>
    /// Progress of playback ranging from 0 to 1.
    /// </summary>
    public float progress
    {
        get
        {
            return (float)index / (sequence.Length-1f);
        }
    }
    /// <summary>
    /// Total number of frames.
    /// </summary>
    public int frames
    {
        get
        {
            if (sequence != null)
                return sequence.Length;
            else return 0;
        }
    }
    /// <summary>
    /// The index of the current displayed frame (starting from 0).
    /// </summary>
    public int currentFrame
    {
        get
        {
            return index;
        }
    }

    public bool showDebug;

    void Awake()
    {
        LoadSequence(sequenceFolder, playOnAwake);
    }

    [SerializeField]
    float time = 0;
    void Update()
    {
        if (isPlaying)
        {
            time += Time.deltaTime;
            if (time > 1f / FPS)
            {
                time = 0;
                PlayBack();
            }
        }
    }

    void PlayBack()
    {
        if (isPlaying)
        {
            if (sequence != null)
            {
                if (sequence.Length > 0)
                {
                    UpdateFrame();
                    if (playAudio) source.pitch = FPS / clipBaseFps;

                    index++;
                    if (index == sequence.Length)
                    {
                        if (loop)
                        {
                            index = 0;
                            if (playAudio)
                            {
                                source.Stop();
                                source.Play();
                            }
                            return;
                        }
                        else
                        {
                            index = 0;
                            isPlaying = false;
                            return;
                        }
                    }
                }
            }
        }
    }

    void UpdateFrame()
    {
        if (mode == PlaybackMode.Material)
            playbackMat.mainTexture = sequence[index];
        else if (mode == PlaybackMode.LegacyGUI)
            current = sequence[index];
        else
            rawImage.texture = sequence[index];
    }

    public void Play()
    {
        Play(0);
    }
    public void Play(float delay)
    {
        if (playAudio)
        {
            if (!source || !clip) Debug.LogError("Playback: AudioSource or AudioClip is null!");
        }

        if (!isPlaying)
        {
            index = 0;
            isPlaying = true;
            time = (1f/FPS) - delay;
        }
        else if (isPaused)
        {
            isPaused = false;
            isPlaying = true;
            time = (1f / FPS) - delay;
        }
        if (playAudio)
        {
            if (source.clip != clip) source.clip = clip;
            source.pitch = FPS / clipBaseFps;
            source.PlayDelayed(delay);
        }
    }

    public void Pause(bool value)
    {
        isPaused = value;
        isPlaying = !value;
        if (value)
        {
            if (playAudio)
            source.Pause();
        }
        else
        {
            time = 1f / FPS;
            if (playAudio)
            source.Play();
        }
    }

    public void Stop()
    {
        isPlaying = false;
        isPaused = false;
        index = 0;
        if (playAudio) source.Stop();
    }
    /// <summary>
    /// If clear = true and Playback is in uGUI mode, it will also clear the texture from the RawImage.
    /// </summary>
    public void Stop(bool clear)
    {
        isPlaying = false;
        isPaused = false;
        index = 0;
        if (playAudio) source.Stop();
        if (mode == PlaybackMode.uGUI && clear) rawImage.texture = null;
    }

    public void LoadSequence(string resourceFolder, bool play = false)
    {
        Object[] obj = Resources.LoadAll(resourceFolder, typeof(Texture2D));
        if (obj.Length > 0)
        {
            sequence = new Texture2D[obj.Length];
            for (int i = 0; i < obj.Length; i++)
            {
                sequence[i] = (Texture2D)obj[i];
            }
            index = 0;
            UpdateFrame();
            if (play) Play(startDelay);
        }
        else Debug.LogError("Playback: Can't find sequence folder, it needs to be in 'Resources' folder");
    }
    public void LoadSequence(Texture2D[] textures,bool play = false)
    {
        if (textures != null)
        {
            sequence = textures;
            if (play) Play();
        }
    }

    /// <summary>
    /// Reset the sequence array.
    /// </summary>
    public void Clear()
    {
        Stop();
        sequence = new Texture2D[0];
        if (mode == PlaybackMode.uGUI) rawImage.texture = null;
    }

    /// <summary>
    /// Get the texture that should be displayed right now.
    /// </summary>
    public Texture2D GetTexture()
    {
        return current;
    }
}

public enum PlaybackMode { Material = 0, LegacyGUI = 1, uGUI = 2 }