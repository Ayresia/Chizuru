using System;
using System.Threading.Tasks;

namespace Chizuru
{
    class Program
    {
        public static async Task Main(String[] args) => await new Startup().InitAsync();
    }
}
