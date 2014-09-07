using System;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace Common
{
    public class ValidationManager
    {
        public static bool IsNullOrEmpty(object inValue)
        {
            bool bEmpty = false;
            try
            {
                if (inValue.ToString() == null || inValue.ToString().Replace(" ","").Length == 0)
                {
                    bEmpty = true;
                }
            }
            catch
            {
                bEmpty = true;
            }
            return bEmpty;
        }

        public static bool IsDate(object inValue)
        {
            bool bValid;
            try
            {
                DateTime myDT = DateTime.Parse(inValue.ToString());
                bValid = true;
            }
            catch
            {
                bValid = false;
            }
            if (bValid)
            {
                if (Convert.ToDateTime(inValue.ToString()) < Convert.ToDateTime("01/01/1901"))
                {
                    bValid = false;
                }
            }
            return bValid;
        }

        public static bool IsNumeric(object inValue)
        {
            bool bValid;
            try
            {
                Double.Parse(inValue.ToString());
                bValid = true;
            }
            catch
            {
                bValid = false;
            }

            return bValid;
        }

        public static bool IsBoolean(object inValue)
        {
            bool bValid;
            try
            {
                if(IsNumeric(inValue))
                {
                    if(Convert.ToInt32(inValue)==1)
                    {
                        bValid = true;
                    }
                    else
                    {
                        bValid = false;
                    }
                }
                else
                {
                    Boolean.Parse(inValue.ToString().ToLower());
                    bValid = true;
                }
            }
            catch
            {
                bValid = false;
            }

            return bValid;
        }

        public static object IIF(bool condition, object left, object right)
        {
            return condition ? left : right;
        }

        public static bool IsFileLocked(string file)
        {
            FileStream stream = null;
            FileInfo myFileInfo = new FileInfo(file);
            try
            {
                stream = myFileInfo.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            //file is not locked
            return false;
        }
    }
}