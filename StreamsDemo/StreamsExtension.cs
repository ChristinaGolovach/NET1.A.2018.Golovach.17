﻿using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace StreamsDemo
{
    // C# 6.0 in a Nutshell. Joseph Albahari, Ben Albahari. O'Reilly Media. 2015
    // Chapter 15: Streams and I/O
    // Chapter 6: Framework Fundamentals - Text Encodings and Unicode
    // https://msdn.microsoft.com/ru-ru/library/system.text.encoding(v=vs.110).aspx

    public static class StreamsExtension
    {

        #region Public members

        #region TODO: Implement by byte copy logic using class FileStream as a backing store stream .

        public static int ByByteCopy(string sourcePath, string destinationPath)
        {
            InputValidation(sourcePath, destinationPath);

            Stream destinationStream = null;
            int byteCount = 0;
            int i = 0;
            try
            {
                destinationStream = new FileStream(destinationPath, FileMode.Create);

                using (Stream sourceStream = new FileStream(sourcePath, FileMode.Open))
                {
                    while (i++ < sourceStream.Length)
                    {
                        byte writedByte = (byte)sourceStream.ReadByte();
                        destinationStream.WriteByte(writedByte);
                        byteCount++;
                    }
                }
            }
            finally
            {
                if (destinationStream != null)
                {
                    destinationStream.Dispose();
                }
            }

            return byteCount;
        }

        #endregion

        #region TODO: Implement by byte copy logic using class MemoryStream as a backing store stream.

        public static int InMemoryByByteCopy(string sourcePath, string destinationPath)
        {
            InputValidation(sourcePath, destinationPath);

            string sourceText = "";
            int byteCount = 0;

            // TODO: step 1. Use StreamReader to read entire file in string
            using (StreamReader streamReader = new StreamReader(sourcePath, Encoding.ASCII))
            {
                sourceText = streamReader.ReadToEnd();          
            }

            // TODO: step 2. Create byte array on base string content - use  System.Text.Encoding class
            byte[] sourceBytes = Encoding.UTF8.GetBytes(sourceText);
            byte[] destinationBytes = new byte[sourceBytes.Length];

            // TODO: step 3. Use MemoryStream instance to read from byte array (from step 2)
            // TODO: step 4. Use MemoryStream instance (from step 3) to write it content in new byte array
            using (Stream memoryStream = new MemoryStream(sourceBytes))
            {
                for (int i = 0; i < sourceBytes.Length; i++)
                {
                    destinationBytes[i] = (byte)memoryStream.ReadByte();
                }
            }

            // TODO: step 5. Use Encoding class instance (from step 2) to create char array on byte array content
            char[] destinationText = Encoding.ASCII.GetChars(destinationBytes);

            // TODO: step 6. Use StreamWriter here to write char array content in new file
            using (StreamWriter streamWriter = new StreamWriter(destinationPath))
            {
                for (int i = 0; i < destinationText.Length; i++)
                {
                    streamWriter.Write(destinationText[i]);
                    byteCount++;
                }
            }

            return byteCount;
        }

        #endregion

        #region TODO: Implement by block copy logic using FileStream buffer.

        public static int ByBlockCopy(string sourcePath, string destinationPath)
        {
            InputValidation(sourcePath, destinationPath);

            Stream destinationStream = null;
            int bytePortion = 1024;
            int byteCount = 0;
            try
            {
                destinationStream = new FileStream(destinationPath, FileMode.Create);

                using (Stream sourceStream = new FileStream(sourcePath, FileMode.Open))
                {
                    long length = sourceStream.Length;
                    byte[] sourceBytes = new byte[bytePortion];

                    while (byteCount < sourceStream.Length)
                    {
                        byteCount += sourceStream.Read(sourceBytes, 0, bytePortion-1);                       
                        destinationStream.Write(sourceBytes, 0, bytePortion-1);
                    }                
                }
            }
            finally
            {
                if (destinationStream != null)
                {
                    destinationStream.Dispose();
                }
            }

            return byteCount;
        }

        #endregion

        #region TODO: Implement by block copy logic using MemoryStream.

        public static int InMemoryByBlockCopy(string sourcePath, string destinationPath)
        {
            // TODO: Use InMemoryByByteCopy method's approach
            throw new NotImplementedException();
        }

        #endregion

        #region TODO: Implement by block copy logic using class-decorator BufferedStream.

        public static int BufferedCopy(string sourcePath, string destinationPath)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region TODO: Implement by line copy logic using FileStream and classes text-adapters StreamReader/StreamWriter

        public static int ByLineCopy(string sourcePath, string destinationPath)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region TODO: Implement content comparison logic of two files 

        public static bool IsContentEquals(string sourcePath, string destinationPath)
        {
            throw new NotImplementedException();
        }

        #endregion

        #endregion

        #region Private members

        #region TODO: Implement validation logic

        private static void InputValidation(string sourcePath, string destinationPath)
        {
            if (string.IsNullOrEmpty(sourcePath))
            {
                throw new ArgumentException($"The {nameof(sourcePath)} can not be null or empty.");
            }

            if (string.IsNullOrEmpty(destinationPath))
            {
                throw new ArgumentException($"The {nameof(destinationPath)} can not be null or empty.");
            }

            if (!File.Exists(sourcePath))
            {
                throw new ArgumentException($"File does not exists for path {sourcePath}.");
            }
        }

        #endregion

        #endregion

    }
}