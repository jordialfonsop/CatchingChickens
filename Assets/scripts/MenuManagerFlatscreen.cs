using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManagerFlatscreen : MonoBehaviour
{
    public static MenuManagerFlatscreen Instance; // 1

    [SerializeField] public MenuP1 P1;
    [SerializeField] public MenuP2 P2;
    [SerializeField] public ExitP1 Exit1;
    [SerializeField] public ExitP2 Exit2;
    float a;

    private int playcount = 0;

    public Animator transition;
    public AudioSource music;
    public float waitTime = 1f;
    public float fadeTime = 1f;
    
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }
    void Update()
    {
        P1 = P1.GetComponent<MenuP1>();
        P2 = P2.GetComponent<MenuP2>();
        Exit1 = Exit1.GetComponent<ExitP1>();
        Exit2 = Exit2.GetComponent<ExitP2>();
        if (P1.is1Inside == true && P2.is2Inside == true){
            StartCoroutine(LoadLevel("Game Flatscreen", music));
        }
        if (Exit1.is1Exiting == true && Exit2.is2Exiting == true){
            StartCoroutine(QuitGame(music));
        }
    }

    IEnumerator LoadLevel(string level, AudioSource audioSource){
        if (playcount == 0){
            playcount++;
            transition.SetTrigger("Start");

            float startVolume = audioSource.volume;
    
            while (audioSource.volume > 0) {
                audioSource.volume -= startVolume * Time.deltaTime / fadeTime;
    
                yield return null;
            }

            yield return new WaitForSeconds(waitTime);

            SceneManager.LoadScene(level);
        }
    }

    IEnumerator QuitGame(AudioSource audioSource){
        if (playcount == 0){
            playcount++;
            float startVolume = audioSource.volume;
    
            while (audioSource.volume > 0) {
                audioSource.volume -= startVolume * Time.deltaTime / fadeTime;
    
                yield return null;
            }

            SoundManager.Instance.PlayQuit();

            yield return new WaitForSeconds(3);

            Application.Quit();
        }
    }

}
