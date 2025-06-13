using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    private Rigidbody2D rb;
    private JumpPlayer jumpScript;
    private DuckPlayer duckScript;


    [SerializeField] float maxMoveSpeed = 10f; // Velocidad Máxima Horizontal
    [SerializeField] float hAcc = 2f; // Aceleración Horizontal
    [SerializeField] float hDeacc = 0.2f; // Desaceleración Horizontal
    [SerializeField] private float maxFallSpeed = 40; // Velocidad máxima de caída
    [SerializeField] private float jumpStrenght = 20; // Fuerza de Salto

    // TRUE = Right || LEFT = Left
    private bool directionLook = true; // Dirección en la que el jugador está mirando
    private float Horizontal = 0; // Velocidad Actual
    public bool duck;
    public bool jump;

    void Start()
    {
        jumpScript = GetComponent<JumpPlayer>();
        duckScript = GetComponent<DuckPlayer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Acceleration();

        // bool jump = Input.GetButtonDown("Jump");
        duck = Input.GetKey(KeyCode.DownArrow);
        jump = (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow)) && duck == false && !duckScript.GetCanStand();
            
        if (jump) { jumpScript.Jump(jumpStrenght); }
        if (duck) { duckScript.Duck(0.95f, -0.05f, false); }
        else
        {
            if (!duckScript.GetCanStand()) { duckScript.Duck(1.9f, 0, true); }
        }

        Move(Horizontal);
    }

    // Esta función maneja la aceleración del jugador
    private void Acceleration()
    {
        float direction = Input.GetAxisRaw("Horizontal");
        if (direction > 0)
        {
            directionLook = true;
            if (Horizontal < maxMoveSpeed) { Horizontal += hAcc; }
            else { Horizontal = maxMoveSpeed; }
        }
        else if (direction < 0)
        {
            directionLook = false;
            if (Horizontal > -maxMoveSpeed) { Horizontal -= hAcc; }
            else { Horizontal = -maxMoveSpeed; }
        }
        else
        {
            if (Horizontal > 0.5) { Horizontal -= hDeacc; }
            else if (Horizontal < -0.5) { Horizontal += hDeacc; }
            else { Horizontal = 0; }
        }
    }

    // Esta función aplica la velocidad actual al RigidBody
    private void Move(float hInput)
    {
        Vector2 xVel = rb.velocity;
        xVel.x = hInput;
        rb.velocity = xVel;
    }

    // Esta función sirve para aplicar una velocidad horizontal instantánea, principalmente usado en los WallJumps
    public void HorizontalBoost(float boost) => Horizontal = boost;

    // Getters
    public bool GetDirectionLook() => directionLook;   
    public float GetMaxFallSpeed() => maxFallSpeed;
    
}
