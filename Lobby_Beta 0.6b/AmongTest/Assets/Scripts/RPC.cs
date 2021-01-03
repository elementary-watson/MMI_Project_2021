using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;


public class RPC : MonoBehaviour
{
    PhotonView photonView;
    public Text updateText;
    int counter = 0;


    // Start is called before the first frame update
    void Start()
    {
        photonView = gameObject.GetComponent<PhotonView>();
        updateText.text = "" + counter;
        print(counter);
    }


    

    public void rufe_RPC_auf()
    {
        photonView.RPC("refresh", RpcTarget.All);
    }


    [PunRPC]
    public void refresh()
    {
        counter += 1;
        updateText.text = "" + counter;
        print(counter);
    }
}
