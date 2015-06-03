using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outlook = Microsoft.Office.Interop.Outlook;
namespace Myth.FileSplitSender.Core
{
    public static class FileSender
    {
        public static void SendFileByOutlook(string srcFolderPath, string targetEmailAddress)
        {
            if (!Directory.Exists(srcFolderPath))
            {
                return;
            }

            DirectoryInfo srcFolder = new DirectoryInfo(srcFolderPath);
            Outlook.Application app = new Outlook.ApplicationClass();
            foreach (FileInfo file in srcFolder.EnumerateFiles())
            {
                Outlook.MailItem mail = (Outlook.MailItem)app.CreateItem(Outlook.OlItemType.olMailItem);
                mail.To = targetEmailAddress;
                mail.Attachments.Add(file.FullName, Outlook.OlAttachmentType.olByValue, Type.Missing, Type.Missing);
                mail.Subject = Path.GetFileNameWithoutExtension(Path.GetFileNameWithoutExtension(file.Name));
                mail.Body = file.FullName;
                //mail.Display(true);
                mail.Send();
            }
        }
    }
}
