using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{

    Animator playerAnimator;
    Rigidbody playerRB;
    int collectedCount;
    bool started = false;
    bool isGrounded = true;
    public bool isBarrierClose = false;
    public ParticleSystem finishParticle;
    public float moveSpeed;
    public float jumpForce;
    public TextMeshProUGUI ScoreText;
    public GameObject WinPanel;
    public GameObject LostPanel;

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerRB = GetComponent<Rigidbody>();
        finishParticle.Stop();
    }


    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            started = true;
        }
        if (started)
        {
            transform.Translate(0, 0, moveSpeed * Time.deltaTime); //moving forward
            playerAnimator.SetFloat("Blend", moveSpeed, 0.3f, Time.deltaTime); //for player running animation
        }
        if (isBarrierClose && isGrounded)
        {
            isGrounded = false; //prevent multiple jumping
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Collectable":
                TriggerWithCollectable(other.gameObject);
                break;
            case "EnemyTurtle":
                TriggerWithEnemyTurtle();
                break;
            case "EnemySlime":
                TriggerWithEnemySlime();
                break;
            case "FinishLine":
                CheckScore();
                break;
        }
    }

    void TriggerWithCollectable(GameObject other)
    {
        Transform collectPoint = transform.GetChild(0);
        other.transform.position = collectPoint.position + new Vector3(0, 0.5f, 0) * collectedCount;
        other.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        other.transform.SetParent(collectPoint);
        collectedCount = collectedCount + 1;
    }

    void TriggerWithEnemyTurtle()
    {
        if (collectedCount > 0)
        {
            Destroy(transform.GetChild(0).GetChild(collectedCount - 1).gameObject, 0f);
            collectedCount = collectedCount - 1;
        } else
        {
            CheckScore();
        }
    }

    void TriggerWithEnemySlime()
    {
        if (collectedCount > 0)
        {
            for (int index = 0; index < collectedCount; index++)
            {
                Destroy(transform.GetChild(0).GetChild(index).gameObject, 0f);
            }
            collectedCount = 0;
        } else
        {
            CheckScore();
        }
    }

    void CheckScore()
    {
        if (collectedCount <= 0)
        {
            ShowLostPanel();
        }
        else
        {
            ShowWinPanel();
        }
    }

    void ShowLostPanel()
    {
        LostPanel.SetActive(true);
        Time.timeScale = 0;
    }

    void ShowWinPanel()
    {
        started = false;
        playerAnimator.SetFloat("Blend", 0, 0, 0);
        finishParticle.Play();
        ScoreText.text = $"You have collected {collectedCount} items";
        WinPanel.SetActive(true);
        StartCoroutine(StopGame());
    }

    IEnumerator StopGame()
    {
        yield return new WaitForSeconds(5f);
        finishParticle.Stop();
        yield return new WaitForSeconds(2f);
        Time.timeScale = 0;
    }

}
