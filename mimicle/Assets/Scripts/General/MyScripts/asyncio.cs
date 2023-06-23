using System.Threading;
using System.Threading.Tasks;

namespace Mimical.Extend
{
    public static class Asyncio
    {
        public static async Task _Sleep(int s) => await Task.Delay(s * 1000);
        public static async Task _Sleep(float ms) => await Task.Delay((ms / 1000).Int());
        public static void Sleep(int ms) => Thread.Sleep(ms);
    }
}