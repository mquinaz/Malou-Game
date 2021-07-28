using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Route : MonoBehaviour
{
    Menu auxScript;
    List<Menu.Player> lisPlayer;
    Transform[] childObjects;
    public List<Transform> childNodeList = new List<Transform>();
    GameObject[] playerObjects;
    float initialXpos = -3.45f;
    float finalXpos = -3.13f;
    float initialZpos = -3.25f;
    float finalZpos = -2.25f;
    float initialYpos = 0;
    int turn = 1;

    public int totalPlayers()
    {
        return lisPlayer.Count;
    }

    public string getNamePlayer(int player)
    {
        return lisPlayer[player-1].name;
    }

    void Start()
    {
        auxScript = GameObject.Find("TransitionSceneHelper").GetComponent<Menu>();
        lisPlayer = auxScript.lisPlayer;
        for (int i = 0; i < lisPlayer.Count; i++)
        {
            Menu.Player aux = new Menu.Player();
            aux.nr = i + 1;
            aux.name = lisPlayer[i].name;
            lisPlayer[i] = aux;
            if (GameObject.Find("Player" + lisPlayer[i].nr) != null)
            {
                GameObject.Find("Player" + lisPlayer[i].nr).transform.GetChild(0).gameObject.GetComponent<Text>().text = lisPlayer[i].name;
            }
            if (GameObject.Find("Player" + lisPlayer[i].nr) == null)
            {
                GameObject copy = GameObject.Find("Player" + (lisPlayer[i].nr-1));
                GameObject instance = GameObject.Instantiate(copy) as GameObject;
                instance.transform.SetParent(GameObject.Find("Content List").transform, false);
                instance.name = "Player" + lisPlayer[i].nr;
                instance.transform.GetChild(0).gameObject.GetComponent<Text>().text = lisPlayer[i].name;
            }
        }
        Destroy(GameObject.Find("TransitionSceneHelper"));
        /*
        for (int i = 0; i < lisPlayer.Count; i++)
        {
            Debug.Log(lisPlayer[i].nr);
            Debug.Log(lisPlayer[i].name);
        }
         * */
        FillNodes();
        playerObjects = new GameObject[lisPlayer.Count];
        for (int i = 0; i < lisPlayer.Count; i++)
        {
            GameObject copy1 = GameObject.Find("PlayerExample");
            playerObjects[i] = GameObject.Instantiate(copy1) as GameObject;
            float posPlayerx = Random.Range( initialXpos, finalXpos);
            float posPlayerz = Random.Range( initialZpos, finalZpos);
            playerObjects[i].transform.position = new Vector3(posPlayerx, initialYpos, posPlayerz);
            playerObjects[i].transform.localScale = new Vector3(0.1f,0.1f,0.1f);
            playerObjects[i].transform.SetParent(GameObject.Find("Stone").transform,false);
            playerObjects[i].name = "PlayerStone" + (i+1);

            playerObjects[i].transform.GetChild(0).gameObject.GetComponent<TextMesh>().text = lisPlayer[i].name;
            playerObjects[i].transform.GetChild(0).gameObject.name = "Name" + (i + 1);
            Vector3 auxNamePos = new Vector3(GameObject.Find("PlayerStone" + (i + 1)).transform.position.x, 1f, GameObject.Find("PlayerStone" + (i + 1)).transform.position.z);
            playerObjects[i].transform.GetChild(0).gameObject.transform.position = /*auxNamePos;*/ GameObject.Find("PlayerStone" + (i + 1)).transform.position;
        }
        GameObject.Find("Player" + turn).GetComponent<Image>().color = new Color32(255, 78, 140, 255);
    }

    public void ChangePlayer(int number)
    {
        if (number == 1 || number > lisPlayer.Count)
        {
            number = 1;
            GameObject.Find("Player" + number).GetComponent<Image>().color = new Color32(255, 78, 140, 255);
            GameObject.Find("Player" + lisPlayer.Count).GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            return;
        }
        GameObject.Find("Player" + number).GetComponent<Image>().color = new Color32(255, 78, 140, 255);
        GameObject.Find("Player" + (number - 1)).GetComponent<Image>().color = new Color32(255, 255, 255, 255);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        FillNodes();

        for (int i = 0; i < childNodeList.Count; i++)
        {
            Vector3 currentPos = childNodeList[i].position;
            if (i > 0)
            {
                Vector3 prevPos = childNodeList[i - 1].position;
                Gizmos.DrawLine(prevPos, currentPos);
            }
        }
    }

    void FillNodes()
    {
        childNodeList.Clear();
        childObjects = GetComponentsInChildren<Transform>();
        foreach (Transform child in childObjects)
        {
            if (child != this.transform)
            {
                childNodeList.Add(child);
            }
        }
    }
}
