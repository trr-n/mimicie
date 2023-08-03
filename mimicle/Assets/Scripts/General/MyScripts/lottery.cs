using System;
using System.Collections;
using System.Collections.Generic;

namespace Self.Utils
{
    public static class Lottery
    {
        // https://youtu.be/3CQCBQRq0FA
        public static int Weighted(params float[] weights)
        {
            if (weights.Length <= 0)
            {
                return -1;
            }

            float[] cumulativeWeights = new float[weights.Length];

            float totalWeight = 0f;
            for (int i = 0; i < weights.Length; i++)
            {
                totalWeight += weights[i];
                cumulativeWeights[i] = totalWeight;
            }

            float rndNum = Rnd.Float(max: totalWeight);

            int min = 0, max = cumulativeWeights.Length - 1;
            while (min < max)
            {
                int center = (min + max) / 2;
                float centerPoint = cumulativeWeights[center];

                if (rndNum > centerPoint)
                {
                    min = center + 1;
                }

                else
                {
                    float pre = center > 0 ? cumulativeWeights[center - 1] : 0;
                    if (rndNum >= pre)
                    {
                        return center;
                    }
                    max = center;
                }
            }

            return max;
        }

        public static T Weighted<T>(params KeyValuePair<T, float>[] pairs)
        {
            if (pairs.Length == 1)
            {
                return pairs[0].Key;
            }

            float[] weights = new float[pairs.Length];
            for (int i = 0; i < pairs.Length; i++)
            {
                weights[i] = pairs[i].Value;
            }

            int choice = Weighted(weights);
            return pairs[choice].Key;
        }

        public static T Weighted<T>(params KeyValuePair<T, int>[] pairs) => Weighted(pairs);

        public static T Weighted<T>(params Pair<T, float>[] pairs)
        {
            if (pairs.Length == 1)
            {
                return pairs[0].Key;
            }

            float[] weights = new float[pairs.Length];
            for (int i = 0; i < pairs.Length; i++)
            {
                weights[i] = pairs[i].Value;
            }

            int choice = Weighted(weights);
            return pairs[choice].Key;
        }
    }
}