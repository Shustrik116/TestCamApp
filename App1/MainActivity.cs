using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Media;
using System.Threading.Tasks;
using Android.Hardware;
using System;

namespace App1
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]

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
                var stop = FindViewById<Button>(Resource.Id.Stop);
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
                //camera.Unlock();
                //////////
                ///

                //record.Click += async delegate
                //{
                //    i = 1;
                //    while (i == 1)
                //    {
                //        //camera.Unlock();
                //        //video.StopPlayback();
                //        //frontvideo.StopPlayback();
                //        //recorder = new MediaRecorder();
                //        //frontrecorder = new MediaRecorder();
                //        //frontrecorder.SetCamera(camera);
                //        //recorder.SetVideoSource(VideoSource.Camera);
                //        //recorder.SetAudioSource(AudioSource.Mic);
                //        //recorder.SetOutputFormat(OutputFormat.Default);
                //        //recorder.SetVideoEncoder(VideoEncoder.Default);
                //        //recorder.SetAudioEncoder(AudioEncoder.Default);
                //        //recorder.SetOutputFile(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/test" + a + ".mp4");
                //        //recorder.SetPreviewDisplay(video.Holder.Surface);  
                //        //frontrecorder.SetVideoSource(VideoSource.Camera);
                //        //frontrecorder.SetOutputFormat(OutputFormat.Default);
                //        //frontrecorder.SetVideoEncoder(VideoEncoder.Default);
                //        //frontrecorder.SetOutputFile(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/fronttest" + a + ".mp4");
                //        //frontrecorder.SetPreviewDisplay(frontvideo.Holder.Surface);
                //        //recorder.Prepare();
                //        //recorder.Start();
                //        //await Task.Delay(10);
                //        //frontrecorder.Prepare();
                //        //frontrecorder.Start();
                //        //await Task.Delay(5000);
                //        //a++;
                //        //recorder.Stop();
                //        //recorder.Release();
                //        //await Task.Delay(10);
                //        //frontrecorder.Stop();
                //        //frontrecorder.Release();
                //        //camera.Lock();
                //        ///
                //    }
                //};


                record.Click += async delegate
                {
                    i = 1;

                    while (i == 1)
                    {
                        try
                        {
                            rearcamera.Unlock();
                            //video.StopPlayback();
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
                            //recorder.Release();
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }

                    }

                };
                record.Click += async delegate
                {
                    b = 1;
                    while (b == 1)
                    {
                        try
                        {
                            camera.Unlock();
                            //frontvideo.StopPlayback();
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
                            //frontrecorder.Release();
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }
                    }
                };

                stop.Click += async delegate
                {
                    i = 0;
                    b = 0;
                    frontrecorder.Stop();
                    frontrecorder.Release();
                    recorder.Stop();
                    recorder.Release();
                    //if (recorder != null)
                    //{
                    //    recorder.Stop();
                    //    recorder.Release();
                    //}
                };
            }
            catch (System.Exception ex)
            {
                throw;
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