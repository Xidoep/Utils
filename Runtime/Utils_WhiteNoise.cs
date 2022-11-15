using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

namespace XS_Utils
{
    public static class Utils_WhiteNoise
    {
        //Exemple random chest generation on 3x3 grid
        public static void RandomChest(float seed)
        {
            //float seed = UnityEngine.Random.value;
            Debugar.Log($"{seed:f10}");
            float[,] spawnGrid = new float[3, 3];
            Vector2 spawnRange = new Vector2(60, 80);
            for (int x = 0; x < spawnGrid.GetLength(0); x++)
            {
                for (int y = 0; y < spawnGrid.GetLength(1); y++)
                {
                    float noise = math.floor(Utils_WhiteNoise.Rand2dTo1d(new float2(x, y) + seed) * 100);
                    Debugar.Log($"{x},{y} = {noise >= spawnRange.x && noise <= spawnRange.y}");
                }
            }
        }

        public static float Calculate(float2 uv)
        {
            return math.frac(math.sin(math.dot(uv, new float2(12.9898f, 78.233f))) * 43758.5453f);
        }


        public static float Rand3dTo1d(float3 value)
        {
            return Rand3dTo1d(value, new float3(12.9898f, 78.233f, 37.719f));
        }
        //get a scalar random value from a 3d value
        public static float Rand3dTo1d(float3 value, float3 dotDir)
        {
            //make value smaller to avoid artefacts
            float3 smallValue = math.sin(value);
            //get scalar value from 3d vector
            float random = math.dot(smallValue, dotDir);
            //make value more random by making it bigger and then taking the factional part
            random = math.frac(math.sin(random) * 143758.5453f);
            return random;
        }
		public static float Rand2dTo1d(float2 value)
		{
			return Rand2dTo1d(value, new float2(12.9898f, 78.233f));
		}

		public static float Rand2dTo1d(float2 value, float2 dotDir)
		{
			float2 smallValue = math.sin(value);
			float random = math.dot(smallValue, dotDir);
			random = math.frac(math.sin(random) * 143758.5453f);
			return random;
		}

		public static float Rand1dTo1d(float value, float mutator = 0.546f)
		{
			float random = math.frac(math.sin(value + mutator) * 143758.5453f);
			return random;
		}

		//to 2d functions

		public static float2 Rand3dTo2d(float3 value)
		{
			return new float2(
				Rand3dTo1d(value, new float3(12.989f, 78.233f, 37.719f)),
				Rand3dTo1d(value, new float3(39.346f, 11.135f, 83.155f))
			);
		}

		public static float2 Rand2dTo2d(float2 value)
		{
			return new float2(
				Rand2dTo1d(value, new float2(12.989f, 78.233f)),
				Rand2dTo1d(value, new float2(39.346f, 11.135f))
			);
		}

		public static float2 Rand1dTo2d(float value)
		{
			return new float2(
				Rand2dTo1d(value, 3.9812f),
				Rand2dTo1d(value, 7.1536f)
			);
		}

		//to 3d functions

		public static float3 Rand3dTo3d(float3 value)
		{
			return new float3(
				Rand3dTo1d(value, new float3(12.989f, 78.233f, 37.719f)),
				Rand3dTo1d(value, new float3(39.346f, 11.135f, 83.155f)),
				Rand3dTo1d(value, new float3(73.156f, 52.235f, 09.151f))
			);
		}

		public static float3 Rand2dTo3d(float2 value)
		{
			return new float3(
				Rand2dTo1d(value, new float2(12.989f, 78.233f)),
				Rand2dTo1d(value, new float2(39.346f, 11.135f)),
				Rand2dTo1d(value, new float2(73.156f, 52.235f))
			);
		}

		public static float3 Rand1dTo3d(float value)
		{
			return new float3(
				Rand1dTo1d(value, 3.9812f),
				Rand1dTo1d(value, 7.1536f),
				Rand1dTo1d(value, 5.7241f)
			);
		}


	}
}

