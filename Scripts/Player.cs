using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    GameObject body;
    [SerializeField]
    GameObject lift;
    [SerializeField]
    GameObject thrust;
    [SerializeField]
    GameObject tank;
    [SerializeField]
    GameObject burst;
    [SerializeField]
    GameObject flashlightHoldPoint;
    [SerializeField]
    GameObject flareLaunchPoint;
    [SerializeField]
    float flareLaunchForce;
    [SerializeField]
    GameObject floodlightLaunchPoint;
    [SerializeField]
    float floodlightLaunchForce;
    [SerializeField]
    GameObject flashlightFindMessage;
    [SerializeField]
    GameObject flareFindMessage;
    [SerializeField]
    GameObject floodlightFindMessage;
    [SerializeField]
    GameObject outOfFuelMessage;

    bool hasFlashlight = false;
    bool hasFlare = false;
    bool hasFloodlight = false;
    bool grounded = false;
    bool refuel = false;
    float maxFuelSeconds = 60.0f;
    float fuelSeconds = 0.0f;
    float maxLiftSeconds = 2.0f;
    float liftSeconds = 0.0f;
    Vector2 movement = Vector2.zero;
    float focusDelta = 120.0f;
    float maxSpeed = 10.0f;
    Vector3 liftForce = new Vector3(0.0f, 13.0f, 0.0f);
    Vector3 flyingThrustForce = new Vector3(10.0f, 0.0f, 0.0f);
    Vector3 groundedThrustForce = new Vector3(8.5f, 0.0f, 0.0f);
    GameObject flashlight;
    GameObject flare;
    GameObject floodlight;
    bool won = false;
    bool rotatedFlashlight = false;
    bool toggledFlashlight = false;

    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        fuelSeconds = maxFuelSeconds;
        liftSeconds = maxLiftSeconds;
    }

    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        UpdateFlashlight();
        UpdateFlare();
        UpdateFloodlight();
    }

    void FixedUpdate()
    {
        UpdateVertical();
        UpdateHorizontal();
        grounded = false;
        if (!won && fuelSeconds <= 0.0f && liftSeconds <= 0.0f && !outOfFuelMessage.activeSelf)
        {
            if (flashlightFindMessage != null)
            {
                flashlightFindMessage.SetActive(false);
            }
            if (flareFindMessage != null)
            {
                flareFindMessage.SetActive(false);
            }
            if (floodlightFindMessage != null)
            {
                floodlightFindMessage.SetActive(false);
            }
            outOfFuelMessage.SetActive(true);
        }
    }

    void UpdateVertical()
    {
        if (movement.y <= 0.0f || refuel)
        {
            if (fuelSeconds > 0.0f)
            {
                refuel = movement.y > 0.0f;
                if (lift.activeSelf)
                {
                    lift.SetActive(false);
                }
                if (liftSeconds < maxLiftSeconds)
                {
                    float delta = Time.deltaTime / 3;
                    if (liftSeconds + delta > maxLiftSeconds)
                    {
                        delta = maxLiftSeconds - liftSeconds;
                    }
                    if (delta > fuelSeconds)
                    {
                        delta = fuelSeconds;
                    }
                    liftSeconds += delta;
                    fuelSeconds -= delta;
                    tank.SendMessage("SetPercentage", fuelSeconds / maxFuelSeconds);
                    burst.SendMessage("SetPercentage", liftSeconds / maxLiftSeconds);
                }
            }
        }
        else if (liftSeconds > 0.0f)
        {
            liftSeconds -= Time.deltaTime;
            if (liftSeconds <= 0.0f)
            {
                liftSeconds = 0.0f;
                refuel = true;
            }
            rb.AddForce(liftForce);
            burst.SendMessage("SetPercentage", liftSeconds / maxLiftSeconds);
            if (!lift.activeSelf)
            {
                lift.SetActive(true);
            }
        }
    }

    void UpdateHorizontal()
    {
        if (fuelSeconds > 0.0f)
        {
            if (movement.x != 0.0f)
            {
                Vector3 force = grounded ? groundedThrustForce : flyingThrustForce;
                Vector3 scale = body.transform.localScale;
                if (movement.x < 0.0f)
                {
                    force.x *= -1;
                    scale.x = -1;
                }
                else
                {
                    scale.x = 1.0f;
                }
                if (Mathf.Abs(rb.velocity.x) >= maxSpeed)
                {
                    if (
                        (rb.velocity.x < 0 && force.x < 0)
                        || (rb.velocity.x > 0 && force.x > 0)
                    )
                    {
                        force.x = 0.0f;
                    }
                }
                body.transform.localScale = scale;
                rb.AddForce(force);
                fuelSeconds -= Time.deltaTime;
                if (fuelSeconds < 0.0f)
                {
                    fuelSeconds = 0.0f;
                }
                tank.SendMessage("SetPercentage", fuelSeconds / maxFuelSeconds);
                if (!thrust.activeSelf)
                {
                    thrust.SetActive(true);
                }
            }
            else if (thrust.activeSelf)
            {
                thrust.SetActive(false);
            }
        }
    }

    void UpdateFlashlight()
    {
        if (hasFlashlight)
        {
            bool toggle = Input.GetButtonDown("Flashlight");
            float delta = Input.GetAxis("Focus") * focusDelta * Time.deltaTime;
            float newX = delta + flashlight.transform.localEulerAngles.x;
            if (newX < 300.0f && newX > 60.0f)
            {
                delta = 0.0f;
            }
            flashlight.transform.Rotate(delta, 0.0f, 0.0f);
            if (toggle)
            {
                flashlight.SetActive(!flashlight.activeSelf);
            }
            if (flashlightFindMessage != null)
            {
                if (delta != 0.0f)
                {
                    rotatedFlashlight = true;
                }
                if (toggle)
                {
                    toggledFlashlight = true;
                }
                if (rotatedFlashlight && toggledFlashlight)
                {
                    Destroy(flashlightFindMessage);
                }
            }
        }
    }

    void UpdateFlare()
    {
        if (hasFlare && Input.GetButtonDown("Flare"))
        {
            flare.SetActive(!flare.activeSelf);
            if (flare.activeSelf)
            {
                flare.transform.position = flareLaunchPoint.transform.position;
                flare.SendMessage("Launch", rb.velocity + (flashlight.transform.forward * flareLaunchForce));
            }
            if (flareFindMessage != null)
            {
                Destroy(flareFindMessage);
            }
        }
    }

    void UpdateFloodlight()
    {
        if (hasFloodlight && Input.GetButtonDown("Floodlight"))
        {
            floodlight.SetActive(!floodlight.activeSelf);
            if (floodlight.activeSelf)
            {
                floodlight.transform.position = floodlightLaunchPoint.transform.position;
                floodlight.SendMessage("Launch", rb.velocity + (flashlight.transform.forward * floodlightLaunchForce));
            }
            if (floodlightFindMessage != null)
            {
                Destroy(floodlightFindMessage);
            }
        }
    }

    void OnGround()
    {
        grounded = true;
    }

    void OnGameStart()
    {
        enabled = true;
    }

    void GetFlashlight(GameObject item)
    {
        hasFlashlight = true;
        flashlight = item;
        flashlight.transform.SetParent(flashlightHoldPoint.transform.parent);
        flashlight.transform.localPosition = flashlightHoldPoint.transform.localPosition;
        flashlight.transform.localEulerAngles = flashlightHoldPoint.transform.localEulerAngles;
        Destroy(flashlightHoldPoint);
        flashlightFindMessage.SetActive(true);
    }

    void GetFlare(GameObject item)
    {
        hasFlare = true;
        flare = item;
        flareFindMessage.SetActive(true);
    }

    void GetFloodlight(GameObject item)
    {
        hasFloodlight = true;
        floodlight = item;
        floodlightFindMessage.SetActive(true);
    }

    void Won()
    {
        won = true;
        outOfFuelMessage.SetActive(false);
    }
}
