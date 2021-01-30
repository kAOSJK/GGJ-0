﻿using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    /* MOVE VARs */
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveLimiter; /* diagonal limiter */

    /* DASH VARs */
    [SerializeField] private bool allowDash;
    [ConditionalHide("allowDash", true)]
    [SerializeField] private float dashSpeed;
    [ConditionalHide("allowDash", true)]
    [SerializeField] private float dashDuration;
    [SerializeField] private bool cameraShake;
    [ConditionalHide("cameraShake", true)]
    [SerializeField] private GameObject camera;
    [ConditionalHide("cameraShake", true)]
    [SerializeField] private float shakeDuration;
    [ConditionalHide("cameraShake", true)]
    [SerializeField] private float shakeMagnitude;

    /* SNIFF VARs */
    [SerializeField] private bool allowSniff;
    [ConditionalHide("allowSniff", true)]
    [SerializeField] private float sniffDuration;

    /* ALLUMETTE VARs */
    [SerializeField] private bool allowAllumette;
    [ConditionalHide("allowAllumette", true)]
    [SerializeField] private int allumetteNumber;
    [ConditionalHide("allowAllumette", true)]
    [SerializeField] private float allumetteDuration;
    [ConditionalHide("allowAllumette", true)]
    [SerializeField] private float[] allumetteBehaviour;
    /* ALLUMETTE UI VARs */
    [SerializeField] private bool allowAllumetteUI;
    [ConditionalHide("allowAllumetteUI", true)]
    [SerializeField] private Text allumetteUI;
    [ConditionalHide("allowAllumetteUI", true)]
    [SerializeField] private int allumetteUITextSize;
    [ConditionalHide("allowAllumetteUI", true)]
    [SerializeField] private Color allumetteUITextColor;
    [ConditionalHide("allowAllumetteUI", true)]
    [SerializeField] private bool isAllumetteUITextBold;

    /* PRIVATE VARs */
    private Rigidbody2D body; /* rigidbody */
    private Animator animator; /* animator */
    private bool isFacingRight = false; /* direction */
    private bool canMove = true; /* able movement */
    private bool hasAnAllumette = false; /* has an allumette */
    private float horizontal; /* horizontal input */
    private float vertical; /* vertical input */

    /* PRIVATE HOLDING VARs */
    private GameObject allumette;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        /* UI Allumette */
        if (allowAllumetteUI)
        {
            allumetteUI.text = allumetteNumber.ToString();
            allumetteUI.fontSize = allumetteUITextSize;
            allumetteUI.color = allumetteUITextColor;
            if (isAllumetteUITextBold) allumetteUI.fontStyle = UnityEngine.FontStyle.Bold;
        }
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal"); /* -1 is left, 0 is null, 1 is right */
        vertical = Input.GetAxisRaw("Vertical"); /* -1 is down, 0 is null, 1 is right */
        
        if (allowDash && Input.GetKeyDown("space"))
        {
            StartCoroutine(DashBoost());
        }

        if (allowAllumette && !hasAnAllumette && allumetteNumber > 0 && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(CreateAllumette());
        }
    }

    private void FixedUpdate()
    {
        if (horizontal != 0 && vertical != 0) /* check for diagonal movement */
        {
            /* limit movement speed diagonally */
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        }

        if (canMove)
            body.velocity = new Vector2(horizontal * moveSpeed, vertical * moveSpeed);

        /* Flip */
        if (horizontal > 0 && !isFacingRight)
        {
            //Si le mouvement est d'aller à droite et que le joueur regarde à gauche
            Flip();
        }

        if (horizontal < 0 && isFacingRight)
        {
            //Si le mouvement est d'aller à gauche et que le joueur regarde à droite
            Flip();
        }

        /* Animation */
        float playerSpeed = body.velocity.x > 0 ? body.velocity.x : body.velocity.x * -1; /* X SPEED */
        playerSpeed += body.velocity.y > 0 ? body.velocity.y : body.velocity.y * -1; /* Y SPEED */
        animator.SetFloat("Speed", playerSpeed);
    }

    private IEnumerator DashBoost()
    {
        Debug.Log("Started Dash at : " + Time.time);

        /* Animation */
        string boolToChange = horizontal != 0 || vertical != 0 ? "isDashing" : "isSniffing";
        animator.SetBool(boolToChange, true);

        if (boolToChange == "isDashing")
        {
            moveSpeed += dashSpeed;

            if (cameraShake)
                StartCoroutine(camera.GetComponent<CameraShake>().Shake(shakeDuration, shakeMagnitude));
        }
        else if (boolToChange == "isSniffing")
        {
            canMove = false; /* disable movement */
        }

        if (boolToChange == "isDashing")
            yield return new WaitForSeconds(dashDuration);
        else
            yield return new WaitForSeconds(sniffDuration);

        if (boolToChange == "isDashing")
            moveSpeed -= dashSpeed; /* disable dash boost */
        else if (boolToChange == "isSniffing")
            canMove = true; /* able movement */

        /* Animation */
        animator.SetBool(boolToChange, false);

        Debug.Log("Finished Dash at : " + Time.time);
    }

    private IEnumerator CreateAllumette()
    {
        Debug.Log("Started Dash at : " + Time.time);

        /* Create Allumette GameObject */
        allumette = Instantiate(Resources.Load("Allumette") as GameObject, transform.position, Quaternion.identity);

        allumette.transform.parent = transform;
        Light2D lightScript = allumette.GetComponent<Light2D>();

        /* Decrement Allumette Counter */
        allumetteNumber--;

        /* Has an allumette */
        hasAnAllumette = true;

        /* UI Management */
        if (allowAllumetteUI)
            allumetteUI.text = allumetteNumber.ToString();

        /* Light Management */
        float elapsed = 0.0f;

        while (elapsed < allumetteDuration)
        {
            if (elapsed < allumetteDuration / 4)
                lightScript.intensity = allumetteBehaviour[0];
            else if (elapsed <= (allumetteDuration / 4 * 2) && elapsed >= allumetteDuration / 4)
                lightScript.intensity = allumetteBehaviour[1];
            else if (elapsed <= (allumetteDuration / 4 * 3) && elapsed >= allumetteDuration / 4)
                lightScript.intensity = allumetteBehaviour[2];
            else
                lightScript.intensity = allumetteBehaviour[3];

            elapsed += Time.deltaTime;

            yield return null;
        }

        //yield return new WaitForSeconds(allumetteDuration);

        /* Destroy Allumette */
        Destroy(allumette);

        /* Doesn't Has an allumette */
        hasAnAllumette = false;

        Debug.Log("Finished Dash at : " + Time.time);
    }

    void Flip()
    {
        //Change le boolean qui permet de savoir si il regarde à droite ou non
        isFacingRight = !isFacingRight;
        //Change la direction du joueur
        transform.Rotate(0f, 180f, 0f);
        //Active la trainée de pas
    }
}
