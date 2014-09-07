using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.MediaCenter.Samples.MediaState;

namespace WMC_Manager
{
    public class WMC
    {
        MediaState ms;
        public WMC()
        {
            ms = new MediaState();
            HookEvents(); //typed first
            HookEvent(); //generic
            ms.Connect();
        }

        private void Dump(string message)
        {
            System.Diagnostics.Debug.Print(message);
        }

        private void HookEvent()
        {
            ms.OnMSASEvent += new Microsoft.MediaCenter.Samples.MediaState.MediaState.MSASEventHandler(ms_OnMSASEvent);
        }

        private void ms_OnMSASEvent(object state, MediaStatusEventArgs args)
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

        private void HookEvents()
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
        private void MediaCenter_EjectingChanged(object sender, EventArgs e)
        {
            Dump("MediaCenter_EjectingChanged");
        }

        private void MediaCenter_Ended(object sender, EventArgs e)
        {
            //Dump("MediaCenter_Ended");
            //if (!stopping)
            //{
            //    Power();
            //}
        }

        private void MediaCenter_ErrorChanged(object sender, EventArgs e)
        {
            Dump("MediaCenter_ErrorChanged");
        }

        private void MediaCenter_Forwarding(object sender, EventArgs e)
        {
            Dump("MediaCenter_Forwarding");
        }

        private void MediaCenter_GuideLoadedChanged(object sender, EventArgs e)
        {
            Dump("MediaCenter_GuideLoadedChanged");
        }

        private void MediaCenter_MediaChanged(object sender, EventArgs e)
        {
            Dump("MediaCenter_MediaChanged");
        }

        private void MediaCenter_MuteChanged(object sender, EventArgs e)
        {
            Dump("MediaCenter_MuteChanged");
        }

        private void MediaCenter_NavigationChanged(object sender, EventArgs e)
        {
            Dump("MediaCenter_NavigationChanged");
        }

        private void MediaCenter_Pausing(object sender, EventArgs e)
        {
            Dump("MediaCenter_Pausing");
        }

        private void MediaCenter_Playing(object sender, EventArgs e)
        {
            Dump("MediaCenter_Playing");
        }

        private void MediaCenter_Rewinding(object sender, EventArgs e)
        {
            Dump("MediaCenter_Rewinding");
        }

        private void MediaCenter_Started(object sender, EventArgs e)
        {
            Dump("MediaCenter_Started");
        }

        private void MediaCenter_Stopping(object sender, EventArgs e)
        {
            Dump("MediaCenter_Stopping");
        }

        private void MediaCenter_VolumeChanged(object sender, EventArgs e)
        {
            Dump("MediaCenter_VolumeChanged");
        }
        #endregion

        #region CD
        private void CD_EjectingChanged(object sender, EventArgs e)
        {
            Dump("CD_EjectingChanged");
        }

        private void CD_Ended(object sender, EventArgs e)
        {
            Dump("CD_Ended");
        }

        private void CD_ErrorChanged(object sender, EventArgs e)
        {
            Dump("CD_ErrorChanged");
        }

        private void CD_Forwarding(object sender, EventArgs e)
        {
            Dump("CD_Forwarding");
        }

        private void CD_GuideLoadedChanged(object sender, EventArgs e)
        {
            Dump("CD_GuideLoadedChanged");
        }

        private void CD_MediaChanged(object sender, EventArgs e)
        {
            Dump("CD_MediaChanged");
        }

        private void CD_Pausing(object sender, EventArgs e)
        {
            Dump("CD_Pausing");
        }

        private void CD_Playing(object sender, EventArgs e)
        {
            Dump("CD_Playing");
        }

        private void CD_RepeatSetChanged(object sender, EventArgs e)
        {
            Dump("CD_RepeatSetChanged");
        }

        private void CD_Rewinding(object sender, EventArgs e)
        {
            Dump("CD_Rewinding");
        }

        private void CD_ShuffleChanged(object sender, EventArgs e)
        {
            Dump("CD_ShuffleChanged");
        }

        private void CD_Started(object sender, EventArgs e)
        {
            Dump("CD_Started");
        }

        private void CD_Stopping(object sender, EventArgs e)
        {
            Dump("CD_Stopping");
        }

        private void CD_TrackTimeChanged(object sender, EventArgs e)
        {
            Dump("CD_TrackTimeChanged");
        }

        private void CD_VisualizationChanged(object sender, EventArgs e)
        {
            Dump("CD_VisualizationChanged");
        }
        #endregion

        #region DVD
        private void DVD_ChapterChanged(object sender, EventArgs e)
        {
            Dump("DVD_ChapterChanged");
        }

        private void DVD_EjectingChanged(object sender, EventArgs e)
        {
            Dump("DVD_EjectingChanged");
        }

        private void DVD_Ended(object sender, EventArgs e)
        {
            Dump("DVD_Ended");
        }

        private void DVD_ErrorChanged(object sender, EventArgs e)
        {
            Dump("DVD_ErrorChanged");
        }

        private void DVD_Forwarding(object sender, EventArgs e)
        {
            Dump("DVD_Forwarding");
        }

        private void DVD_GuideLoadedChanged(object sender, EventArgs e)
        {
            Dump("DVD_GuideLoadedChanged");
        }

        private void DVD_MediaChanged(object sender, EventArgs e)
        {
            Dump("DVD_MediaChanged");
        }

        private void DVD_Pausing(object sender, EventArgs e)
        {
            Dump("DVD_Pausing");
        }

        private void DVD_Playing(object sender, EventArgs e)
        {
            Dump("DVD_Playing");
        }

        private void DVD_Rewinding(object sender, EventArgs e)
        {
            Dump("DVD_Rewinding");
        }

        private void DVD_Started(object sender, EventArgs e)
        {
            Dump("DVD_Started");
        }

        private void DVD_Stopping(object sender, EventArgs e)
        {
            Dump("DVD_Stopping");
        }

        private void DVD_TrackTimeChanged(object sender, EventArgs e)
        {
            Dump("DVD_TrackTimeChanged");
        }
        #endregion

        #region Music
        private void Music_EjectingChanged(object sender, EventArgs e)
        {
            Dump("Music_EjectingChanged");
        }

        private void Music_Ended(object sender, EventArgs e)
        {
            Dump("Music_Ended");
        }

        private void Music_ErrorChanged(object sender, EventArgs e)
        {
            Dump("Music_ErrorChanged");
        }

        private void Music_Forwarding(object sender, EventArgs e)
        {
            Dump("Music_Forwarding");
        }

        private void Music_GuideLoadedChanged(object sender, EventArgs e)
        {
            Dump("Music_GuideLoadedChanged");
        }

        private void Music_MediaChanged(object sender, EventArgs e)
        {
            Dump("Music_MediaChanged");
        }

        private void Music_Pausing(object sender, EventArgs e)
        {
            Dump("Music_Pausing");
        }

        private void Music_Playing(object sender, EventArgs e)
        {
            Dump("Music_Playing");
        }

        private void Music_RepeatSetChanged(object sender, EventArgs e)
        {
            Dump("Music_RepeatSetChanged");
        }

        private void Music_Rewinding(object sender, EventArgs e)
        {
            Dump("Music_Rewinding");
        }

        private void Music_ShuffleChanged(object sender, EventArgs e)
        {
            Dump("Music_ShuffleChanged");
        }

        private void Music_Started(object sender, EventArgs e)
        {
            Dump("Music_Started");
        }

        private void Music_Stopping(object sender, EventArgs e)
        {
            Dump("Music_Stopping");
        }

        private void Music_TrackTimeChanged(object sender, EventArgs e)
        {
            Dump("Music_TrackTimeChanged");
        }

        private void Music_VisualizationChanged(object sender, EventArgs e)
        {
            Dump("Music_VisualizationChanged");
        }
        #endregion

        #region PhoneCall
        private void PhoneCall_EjectingChanged(object sender, EventArgs e)
        {
            Dump("PhoneCall_EjectingChanged");
        }

        private void PhoneCall_Ended(object sender, EventArgs e)
        {
            Dump("PhoneCall_Ended");
        }

        private void PhoneCall_ErrorChanged(object sender, EventArgs e)
        {
            Dump("PhoneCall_ErrorChanged");
        }

        private void PhoneCall_Forwarding(object sender, EventArgs e)
        {
            Dump("PhoneCall_Forwarding");
        }

        private void PhoneCall_GuideLoadedChanged(object sender, EventArgs e)
        {
            Dump("PhoneCall_GuideLoadedChanged");
        }

        private void PhoneCall_MediaChanged(object sender, EventArgs e)
        {
            Dump("PhoneCall_MediaChanged");
        }

        private void PhoneCall_Pausing(object sender, EventArgs e)
        {
            Dump("PhoneCall_Pausing");
        }

        private void PhoneCall_Playing(object sender, EventArgs e)
        {
            Dump("PhoneCall_Playing");
        }

        private void PhoneCall_Rewinding(object sender, EventArgs e)
        {
            Dump("PhoneCall_Rewinding");
        }

        private void PhoneCall_Started(object sender, EventArgs e)
        {
            Dump("PhoneCall_Started");
        }

        private void PhoneCall_Stopping(object sender, EventArgs e)
        {
            Dump("PhoneCall_Stopping");
        }
        #endregion

        #region Pictures
        private void Pictures_CurrentPictureChanged(object sender, EventArgs e)
        {
            Dump("Pictures_CurrentPictureChanged");
        }

        private void Pictures_EjectingChanged(object sender, EventArgs e)
        {
            Dump("Pictures_EjectingChanged");
        }

        private void Pictures_Ended(object sender, EventArgs e)
        {
            Dump("Pictures_Ended");
        }

        private void Pictures_ErrorChanged(object sender, EventArgs e)
        {
            Dump("Pictures_ErrorChanged");
        }

        private void Pictures_Forwarding(object sender, EventArgs e)
        {
            Dump("Pictures_Forwarding");
        }

        private void Pictures_GuideLoadedChanged(object sender, EventArgs e)
        {
            Dump("Pictures_GuideLoadedChanged");
        }

        private void Pictures_MediaChanged(object sender, EventArgs e)
        {
            Dump("Pictures_MediaChanged");
        }

        private void Pictures_Pausing(object sender, EventArgs e)
        {
            Dump("Pictures_Pausing");
        }

        private void Pictures_Playing(object sender, EventArgs e)
        {
            Dump("Pictures_Playing");
        }

        private void Pictures_Rewinding(object sender, EventArgs e)
        {
            Dump("Pictures_Rewinding");
        }

        private void Pictures_Started(object sender, EventArgs e)
        {
            Dump("Pictures_Started");
        }

        private void Pictures_Stopping(object sender, EventArgs e)
        {
            Dump("Pictures_Stopping");
        }
        #endregion

        #region Radio
        private void Radio_EjectingChanged(object sender, EventArgs e)
        {
            Dump("Radio_EjectingChanged");
        }

        private void Radio_Ended(object sender, EventArgs e)
        {
            Dump("Radio_Ended");
        }

        private void Radio_ErrorChanged(object sender, EventArgs e)
        {
            Dump("Radio_ErrorChanged");
        }

        private void Radio_Forwarding(object sender, EventArgs e)
        {
            Dump("Radio_Forwarding");
        }

        private void Radio_FrequencyChanged(object sender, EventArgs e)
        {
            Dump("Radio_FrequencyChanged");
        }

        private void Radio_GuideLoadedChanged(object sender, EventArgs e)
        {
            Dump("Radio_GuideLoadedChanged");
        }

        private void Radio_MediaChanged(object sender, EventArgs e)
        {
            Dump("Radio_MediaChanged");
        }

        private void Radio_Pausing(object sender, EventArgs e)
        {
            Dump("Radio_Pausing");
        }

        private void Radio_Playing(object sender, EventArgs e)
        {
            Dump("Radio_Playing");
        }

        private void Radio_Rewinding(object sender, EventArgs e)
        {
            Dump("Radio_Rewinding");
        }

        private void Radio_Started(object sender, EventArgs e)
        {
            Dump("Radio_Started");
        }

        private void Radio_Stopping(object sender, EventArgs e)
        {
            Dump("Radio_Stopping");
        }
        #endregion

        #region TV
        private void TV_EjectingChanged(object sender, EventArgs e)
        {
            Dump("TV_EjectingChanged");
        }

        private void TV_Ended(object sender, EventArgs e)
        {
            Dump("TV_Ended");
        }

        private void TV_ErrorChanged(object sender, EventArgs e)
        {
            Dump("TV_ErrorChanged");
        }

        private void TV_Forwarding(object sender, EventArgs e)
        {
            Dump("TV_Forwarding");
        }

        private void TV_GuideLoadedChanged(object sender, EventArgs e)
        {
            Dump("TV_GuideLoadedChanged");
        }

        private void TV_MediaChanged(object sender, EventArgs e)
        {
            Dump("TV_MediaChanged");
        }

        private void TV_Pausing(object sender, EventArgs e)
        {
            Dump("TV_Pausing");
        }

        private void TV_Playing(object sender, EventArgs e)
        {
            Dump("TV_Playing");
        }

        private void TV_Rewinding(object sender, EventArgs e)
        {
            Dump("TV_Rewinding");
        }

        private void TV_Started(object sender, EventArgs e)
        {
            Dump("TV_Started");
        }

        private void TV_Stopping(object sender, EventArgs e)
        {
            Dump("TV_Stopping");
        }

        private void TV_TrackTimeChanged(object sender, EventArgs e)
        {
            Dump("TV_TrackTimeChanged");
        }
        #endregion

        #region TVRecorded
        private void TVRecorded_EjectingChanged(object sender, EventArgs e)
        {
            Dump("TVRecorded_EjectingChanged");
        }

        private void TVRecorded_Ended(object sender, EventArgs e)
        {
            Dump("TVRecorded_Ended");
        }

        private void TVRecorded_ErrorChanged(object sender, EventArgs e)
        {
            Dump("TVRecorded_ErrorChanged");
        }

        private void TVRecorded_Forwarding(object sender, EventArgs e)
        {
            Dump("TVRecorded_Forwarding");
        }

        private void TVRecorded_GuideLoadedChanged(object sender, EventArgs e)
        {
            Dump("TVRecorded_GuideLoadedChanged");
        }

        private void TVRecorded_MediaChanged(object sender, EventArgs e)
        {
            Dump("TVRecorded_MediaChanged");
        }

        private void TVRecorded_Pausing(object sender, EventArgs e)
        {
            Dump("TVRecorded_Pausing");
        }

        private void TVRecorded_Playing(object sender, EventArgs e)
        {
            Dump("TVRecorded_Playing");
        }

        private void TVRecorded_Rewinding(object sender, EventArgs e)
        {
            Dump("TVRecorded_Rewinding");
        }

        private void TVRecorded_Started(object sender, EventArgs e)
        {
            Dump("TVRecorded_Started");
        }

        private void TVRecorded_Stopping(object sender, EventArgs e)
        {
            Dump("TVRecorded_Stopping");
        }

        private void TVRecorded_TrackTimeChanged(object sender, EventArgs e)
        {
            Dump("TVRecorded_TrackTimeChanged");
        }
        #endregion

        #region TVRecording
        private void TVRecording_EjectingChanged(object sender, EventArgs e)
        {
            Dump("TVRecording_EjectingChanged");
        }

        private void TVRecording_Ended(object sender, EventArgs e)
        {
            Dump("TVRecording_Ended");
        }

        private void TVRecording_ErrorChanged(object sender, EventArgs e)
        {
            Dump("TVRecording_ErrorChanged");
        }

        private void TVRecording_Forwarding(object sender, EventArgs e)
        {
            Dump("TVRecording_Forwarding");
        }

        private void TVRecording_GuideLoadedChanged(object sender, EventArgs e)
        {
            Dump("TVRecording_GuideLoadedChanged");
        }

        private void TVRecording_MediaChanged(object sender, EventArgs e)
        {
            Dump("TVRecording_MediaChanged");
        }

        private void TVRecording_Pausing(object sender, EventArgs e)
        {
            Dump("TVRecording_Pausing");
        }

        private void TVRecording_Playing(object sender, EventArgs e)
        {
            Dump("TVRecording_Playing");
        }

        private void TVRecording_Rewinding(object sender, EventArgs e)
        {
            Dump("TVRecording_Rewinding");
        }

        private void TVRecording_Started(object sender, EventArgs e)
        {
            Dump("TVRecording_Started");
        }

        private void TVRecording_Stopping(object sender, EventArgs e)
        {
            Dump("TVRecording_Stopping");
        }

        private void TVRecording_TrackTimeChanged(object sender, EventArgs e)
        {
            Dump("TVRecording_TrackTimeChanged");
        }
        #endregion

        #region Video
        private void Video_EjectingChanged(object sender, EventArgs e)
        {
            Dump("Video_EjectingChanged");
        }

        private void Video_Ended(object sender, EventArgs e)
        {
            Dump("Video_Ended");
        }

        private void Video_ErrorChanged(object sender, EventArgs e)
        {
            Dump("Video_ErrorChanged");
        }

        private void Video_Forwarding(object sender, EventArgs e)
        {
            Dump("Video_Forwarding");
        }

        private void Video_GuideLoadedChanged(object sender, EventArgs e)
        {
            Dump("Video_GuideLoadedChanged");
        }

        private void Video_MediaChanged(object sender, EventArgs e)
        {
            Dump("Video_MediaChanged");
        }

        private void Video_Pausing(object sender, EventArgs e)
        {
            Dump("Video_Pausing");
        }

        private void Video_Playing(object sender, EventArgs e)
        {
            Dump("Video_Playing");
        }

        private void Video_Rewinding(object sender, EventArgs e)
        {
            Dump("Video_Rewinding");
        }

        private void Video_Started(object sender, EventArgs e)
        {
            Dump("Video_Started");
        }

        private void Video_Stopping(object sender, EventArgs e)
        {
            Dump("Video_Stopping");
        }

        private void Video_TrackTimeChanged(object sender, EventArgs e)
        {
            Dump("Video_TrackTimeChanged");
        }
        #endregion

    }
}
