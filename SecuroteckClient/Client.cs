using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace SecuroteckClient
{
    #region Task 8 and beyond
    class Client
    {
        static bool startup = false;

        static string userName = null;
        static string apiKey = null;
        static string publickey = null;

        static string sign_original = null;
        static void Main()
        {
            string currentMsg = null;
            string originalMsg = null;
            HttpClient client = null;
            while (true)
            {
                if (startup == false)
                {
                    startup = true;
                    Console.Clear();
                    Console.Write("Hello.What would you like to do?\r\n");
                    currentMsg = Console.ReadLine();
                }
                else
                {
                    currentMsg = Console.ReadLine();
                    Console.Clear();
                }
                originalMsg = currentMsg;
                string[] Client_msg = Regex.Split(currentMsg.ToLower(), @"\s+");
                try
                {
                    if (Client_msg.Length > 1)
                    {
                        switch (Client_msg[0])
                        {
                            case "talkback":

                                if (Client_msg[1].Contains("sort") && Client_msg.Length > 2)
                                {
                                    client = new HttpClient();


                                    string debug_numbers = null;
                                    string[] talkback_msg = originalMsg.Split(new char[] { ' ' }, 3);

                                    string[] digits = talkback_msg[2].Split(',', '[', ']');
                                    foreach (string value in digits)
                                    {
                                        //
                                        // Parse the value to get the number.
                                        //
                                        int integer;
                                        if (int.TryParse(value, out integer))
                                        {
                                            debug_numbers += "&integers=";
                                            debug_numbers += value;
                                        }
                                    }
                                    client.BaseAddress = new Uri("http://localhost:24702/api/talkback/sort?" + debug_numbers);
                                    Sendstuff(client, "HeaderName", "header", "body", HttpMethod.Get, 0);
                                    // TalkBack Hello
                                    break;
                                }
                                else if (Client_msg[1].Contains("hello"))
                                {
                                    //TalkBack Sort < integer array >  -  TalkBack Sort [6,1,8,4,3]
                                    client = new HttpClient();
                                    client.BaseAddress = new Uri("http://localhost:24702/api/talkback/Hello");
                                    Sendstuff(client, "HeaderName", "header", "body", HttpMethod.Get, 0);
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid talkback command!");
                                    Console.Write("What would you like to do?\r\n");
                                    break;
                                }

                            case "user":
                                if (Client_msg[1].Contains("get") && Client_msg.Length > 2)
                                {
                                    string[] talkback_msg = originalMsg.Split(new char[] { ' ' }, 3);

                                    client = new HttpClient();
                                    client.BaseAddress = new Uri("http://localhost:24702/api/user/new?username=" + talkback_msg[2]);

                                    Sendstuff(client, "HeaderName", "header", "body", HttpMethod.Get, 0);
                                    break;
                                }
                                else if (Client_msg[1].Contains("post") && Client_msg.Length > 2)
                                {
                                    //User Post < name >  -  User Post UserOne
                                    string[] talkback_msg = originalMsg.Split(new char[] { ' ' }, 3);

                                    client = new HttpClient();
                                    client.BaseAddress = new Uri("http://localhost:24702/api/user/new");

                                    Sendstuff(client, "HeaderName", "header", talkback_msg[2], HttpMethod.Post, 1);
                                    userName = talkback_msg[2];

                                    break;
                                }
                                else if (Client_msg[1].Contains("set") && Client_msg.Length > 3)
                                {
                                    // User Set < name > < apikey >  -  User Set UserOne 004b620d - a523 - 4b1b8bdc - 857d8a5541b9
                                    string[] talkback_msg = originalMsg.Split(new char[] { ' ' }, 4);

                                    userName = talkback_msg[2];
                                    apiKey = talkback_msg[3];
                                    Console.WriteLine("Stored");
                                    Console.Write("What would you like to do?\r\n");
                                    break;
                                }
                                else if (Client_msg[1].Contains("delete"))
                                {
                                    client = new HttpClient();
                                    client.BaseAddress = new Uri("http://localhost:24702/api/user/removeuser?username=" + userName);

                                    // User Delete 
                                    Sendstuff(client, "ApiKey", apiKey, "body", HttpMethod.Delete, 0);
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid user command!");
                                    Console.Write("What would you like to do?\r\n");
                                    break;
                                }

                            case "protected":
                                if (Client_msg[1].Contains("hello"))
                                {
                                    if (userName != null)
                                    {
                                        // Protected Hello
                                        client = new HttpClient();
                                        client.BaseAddress = new Uri("http://localhost:24702/api/protected/hello");

                                        // User Delete 
                                        Sendstuff(client, "ApiKey", apiKey, "body", HttpMethod.Get, 0);
                                    }
                                    else
                                    {
                                        Console.WriteLine("You need to do a User Post or User Set First");
                                        Console.Write("What would you like to do?\r\n");
                                    }
                                    break;
                                }
                                else if (Client_msg[1].Contains("sha1") && Client_msg.Length > 2)
                                {
                                    // Protected SHA1 < message >  -  Protected SHA1 Hello 
                                    if (userName != null && apiKey != null)
                                    {
                                        string[] talkback_msg = originalMsg.Split(new char[] { ' ' }, 3);
                                        // Protected Hello
                                        client = new HttpClient();
                                        client.BaseAddress = new Uri("http://localhost:24702/api/protected/sha1?message=" + talkback_msg[2]);

                                        // User Delete 
                                        Sendstuff(client, "ApiKey", apiKey, "body", HttpMethod.Get, 0);
                                    }
                                    else
                                    {
                                        Console.WriteLine("You need to do a User Post or User Set First");
                                        Console.Write("What would you like to do?\r\n");
                                    }
                                    break;
                                }
                                else if (Client_msg[1].Contains("sha256") && Client_msg.Length > 2)
                                {
                                    // Protected SHA256 <message>  -  Protected SHA256 Hello
                                    if (userName != null && apiKey != null)
                                    {

                                        string[] talkback_msg = originalMsg.Split(new char[] { ' ' }, 3);
                                        client = new HttpClient();
                                        client.BaseAddress = new Uri("http://localhost:24702/api/protected/sha256?message=" + talkback_msg[2]);
                                        Sendstuff(client, "ApiKey", apiKey, "body", HttpMethod.Get, 0);
                                    }
                                    else
                                    {
                                        Console.WriteLine("You need to do a User Post or User Set First");
                                        Console.WriteLine("What would you like to do?\r\n");
                                    }
                                    break;
                                }
                                else if (Client_msg[1].Contains("get") && Client_msg.Length > 2 && Client_msg[2].Contains("publickey"))
                                {
                                    // Protected SHA256 <message>  -  Protected SHA256 Hello
                                    if (userName != null && apiKey != null)
                                    {

                                        // Protected Hello
                                        client = new HttpClient();
                                        client.BaseAddress = new Uri("http://localhost:24702/api/protected/getpublickey");

                                        // User Delete 
                                        Sendstuff(client, "ApiKey", apiKey, "body", HttpMethod.Get, 2);
                                    }
                                    else
                                    {
                                        Console.WriteLine("You need to do a User Post or User Set First");
                                        Console.WriteLine("What would you like to do?\r\n");
                                    }
                                    break;
                                }
                                else if (Client_msg[1].Contains("sign") && Client_msg.Length > 2)
                                {
                                    // Protected SHA256 <message>  -  Protected SHA256 Hello
                                    if (userName != null && apiKey != null)
                                    {

                                        string[] talkback_msg = originalMsg.Split(new char[] { ' ' }, 3);
                                        client = new HttpClient();
                                        sign_original = talkback_msg[2];
                                        client.BaseAddress = new Uri("http://localhost:24702/api/protected/sign?message=" + talkback_msg[2]);
                                        Sendstuff(client, "ApiKey", apiKey, "body", HttpMethod.Get, 3);
                                    }
                                    else
                                    {
                                        Console.WriteLine("You need to do a User Post or User Set First");
                                        Console.WriteLine("What would you like to do?\r\n");
                                    }
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid protected command!");
                                    Console.WriteLine("What would you like to do?\r\n");
                                    break;
                                }

                            default:
                                Console.WriteLine("Invalid Input");
                                Console.WriteLine("What would you like to do?\r\n");
                                break;
                        }
                    }
                    else
                    {
                        switch (Client_msg[0])
                        {
                            case "exit":
                                if (Client_msg.Length == 1)
                                {
                                    Environment.Exit(0);
                                }
                                break;

                            default:
                                Console.WriteLine("Invalid Input!");
                                Console.WriteLine("What would you like to do?\r\n");
                                break;
                        }

                    }
                }
                catch
                {
                    Console.WriteLine("ERROR: Command caused client to crash!");
                    Console.WriteLine("What would you like to do?\r\n");
                }
            }
        }
        static async void Sendstuff(HttpClient client, string headerName, string header, string body, HttpMethod method, int type)
        {
            HttpRequestMessage httpmsg;
            if (method != HttpMethod.Post)
            {
                httpmsg = new HttpRequestMessage()
                {
                    Method = method,
                    RequestUri = new Uri(client.BaseAddress.ToString())
                };
            }
            else
            {
                httpmsg = new HttpRequestMessage()
                {
                    Method = method,
                    RequestUri = new Uri(client.BaseAddress.ToString()),
                    Content = new StringContent("\"" + body + "\"", Encoding.UTF8, "application/json")
                };
            }
            httpmsg.Headers.Add(headerName, header);


            Console.WriteLine("...please wait...");
            var returnthing = await client.SendAsync(httpmsg);
            Task<string> TRmsg = returnthing.Content.ReadAsStringAsync();
            TRmsg.Wait();
            string Rmsg = TRmsg.Result;
            if (type == 0)
            {
                Rmsg = Remove_quotes(Rmsg);
                Console.WriteLine(Rmsg);
                Console.WriteLine("What would you like to do?\r\n");
            }
            else if (type == 1)
            {
                if (Rmsg.StartsWith("\""))
                {
                    Rmsg = Rmsg.Remove(0, 1);
                }
                if (Rmsg.EndsWith("\""))
                {
                    Rmsg = Rmsg.Remove(Rmsg.Length - 1, 1);
                }

                if (Rmsg.Length > 37)
                {
                    Console.WriteLine(Rmsg);
                }
                else
                {
                    apiKey = Rmsg;
                    // 838f6930-9548-e811-8586-708bcd8cea91
                    // 00d30346-9548-e811-8586-708bcd8cea91
                    // 1414cb5a-9548-e811-8586-708bcd8cea91
                    Console.WriteLine("Got API Key");
                    Console.WriteLine("What would you like to do?\r\n");
                }

            }
            else if (type == 2)
            {
                if (Rmsg.Contains("RSAKeyValue"))
                {
                    if (Rmsg.StartsWith("\""))
                    {
                        Rmsg = Rmsg.Remove(0, 1);
                    }
                    if (Rmsg.EndsWith("\""))
                    {
                        Rmsg = Rmsg.Remove(Rmsg.Length - 1, 1);
                    }

                    publickey = Rmsg.Trim('"');

                    Console.WriteLine("Got Public Key");
                    Console.WriteLine("What would you like to do?\r\n");
                }
                else
                {
                    Console.WriteLine(Rmsg);
                    Console.WriteLine("What would you like to do?\r\n");
                }

            }
            else if (type == 3)
            {
                if (!Rmsg.Contains("Unauthorized"))
                {
                    if (publickey != null)
                    {
                        RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
                        RSA.FromXmlString(publickey);

                        if (Rmsg.StartsWith("\""))
                        {
                            Rmsg = Rmsg.Remove(0, 1);
                        }
                        if (Rmsg.EndsWith("\""))
                        {
                            Rmsg = Rmsg.Remove(Rmsg.Length - 1, 1);
                        }

                        string[] split_hex = Rmsg.Split('-');
                        byte[] final_Bytes = new byte[split_hex.Length];

                        for (int i = 0; i < split_hex.Length; i++)
                        {
                            final_Bytes[i] = byte.Parse(split_hex[i], System.Globalization.NumberStyles.AllowHexSpecifier);
                        }

                        byte[] compare_Original = Encoding.UTF8.GetBytes(sign_original);

                        SHA1 sha1 = new SHA1Managed();
                        byte[] hased_data = sha1.ComputeHash(compare_Original);

                        if (RSA.VerifyHash(hased_data, "SHA1", final_Bytes))
                        {
                            Console.WriteLine("Message was successfully signed");
                        }
                        else
                        {
                            Console.WriteLine("Message was not successfully signed");
                        }

                        Console.WriteLine("What would you like to do?\r\n");
                    }
                    else
                    {

                        Console.WriteLine("“Client doesn’t yet have the public key");
                        Console.WriteLine("What would you like to do?\r\n");
                    }
                }
                else
                {
                    Rmsg = Remove_quotes(Rmsg);
                    Console.WriteLine(Rmsg);
                    Console.WriteLine("What would you like to do?\r\n");
                }
            }

        }

        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
        public static string Remove_quotes(string gimme)
        {
            if (gimme.StartsWith("\""))
            {
                gimme = gimme.Remove(0, 1);
            }
            if (gimme.EndsWith("\""))
            {
                gimme = gimme.Remove(gimme.Length - 1, 1);
            }
            return gimme;
        }


    }
    #endregion
}
