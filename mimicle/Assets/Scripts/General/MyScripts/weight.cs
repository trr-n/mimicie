namespace Mimical.Extend
{
    public class Weight
    {
        float[] weight;
        float totalWeight;
        float[] cumulativeWeights;

        public Weight(params float[] weight)
        {
            this.weight = weight;
            cumulativeWeights = new float[this.weight.Length];
            for (var i = 0; i < this.weight.Length; i++)
            {
                totalWeight += this.weight[i];
                cumulativeWeights[i] = totalWeight;
            }
        }

        public int Choose()
        {
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