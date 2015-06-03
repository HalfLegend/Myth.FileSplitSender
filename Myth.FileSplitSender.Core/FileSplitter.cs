using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Security.Cryptography;
namespace Myth.FileSplitSender.Core
{
    public static class FileSplitter
    {
        private const string SeperateFileExtension = ".MythSep";

        public static Task SplitFileAsync(string srcFilePath, string targetFolderPath, long seperateFileSizeByByte,
            Action<string, double> progressUpdateAction)
        {
            Task t = new Task(() =>
            {
                FileStream fileStream = new FileStream(srcFilePath, FileMode.Open);
                string shortFileName = Path.GetFileName(srcFilePath);

                uint fileIndex = 0;

                byte[] buffer = new byte[seperateFileSizeByByte];

                int sizeRead;
                long maxSize = fileStream.Length;
                long totalRead = 0;
                //文件格式
                do
                {
                    sizeRead = fileStream.Read(buffer, 0, (int) seperateFileSizeByByte);
                    totalRead += sizeRead;
                    if (sizeRead == 0)
                    {
                        break;
                    }
                    Debug.Assert(shortFileName != null, "shortFileName != null");
                    string targetFilePath = Path.Combine(targetFolderPath, shortFileName);
                    targetFilePath += "." + fileIndex.ToString("0000") + SeperateFileExtension;
                    using (FileStream fileToWrite = new FileStream(targetFilePath, FileMode.CreateNew))
                    {
                        fileToWrite.Write(buffer, 0, sizeRead);
                    }
                    progressUpdateAction(targetFilePath + "已写入", (double)totalRead / maxSize);
                    fileIndex++;
                    MD5 md5 = new MD5Cng();
                    
                } while (sizeRead >= seperateFileSizeByByte);
                //progressUpdateAction("已完成", 1.0);
            });

            t.Start();
            return t;
        }
    }
}
