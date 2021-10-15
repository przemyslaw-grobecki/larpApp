using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Realms;
using Realms.Sync;

namespace RealmTry.Services
{
    class RealmDB
    {
        private static readonly string myRealmAppId = "xxxxxxxxxxxxxxxx";
        private static Realms.Sync.App app;
        public static Realms.Sync.App App
        {
            get
            {
                if(app == null)
                {
                    app = Realms.Sync.App.Create(myRealmAppId);
                }
                return app;
            }
        }
        public static void Login()
        {
            App.LogInAsync(Realms.Sync.Credentials.Anonymous());
            user = App.CurrentUser;
        }
        private static Realms.Sync.User user;
        public static Realms.Sync.User User
        {
            get
            {
                if(user == null)
                {
                    Login();
                }
                return user;
            }
        }
        private static Realms.Sync.SyncConfiguration configuration;
        public static Realms.Sync.SyncConfiguration Configuration
        {
            get
            {
                if (configuration == null)
                {
                    configuration = new SyncConfiguration("_partitionKey", User);
                }
                return configuration;
            }
        }

        internal static readonly char[] chars =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();

        public static string GetUniqueKey(int size)
        {
            byte[] data = new byte[4 * size];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetBytes(data);
            }
            StringBuilder result = new StringBuilder(size);
            for (int i = 0; i < size; i++)
            {
                var rnd = BitConverter.ToUInt32(data, i * 4);
                var idx = rnd % chars.Length;

                result.Append(chars[idx]);
            }

            return result.ToString();
        }

        public static string CurrentlyLoggedUserId { get; set; }
    }
}
