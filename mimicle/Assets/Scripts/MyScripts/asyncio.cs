using System.Threading.Tasks;

namespace Mimical.Extend
{
    public static class asyncio
    {
        // public static async Task<int> counting(int kankaku) => await 1;

        public static async Task Sleep(int seconds) => await Task.Delay(seconds * 1000);

        public static async Task Sleep(float seconds) => await Task.Delay((seconds / 1000).ToInt());
    }
}