using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XS_Utils
{
    public static class XS_ParticleSystem
    {
        static ParticleSystem.MainModule mainModule;
        static ParticleSystem.EmissionModule emissionModule;
        public static void Emit(this ParticleSystem particleSystem, int count)
        {
            ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams();
            particleSystem.Emit(emitParams, count);
        }
        public static void Emit(this ParticleSystem particleSystem, int count, float time)
        {
            //particleSystem.Params
            ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams();
            mainModule = particleSystem.main;
            mainModule.startLifetime = time;
            particleSystem.Emit(emitParams, count);
        }
        public static void EmisionRateOverTime(this ParticleSystem particleSystem, float count)
        {
            emissionModule = particleSystem.emission;
            emissionModule.rateOverTime = new ParticleSystem.MinMaxCurve(count);
        }
        public static void StopAction(this ParticleSystem particleSystem, ParticleSystemStopAction action)
        {
            mainModule = particleSystem.main;
            mainModule.stopAction = action;
        }
        public static void Stop(this ParticleSystem particleSystem) => particleSystem.Stop();

        public static bool IsEmitting(this ParticleSystem particleSystem) => particleSystem.isEmitting;
    }
}

