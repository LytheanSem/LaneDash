using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetect : MonoBehaviour
{
  [SerializeField] GameObject thePlayer;
  [SerializeField] GameObject playerAnim;
  [SerializeField] GameObject fadeOut;


    void OnTriggerEnter (Collider other)
    {
      StartCoroutine(CollisionEnd());
    }

    IEnumerator CollisionEnd()
    {
      thePlayer.GetComponent<PlayerMovement>().enabled = false;
      playerAnim.GetComponent<Animator>().Play("Stumble Backwards");
      yield return new WaitForSeconds(0);
      fadeOut.SetActive(true);
    }
}
