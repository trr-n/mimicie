namespace UnionEngine.Extend
{
    public static class Lottery
    {
        public static int ChoiceByWeights(params float[] weights)
        {
            float[] weight, cumulativeWeights;
            float totalWeight = 0;
            weight = weights;
            cumulativeWeights = new float[weights.Length];
            for (int i = 0; i < weight.Length; i++)
            {
                totalWeight += weight[i];
                cumulativeWeights[i] = totalWeight;
            }
            var randomPoint = Rnd.Float(max: totalWeight);
            int mini = 0, maxi = cumulativeWeights.Length - 1;
            while (mini < maxi)
            {
                var centeri = (mini + maxi) / 2;
                var centerPoint = cumulativeWeights[centeri];
                if (randomPoint > centerPoint)
                {
                    mini = centeri + 1;
                }
                else
                {
                    var pre = centeri > 0 ? cumulativeWeights[centeri - 1] : 0;
                    if (randomPoint >= pre)
                    {
                        return centeri;
                    }
                    maxi = centeri;
                }
            }
            return maxi;
        }
    }
}