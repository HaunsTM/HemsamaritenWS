namespace SurveillanceCam2DB.BLL.Interfaces
{
    using System;
    using System.Threading.Tasks;

    using AForge.Video.FFMPEG;

    public interface IVideoDealer
    {
        event VideoDealer.VideoDealerStartUpEventHandler BeginCreatingVideoFile;

        event VideoDealer.VideoDealerProcessEventHandler VideoFrameWritten;

        event VideoDealer.VideoDealerFinishEventHandler VideoFileCreated;

        event VideoDealer.VideoDealerErrorEventHandler VideoCreatorError;

        string DbConnectionStringName { get; }

        /// <summary>
        /// Creates a video file asynchronously.
        /// </summary>
        /// <param name="startTime">From where to start creating the vide (with source in database).</param>
        /// <param name="endTime">From where to end creating the video (with source in database).</param>
        /// <param name="codec">Enumeration of some video codecs from FFmpeg library, which are available for writing video files.</param>
        /// <param name="outputFileName"></param>
        /// <param name="width">Frame width of the opened video file.</param>
        /// <param name="height">Frame height of the opened video file.</param>
        /// <param name="frameRateMs">Frame rate of the video file. (in milli seconds)</param>
        /// <returns>A boolean value indicating if the movie was successfully created.</returns>
        Task<bool> CreateVideoAsync(DateTime startTime, DateTime endTime, VideoCodec codec, string outputFileName, int width, int height, int frameRateMs);

        /// <summary>
        /// Creates a video file.
        /// </summary>
        /// <param name="startTime">From where to start creating the vide (with source in database).</param>
        /// <param name="endTime">From where to end creating the video (with source in database).</param>
        /// <param name="codec">Enumeration of some video codecs from FFmpeg library, which are available for writing video files.</param>
        /// <param name="outputFileName"></param>
        /// <param name="width">Frame width of the opened video file.</param>
        /// <param name="height">Frame height of the opened video file.</param>
        /// <param name="frameRateMs">Frame rate of the video file. (in milli seconds)</param>
        /// <returns>A boolean value indicating if the movie was successfully created.</returns>
        bool CreateVideo(DateTime startTime, DateTime endTime, VideoCodec codec, string outputFileName, int width, int height, int frameRateMs);
    }
}