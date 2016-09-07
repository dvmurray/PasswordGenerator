using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;

namespace PasswordGenerator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            FileStream fs = File.Create(@"C:\Users\dmurray\Desktop\passwords.txt"); 

            for (int i = 0; i < 25000; i++) {
                //length of alpah and numeric sections
                int maxSizeA = 4;
                int maxSizeN = 3;
                //declare stringbuilder
                StringBuilder strPassword = new StringBuilder(maxSizeA + maxSizeN);
                //call function to build alpha and numeric sections, append to StringBuilder 
                strPassword.Append(AlphaString(maxSizeA));
                strPassword.Append(NumericString(maxSizeN));
                //reorder StringBuilder
                Random num = new Random();
                string rand = new string(strPassword.ToString().ToCharArray().OrderBy(s => (num.Next(2) % 2) == 0).ToArray()) + "\r\n";

                byte[] info = new UTF8Encoding(true).GetBytes(rand);  
                fs.Write(info,0,info.Length);
                fs.Flush();
            }
            fs.Close();
            MessageBox.Show("Complete"); 
        }

        private String NumericString(int maxSize) {
            char[] chars = new char[8];
            chars = "23456789".ToCharArray();
            return GenerateRandomSequence(chars, maxSize);
        }
        private String AlphaString(int maxSize) {

            char[] chars = new char[24];
            chars = "ABCDEFGHJKLMNPQRSTUVWXYZ".ToCharArray();

            return GenerateRandomSequence(chars,maxSize);

        }
        private String GenerateRandomSequence(char[] chars, int maxSize) {

            byte[] data = new byte[1];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetNonZeroBytes(data);
                data = new byte[maxSize];
                crypto.GetNonZeroBytes(data);
            }
            StringBuilder result = new StringBuilder(maxSize);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }
    }
}
