using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Joeri.Tools
{
    public class Timer
    {
        public float time = 0f;
        public float timer = 0f;

        public bool hasReached => timer >= time;
        public float percent  => GetPercent();

        public Timer()
        {
            time = 0f;
        }

        public Timer(float _time)
        {
            time = _time;
        }

        public bool HasReached(float deltaTime)
        {
            Add(deltaTime);
            if (timer < time) return false;
            return true;
        }

        public bool ResetOnReach(float deltaTime)
        {
            if (HasReached(deltaTime))
            {
                Reset();
                return true;
            }
            return false;
        }

        public float Add(float amount)
        {
            timer += amount;
            if (timer < 0f) timer = 0f;
            return timer;
        }

        public float GetPercent(bool clamped = false)
        {
            var percent = 1f;

            if (time > 0)
            {
                percent = timer / time;
                if (clamped) percent = Mathf.Clamp01(percent);
            }
            return percent;
        }

        public void Reset()
        {
            timer = 0f;
        }

        public void Reset(float newTime)
        {
            time = newTime;
            timer = 0f;
        }
    }
}
