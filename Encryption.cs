using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Windows.Forms;
using System.Xml;

namespace DataApp
{
    internal class Encryption
    {
        //===========================This method encrypts data=================================
        public static void Encrypt(XmlDocument Doc, SymmetricAlgorithm Key)
        {
            string ElementName = "config";

            XmlElement elementToEncrypt = Doc.GetElementsByTagName(ElementName)[0] as XmlElement;

            if (elementToEncrypt == null)
            {
                throw new XmlException("The specified element was not found");
            }

            EncryptedXml eXml = new();

            byte[] encryptedElement = eXml.EncryptData(elementToEncrypt, Key, false);

            EncryptedData edElement = new()
            {
                Type = EncryptedXml.XmlEncElementUrl
            };

            string encryptionMethod = null;

            if (Key is Aes)
            {
                encryptionMethod = EncryptedXml.XmlEncAES256Url;
            }
            else
            {
                throw new CryptographicException("The specified algorithm is not supported or not recommended for XML Encryption.");
            }

            edElement.EncryptionMethod = new EncryptionMethod(encryptionMethod);
            edElement.CipherData.CipherValue = encryptedElement;

            EncryptedXml.ReplaceElement(elementToEncrypt, edElement, false);
        }
        //=====================================================================================

        //============================This method decrypts data================================
        public static void Decrypt(XmlDocument Doc, SymmetricAlgorithm Alg)
        {
            XmlElement encryptedElement = Doc.GetElementsByTagName("EncryptedData")[0] as XmlElement;

            if (encryptedElement == null)
            {
                throw new XmlException("The EncryptedData element was not found.");
            }

            EncryptedData edElement = new();
            edElement.LoadXml(encryptedElement);

            EncryptedXml exml = new();

            byte[] rgbOutput = exml.DecryptData(edElement, Alg);

            exml.ReplaceData(encryptedElement, rgbOutput);
        }
        //=====================================================================================
    }
}
