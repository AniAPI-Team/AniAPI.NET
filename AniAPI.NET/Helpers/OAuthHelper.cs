using AniAPI.NET.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AniAPI.NET.Helpers
{
    public class OAuthHelper
    {
        private readonly string _authPath = "https://api.aniapi.com/v1/oauth";
        private readonly string _codePath = "https://api.aniapi.com/v1/oauth/token";

        private string _clientId;
        private string _clientSecret;
        private string _redirectUri;

        private AuthType _type;

        public OAuthHelper(string clientId, string redirectUri)
        {
            _clientId = clientId;
            _redirectUri = redirectUri;

            _type = AuthType.IMPLICIT_GRANT;
        }

        public OAuthHelper(string clientId, string clientSecret, string redirectUri)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
            _redirectUri = redirectUri;

            _type = AuthType.AUTHORIZATION_CODE;
        }

        public async Task<string> Login()
        {
            string token = null;

            string url = _authPath;
            string state = generateRandomState();

            switch (_type)
            {
                case AuthType.IMPLICIT_GRANT:
                    if (string.IsNullOrEmpty(_clientId))
                    {
                        throw new ArgumentException("Could not be null", "clientId");
                    }

                    if (string.IsNullOrEmpty(_redirectUri))
                    {
                        throw new ArgumentException("Could not be null", "redirectUri");
                    }

                    url += $"?response_type=token&client_id={_clientId}&redirect_uri={_redirectUri}&state={state}";
                    break;
                case AuthType.AUTHORIZATION_CODE:
                    if (string.IsNullOrEmpty(_clientId))
                    {
                        throw new ArgumentException("Could not be null", "clientId");
                    }

                    if (string.IsNullOrEmpty(_clientSecret))
                    {
                        throw new ArgumentException("Could not be null", "clientSecret");
                    }

                    if (string.IsNullOrEmpty(_clientId))
                    {
                        throw new ArgumentException("Could not be null", "clientId");
                    }

                    url += $"?response_type=code&client_id={_clientId}&redirect_uri={_redirectUri}&state={state}";
                    break;
            }

            openAuthPage(url);

            bool canExit = false;
            int timeout = 1000 * 60 * 3;

            using (ManualResetEvent canExitHandle = new ManualResetEvent(canExit))
            {
                Task.Factory.StartNew(() =>
                {
                    while (true)
                    {
                        if (timeout <= 0 || !string.IsNullOrEmpty(token))
                        {
                            canExit = true;
                            canExitHandle.Set();
                            break;
                        }

                        Thread.Sleep(1000);
                        timeout -= 1000;
                    }

                });

                using (HttpListener listener = new HttpListener())
                {
                    listener.Prefixes.Add($"{_redirectUri}/");
                    listener.Start();

                    while (!canExit)
                    {
                        IAsyncResult waitResult = listener.BeginGetContext((IAsyncResult result) =>
                        {
                            try
                            {
                                HttpListenerContext context = listener.EndGetContext(result);

                                switch (_type)
                                {
                                    case AuthType.IMPLICIT_GRANT:
                                        if (context.Request.QueryString.Count <= 0)
                                        {
                                            using (StreamWriter writer = new StreamWriter(context.Response.OutputStream))
                                            {
                                                writer.WriteLine($@"
                                                    <html><body><script type=""text/javascript"">
                                                        if(window.location.hash) {{
                                                            const hashes = window.location.hash.substring(1).split('&');
                                                            const token = hashes[0].split('=')[1];
                                                            const state = hashes[1].split('=')[1];
                                            
                                                            fetch('{_redirectUri}?token=' + token + '&state=' + state, {{
                                                                method: 'GET'  
                                                            }});
                                                        }}
                                                    </script></body></html>
                                                ");
                                            }
                                        }
                                        else
                                        {
                                            if (context.Request.QueryString["state"] != state)
                                            {
                                                throw new ArgumentException("Value must match with the generated one!", "state");
                                            }

                                            token = context.Request.QueryString["token"];
                                        }
                                        break;
                                    case AuthType.AUTHORIZATION_CODE:
                                        if (context.Request.QueryString.Count > 0)
                                        {
                                            if (context.Request.QueryString["state"] != state)
                                            {
                                                throw new ArgumentException("Value must match with the generated one!", "state");
                                            }

                                            string code = context.Request.QueryString["code"];

                                            HttpClient client = new HttpClient();
                                            string url = $"{_codePath}?client_id={_clientId}&client_secret={_clientSecret}&code={code}&redirect_uri={_redirectUri}";

                                            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
                                            HttpResponseMessage response = client.SendAsync(request).Result;

                                            APIResponse<string> tokenResult= JsonConvert.DeserializeObject<APIResponse<string>>(response.Content.ReadAsStringAsync().Result);

                                            if(tokenResult.StatusCode == 200)
                                            {
                                                token = tokenResult.Data;
                                            }
                                        }
                                        break;
                                }
                            }
                            catch { }
                        }, null);

                        WaitHandle.WaitAny(new WaitHandle[]
                        {
                            waitResult.AsyncWaitHandle,
                            canExitHandle
                        });
                    }

                    listener.Stop();
                }
            }

            return token;
        }

        private void openAuthPage(string url)
        {
            if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Process.Start("xdg-open", url);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                Process.Start("open", url);
            }
            else
            {
                throw new Exception("OS not supported!");
            }
        }

        private string generateRandomState()
        {
            return Guid.NewGuid().ToString();
        }

        private enum AuthType
        {
            IMPLICIT_GRANT,
            AUTHORIZATION_CODE
        }
    }
}
