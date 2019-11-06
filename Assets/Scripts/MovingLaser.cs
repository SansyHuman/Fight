using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingLaser : MonoBehaviour
{
    public enum MoveMode
    {
        Horizontal, Vertical
    }

    [SerializeField] private MoveMode moveMode;
    [SerializeField] private float min;
    [SerializeField] private float max;
    [SerializeField] private float velocity;
    [SerializeField] private bool moveBackwardInitially;

    private bool backward = false;

    private Transform tr;

    private void Awake()
    {
        tr = GetComponent<Transform>();
        Vector3 pos = tr.position;

        switch (moveMode)
        {
            case MoveMode.Horizontal:
                if (pos.x < min)
                    pos.x = min;
                if (pos.x > max)
                    pos.x = max;
                break;
            case MoveMode.Vertical:
                if (pos.y < min)
                    pos.y = min;
                if (pos.y > max)
                    pos.y = max;
                break;
        }

        tr.position = pos;
        backward = moveBackwardInitially;
    }

    private void FixedUpdate()
    {
        Vector3 pos = tr.position;
        switch (moveMode)
        {
            case MoveMode.Horizontal:
                pos.x += backward ? -velocity * Time.deltaTime : velocity * Time.deltaTime;
                if (pos.x < min)
                {
                    pos.x = min;
                    backward = false;
                }
                if (pos.x > max)
                {
                    pos.x = max;
                    backward = true;
                }
                break;
            case MoveMode.Vertical:
                pos.y += backward ? -velocity * Time.deltaTime : velocity * Time.deltaTime;
                if (pos.y < min)
                {
                    pos.y = min;
                    backward = false;
                }
                if (pos.y > max)
                {
                    pos.y = max;
                    backward = true;
                }
                break;
        }

        tr.position = pos;
    }
}
