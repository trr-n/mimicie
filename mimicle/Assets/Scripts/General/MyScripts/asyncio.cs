using System.Threading.Tasks;

namespace Mimical.Extend
{
    public static class asyncio
    {
        public static async Task Sleep(int s) => await Task.Delay(s * 1000);

        public static async Task Sleep(float ms)
        => await Task.Delay((ms / 1000).Int());
    }
}