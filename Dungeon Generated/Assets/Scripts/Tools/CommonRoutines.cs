using System;
using System.Collections;
using UnityEngine;

namespace Dodelie.Tools
{
    public static class CommonRoutines
    {
        /// <summary>
        /// Waits for the specified amount of seconds before trigerring the event.
        /// </summary>
        public static IEnumerator WaitForSeconds(float time, Action onFinish)
        {
            yield return new WaitForSeconds(time);
            onFinish.Invoke();
        }

        /// <summary>
        /// Waits for the specified amount of unscaled seconds before trigerring the event.
        /// </summary>
        public static IEnumerator WaitForSecondsRealtime(float time, Action onFinish)
        {
            yield return new WaitForSecondsRealtime(time);
            onFinish.Invoke();
        }

        /// <summary>
        /// Progresses a timer, and calls the 'onTick' each frame, before calling the 'onFinish' event.
        /// </summary>
        public static IEnumerator Progression(float time, ProgressionEvent onTick, Action onFinish)
        {
            var timer = 0f;

            while (timer < time)
            {
                timer += Time.deltaTime;
                onTick.Invoke(timer / time);
                yield return null;
            }
            onFinish.Invoke();
        }

        /*
        public static IEnumerator DoubleProgression(float time, float subTime, Action onTick, Action onFinish)
        {
            var timer = 0f;
            var subTimer = 0f;

            while (timer < time)
            {
                timer += Time.deltaTime;
                while (subTimer < subTime)
                {
                    subTimer += Time.deltaTime;
                    yield return null;
                }
                onTick.Invoke();
                subTimer = 0f;
            }
            onFinish.Invoke();
        }
        */

        /// <summary>
        /// Progresses a timer, and calls the 'onTick' each frame with an AnimationCurve 0-1 float as parameter, before calling the 'onFinish' event.
        /// </summary>
        public static IEnumerator CustomProgression(float time, AnimationCurve curve, ProgressionEvent onTick, Action onFinish)
        {
            var timer = 0f;

            while (timer < time)
            {
                timer += Time.deltaTime;
                onTick.Invoke(curve.Evaluate(timer / time));
                yield return null;
            }
            onFinish.Invoke();
        }

        public delegate void ProgressionEvent(float progression);
    }
}