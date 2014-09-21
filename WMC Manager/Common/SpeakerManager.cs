using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;
using System.Security;
using CoreAudioApi;

public class SpeakerManager
{
    static string audioProfilesPath = "Audio Profiles";
    static string UserDomain;
    static string UserName;
    static SecureString UserSecurePassword;

    public static void SetCredentials(string userName, string userPassword, string userDomain = null)
    {
        UserSecurePassword = new SecureString();
        for (int i = 0; i < userPassword.Length; i++)
        {
            UserSecurePassword.AppendChar(userPassword[i]);
        }
        UserName = userName;
        UserDomain = userDomain;
    }
    public static string ChangeSoundProfile()
    {
        if (!Directory.Exists(audioProfilesPath))
        {
            Directory.CreateDirectory(audioProfilesPath);
        }
        string deviceDesc = DefaultDeviceDesc;
        string[] audioProfiles = Directory.GetFiles(audioProfilesPath, string.Format("*{0}*.reg", deviceDesc), SearchOption.TopDirectoryOnly).OrderByDescending(d => new FileInfo(d).CreationTime).ToArray();
        if (audioProfiles.Length > 1)
        {
            bool defaultMatch = false;
            for (int i = 0; i < audioProfiles.Length; i++)
            {
                FileInfo myAudioProfile = new FileInfo(audioProfiles[i]);
                if (myAudioProfile.Name.IndexOf("Default") > -1)
                {
                    string[] parseDesc = myAudioProfile.Name.Split(new char[] { '(', ')' });
                    if (parseDesc.Length == 3)
                    {
                        //File.Delete(myAudioProfile.FullName);
                        File.WriteAllText(myAudioProfile.FullName, saveCurrentProfile(parseDesc[1]));
                    }
                    File.Move(myAudioProfile.FullName, myAudioProfile.FullName.Replace("Default", ""));
                    int nextIndex = i + 1;
                    bool isEnd = nextIndex >= audioProfiles.Length;
                    if (isEnd)
                    {
                        nextIndex = 0;
                    }
                    FileInfo myNextAudioProfile = new FileInfo(audioProfiles[nextIndex]);
                    importRegProfile(myNextAudioProfile.FullName);
                    File.Move(myNextAudioProfile.FullName, myNextAudioProfile.FullName.Replace(myNextAudioProfile.Name, "Default" + myNextAudioProfile.Name));
                    if (!isEnd)
                    {
                        setDeviceByID(getDeviceIDByDesc(deviceDesc));
                    }
                    else
                    {
                        ChangeSoundCard();
                    }
                    defaultMatch = true;
                    break;
                }
            }
            if (!defaultMatch)
            {
                FileInfo myDefaultAudioProfile = new FileInfo(audioProfiles[0]);
                importRegProfile(myDefaultAudioProfile.FullName);
                setDeviceByID(getDeviceIDByDesc(deviceDesc));
                File.Move(myDefaultAudioProfile.FullName, myDefaultAudioProfile.FullName.Replace(myDefaultAudioProfile.Name, "Default" + myDefaultAudioProfile.Name));
                defaultMatch = true;
            }
        }
        else
        {
            ChangeSoundCard();
        }
        return CurrentSoundProfile + Environment.NewLine + "Volume Reset To: " + FixDefaultAudioLevel() + "%";
    }

    static void CreateSoundProfile()
    {
    }
    static string ChangeSoundCard()
    {
        Process compiler = new Process();
        compiler.StartInfo.FileName = "EndPointController.exe";
        compiler.StartInfo.UseShellExecute = false;
        compiler.StartInfo.RedirectStandardOutput = true;
        compiler.StartInfo.CreateNoWindow = true;
        compiler.Start();
        string[] procOutput = compiler.StandardOutput.ReadToEnd().Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
        compiler.WaitForExit();
        List<int> myDeviceIDs = new List<int>();
        int defaultDeviceID = getDeviceIDByDesc(DefaultDeviceDesc);
        for (int i = 0; i < procOutput.Length; i++)
        {
            int parsedID = parseID(procOutput[i]);
            if (parsedID > -1)
            {
                myDeviceIDs.Add(parsedID);
            }
        }
        for (int i = 0; i < myDeviceIDs.Count; i++)
        {
            if (myDeviceIDs[i] == defaultDeviceID)
            {
                int nextIndex = i + 1;
                if (nextIndex >= myDeviceIDs.Count)
                {
                    nextIndex = 0;
                }
                setDeviceByID(nextIndex);
                break;
            }
        }
        return DefaultDeviceDesc;
    }
    static string CurrentSoundProfile
    {
        get
        {
            if (!Directory.Exists(audioProfilesPath))
            {
                Directory.CreateDirectory(audioProfilesPath);
            }
            string[] audioProfiles = Directory.GetFiles(audioProfilesPath, string.Format("*{0}*.reg", DefaultDeviceDesc));
            string profileDesc = "Default";
            if (audioProfiles.Length > 1)
            {
                for (int i = 0; i < audioProfiles.Length; i++)
                {
                    FileInfo myAudioProfile = new FileInfo(audioProfiles[i]);
                    if (myAudioProfile.Name.IndexOf("Default") > -1)
                    {
                        string[] parseDesc = myAudioProfile.Name.Split(new char[] { '(', ')' });
                        if (parseDesc.Length == 3)
                        {
                            profileDesc = parseDesc[1];
                        }
                        break;
                    }
                }
            }
            return DefaultDeviceDesc + ": " + profileDesc;
        }
    }
    public static void ChangeSpeakerProfile()
    {
        CurrentSpeakerProfileID++;
    }
    static int currentSpeakerIndexTemp = 0;
    static int CurrentSpeakerProfileID
    {
        get
        {
            if (currentSpeakerIndexTemp > 4)
            {
                currentSpeakerIndexTemp = 0;
            }
            return currentSpeakerIndexTemp;
        }
        set
        {
            currentSpeakerIndexTemp = value;
        }
    }
    public static string CurrentSpeakerMap
    {
        get
        {
            switch (CurrentSpeakerProfileID)
            {
                default:
                    return string.Format("L-O-R{0}--O--{0}O-X-O{0}--O--{0}O---O{0}", Environment.NewLine);
                case 1:
                    return string.Format("X-X-X{0}--X--{0}L-O-R{0}--O--{0}O---O{0}", Environment.NewLine);
                case 2:
                    return string.Format("O-X-O{0}--O--{0}R-O-L{0}--X--{0}X---X{0}", Environment.NewLine);
                case 3:
                    return string.Format("O-X-L{0}--O-O{0}O---R{0}--X--{0}X---X{0}", Environment.NewLine);
                case 4:
                    return string.Format("X-X-X{0}--X--{0}O---L{0}--O-O{0}O---R{0}", Environment.NewLine);
            }
        }
    }
    static string DefaultDeviceDesc
    {
        get
        {
            MMDevice myDevice = new MMDeviceEnumerator().GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia);
            if (myDevice == null)
            {
                throw new Exception("Could not find sound card!");
            }
            return myDevice.FriendlyName;
        }
    }
    static int FixDefaultAudioLevel()
    {
        //device.AudioEndpointVolume.MasterVolumeLevelScalar
        Thread.Sleep(150);
        MMDevice myDevice = new MMDeviceEnumerator().GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia);
        if (myDevice == null)
        {
            throw new Exception("Could not find sound card!");
        }
        
        float originalValue = myDevice.AudioEndpointVolume.MasterVolumeLevelScalar;
        myDevice.AudioEndpointVolume.VolumeStepDown();
        myDevice.AudioEndpointVolume.VolumeStepUp();
        myDevice.AudioEndpointVolume.MasterVolumeLevelScalar = originalValue;
        return (int)(myDevice.AudioEndpointVolume.MasterVolumeLevelScalar * 100);
    }
    static void importRegProfile(string profilePath)
    {
        ProcessStartInfo psi = new ProcessStartInfo
        {
            FileName = @"C:\Windows\regedit.exe",
            UserName = UserName,
            Domain = UserDomain,
            Password = UserSecurePassword,
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true,
            Arguments = "/s \"" + profilePath + "\""
        };
        Process.Start(psi).WaitForExit();
    }
    public static string saveCurrentProfile(string desc)
    {
        if (!Directory.Exists(audioProfilesPath))
        {
            Directory.CreateDirectory(audioProfilesPath);
        }
        MMDevice myDevice = new MMDeviceEnumerator().GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia);
        if (myDevice == null)
        {
            throw new Exception("Could not find sound card!");
        }

        string exportPath = Path.Combine(new DirectoryInfo(audioProfilesPath).FullName, "tmp.reg");

        //string exportPath = Path.Combine(new DirectoryInfo(audioProfilesPath).FullName, string.Format("{0}({1}).reg", myDevice.FriendlyName, desc));
        string registryPath = string.Format(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\MMDevices\Audio\Render\{0}\Properties", System.Text.RegularExpressions.Regex.Matches(myDevice.ID, @"(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}")[0]);
        string path = "\"" + exportPath + "\"";
        string key = "\"" + registryPath + "\"";
        //if (File.Exists(exportPath))
        if(true)
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = @"C:\Windows\regedit.exe",
                UserName = UserName,
                Domain = UserDomain,
                Password = UserSecurePassword,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
                Arguments = "/e " + path + " " + key + ""
            };
            Process.Start(psi).WaitForExit();
        }
        string result = File.ReadAllText(exportPath);
        File.Delete(exportPath);
        return result;
    }
    static int getDeviceIDByDesc(string deviceDesc)
    {
        Process compiler = new Process();
        compiler.StartInfo.FileName = "EndPointController.exe";
        compiler.StartInfo.UseShellExecute = false;
        compiler.StartInfo.RedirectStandardOutput = true;
        compiler.StartInfo.CreateNoWindow = true;
        compiler.Start();
        string[] procOutput = compiler.StandardOutput.ReadToEnd().Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
        compiler.WaitForExit();
        for (int i = 0; i < procOutput.Length; i++)
        {
            if (procOutput[i].IndexOf(deviceDesc) > -1)
            {
                return parseID(procOutput[i]);
            }
        }
        return -1;
    }
    static void setDeviceByID(int id)
    {
        Process compiler = new Process();
        compiler.StartInfo.FileName = "EndPointController.exe";
        compiler.StartInfo.Arguments = id.ToString();
        compiler.StartInfo.UseShellExecute = false;
        compiler.StartInfo.RedirectStandardOutput = true;
        compiler.StartInfo.CreateNoWindow = true;
        compiler.Start();
        compiler.WaitForExit();
    }
    static int parseID(string input)
    {
        string matchDescIndex = "Audio Device ";
        if (input.IndexOf(matchDescIndex) > -1)
        {
            return Convert.ToInt32(input.Substring(matchDescIndex.Length, input.IndexOf(":") - matchDescIndex.Length));
        }
        else
        {
            return -1;
        }
    }
}
