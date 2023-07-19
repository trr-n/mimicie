using System;
using System.Collections;
using System.Collections.Generic;

namespace Feather.Utils
{
    public static class Lottery
    {
        public static int ChoiceIndexByWeights(params float[] weights)
        {
            float[] weight = weights;
            float[] cumulativeWeights = new float[weights.Length];

            float totalWeight = 0f;
            for (int i = 0; i < weight.Length; i++)
            {
                totalWeight += weight[i];
                cumulativeWeights[i] = totalWeight;
            }

            var rndNum = Rnd.Float(max: totalWeight);

            int min = 0, max = cumulativeWeights.Length - 1;
            while (min < max)
            {
                var centeri = (min + max) / 2;
                var centerPoint = cumulativeWeights[centeri];

                if (rndNum > centerPoint)
                {
                    min = centeri + 1;
                }

                else
                {
                    var pre = centeri > 0 ? cumulativeWeights[centeri - 1] : 0;
                    if (rndNum >= pre)
                    {
                        return centeri;
                    }
                    max = centeri;
                }
            }
            return max;
        }

        // public static T ChoChoi<T>(T[] items, float[] weights)
        // {
        //     if (items.Length != weights.Length)
        //     {
        //         throw new System.Exception("数おおてへん");
        //     }

        //     int choice = 0;

        //     float totalWeight = 0f;
        //     foreach (var weight in weights)
        //     {
        //         totalWeight += weight;
        //     }

        //     float rnd = Rnd.Float(max: totalWeight);

        //     for (int index = 0; index < items.Length; index++)
        //     {
        //         rnd -= weights[index];
        //         if (rnd < 0f)
        //         {
        //             choice = index;
        //             break;
        //         }
        //     }

        //     return items[choice];
        // }

        public static T ChoiceByWeights<T>(params KeyValuePair<T, float>[] pairs)
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

            int choice = ChoiceIndexByWeights(weights);
            return pairs[choice].Key;
        }

        public static T ChoiceByWeights<T>(params KeyValuePair<T, int>[] pairs) => ChoiceByWeights(pairs);

        public static T ChoiceByWeights<T>(params Pair<T, float>[] pairs)
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

            int choice = ChoiceIndexByWeights(weights);
            return pairs[choice].Key;
        }

    }
}