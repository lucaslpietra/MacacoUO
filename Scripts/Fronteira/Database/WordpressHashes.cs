using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

// Implementacao tosca do hash q o WP faz
public class WordpressHashes
{
    private static string itoa64 = "./0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

    public static bool Correct(string password, string hashFromDatabase)
    {
        return MD5Encode(password, hashFromDatabase) == hashFromDatabase;
    }

    static string MD5Encode(string password, string hash)
    {
        string output = "*0";
        if (hash == null)
        {
            return output;
        }

        if (hash.StartsWith(output))
            output = "*1";

        string id = hash.Substring(0, 3);

        if (id != "$P$" && id != "$H$")
            return output;

        int count_log2 = itoa64.IndexOf(hash[3]);
        if (count_log2 < 7 || count_log2 > 30)
            return output;

        int count = 1 << count_log2;

        string salt = hash.Substring(4, 8);
        if (salt.Length != 8)
            return output;

        byte[] hashBytes = { };
        using (MD5 md5Hash = MD5.Create())
        {
            hashBytes = md5Hash.ComputeHash(Encoding.ASCII.GetBytes(salt + password));
            byte[] passBytes = Encoding.ASCII.GetBytes(password);
            do
            {
                hashBytes = md5Hash.ComputeHash(hashBytes.Concat(passBytes).ToArray());
            } while (--count > 0);
        }

        output = hash.Substring(0, 12);
        string newHash = Encode64(hashBytes, 16);

        return output + newHash;
    }

    static string Encode64(byte[] input, int count)
    {
        StringBuilder sb = new StringBuilder();
        int i = 0;
        do
        {
            int value = (int)input[i++];
            sb.Append(itoa64[value & 0x3f]);
            if (i < count)
                value = value | ((int)input[i] << 8);
            sb.Append(itoa64[(value >> 6) & 0x3f]);
            if (i++ >= count)
                break;
            if (i < count)
                value = value | ((int)input[i] << 16);
            sb.Append(itoa64[(value >> 12) & 0x3f]);
            if (i++ >= count)
                break;
            sb.Append(itoa64[(value >> 18) & 0x3f]);
        } while (i < count);

        return sb.ToString();
    }
}
