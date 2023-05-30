using System.Threading.Tasks;

namespace Mimic.Extend
{
    public static class asyncio
    {
        // public static async Task<int> counting(int kankaku) => await 1;

        public static async Task sleep(int s) => await Task.Delay(s);
    }
}