using System.Security.Cryptography;

namespace MazePathfinder.Extensions
{
    public class RandomDigit
    {
        /// <summary>
        /// 乱数を生成する
        /// </summary>
        /// <param name="upper">生成する乱数の最大値</param>
        /// <returns>乱数</returns>
        public static int GetRandomNumber(int upper)
        {
            byte[] bytes = new byte[10];
            int modulo = upper - 1;
            int upperLimit = 256 - (256 % modulo);
            RandomNumberGenerator csp = RandomNumberGenerator.Create();
            csp.GetBytes(bytes);
            int result = bytes[bytes[0] % bytes.Length] % modulo + 1;
            for (int i = 0; i < bytes.Length; i++)
            {
                if (bytes[i] < upperLimit)
                {
                    result = bytes[i] % modulo + 1;
                }
            }
            return result;
        }
    }
}
