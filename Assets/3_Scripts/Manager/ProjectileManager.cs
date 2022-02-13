using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager
{
    private static ProjectileManager instance;

    public static ProjectileManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ProjectileManager();
            }
            return instance;
        }
    }

    public delegate void TimerDelegate();

    private List<ObjectTimer> deactivateTimerList;

    private ProjectileManager()
    {
        Initialize();
    }

    public void Initialize()
    {
        deactivateTimerList = new List<ObjectTimer>();
    }

    public void Refresh()
    {
        for (int i = deactivateTimerList.Count - 1; i >= 0; i--)
        {
            if(deactivateTimerList[i].timeDelay <= Time.time)
            {

            }
        }
    }

    private class ObjectTimer
    {
        public float timeDelay;
        public TimerDelegate timerDelegate;
    }
}
