﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmIndicator : MonoBehaviour
{
    private Time m_time;
    private IEnumerator m_coroutine;
    private bool m_continueCourutine;

    public enum Status { green, red }
    public Status status;
    public float m_beatPerSecond;
    public float m_reactionTime;
    private EnemyController enemyCont;
    private bool _enemyDidAction;
    // Use this for initialization
    void Start()
    {
        enemyCont = GameObject.FindGameObjectWithTag("EnemyController").GetComponent<EnemyController>();
        status = Status.red;
        //m_continueCourutine = true;
        //m_coroutine = WaitOnBeat();
        //StartCoroutine(m_coroutine);
    }

    // Update is called once per frame
    void Update()
    {

    }

    /*
    IEnumerator WaitOnBeat()
    {
        if (m_beatPerSecond < m_reactionTime)
        {
            Debug.Log("Error: reactionTime is bigger than beat!");
        }
        else
        {
            while (m_continueCourutine)
            {
                status = Status.red;
                GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
                yield return new WaitForSecondsRealtime(m_beatPerSecond);
                _enemyDidAction = false;
                   
                status = Status.green;
                GetComponent<SpriteRenderer>().color = new Color(0, 1, 0);
                yield return new WaitForSecondsRealtime(m_reactionTime);
                ExecuteEnemyActions();
            }
        }
    }
    */

    IEnumerator WaitOnBeat()
    {
        status = Status.green;
        _enemyDidAction = false;
        GetComponent<SpriteRenderer>().color = new Color(0, 1, 0);
        yield return new WaitForSecondsRealtime(m_reactionTime);
        status = Status.red;
        GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
        ExecuteEnemyActions();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Bubble")
        {
            StartCoroutine(WaitOnBeat());
        }
    }

    public void ExecuteEnemyActions()
    {
        if (!_enemyDidAction)
        {
            _enemyDidAction = true;
            enemyCont.ExecuteActions();
        }
    }
}
