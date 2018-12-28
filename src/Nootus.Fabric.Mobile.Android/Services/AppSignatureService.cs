using Android.Content;
using Android.Content.PM;
using Android.Util;
using Java.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nootus.Fabric.Mobile.Droid.Services
{
    public class AppSignatureService : ContextWrapper
    {
        private static readonly string TAG = "X:" + typeof(AppSignatureService).Name;
        private static readonly string HASH_TYPE = "SHA-256";
        public static readonly int NUM_HASHED_BYTES = 9;
        public static readonly int NUM_BASE64_CHAR = 11;

        public AppSignatureService(Context context) : base(context)
        {
        }


        public List<string> GetAppSignatures()
        {
            List<string> appCodes = new List<string>();

            try
            {
                // Get all package signatures for the current package
                string packageName = PackageName;
                PackageManager packageManager = PackageManager;
                var signatures = packageManager.GetPackageInfo(packageName, PackageInfoFlags.Signatures).Signatures;
                        

                // For each signature create a compatible hash
                foreach (var signature in signatures)
                {
                    string hash = Hash(packageName, signature.ToCharsString());
                    if (hash != null)
                    {
                        appCodes.Add(hash);
                    }
                }
            }
            catch (PackageManager.NameNotFoundException exp)
            {
                Log.Error(TAG, "Unable to find package to obtain hash.", exp);
            }
            return appCodes;
        }

        private static String Hash(string packageName, string signature)
        {
            string appInfo = packageName + " " + signature;
            try
            {
                MessageDigest messageDigest = MessageDigest.GetInstance(HASH_TYPE);
                byte[] input = Encoding.UTF8.GetBytes(appInfo);
                messageDigest.Update(input);
                byte[] hashSignature = messageDigest.Digest();

                // truncated into NUM_HASHED_BYTES
                //hashSignature = Arrays.copyOfRange(hashSignature, 0, NUM_HASHED_BYTES);
                Array.Copy(hashSignature, hashSignature, NUM_HASHED_BYTES);

                // encode into Base64
                string base64Hash = Base64.EncodeToString(hashSignature, Base64Flags.NoPadding | Base64Flags.NoWrap);
                base64Hash = base64Hash.Substring(0, NUM_BASE64_CHAR);

                Log.Debug(TAG, string.Format("pkg: %s -- hash: %s", packageName, base64Hash));
                return base64Hash;
            }
            catch (NoSuchAlgorithmException exp)
            {
                Log.Error(TAG, "hash:NoSuchAlgorithm", exp);
            }
            catch(System.Exception exp)
            {
                Log.Error(TAG, "hash:Exception", exp);
            }
            return null;
        }
    }
}