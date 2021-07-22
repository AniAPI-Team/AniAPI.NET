using System;

namespace AniAPI.NET.OAuth.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Test();
        }

        private static async void Test()
        {
            try
            {
                AniAPI.Instance.UseAuthorizationCode("d05c6966-9ca6-43b7-afc4-d0c1376261bc", "f060ce03-1cec-4b27-8dbc-d04ec5f6f55f", "http://localhost:3000/auth");
                await AniAPI.Instance.Login();
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
