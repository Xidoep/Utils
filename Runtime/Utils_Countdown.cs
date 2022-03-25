using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XS_Utils
{
    /// <summary>
    /// Non-static class made to have a "time" with all possible functionalities.
    /// It have to be initated with the builder, with a time and a funtions to call on end.
    /// </summary>
    public class XS_Countdown
    {
        float time;
        Action onEnd;
        bool active;
        float currentTime;
        bool Ended => currentTime <= 0;

        public XS_Countdown(float time, Action onEnd)
        {
            active = false;
            SetCurrentTime(time);
            this.time = time;
            this.onEnd = onEnd;
        }

        /// <summary>
        /// Sets the time if you want to set it after create it.
        /// </summary>
        void SetCurrentTime(float time)
        {
            currentTime = time;
        }


        /// <summary>
        /// Start the countdown
        /// </summary>
        public void Start()
        {
            active = true;
            SetCurrentTime(time);
        }

        /// <summary>
        /// Start the countdown setting the time before start.
        /// </summary>
        /// <param name="time"></param>
        public void Start(float time)
        {
            this.time = time;
            Start();
        }
        /// <summary>
        /// Activate the countdown without resestart the current time. 
        /// </summary>
        public void Continue()
        {
            active = true;
        }
        /// <summary>
        /// It must to be called every frame on yout Update main class, to be sure the countdown start right after it activates.
        /// </summary>
        public void Update()
        {
            if (!active)
                return;

            currentTime -= Time.unscaledDeltaTime;

            if (Ended)
            {
                onEnd.Invoke();
                active = false;
            }
        }
        /// <summary>
        /// Stopts the countdown
        /// </summary>
        public void Stop(bool restart = false)
        {
            active = false;
        }
    }
}