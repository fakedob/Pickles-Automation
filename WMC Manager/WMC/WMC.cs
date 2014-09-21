using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.MediaCenter.Samples.MediaState;

namespace WMC_Manager
{
    public static class WMC
    {
        static MediaState ms;
        public delegate void MceEvent(string eventName);

        public static MceEvent OnMCEEvent;

        public static void Initialize()
        {
            ms = new MediaState();
            HookEvents(); //typed first
            HookEvent(); //generic
            ms.Connect();
        }

        private static void Dump(string message)
        {
            if (OnMCEEvent != null)
            {
                OnMCEEvent(message);
            }
            //System.Diagnostics.Debug.Print(message);
        }

        private static void HookEvent()
        {
            ms.OnMSASEvent += new Microsoft.MediaCenter.Samples.MediaState.MediaState.MSASEventHandler(ms_OnMSASEvent);
        }

        private static void ms_OnMSASEvent(object state, MediaStatusEventArgs args)
        {
            MediaState typedState = (MediaState)state;
            //Dump("MediaState_OnMSASEvent");
            string strOut = "     " + args.Session.ToString() + " " + args.SessionID.ToString() + " " + args.Tag.ToString();
            strOut = strOut + " " + args.Value.ToString();

            /*
            string strVal = String.Empty;
            ValueVariant v = args.Value;
            switch(v.Type)
            {
                case ValueDataType.Bool:
                    strVal = v.Bool.ToString();
                    break;
                case ValueDataType.Int:
                    strVal = v.Int.ToString();
                    break;
                case ValueDataType.None:
                    break;
                case ValueDataType.Str:
                    strVal = v.Str;
                    break;
                case ValueDataType.Tag:
                    strVal = v.ToString(); //TODO
                    break;
            }
            strOut = strOut + " " + strVal;
            */

            Dump(strOut);
        }

        private static void HookEvents()
        {
            ms.MediaCenter.EjectingChanged += new EventHandler(MediaCenter_EjectingChanged);
            ms.MediaCenter.Ended += new EventHandler(MediaCenter_Ended);
            ms.MediaCenter.ErrorChanged += new EventHandler(MediaCenter_ErrorChanged);
            ms.MediaCenter.Forwarding += new EventHandler(MediaCenter_Forwarding);
            ms.MediaCenter.GuideLoadedChanged += new EventHandler(MediaCenter_GuideLoadedChanged);
            ms.MediaCenter.MediaChanged += new EventHandler(MediaCenter_MediaChanged);
            ms.MediaCenter.MuteChanged += new EventHandler(MediaCenter_MuteChanged);
            ms.MediaCenter.NavigationChanged += new EventHandler(MediaCenter_NavigationChanged);
            ms.MediaCenter.Pausing += new EventHandler(MediaCenter_Pausing);
            ms.MediaCenter.Playing += new EventHandler(MediaCenter_Playing);
            ms.MediaCenter.Rewinding += new EventHandler(MediaCenter_Rewinding);
            ms.MediaCenter.Started += new EventHandler(MediaCenter_Started);
            ms.MediaCenter.Stopping += new EventHandler(MediaCenter_Stopping);
            ms.MediaCenter.VolumeChanged += new EventHandler(MediaCenter_VolumeChanged);

            ms.CD.EjectingChanged += new EventHandler(CD_EjectingChanged);
            ms.CD.Ended += new EventHandler(CD_Ended);
            ms.CD.ErrorChanged += new EventHandler(CD_ErrorChanged);
            ms.CD.Forwarding += new EventHandler(CD_Forwarding);
            ms.CD.GuideLoadedChanged += new EventHandler(CD_GuideLoadedChanged);
            ms.CD.MediaChanged += new EventHandler(CD_MediaChanged);
            ms.CD.Pausing += new EventHandler(CD_Pausing);
            ms.CD.Playing += new EventHandler(CD_Playing);
            ms.CD.RepeatSetChanged += new EventHandler(CD_RepeatSetChanged);
            ms.CD.Rewinding += new EventHandler(CD_Rewinding);
            ms.CD.ShuffleChanged += new EventHandler(CD_ShuffleChanged);
            ms.CD.Started += new EventHandler(CD_Started);
            ms.CD.Stopping += new EventHandler(CD_Stopping);
            ms.CD.TrackTimeChanged += new EventHandler(CD_TrackTimeChanged);
            ms.CD.VisualizationChanged += new EventHandler(CD_VisualizationChanged);

            ms.DVD.ChapterChanged += new EventHandler(DVD_ChapterChanged);
            ms.DVD.EjectingChanged += new EventHandler(DVD_EjectingChanged);
            ms.DVD.Ended += new EventHandler(DVD_Ended);
            ms.DVD.ErrorChanged += new EventHandler(DVD_ErrorChanged);
            ms.DVD.Forwarding += new EventHandler(DVD_Forwarding);
            ms.DVD.GuideLoadedChanged += new EventHandler(DVD_GuideLoadedChanged);
            ms.DVD.MediaChanged += new EventHandler(DVD_MediaChanged);
            ms.DVD.Pausing += new EventHandler(DVD_Pausing);
            ms.DVD.Playing += new EventHandler(DVD_Playing);
            ms.DVD.Rewinding += new EventHandler(DVD_Rewinding);
            ms.DVD.Started += new EventHandler(DVD_Started);
            ms.DVD.Stopping += new EventHandler(DVD_Stopping);
            ms.DVD.TrackTimeChanged += new EventHandler(DVD_TrackTimeChanged);

            ms.Music.EjectingChanged += new EventHandler(Music_EjectingChanged);
            ms.Music.Ended += new EventHandler(Music_Ended);
            ms.Music.ErrorChanged += new EventHandler(Music_ErrorChanged);
            ms.Music.Forwarding += new EventHandler(Music_Forwarding);
            ms.Music.GuideLoadedChanged += new EventHandler(Music_GuideLoadedChanged);
            ms.Music.MediaChanged += new EventHandler(Music_MediaChanged);
            ms.Music.Pausing += new EventHandler(Music_Pausing);
            ms.Music.Playing += new EventHandler(Music_Playing);
            ms.Music.RepeatSetChanged += new EventHandler(Music_RepeatSetChanged);
            ms.Music.Rewinding += new EventHandler(Music_Rewinding);
            ms.Music.ShuffleChanged += new EventHandler(Music_ShuffleChanged);
            ms.Music.Started += new EventHandler(Music_Started);
            ms.Music.Stopping += new EventHandler(Music_Stopping);
            ms.Music.TrackTimeChanged += new EventHandler(Music_TrackTimeChanged);
            ms.Music.VisualizationChanged += new EventHandler(Music_VisualizationChanged);

            ms.PhoneCall.EjectingChanged += new EventHandler(PhoneCall_EjectingChanged);
            ms.PhoneCall.Ended += new EventHandler(PhoneCall_Ended);
            ms.PhoneCall.ErrorChanged += new EventHandler(PhoneCall_ErrorChanged);
            ms.PhoneCall.Forwarding += new EventHandler(PhoneCall_Forwarding);
            ms.PhoneCall.GuideLoadedChanged += new EventHandler(PhoneCall_GuideLoadedChanged);
            ms.PhoneCall.MediaChanged += new EventHandler(PhoneCall_MediaChanged);
            ms.PhoneCall.Pausing += new EventHandler(PhoneCall_Pausing);
            ms.PhoneCall.Playing += new EventHandler(PhoneCall_Playing);
            ms.PhoneCall.Rewinding += new EventHandler(PhoneCall_Rewinding);
            ms.PhoneCall.Started += new EventHandler(PhoneCall_Started);
            ms.PhoneCall.Stopping += new EventHandler(PhoneCall_Stopping);

            ms.Pictures.CurrentPictureChanged += new EventHandler(Pictures_CurrentPictureChanged);
            ms.Pictures.EjectingChanged += new EventHandler(Pictures_EjectingChanged);
            ms.Pictures.Ended += new EventHandler(Pictures_Ended);
            ms.Pictures.ErrorChanged += new EventHandler(Pictures_ErrorChanged);
            ms.Pictures.Forwarding += new EventHandler(Pictures_Forwarding);
            ms.Pictures.GuideLoadedChanged += new EventHandler(Pictures_GuideLoadedChanged);
            ms.Pictures.MediaChanged += new EventHandler(Pictures_MediaChanged);
            ms.Pictures.Pausing += new EventHandler(Pictures_Pausing);
            ms.Pictures.Playing += new EventHandler(Pictures_Playing);
            ms.Pictures.Rewinding += new EventHandler(Pictures_Rewinding);
            ms.Pictures.Started += new EventHandler(Pictures_Started);
            ms.Pictures.Stopping += new EventHandler(Pictures_Stopping);

            ms.Radio.EjectingChanged += new EventHandler(Radio_EjectingChanged);
            ms.Radio.Ended += new EventHandler(Radio_Ended);
            ms.Radio.ErrorChanged += new EventHandler(Radio_ErrorChanged);
            ms.Radio.Forwarding += new EventHandler(Radio_Forwarding);
            ms.Radio.FrequencyChanged += new EventHandler(Radio_FrequencyChanged);
            ms.Radio.GuideLoadedChanged += new EventHandler(Radio_GuideLoadedChanged);
            ms.Radio.MediaChanged += new EventHandler(Radio_MediaChanged);
            ms.Radio.Pausing += new EventHandler(Radio_Pausing);
            ms.Radio.Playing += new EventHandler(Radio_Playing);
            ms.Radio.Rewinding += new EventHandler(Radio_Rewinding);
            ms.Radio.Started += new EventHandler(Radio_Started);
            ms.Radio.Stopping += new EventHandler(Radio_Stopping);

            ms.TV.EjectingChanged += new EventHandler(TV_EjectingChanged);
            ms.TV.Ended += new EventHandler(TV_Ended);
            ms.TV.ErrorChanged += new EventHandler(TV_ErrorChanged);
            ms.TV.Forwarding += new EventHandler(TV_Forwarding);
            ms.TV.GuideLoadedChanged += new EventHandler(TV_GuideLoadedChanged);
            ms.TV.MediaChanged += new EventHandler(TV_MediaChanged);
            ms.TV.Pausing += new EventHandler(TV_Pausing);
            ms.TV.Playing += new EventHandler(TV_Playing);
            ms.TV.Rewinding += new EventHandler(TV_Rewinding);
            ms.TV.Started += new EventHandler(TV_Started);
            ms.TV.Stopping += new EventHandler(TV_Stopping);
            ms.TV.TrackTimeChanged += new EventHandler(TV_TrackTimeChanged);

            ms.TVRecorded.EjectingChanged += new EventHandler(TVRecorded_EjectingChanged);
            ms.TVRecorded.Ended += new EventHandler(TVRecorded_Ended);
            ms.TVRecorded.ErrorChanged += new EventHandler(TVRecorded_ErrorChanged);
            ms.TVRecorded.Forwarding += new EventHandler(TVRecorded_Forwarding);
            ms.TVRecorded.GuideLoadedChanged += new EventHandler(TVRecorded_GuideLoadedChanged);
            ms.TVRecorded.MediaChanged += new EventHandler(TVRecorded_MediaChanged);
            ms.TVRecorded.Pausing += new EventHandler(TVRecorded_Pausing);
            ms.TVRecorded.Playing += new EventHandler(TVRecorded_Playing);
            ms.TVRecorded.Rewinding += new EventHandler(TVRecorded_Rewinding);
            ms.TVRecorded.Started += new EventHandler(TVRecorded_Started);
            ms.TVRecorded.Stopping += new EventHandler(TVRecorded_Stopping);
            ms.TVRecorded.TrackTimeChanged += new EventHandler(TVRecorded_TrackTimeChanged);

            ms.TVRecording.EjectingChanged += new EventHandler(TVRecording_EjectingChanged);
            ms.TVRecording.Ended += new EventHandler(TVRecording_Ended);
            ms.TVRecording.ErrorChanged += new EventHandler(TVRecording_ErrorChanged);
            ms.TVRecording.Forwarding += new EventHandler(TVRecording_Forwarding);
            ms.TVRecording.GuideLoadedChanged += new EventHandler(TVRecording_GuideLoadedChanged);
            ms.TVRecording.MediaChanged += new EventHandler(TVRecording_MediaChanged);
            ms.TVRecording.Pausing += new EventHandler(TVRecording_Pausing);
            ms.TVRecording.Playing += new EventHandler(TVRecording_Playing);
            ms.TVRecording.Rewinding += new EventHandler(TVRecording_Rewinding);
            ms.TVRecording.Started += new EventHandler(TVRecording_Started);
            ms.TVRecording.Stopping += new EventHandler(TVRecording_Stopping);
            ms.TVRecording.TrackTimeChanged += new EventHandler(TVRecording_TrackTimeChanged);

            ms.Video.EjectingChanged += new EventHandler(Video_EjectingChanged);
            ms.Video.Ended += new EventHandler(Video_Ended);
            ms.Video.ErrorChanged += new EventHandler(Video_ErrorChanged);
            ms.Video.Forwarding += new EventHandler(Video_Forwarding);
            ms.Video.GuideLoadedChanged += new EventHandler(Video_GuideLoadedChanged);
            ms.Video.MediaChanged += new EventHandler(Video_MediaChanged);
            ms.Video.Pausing += new EventHandler(Video_Pausing);
            ms.Video.Playing += new EventHandler(Video_Playing);
            ms.Video.Rewinding += new EventHandler(Video_Rewinding);
            ms.Video.Started += new EventHandler(Video_Started);
            ms.Video.Stopping += new EventHandler(Video_Stopping);
            ms.Video.TrackTimeChanged += new EventHandler(Video_TrackTimeChanged);
        }

        #region MediaCenter
        private static void MediaCenter_EjectingChanged(object sender, EventArgs e)
        {
            Dump("MediaCenter_EjectingChanged");
        }

        private static void MediaCenter_Ended(object sender, EventArgs e)
        {
            Dump("MediaCenter_Ended");
        }

        private static void MediaCenter_ErrorChanged(object sender, EventArgs e)
        {
            Dump("MediaCenter_ErrorChanged");
        }

        private static void MediaCenter_Forwarding(object sender, EventArgs e)
        {
            Dump("MediaCenter_Forwarding");
        }

        private static void MediaCenter_GuideLoadedChanged(object sender, EventArgs e)
        {
            Dump("MediaCenter_GuideLoadedChanged");
        }

        private static void MediaCenter_MediaChanged(object sender, EventArgs e)
        {
            Dump("MediaCenter_MediaChanged");
        }

        private static void MediaCenter_MuteChanged(object sender, EventArgs e)
        {
            Dump("MediaCenter_MuteChanged");
        }

        private static void MediaCenter_NavigationChanged(object sender, EventArgs e)
        {
            Dump("MediaCenter_NavigationChanged");
        }

        private static void MediaCenter_Pausing(object sender, EventArgs e)
        {
            Dump("MediaCenter_Pausing");
        }

        private static void MediaCenter_Playing(object sender, EventArgs e)
        {
            Dump("MediaCenter_Playing");
        }

        private static void MediaCenter_Rewinding(object sender, EventArgs e)
        {
            Dump("MediaCenter_Rewinding");
        }

        private static void MediaCenter_Started(object sender, EventArgs e)
        {
            Dump("MediaCenter_Started");
        }

        private static void MediaCenter_Stopping(object sender, EventArgs e)
        {
            Dump("MediaCenter_Stopping");
        }

        private static void MediaCenter_VolumeChanged(object sender, EventArgs e)
        {
            Dump("MediaCenter_VolumeChanged");
        }
        #endregion

        #region CD
        private static void CD_EjectingChanged(object sender, EventArgs e)
        {
            Dump("CD_EjectingChanged");
        }

        private static void CD_Ended(object sender, EventArgs e)
        {
            Dump("CD_Ended");
        }

        private static void CD_ErrorChanged(object sender, EventArgs e)
        {
            Dump("CD_ErrorChanged");
        }

        private static void CD_Forwarding(object sender, EventArgs e)
        {
            Dump("CD_Forwarding");
        }

        private static void CD_GuideLoadedChanged(object sender, EventArgs e)
        {
            Dump("CD_GuideLoadedChanged");
        }

        private static void CD_MediaChanged(object sender, EventArgs e)
        {
            Dump("CD_MediaChanged");
        }

        private static void CD_Pausing(object sender, EventArgs e)
        {
            Dump("CD_Pausing");
        }

        private static void CD_Playing(object sender, EventArgs e)
        {
            Dump("CD_Playing");
        }

        private static void CD_RepeatSetChanged(object sender, EventArgs e)
        {
            Dump("CD_RepeatSetChanged");
        }

        private static void CD_Rewinding(object sender, EventArgs e)
        {
            Dump("CD_Rewinding");
        }

        private static void CD_ShuffleChanged(object sender, EventArgs e)
        {
            Dump("CD_ShuffleChanged");
        }

        private static void CD_Started(object sender, EventArgs e)
        {
            Dump("CD_Started");
        }

        private static void CD_Stopping(object sender, EventArgs e)
        {
            Dump("CD_Stopping");
        }

        private static void CD_TrackTimeChanged(object sender, EventArgs e)
        {
            Dump("CD_TrackTimeChanged");
        }

        private static void CD_VisualizationChanged(object sender, EventArgs e)
        {
            Dump("CD_VisualizationChanged");
        }
        #endregion

        #region DVD
        private static void DVD_ChapterChanged(object sender, EventArgs e)
        {
            Dump("DVD_ChapterChanged");
        }

        private static void DVD_EjectingChanged(object sender, EventArgs e)
        {
            Dump("DVD_EjectingChanged");
        }

        private static void DVD_Ended(object sender, EventArgs e)
        {
            Dump("DVD_Ended");
        }

        private static void DVD_ErrorChanged(object sender, EventArgs e)
        {
            Dump("DVD_ErrorChanged");
        }

        private static void DVD_Forwarding(object sender, EventArgs e)
        {
            Dump("DVD_Forwarding");
        }

        private static void DVD_GuideLoadedChanged(object sender, EventArgs e)
        {
            Dump("DVD_GuideLoadedChanged");
        }

        private static void DVD_MediaChanged(object sender, EventArgs e)
        {
            Dump("DVD_MediaChanged");
        }

        private static void DVD_Pausing(object sender, EventArgs e)
        {
            Dump("DVD_Pausing");
        }

        private static void DVD_Playing(object sender, EventArgs e)
        {
            Dump("DVD_Playing");
        }

        private static void DVD_Rewinding(object sender, EventArgs e)
        {
            Dump("DVD_Rewinding");
        }

        private static void DVD_Started(object sender, EventArgs e)
        {
            Dump("DVD_Started");
        }

        private static void DVD_Stopping(object sender, EventArgs e)
        {
            Dump("DVD_Stopping");
        }

        private static void DVD_TrackTimeChanged(object sender, EventArgs e)
        {
            Dump("DVD_TrackTimeChanged");
        }
        #endregion

        #region Music
        private static void Music_EjectingChanged(object sender, EventArgs e)
        {
            Dump("Music_EjectingChanged");
        }

        private static void Music_Ended(object sender, EventArgs e)
        {
            Dump("Music_Ended");
        }

        private static void Music_ErrorChanged(object sender, EventArgs e)
        {
            Dump("Music_ErrorChanged");
        }

        private static void Music_Forwarding(object sender, EventArgs e)
        {
            Dump("Music_Forwarding");
        }

        private static void Music_GuideLoadedChanged(object sender, EventArgs e)
        {
            Dump("Music_GuideLoadedChanged");
        }

        private static void Music_MediaChanged(object sender, EventArgs e)
        {
            Dump("Music_MediaChanged");
        }

        private static void Music_Pausing(object sender, EventArgs e)
        {
            Dump("Music_Pausing");
        }

        private static void Music_Playing(object sender, EventArgs e)
        {
            Dump("Music_Playing");
        }

        private static void Music_RepeatSetChanged(object sender, EventArgs e)
        {
            Dump("Music_RepeatSetChanged");
        }

        private static void Music_Rewinding(object sender, EventArgs e)
        {
            Dump("Music_Rewinding");
        }

        private static void Music_ShuffleChanged(object sender, EventArgs e)
        {
            Dump("Music_ShuffleChanged");
        }

        private static void Music_Started(object sender, EventArgs e)
        {
            Dump("Music_Started");
        }

        private static void Music_Stopping(object sender, EventArgs e)
        {
            Dump("Music_Stopping");
        }

        private static void Music_TrackTimeChanged(object sender, EventArgs e)
        {
            Dump("Music_TrackTimeChanged");
        }

        private static void Music_VisualizationChanged(object sender, EventArgs e)
        {
            Dump("Music_VisualizationChanged");
        }
        #endregion

        #region PhoneCall
        private static void PhoneCall_EjectingChanged(object sender, EventArgs e)
        {
            Dump("PhoneCall_EjectingChanged");
        }

        private static void PhoneCall_Ended(object sender, EventArgs e)
        {
            Dump("PhoneCall_Ended");
        }

        private static void PhoneCall_ErrorChanged(object sender, EventArgs e)
        {
            Dump("PhoneCall_ErrorChanged");
        }

        private static void PhoneCall_Forwarding(object sender, EventArgs e)
        {
            Dump("PhoneCall_Forwarding");
        }

        private static void PhoneCall_GuideLoadedChanged(object sender, EventArgs e)
        {
            Dump("PhoneCall_GuideLoadedChanged");
        }

        private static void PhoneCall_MediaChanged(object sender, EventArgs e)
        {
            Dump("PhoneCall_MediaChanged");
        }

        private static void PhoneCall_Pausing(object sender, EventArgs e)
        {
            Dump("PhoneCall_Pausing");
        }

        private static void PhoneCall_Playing(object sender, EventArgs e)
        {
            Dump("PhoneCall_Playing");
        }

        private static void PhoneCall_Rewinding(object sender, EventArgs e)
        {
            Dump("PhoneCall_Rewinding");
        }

        private static void PhoneCall_Started(object sender, EventArgs e)
        {
            Dump("PhoneCall_Started");
        }

        private static void PhoneCall_Stopping(object sender, EventArgs e)
        {
            Dump("PhoneCall_Stopping");
        }
        #endregion

        #region Pictures
        private static void Pictures_CurrentPictureChanged(object sender, EventArgs e)
        {
            Dump("Pictures_CurrentPictureChanged");
        }

        private static void Pictures_EjectingChanged(object sender, EventArgs e)
        {
            Dump("Pictures_EjectingChanged");
        }

        private static void Pictures_Ended(object sender, EventArgs e)
        {
            Dump("Pictures_Ended");
        }

        private static void Pictures_ErrorChanged(object sender, EventArgs e)
        {
            Dump("Pictures_ErrorChanged");
        }

        private static void Pictures_Forwarding(object sender, EventArgs e)
        {
            Dump("Pictures_Forwarding");
        }

        private static void Pictures_GuideLoadedChanged(object sender, EventArgs e)
        {
            Dump("Pictures_GuideLoadedChanged");
        }

        private static void Pictures_MediaChanged(object sender, EventArgs e)
        {
            Dump("Pictures_MediaChanged");
        }

        private static void Pictures_Pausing(object sender, EventArgs e)
        {
            Dump("Pictures_Pausing");
        }

        private static void Pictures_Playing(object sender, EventArgs e)
        {
            Dump("Pictures_Playing");
        }

        private static void Pictures_Rewinding(object sender, EventArgs e)
        {
            Dump("Pictures_Rewinding");
        }

        private static void Pictures_Started(object sender, EventArgs e)
        {
            Dump("Pictures_Started");
        }

        private static void Pictures_Stopping(object sender, EventArgs e)
        {
            Dump("Pictures_Stopping");
        }
        #endregion

        #region Radio
        private static void Radio_EjectingChanged(object sender, EventArgs e)
        {
            Dump("Radio_EjectingChanged");
        }

        private static void Radio_Ended(object sender, EventArgs e)
        {
            Dump("Radio_Ended");
        }

        private static void Radio_ErrorChanged(object sender, EventArgs e)
        {
            Dump("Radio_ErrorChanged");
        }

        private static void Radio_Forwarding(object sender, EventArgs e)
        {
            Dump("Radio_Forwarding");
        }

        private static void Radio_FrequencyChanged(object sender, EventArgs e)
        {
            Dump("Radio_FrequencyChanged");
        }

        private static void Radio_GuideLoadedChanged(object sender, EventArgs e)
        {
            Dump("Radio_GuideLoadedChanged");
        }

        private static void Radio_MediaChanged(object sender, EventArgs e)
        {
            Dump("Radio_MediaChanged");
        }

        private static void Radio_Pausing(object sender, EventArgs e)
        {
            Dump("Radio_Pausing");
        }

        private static void Radio_Playing(object sender, EventArgs e)
        {
            Dump("Radio_Playing");
        }

        private static void Radio_Rewinding(object sender, EventArgs e)
        {
            Dump("Radio_Rewinding");
        }

        private static void Radio_Started(object sender, EventArgs e)
        {
            Dump("Radio_Started");
        }

        private static void Radio_Stopping(object sender, EventArgs e)
        {
            Dump("Radio_Stopping");
        }
        #endregion

        #region TV
        private static void TV_EjectingChanged(object sender, EventArgs e)
        {
            Dump("TV_EjectingChanged");
        }

        private static void TV_Ended(object sender, EventArgs e)
        {
            Dump("TV_Ended");
        }

        private static void TV_ErrorChanged(object sender, EventArgs e)
        {
            Dump("TV_ErrorChanged");
        }

        private static void TV_Forwarding(object sender, EventArgs e)
        {
            Dump("TV_Forwarding");
        }

        private static void TV_GuideLoadedChanged(object sender, EventArgs e)
        {
            Dump("TV_GuideLoadedChanged");
        }

        private static void TV_MediaChanged(object sender, EventArgs e)
        {
            Dump("TV_MediaChanged");
        }

        private static void TV_Pausing(object sender, EventArgs e)
        {
            Dump("TV_Pausing");
        }

        private static void TV_Playing(object sender, EventArgs e)
        {
            Dump("TV_Playing");
        }

        private static void TV_Rewinding(object sender, EventArgs e)
        {
            Dump("TV_Rewinding");
        }

        private static void TV_Started(object sender, EventArgs e)
        {
            Dump("TV_Started");
        }

        private static void TV_Stopping(object sender, EventArgs e)
        {
            Dump("TV_Stopping");
        }

        private static void TV_TrackTimeChanged(object sender, EventArgs e)
        {
            Dump("TV_TrackTimeChanged");
        }
        #endregion

        #region TVRecorded
        private static void TVRecorded_EjectingChanged(object sender, EventArgs e)
        {
            Dump("TVRecorded_EjectingChanged");
        }

        private static void TVRecorded_Ended(object sender, EventArgs e)
        {
            Dump("TVRecorded_Ended");
        }

        private static void TVRecorded_ErrorChanged(object sender, EventArgs e)
        {
            Dump("TVRecorded_ErrorChanged");
        }

        private static void TVRecorded_Forwarding(object sender, EventArgs e)
        {
            Dump("TVRecorded_Forwarding");
        }

        private static void TVRecorded_GuideLoadedChanged(object sender, EventArgs e)
        {
            Dump("TVRecorded_GuideLoadedChanged");
        }

        private static void TVRecorded_MediaChanged(object sender, EventArgs e)
        {
            Dump("TVRecorded_MediaChanged");
        }

        private static void TVRecorded_Pausing(object sender, EventArgs e)
        {
            Dump("TVRecorded_Pausing");
        }

        private static void TVRecorded_Playing(object sender, EventArgs e)
        {
            Dump("TVRecorded_Playing");
        }

        private static void TVRecorded_Rewinding(object sender, EventArgs e)
        {
            Dump("TVRecorded_Rewinding");
        }

        private static void TVRecorded_Started(object sender, EventArgs e)
        {
            Dump("TVRecorded_Started");
        }

        private static void TVRecorded_Stopping(object sender, EventArgs e)
        {
            Dump("TVRecorded_Stopping");
        }

        private static void TVRecorded_TrackTimeChanged(object sender, EventArgs e)
        {
            Dump("TVRecorded_TrackTimeChanged");
        }
        #endregion

        #region TVRecording
        private static void TVRecording_EjectingChanged(object sender, EventArgs e)
        {
            Dump("TVRecording_EjectingChanged");
        }

        private static void TVRecording_Ended(object sender, EventArgs e)
        {
            Dump("TVRecording_Ended");
        }

        private static void TVRecording_ErrorChanged(object sender, EventArgs e)
        {
            Dump("TVRecording_ErrorChanged");
        }

        private static void TVRecording_Forwarding(object sender, EventArgs e)
        {
            Dump("TVRecording_Forwarding");
        }

        private static void TVRecording_GuideLoadedChanged(object sender, EventArgs e)
        {
            Dump("TVRecording_GuideLoadedChanged");
        }

        private static void TVRecording_MediaChanged(object sender, EventArgs e)
        {
            Dump("TVRecording_MediaChanged");
        }

        private static void TVRecording_Pausing(object sender, EventArgs e)
        {
            Dump("TVRecording_Pausing");
        }

        private static void TVRecording_Playing(object sender, EventArgs e)
        {
            Dump("TVRecording_Playing");
        }

        private static void TVRecording_Rewinding(object sender, EventArgs e)
        {
            Dump("TVRecording_Rewinding");
        }

        private static void TVRecording_Started(object sender, EventArgs e)
        {
            Dump("TVRecording_Started");
        }

        private static void TVRecording_Stopping(object sender, EventArgs e)
        {
            Dump("TVRecording_Stopping");
        }

        private static void TVRecording_TrackTimeChanged(object sender, EventArgs e)
        {
            Dump("TVRecording_TrackTimeChanged");
        }
        #endregion

        #region Video
        private static void Video_EjectingChanged(object sender, EventArgs e)
        {
            Dump("Video_EjectingChanged");
        }

        private static void Video_Ended(object sender, EventArgs e)
        {
            Dump("Video_Ended");
        }

        private static void Video_ErrorChanged(object sender, EventArgs e)
        {
            Dump("Video_ErrorChanged");
        }

        private static void Video_Forwarding(object sender, EventArgs e)
        {
            Dump("Video_Forwarding");
        }

        private static void Video_GuideLoadedChanged(object sender, EventArgs e)
        {
            Dump("Video_GuideLoadedChanged");
        }

        private static void Video_MediaChanged(object sender, EventArgs e)
        {
            Dump("Video_MediaChanged");
        }

        private static void Video_Pausing(object sender, EventArgs e)
        {
            Dump("Video_Pausing");
        }

        private static void Video_Playing(object sender, EventArgs e)
        {
            Dump("Video_Playing");
        }

        private static void Video_Rewinding(object sender, EventArgs e)
        {
            Dump("Video_Rewinding");
        }

        private static void Video_Started(object sender, EventArgs e)
        {
            Dump("Video_Started");
        }

        private static void Video_Stopping(object sender, EventArgs e)
        {
            Dump("Video_Stopping");
        }

        private static void Video_TrackTimeChanged(object sender, EventArgs e)
        {
            Dump("Video_TrackTimeChanged");
        }
        #endregion

    }
}
