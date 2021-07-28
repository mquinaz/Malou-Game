using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Stone : MonoBehaviour
{
    public Route currentRoute;

    int[] routePosition;
    int[] jailFlag;
    public int steps;
    bool isMoving;
    bool flagRolled;
    public DiceScript diceAux;
    public static bool flagRoll;
    int player = 1;
    string firstPlayer;
    string lastPlayer;
    bool firstPlayerFlag;
    bool choicePlayer;
    int auxMovement;
    bool lastShortcut;

    public AudioClip soundMove;
    public AudioClip soundDice;
    public AudioClip soundDare;
    public AudioSource audioControl;

    public Sprite soundtrackMute1;
    public Sprite soundtrackMute2;
    public Sprite effectsMute1;
    public Sprite effectsMute2;
    bool soundtrackFlag;
    bool effectsFlag;

    //int[] movePlayer;
    //int countMovePlayer;
    void Start()
    {
        //movePlayer = new int[4];
        //countMovePlayer = 0;
        soundtrackFlag = true;
        effectsFlag = true;
        audioControl = GetComponent<AudioSource>();
        routePosition = new int[100];
        jailFlag = new int[100];
       // for(int i=0;i<100;i++)            //change initial Pos
       //     routePosition[i] += 43;
        
        firstPlayerFlag = true;
        choicePlayer = false;
        lastShortcut = false;
    }

    public void clickSoundtrack()
    {
        if (soundtrackFlag)
        {
            GameObject.Find("ButtonMuteSoundtrack").GetComponent<Image>().sprite = soundtrackMute2;
            GameObject.Find("Music").GetComponent<AudioSource>().Pause();
            soundtrackFlag = false;
            return;
        }
        if (!soundtrackFlag)
        {
            GameObject.Find("ButtonMuteSoundtrack").GetComponent<Image>().sprite = soundtrackMute1;
            GameObject.Find("Music").GetComponent<AudioSource>().Play();
            soundtrackFlag = true;
        }
    }

    public void clickEffects()
    {
        if (effectsFlag)
        {
            GameObject.Find("ButtonMuteEffects").GetComponent<Image>().sprite = effectsMute2;
            effectsFlag = false;
            return;
        }
        if (!effectsFlag)
        {
            GameObject.Find("ButtonMuteEffects").GetComponent<Image>().sprite = effectsMute1;
            effectsFlag = true;
        }
    }

    public void RollPhase()
    {
        GameObject.Find("CameraDice").GetComponent<Camera>().enabled = true; 
        GameObject.Find("Main Camera").GetComponent<Camera>().enabled = false;
        if (effectsFlag)
            audioControl.PlayOneShot(soundDice);
        diceAux.throwDice();
        flagRoll = true;
        GameObject.Find("ButtonRoll").GetComponent<Image>().enabled = false;
        GameObject.Find("ButtonRoll").GetComponent<Button>().enabled = false;
        GameObject.Find("TextRoll").GetComponent<Text>().enabled = false;
    }

    public void DareFinished()
    {
        if (effectsFlag)
            audioControl.PlayOneShot(soundDare);
        GameObject.Find("ImageDice").GetComponent<Image>().enabled = true;
        GameObject.Find("TextDice").GetComponent<Text>().enabled = true;
        GameObject.Find("ButtonRoll").GetComponent<Image>().enabled = true;
        GameObject.Find("ButtonRoll").GetComponent<Button>().enabled = true;
        GameObject.Find("TextRoll").GetComponent<Text>().enabled = true;
        GameObject.Find("TextDice").GetComponent<Text>().text = "0";
        GameObject.Find("DareCanvas").GetComponent<Canvas>().enabled = false;
        if (routePosition[player] == 40 && steps == 0)
        {
            steps = 1;
            routePosition[player] = 32;
            lastShortcut = true;
            StartCoroutine(Move());
        }
        else{
            player++;
            if (player > currentRoute.totalPlayers())
                player = 1;
            currentRoute.ChangePlayer(player);
        }
    }

    public void FinishGame()
    {
        audioControl.Stop();
        SceneManager.LoadScene(0);
    }

    public void Ladder1choiceYes()
    {
        GameObject.Find("CanvasFirstLadder").GetComponent<Canvas>().enabled = false;
        routePosition[player] = 21;
        steps = auxMovement;
        StartCoroutine(Move());
        choicePlayer = false;
    }

    public void Ladder1choiceNo()
    {
        GameObject.Find("CanvasFirstLadder").GetComponent<Canvas>().enabled = false;
        steps = auxMovement;
        StartCoroutine(Move());
        choicePlayer = false;
    }

    public void Choice2Yes()
    {
        GameObject.Find("Canvas2Choice").GetComponent<Canvas>().enabled = false;
        GameObject.Find("DareButton").GetComponent<Image>().enabled = true;
        GameObject.Find("DareButton").GetComponent<Button>().enabled = true;

        GameObject.Find("ImageDice").GetComponent<Image>().enabled = true;
        GameObject.Find("TextDice").GetComponent<Text>().enabled = true;
        GameObject.Find("ButtonRoll").GetComponent<Image>().enabled = true;
        GameObject.Find("ButtonRoll").GetComponent<Button>().enabled = true;
        GameObject.Find("TextRoll").GetComponent<Text>().enabled = true;
        GameObject.Find("TextDice").GetComponent<Text>().text = "0";
        GameObject.Find("DareCanvas").GetComponent<Canvas>().enabled = false;
        player++;
        currentRoute.ChangePlayer(player);  
        choicePlayer = false;
    }

    public void Choice2No()
    {
        GameObject.Find("Canvas2Choice").GetComponent<Canvas>().enabled = false;
        GameObject.Find("DareButton").GetComponent<Image>().enabled = true;
        GameObject.Find("DareButton").GetComponent<Button>().enabled = true;
        routePosition[player] = -1;
        steps = 1;
        StartCoroutine(Move());
        choicePlayer = false;
    }

    public void Choice3Yes()
    {
        GameObject.Find("Canvas3Choice").GetComponent<Canvas>().enabled = false;
        GameObject.Find("DareButton").GetComponent<Image>().enabled = true;
        GameObject.Find("DareButton").GetComponent<Button>().enabled = true;

        GameObject.Find("ImageDice").GetComponent<Image>().enabled = true;
        GameObject.Find("TextDice").GetComponent<Text>().enabled = true;
        GameObject.Find("ButtonRoll").GetComponent<Image>().enabled = true;
        GameObject.Find("ButtonRoll").GetComponent<Button>().enabled = true;
        GameObject.Find("TextRoll").GetComponent<Text>().enabled = true;
        GameObject.Find("TextDice").GetComponent<Text>().text = "0";
        GameObject.Find("DareCanvas").GetComponent<Canvas>().enabled = false;
        jailFlag[player] = 0;
        choicePlayer = false;
    }

    public void Choice3No()
    {
        GameObject.Find("Canvas3Choice").GetComponent<Canvas>().enabled = false;
        GameObject.Find("DareButton").GetComponent<Image>().enabled = true;
        GameObject.Find("DareButton").GetComponent<Button>().enabled = true;

        GameObject.Find("ImageDice").GetComponent<Image>().enabled = true;
        GameObject.Find("TextDice").GetComponent<Text>().enabled = true;
        GameObject.Find("ButtonRoll").GetComponent<Image>().enabled = true;
        GameObject.Find("ButtonRoll").GetComponent<Button>().enabled = true;
        GameObject.Find("TextRoll").GetComponent<Text>().enabled = true;
        GameObject.Find("TextDice").GetComponent<Text>().text = "0";
        GameObject.Find("DareCanvas").GetComponent<Canvas>().enabled = false;
        jailFlag[player] = 0;
        player++;
        currentRoute.ChangePlayer(player);  
        choicePlayer = false;
    }

    void Update()
    {
        int countActivePlayer = 0;
        for (int i = 1; i <= currentRoute.totalPlayers(); i++)
        {
            //Debug.Log("aux: " + (routePosition[i] + 1) + " - " + currentRoute.childNodeList.Count);
            if (routePosition[i] + 1 != currentRoute.childNodeList.Count)
            {
                countActivePlayer++;
                lastPlayer = currentRoute.getNamePlayer(i);
            }
        }
        // Debug.Log("player " + player + " route- " + (routePosition[player] +1) + " steps-" + steps + " last");
        if (countActivePlayer < 2 && !firstPlayerFlag)
        {
            // GameObject.Find("DareCanvas").SetActive(false);
            //GameObject.Find("CanvasUI").SetActive(false);
            //GameObject.Find("ButtonHide").SetActive(false);
            GameObject.Find("CanvasFinishGame").GetComponent<Canvas>().enabled = true;
            GameObject.Find("Text1place").GetComponent<Text>().text = firstPlayer;
            GameObject.Find("TextLastplace").GetComponent<Text>().text = lastPlayer;
            return;
        }

        if (routePosition[player] + 1  == currentRoute.childNodeList.Count)
        {
            player++;
            if (player > currentRoute.totalPlayers())
                player = 1;
            currentRoute.ChangePlayer(player);
            GameObject.Find("DareCanvas").GetComponent<Canvas>().enabled = false;
            GameObject.Find("ImageDice").GetComponent<Image>().enabled = true;
            GameObject.Find("TextDice").GetComponent<Text>().enabled = true;
            GameObject.Find("ButtonRoll").GetComponent<Image>().enabled = true;
            GameObject.Find("ButtonRoll").GetComponent<Button>().enabled = true;
            GameObject.Find("TextRoll").GetComponent<Text>().enabled = true;
            GameObject.Find("TextDice").GetComponent<Text>().text = "0";
            return;
        }

        if (isMoving)
            GameObject.Find("DareCanvas").GetComponent<Canvas>().enabled = false;

        if (!isMoving && choicePlayer)
        {
            GameObject.Find("DareCanvas").GetComponent<Canvas>().enabled = false;
            //Debug.Log("player pos" + routePosition[player] + "passos" + steps);
            if (routePosition[player] == 4)
                GameObject.Find("CanvasFirstLadder").GetComponent<Canvas>().enabled = true;
        }

        if (jailFlag[player] == 1 && !isMoving)
        {
            choicePlayer = true;
            GameObject.Find("ImageDice").GetComponent<Image>().enabled = false;
            GameObject.Find("TextDice").GetComponent<Text>().enabled = false;
            GameObject.Find("Canvas3Choice").GetComponent<Canvas>().enabled = true;
            GameObject.Find("DareButton").GetComponent<Image>().enabled = false;
            GameObject.Find("DareButton").GetComponent<Button>().enabled = false;
            GameObject.Find("DareCanvas").GetComponent<Canvas>().enabled = true;
            return;
        }

        if (routePosition[player] == 8 && !isMoving)
        {
            choicePlayer = true;
            GameObject.Find("Canvas2Choice").GetComponent<Canvas>().enabled = true;
            GameObject.Find("DareButton").GetComponent<Image>().enabled = false;
            GameObject.Find("DareButton").GetComponent<Button>().enabled = false;
            GameObject.Find("DareCanvas").GetComponent<Canvas>().enabled = true;
            return;
        }

        //last square funcionality ignored 
        /*
        if (routePosition[player] == 45 && !isMoving)
        {
            choicePlayer = true;
            GameObject.Find("Canvas4Choice").GetComponent<Canvas>().enabled = true;
            GameObject.Find("DareButton").GetComponent<Image>().enabled = false;
            GameObject.Find("DareButton").GetComponent<Button>().enabled = false;
            GameObject.Find("DareCanvas").GetComponent<Canvas>().enabled = true;
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100))
                {
                    string ourObject = hit.transform.gameObject.name;
                    if (ourObject[0] == 'N')
                    {
                        string final = ourObject.Substring(4);
                        countMovePlayer++;
                    }
                    //int[] movePlayer;
                    //int countMovePlayer;
                }
            }
            if (countActivePlayer == 2)
            return; 
        }
        */

        if (DiceNumberTextScript.diceNumber != 0 && !isMoving && flagRoll)
        {
            GameObject.Find("Main Camera").GetComponent<Camera>().enabled = true;
            GameObject.Find("CameraDice").GetComponent<Camera>().enabled = false;
            steps = DiceNumberTextScript.diceNumber;
            flagRoll = false;
            //Debug.Log(routePosition[player] + "-" + steps + "-" + currentRoute.childNodeList.Count);

            if (routePosition[player] == 9 && steps == 5)
                routePosition[player] = 17;

            if (routePosition[player] + steps + 1 == currentRoute.childNodeList.Count)
            {
                StartCoroutine(Move());
                return;
            }

            if (routePosition[player] + steps < currentRoute.childNodeList.Count)
            {
                if (routePosition[player] < 5 && routePosition[player] + steps >= 5 && choicePlayer == false)
                {
                    choicePlayer = true;
                    auxMovement = routePosition[player] + steps - 4;
                    steps = 4 - routePosition[player];
                    StartCoroutine(Move());               
                }

                if (choicePlayer == false)
                {
                    GameObject.Find("CanvasFirstLadder").GetComponent<Canvas>().enabled = false;
                    StartCoroutine(Move());
                }
            }

            if (routePosition[player] + steps + 1 > currentRoute.childNodeList.Count)
            {
                player++;
                if (player > currentRoute.totalPlayers())
                    player = 1;
                currentRoute.ChangePlayer(player);
                GameObject.Find("ButtonRoll").GetComponent<Image>().enabled = true;
                GameObject.Find("ButtonRoll").GetComponent<Button>().enabled = true;
                GameObject.Find("TextRoll").GetComponent<Text>().enabled = true;
                GameObject.Find("ImageDice").GetComponent<Image>().enabled = true;
                GameObject.Find("TextDice").GetComponent<Text>().enabled = true;
                GameObject.Find("TextDice").GetComponent<Text>().text = "0";
                if(!choicePlayer)
                    GameObject.Find("DareCanvas").GetComponent<Canvas>().enabled = false;
            }
        }
    }

    IEnumerator Move()
    {
        if (isMoving)
        {
            yield break;
        }
        isMoving = true;

        if (lastShortcut)
        {
            isMoving = false;
            GameObject.Find("DareCanvas").GetComponent<Canvas>().enabled = true;
            GameObject.Find("DareButton").GetComponent<Image>().enabled = false;
            GameObject.Find("DareButton").GetComponent<Button>().enabled = false;
            GameObject.Find("PlayerDareName").GetComponent<Text>().text = currentRoute.getNamePlayer(player);
            GameObject.Find("ImageDare").GetComponent<Image>().sprite = Resources.Load<Sprite>("Dares/shortcut1");
            yield return new WaitForSeconds(2f);
            isMoving = true;
            GameObject.Find("DareButton").GetComponent<Image>().enabled = true;
            GameObject.Find("DareButton").GetComponent<Button>().enabled = true;
        }

        if (routePosition[player] != 35)
        {
            while (steps > 0)
            {
                Vector3 nextPos = currentRoute.childNodeList[routePosition[player] + 1].position;
                while (MoveToNextNode(nextPos))
                {
                    yield return null;
                }
                yield return new WaitForSeconds(0.1f);
                if (effectsFlag)
                    audioControl.PlayOneShot(soundMove);
                steps--;
                routePosition[player]++;
                if (routePosition[player] == 30 && steps == 0)
                {
                    isMoving = false;
                    GameObject.Find("DareCanvas").GetComponent<Canvas>().enabled = true;
                    GameObject.Find("DareButton").GetComponent<Image>().enabled = false;
                    GameObject.Find("DareButton").GetComponent<Button>().enabled = false;
                    GameObject.Find("PlayerDareName").GetComponent<Text>().text = currentRoute.getNamePlayer(player);
                    GameObject.Find("ImageDare").GetComponent<Image>().sprite = Resources.Load<Sprite>("Dares/Dare" + routePosition[player]);
                    yield return new WaitForSeconds(2f);
                    steps = 3;
                    isMoving = true;
                    GameObject.Find("DareButton").GetComponent<Image>().enabled = true;
                    GameObject.Find("DareButton").GetComponent<Button>().enabled = true;
                }

            }
        }

        if (routePosition[player] == 35 && steps != 0)
        {
            isMoving = false;
            GameObject.Find("DareCanvas").GetComponent<Canvas>().enabled = true;
            GameObject.Find("DareButton").GetComponent<Image>().enabled = false;
            GameObject.Find("DareButton").GetComponent<Button>().enabled = false;
            GameObject.Find("PlayerDareName").GetComponent<Text>().text = currentRoute.getNamePlayer(player);
            GameObject.Find("ImageDare").GetComponent<Image>().sprite = Resources.Load<Sprite>("Dares/Dare" + routePosition[player]);
            yield return new WaitForSeconds(2f);
            isMoving = true;
            GameObject.Find("DareButton").GetComponent<Image>().enabled = true;
            GameObject.Find("DareButton").GetComponent<Button>().enabled = true;
            while (steps > 0)
            {
                Vector3 nextPos = currentRoute.childNodeList[routePosition[player] - 1].position;
                while (MoveToNextNode(nextPos))
                {
                    yield return null;
                }
                yield return new WaitForSeconds(0.1f);
                if (effectsFlag)
                    audioControl.PlayOneShot(soundMove);
                steps--;
                routePosition[player]--;
            }
        }

        if (routePosition[player] == 15)                 
            jailFlag[player] = 1;

        if (routePosition[player] == 31 || routePosition[player] == 44)
        {
            isMoving = false;
            GameObject.Find("DareCanvas").GetComponent<Canvas>().enabled = true;
            GameObject.Find("DareButton").GetComponent<Image>().enabled = false;
            GameObject.Find("DareButton").GetComponent<Button>().enabled = false;
            GameObject.Find("PlayerDareName").GetComponent<Text>().text = currentRoute.getNamePlayer(player);
            GameObject.Find("ImageDare").GetComponent<Image>().sprite = Resources.Load<Sprite>("Dares/Dare" + routePosition[player]);
            yield return new WaitForSeconds(2f);          
            steps = 4;
            if (routePosition[player] == 44)
                steps--;
            isMoving = true;
            GameObject.Find("DareButton").GetComponent<Image>().enabled = true;
            GameObject.Find("DareButton").GetComponent<Button>().enabled = true;
            while (steps > 0)
            {
                Vector3 nextPos = currentRoute.childNodeList[routePosition[player] - 1].position;
                while (MoveToNextNode(nextPos))
                {
                    yield return null;
                }
                yield return new WaitForSeconds(0.1f);
                if (effectsFlag)
                    audioControl.PlayOneShot(soundMove);
                steps--;
                routePosition[player]--;
            }
        }

        if(routePosition[player] == 46)
        {
            if (firstPlayerFlag)
                firstPlayer = currentRoute.getNamePlayer(player);
            firstPlayerFlag = false;
        }

        if (routePosition[player] != 0 && routePosition[player] != 46)
        {
            GameObject.Find("ImageDice").GetComponent<Image>().enabled = false;
            GameObject.Find("TextDice").GetComponent<Text>().enabled = false;
            GameObject.Find("DareCanvas").GetComponent<Canvas>().enabled = true;
            GameObject.Find("PlayerDareName").GetComponent<Text>().text = currentRoute.getNamePlayer(player);
            GameObject.Find("ImageDare").GetComponent<Image>().sprite = Resources.Load<Sprite>("Dares/Dare" + routePosition[player]);
        }
        if (routePosition[player] == 0 || lastShortcut)
        {
            DareFinished();
            lastShortcut = false;
        }

        isMoving = false;
        //so we can do movement and stay on top of board
        GameObject.Find("PlayerStone" + player).transform.position += new Vector3(0, 0.1f, 0);
    }

    bool MoveToNextNode(Vector3 goal)
    {
        GameObject.Find("PlayerStone" + player).transform.position = Vector3.MoveTowards(GameObject.Find("PlayerStone" + player).transform.position + new Vector3(0,0.04f,0), goal, 2f * Time.deltaTime);
        return goal != GameObject.Find("PlayerStone" + player).transform.position;
    }
}
