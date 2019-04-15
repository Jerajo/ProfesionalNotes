using System;
using System.Text;
using System.Security.Cryptography;

/*
 * Algoritmo 3DES sacado de StackOverflow.
 * https://stackoverflow.com/questions/11413576/how-to-implement-triple-des-in-c-sharp-complete-example
 * 
 * Algoritmo Rijndael sacado de microsoft.
 * https://msdn.microsoft.com/es-es/library/bb972216.aspx?f=255&MSPPError=-2147217396
 */

namespace PN.Services
{

	/// <summary>
	/// Modulo encargado de los protocolos de seguridad del sistema.
	/// </summary>
	public static class SecurityProtocolService
    {
        /// <summary>
        /// SET: y GET: De _key. Llave utilizada para la encryptación y desencryptación.
        /// </summary>
        public static string Key { get; set; } = "abcdefghijlmnopqrstuvwxy";

        #region TripleDES

        /// <summary>
        /// Método de la clase SecurityProtocol. Utilizado para la encryptación de datos.
        /// </summary>
        /// <param name="toEncrypt">Array de bytes a ser encryptado.</param>
        /// <param name="useHashing">Indica el uso o no, de hash.</param>
        /// <returns>Retorna un byte[] con los datos encryptados.</returns>
        public static byte[] EncryptTripleDES(byte[] toEncrypt, bool useHashing = false)
        {
            TripleDESCryptoServiceProvider tipleDES = new TripleDESCryptoServiceProvider();
            byte[] encrypted = { };
            byte[] keyArray;

            if (useHashing)
            {
                MD5CryptoServiceProvider hashMD5 = new MD5CryptoServiceProvider();
                keyArray = hashMD5.ComputeHash(Encoding.UTF8.GetBytes(Key));
                hashMD5.Clear();
            }
            else keyArray = Encoding.UTF8.GetBytes(Key);

            try
            {
                tipleDES.Key = keyArray;
                tipleDES.Mode = CipherMode.ECB;
                tipleDES.Padding = PaddingMode.PKCS7;
                ICryptoTransform encryptor = tipleDES.CreateEncryptor();
                encrypted = encryptor.TransformFinalBlock(toEncrypt, 0, toEncrypt.Length);
            }
            catch (Exception ex) { throw ex; }
            finally { tipleDES.Clear(); }
            return encrypted;
        }

        /// <summary>
        /// Método de la clase SecurityProtocol. Utilizado para la desencryptación de datos.
        /// </summary>
        /// <param name="toDecrypt">Array de bytes a ser desencryptado.</param>
        /// <param name="useHashing">Indica el uso o no, de hash.</param>
        /// <returns>Retorna un byte[] con los datos desencryptados.</returns>
        public static byte[] DecryptTripleDES(byte[] toDecrypt, bool useHashing = false)
        {
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            byte[] decrypted = { };
            byte[] keyArray;

            if (useHashing)
            {
                MD5CryptoServiceProvider hashMD5 = new MD5CryptoServiceProvider();
                keyArray = hashMD5.ComputeHash(Encoding.UTF8.GetBytes(Key));
                hashMD5.Clear();
            }
            else keyArray = Encoding.UTF8.GetBytes(Key);

            try
            {
                tripleDES.Key = keyArray;
                tripleDES.Mode = CipherMode.ECB;
                tripleDES.Padding = PaddingMode.PKCS7;
                ICryptoTransform descryptor = tripleDES.CreateDecryptor();
                decrypted = descryptor.TransformFinalBlock(toDecrypt, 0, toDecrypt.Length);
            }
            catch (Exception ex) { throw ex; }
            finally { tripleDES.Clear(); }
            return decrypted;
        }

        #endregion

        #region Rijndael

        /// <summary>
        /// Método de la clase SecurityProtocol. Utilizado para la encryptación de datos.
        /// </summary>
        /// <param name="toEncrypt">Array de bytes a ser encryptado.</param>
        /// <param name="key">Llave para la emcriptación.</param>
        /// <returns>Retorna un byte[] con los datos encryptados.</returns>
        public static byte[] Encrypt(byte[] toEncrypt, byte[] key)
        {
            Rijndael miRijndael = Rijndael.Create();
            byte[] encrypted = null;
            byte[] returnValue = null;

            try
            {
                miRijndael.Key = key;
                miRijndael.GenerateIV();
                encrypted = (miRijndael.CreateEncryptor()).TransformFinalBlock(toEncrypt, 0, toEncrypt.Length);
                returnValue = new byte[miRijndael.IV.Length + encrypted.Length];
                miRijndael.IV.CopyTo(returnValue, 0);
                encrypted.CopyTo(returnValue, miRijndael.IV.Length);
            }
            catch (Exception ex) { throw ex; }
            finally { miRijndael.Clear(); }

            return returnValue;
        }

        /// <summary>
        /// Método de la clase SecurityProtocol. Utilizado para la desencryptación de datos. 
        /// </summary>
        /// <param name="toDecrypt">Array de bytes a ser desencryptado.</param>
        /// <param name="key"></param>
        /// <returns>Retorna un byte[] con los datos desencryptados.</returns>
        public static byte[] Decrypt(byte[] toDecrypt, byte[] key)
        {
            Rijndael miRijndael = Rijndael.Create();
            byte[] tempArray = new byte[miRijndael.IV.Length];
            byte[] encrypted = new byte[toDecrypt.Length - miRijndael.IV.Length];
            byte[] returnValue = { };

            try
            {
                miRijndael.Key = key;
                Array.Copy(toDecrypt, tempArray, tempArray.Length);
                Array.Copy(toDecrypt, tempArray.Length, encrypted, 0, encrypted.Length);
                miRijndael.IV = tempArray;
                returnValue = (miRijndael.CreateDecryptor()).TransformFinalBlock(encrypted, 0, encrypted.Length);
            }
            catch (Exception ex) { throw ex; }
            finally { miRijndael.Clear(); }

            return returnValue;
        }

        /// <summary>
        /// Método de la clase SecurityProtocol. Utilizado para la encryptación de datos.
        /// </summary>
        /// <param name="toEncrypt">Array de bytes a ser encryptado.</param>
        /// <returns>Retorna un byte[] con los datos encryptados.</returns>
        public static byte[] EncryptRijndael(byte[] toEncrypt)
        {
            return Encrypt(toEncrypt, (new PasswordDeriveBytes(Key, null)).GetBytes(32));
        }

        /// <summary>
        /// Método de la clase SecurityProtocol. Utilizado para la desencryptación de datos. 
        /// </summary>
        /// <param name="toDecrypt">Array de bytes a ser desencryptado.</param>
        /// <returns>Retorna un byte[] con los datos desencryptados.</returns>
        public static byte[] DecryptRijndael(byte[] toDescrypt)
        {
            return Decrypt(toDescrypt, (new PasswordDeriveBytes(Key, null)).GetBytes(32));
        }

        #endregion

        #region String

        /// <summary>
        /// Método de la clase SecurityProtocol. Utilizado para la encryptación de datos.
        /// </summary>
        /// <param name="toEncrypt">String a ser desencryptado.</param>
        /// <returns>Retorna un string con los datos desencryptados.</returns>
        public static string EncryptString(string toEncrypt)
        {
            byte[] stringInBytes = Encoding.UTF8.GetBytes(toEncrypt);
            byte[] resoult = SecurityProtocolService.EncryptTripleDES(stringInBytes);

            return Convert.ToBase64String(resoult);
        }

        /// <summary>
        /// Método de la clase SecurityProtocol. Utilizado para la desencryptación de datos.
        /// </summary>
        /// <param name="toDecrypt">String a ser encryptado.</param>
        /// <returns>Retorna un string con los datos encryptados.</returns>
        public static string DecryptString(string toDecrypt)
        {
            byte[] stringInBytes = Convert.FromBase64String(toDecrypt);
            byte[] resoult = SecurityProtocolService.DecryptTripleDES(stringInBytes);

            return UnicodeEncoding.UTF8.GetString(resoult);
        }

        #endregion
    }
}
