using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace USBIRToyv2
{
    public class DBManager
    {
        public static DBRemoteList myRemotes;

        public static void InitializeDB(string file)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(DBRemoteList));
            StreamReader reader = new StreamReader(file);
            DBRemoteList myLoadedRemoteList = (DBRemoteList)serializer.Deserialize(reader);
            reader.Close();
            DBManager.myRemotes = myLoadedRemoteList;
        }

        public static void AddRemote(string RemoteDesc)
        {
            if (myRemotes == null)
            {
                myRemotes = new DBRemoteList();
            }
            DBRemote myRemote = new DBRemote();
            myRemote.Frequency = 0;
            myRemote.PR2Code = 0;
            myRemote.RemoteDesc = RemoteDesc;
            myRemote.Keys = new DBRemoteKeyList();
            myRemotes.Add(myRemote);
        }

        public static DBRemoteKey GetDBRemoteKeyByValue(short[] IRPulseArray)
        {
            DBRemoteKey myTempRemoteKey = null;
            foreach (DBRemote myRemote in myRemotes)
            {
                foreach (DBRemoteKey myRemoteKey in myRemote.Keys)
                {
                    bool found = CompareTwoKeys(IRPulseArray, myRemoteKey.Value);
                    //if (!found)
                    //{
                    //    found = CompareTwoBrightKeys(IRPulseArray, myRemoteKey.Value);
                    //}
                    if (found)
                    {
                        myTempRemoteKey = myRemoteKey;
                        //Set Values from Remote
                        myTempRemoteKey.RemoteDesc = myRemote.RemoteDesc;
                        myTempRemoteKey.Frequency = myRemote.Frequency;
                        myTempRemoteKey.PR2Code = myRemote.PR2Code;
                        return myTempRemoteKey;
                    }
                }
            }
            return myTempRemoteKey;
        }
        public static DBRemoteKey GetDBRemoteKeyByDesc(string KeyDesc, string RemoteDesc = null)
        {
            DBRemoteList myTempDBRemoteList;
            if (RemoteDesc != null)
            {
                DBRemote myTempDBRemote = GetDBRemoteByDesc(RemoteDesc);
                if (myTempDBRemote == null)
                {
                    throw new Exception("No such Remote found in DB!");
                }
                else
                {
                    myTempDBRemoteList = new DBRemoteList();
                    myTempDBRemoteList.Add(myTempDBRemote);
                }
            }
            else
            {
                myTempDBRemoteList = myRemotes;
            }


            DBRemoteKey result = null;
            bool found = false;
            foreach (DBRemote myRemote in myTempDBRemoteList)
            {
                foreach(DBRemoteKey myRemoteKey in myRemote.Keys)
                {
                    if (myRemoteKey.KeyDesc == KeyDesc)
                    {
                        result = myRemoteKey;
                        //Set Values from Remote
                        result.RemoteDesc = myRemote.RemoteDesc;
                        result.Frequency = myRemote.Frequency;
                        result.PR2Code = myRemote.PR2Code;
                        found = true;
                        break;
                    }
                }
                if (found)
                {
                    break;
                }
            }
            return result;
        }
        public static DBRemote GetDBRemoteByDesc(string RemoteDesc)
        {
            DBRemote myTempRemote = null;
            foreach (DBRemote myRemote in myRemotes)
            {
                if (RemoteDesc == myRemote.RemoteDesc)
                {
                    myTempRemote = myRemote;
                    break;
                }
            }
            return myTempRemote;
        }
        public static bool CompareTwoKeys(short[] IRPulseArray, short[] myRemoteKey)
        {
            if (IRPulseArray.Length > myRemoteKey.Length)
            {
                return false;
            }
            bool found = true;
            try
            {
                int diff = myRemoteKey.Length - IRPulseArray.Length;
                int diff_com = 0;
                if (diff <= 5)
                {
                    diff_com = diff;
                }
                else
                {
                    return false;
                }
                //if (diff > 0 && diff < 3)
                //{
                //    byte[] temp = new byte[IRPulseArray.Length+diff];
                //    System.Buffer.BlockCopy(IRPulseArray, 0, temp, diff-1, temp.Length);
                //    //IRPulseArray = temp;
                //    string srr = string.Empty;
                //    foreach (byte bb in temp)
                //    {
                //        srr += bb.ToString() + ", ";
                //    }
                //    System.Diagnostics.Debug.Print(srr);
                //}
                for (int x = diff_com; x < myRemoteKey.Length; x++)
                {
                    int currentIRValue = IRPulseArray[x - diff_com];
                    //if (x < 2 && myRemoteKey[x] - 100 <= currentIRValue && currentIRValue <= myRemoteKey[x] + 100)
                    if (x < 2)
                    {
                        continue;
                    }
                    else if (myRemoteKey[x] - 10 <= currentIRValue && currentIRValue <= myRemoteKey[x] + 10)
                    {
                        continue;
                    }
                    else
                    {
                        found = false;
                        break;
                    }
                }
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
                found = false;
            }
            return found;
        }
        public static bool CompareTwoBrightKeys(short[] IRPulseArray, short[] myRemoteKey)
        {
            if (IRPulseArray.Length > myRemoteKey.Length)
            {
                return false;
            }
            bool found = true;
            try
            {
                bool forward = false;
                bool backward = false;
                for (int x = 0; x < myRemoteKey.Length; x++)
                {
                    int currentIRValue = IRPulseArray[x];
                    if (x < 2 && myRemoteKey[x] - 50 <= currentIRValue && currentIRValue <= myRemoteKey[x] + 50)
                    {
                        continue;
                    }
                    else if (myRemoteKey[x] - 10 <= currentIRValue && currentIRValue <= myRemoteKey[x] + 10)
                    {
                        continue;
                    }
                    else if (myRemoteKey[x] - 10 <= IRPulseArray[x + 1] && IRPulseArray[x + 1] <= myRemoteKey[x] + 10)
                    {
                        forward = true;
                        if (backward)
                        {
                            found = false;
                            break;
                        }
                        continue;
                    }
                    else if (myRemoteKey[x + 1] - 10 <= currentIRValue && currentIRValue <= myRemoteKey[x + 1] + 10)
                    {
                        backward = true;
                        if (forward)
                        {
                            found = false;
                            break;
                        }
                        continue;
                    }
                    else
                    {
                        found = false;
                        break;
                    }
                }
            }
            catch
            {
                //found = false;
            }
            return found;
        }



        public static byte[] PrepareKeyForSend(short[] Key)
        {
            List<byte[]> myTempList = new List<byte[]>();
            foreach (short Value in Key)
            {
                myTempList.Add(BitConverter.GetBytes(Value));
            }

            byte[] Result = new byte[myTempList.Count * 2];
            int count = 0;
            for (int i = 0; i < myTempList.Count; i += 2)
            {
                if (BitConverter.IsLittleEndian)
                {
                    Array.Reverse(myTempList[count]);
                }
                Result[i] = myTempList[count][0];
                Result[i + 1] = myTempList[count][1];
                count++;
            }
            return Result;
        }
    }

    [Serializable]
    public class DBRemoteList : List<DBRemote> { }
    [Serializable]
    public class DBRemote
    {
        public decimal Frequency { get; set; }
        public byte PR2Code { get; set; }
        public string RemoteDesc { get; set; }
        public DBRemoteKeyList Keys { get; set; }
    }
    [Serializable]
    public class DBRemoteKeyList : List<DBRemoteKey> { }
    [Serializable]
    public class DBRemoteKey
    {
        public DBRemoteKey()
        {
        }
        public decimal Frequency { get; set; }
        public byte PR2Code { get; set; }
        public string RemoteDesc { get; set; }
        public string KeyDesc { get; set; }
        public short[] Value { get; set; }
    }
}
