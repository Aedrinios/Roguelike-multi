using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class portalManager : MonoBehaviour {

    public bool hasSpawn = false;

    private Animator animator;
    private ArrayList players = new ArrayList();
	private AudioSource audioSource;
    
    public GameObject creditPrefab;

    // Use this for initialization
    void Start () {
		animator = GetComponent<Animator>();
		audioSource = GetComponent<AudioSource> ();
    }
	
	// Update is called once per frame
	void Update () {

        if (hasSpawn == true)
        {
			animator.SetBool("hasSpawn", true);
			audioSource.Play();
        }
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            players.Add(other.gameObject);
            other.gameObject.SetActive(false);

            if (players.Count == GameManager.instance.players.Count)
            {
                Character.comptCouleur = 0;
                // Afficher le crédit 
                GameObject panelCredit = Instantiate(creditPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                panelCredit.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
                
            }
        }
    }
}
