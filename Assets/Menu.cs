using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public AudioClip menuClip;
    public AudioClip menuClip2;
    public AudioSource audioMenu;

    public struct Player
    {
        public int nr;
        public string name;
    }

    public List<Player> lisPlayer;
    int sizeList;
    int flagFade = 0;

    void Start()
    {
        audioMenu = GetComponent<AudioSource>();
        lisPlayer = new List<Player>();
        sizeList = 1;
        GameObject.Find("DeleteButton1").SetActive(false);
        StartCoroutine(InitialTimer());
    }

    void Update()
    {
        if (flagFade == 0)
            GameObject.Find("UIfont").GetComponent<CanvasGroup>().alpha += 0.005f;
        if (flagFade == 1 && GameObject.Find("UIfont").GetComponent<CanvasGroup>().alpha > 0)
            GameObject.Find("UIfont").GetComponent<CanvasGroup>().alpha -= 0.005f;
        if( flagFade == 1 && GameObject.Find("UIfont").GetComponent<CanvasGroup>().alpha <= 0)
            GameObject.Find("UI").GetComponent<CanvasGroup>().alpha += 0.005f;
    }

    IEnumerator InitialTimer()
    {      
        yield return new WaitForSeconds(5);
        flagFade = 1;
    }

    public void deletePlayerRow()
    {
        audioMenu.PlayOneShot(menuClip);
        string buttonNr = EventSystem.current.currentSelectedGameObject.name.Substring(12);
        Debug.Log(buttonNr);
        Destroy(GameObject.Find("Input"+buttonNr));
    }

    public void addPlayerRow()
    {
        audioMenu.PlayOneShot(menuClip);
        sizeList++;
        GameObject copy = GameObject.Find("Input1");
        GameObject instance = GameObject.Instantiate(copy) as GameObject;
        instance.transform.SetParent(GameObject.Find("Content List").transform, false);
        instance.name = "Input" + sizeList;
        instance.transform.GetChild(0).gameObject.name = "InputField" + sizeList;
        instance.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.name = "Text" + sizeList;
        instance.transform.GetChild(0).gameObject.GetComponent<InputField>().text = "";
        instance.transform.GetChild(1).gameObject.SetActive(true);
        instance.transform.GetChild(1).gameObject.name = "DeleteButton" + sizeList;
    }

    public void startGame()
    {
        audioMenu.PlayOneShot(menuClip);
        GameObject parentAux = GameObject.Find("Content List");
        Player aux;
        for (int i = 0; i < parentAux.transform.childCount; i++)
        {
            string auxName = parentAux.transform.GetChild(i).gameObject.transform.GetChild(0).gameObject.GetComponent<InputField>().text;
            if (auxName == "")
                continue;
            aux = new Player();
            aux.nr = i;
            aux.name = auxName;
            lisPlayer.Add(aux);
        }
        DontDestroyOnLoad(GameObject.Find("TransitionSceneHelper"));
        SceneManager.LoadScene(1);
    }

}
