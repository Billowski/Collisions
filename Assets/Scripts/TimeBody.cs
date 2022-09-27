using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TimeBody : MonoBehaviour
{
    private bool isRewinding = false;
    List<PointInTime> pointsInTime;
    Rigidbody rb;

    Vector3 vel;
    Vector3 angVel;

    // Start is called before the first frame update
    void Start()
    {
        pointsInTime = new List<PointInTime>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) StartRewind();
        if (Input.GetKeyUp(KeyCode.Return)) StopRewind();
    }

    private void FixedUpdate()
    {
        if (isRewinding)
            Rewind();
        else
            Record();
    }

    void Rewind()
    {
        if(pointsInTime.Count > 0)
        {
            PointInTime pointInTime = pointsInTime[0];
            rb.position = pointInTime.position;
            rb.rotation = pointInTime.rotation;
            vel = pointInTime.velocity;
            angVel = pointInTime.angularVelocity;
            pointsInTime.RemoveAt(0);
        } else
        {
            StopRewind();
        }
    }

    void Record()
    {
        if (pointsInTime.Count > Mathf.Round(5f / Time.fixedDeltaTime))
        {
            pointsInTime.RemoveAt(pointsInTime.Count - 1);
        }
        pointsInTime.Insert(0, new PointInTime(rb.position, rb.rotation, rb.velocity, rb.angularVelocity));
    }

    public void StartRewind()
    {
        isRewinding = true;
        rb.isKinematic = true;
    }

    public void StopRewind()
    {
        rb.velocity = vel;
        rb.angularVelocity = angVel;
        isRewinding = false;
        rb.isKinematic = false;
    }
}
