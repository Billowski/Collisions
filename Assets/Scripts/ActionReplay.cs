using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionReplay : MonoBehaviour
{
    private bool isInReplayMode;
    private float currentReplayIndex;
    private float indexChangeRate;
    private Rigidbody rb;
    private List<ActionReplayRecord> actionReplayRecords = new List<ActionReplayRecord>();
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            isInReplayMode = !isInReplayMode;

            if (isInReplayMode)
            {
                SetTransform(0);
                rb.isKinematic = true;
            } 
            else
            {
                SetTransform(actionReplayRecords.Count - 1);
                rb.isKinematic = false;
            }
        }

        indexChangeRate = 0;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            indexChangeRate = 1;
        }
        
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            indexChangeRate = -1;
        }
        
        if (Input.GetKey(KeyCode.RightShift))
        {
            indexChangeRate *= 0.5f;
        }
    }

    private void FixedUpdate()
    {
        if (!isInReplayMode)
        {
            actionReplayRecords.Add(new ActionReplayRecord { position = transform.position, rotation = transform.rotation });
        }
        else
        {
            float nextIndex = currentReplayIndex + indexChangeRate;

            if (nextIndex < actionReplayRecords.Count && nextIndex >= 0)
            {
                SetTransform(nextIndex);
            }
        }
    }

    private void SetTransform(float index)
    {
        currentReplayIndex = index;

        ActionReplayRecord actionReplayRecord = actionReplayRecords[(int)index];

        transform.position = actionReplayRecord.position;
        transform.rotation = actionReplayRecord.rotation;
    }
}
