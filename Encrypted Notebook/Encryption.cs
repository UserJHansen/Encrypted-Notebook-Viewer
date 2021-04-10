using System.Windows.Forms;

namespace Encrypted_Notebook
{
    class Encryption
    {
        public static string Encrypt(string Data, KeyFile Key, int iterationCount)
        {
            bool done = false;
            int series = 0;
            while (!done)
            { 
                Data = AESGCM.SimpleEncryptWithPassword(Data, Key.Keys[series]);
                series++;
                iterationCount--;

                if (iterationCount == 0)
                {
                    done = true;
                }
                series = series == Key.Keys.Length ? 0 : series;
            }
            return Data;
        }
        public static string Decrypt(string Data, KeyFile Key, int iterationCount)
        {
            bool done = false;
            int series;
            series = (iterationCount % Key.Keys.Length) == 0 ? Key.Keys.Length : iterationCount % Key.Keys.Length;
            while (!done)
            {
                try
                {
                    Data = AESGCM.SimpleDecryptWithPassword(Data, Key.Keys[series-1]);
                    series--;
                    series = series == 0 ? Key.Keys.Length : series;
                }
                catch
                {
                    MessageBox.Show("Please select the correct Iteration Value or the corrent Key", "Encryption Error");
                    return "";
                }
                iterationCount--;

                if (iterationCount == 0)
                {
                    done = true;
                }
            }
            return Data;
        }
    }
}
