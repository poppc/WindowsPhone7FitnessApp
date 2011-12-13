using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;
using USA_FitnessApp.Model;
using Microsoft.Phone.Net.NetworkInformation;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Specialized;
using USA_FitnessApp.Net.JSONParserObject;
using USA_FitnessApp.Logic;
using USA_FitnessApp.Net.SyncObjects;
using System.Threading;

namespace USA_FitnessApp.Net
{
    public class Communication
    {
        private class RequestType : StringEnum
        {
            public static readonly RequestType LOGIN = new RequestType { Desc = "login" };

            public static readonly RequestType USER_DATA = new RequestType { Desc = "user_data" };

            public static readonly RequestType LIST_NUTRITION = new RequestType { Desc="list_nutrition" };

            public static readonly RequestType TRACK_DIET = new RequestType { Desc = "track_diet" };

            public static readonly RequestType TRACK_WEIGHT = new RequestType { Desc = "track_weight" };

            public static readonly RequestType UPDATE_DIET = new RequestType { Desc = "update_diet" };

            public static readonly RequestType UPDATE_WEIGHT = new RequestType { Desc = "update_weight" };

            public static readonly RequestType LIST_FITNESS = new RequestType { Desc = "list_fitness" };

            public static readonly RequestType UPDATE_FITNESS = new RequestType { Desc = "update_fitness" };

            public static readonly RequestType TRACK_FITNESS = new RequestType { Desc = "track_fitness" };

            //Update Goal

            public static readonly RequestType REGISTER = new RequestType { Desc = "register" };

            //Recommend Nutrition

            //Recommend Fitness

            //Update user_data

            //Track Goal
        }

        private class QueuedRequest
        {
            public RequestType Rtype { get; set; }

            public String PostParas { get; set; }

            public int Retry { get; set; }

            public Boolean UserSpecific { get; set; }

            public int KindOfConnection;

            public static readonly int MAX_RETRY = 3;
        }

        private List<QueuedRequest> queuedRequests = new List<QueuedRequest>();

        private Uri requestUri = new Uri("http://ashley.versvik.net/capstone/request.php");

        private MainLogic mainLogic;

        private int requestCnt = 0;
        private int RequestCnt
        {
            get
            {
                return requestCnt;
            }

            set
            {
                if(value == 0)
                    if (kindOfConnection == (int)KindsOfConnection.Upload)
                    {
                        App.MealViewModel.Status.SyncFinished();
                    }
                    else if (kindOfConnection == (int)KindsOfConnection.Download)
                    {
                        App.MealViewModel.Status.DownloadFinished();
                    }
                    else if (kindOfConnection == (int)KindsOfConnection.Login)
                    {
                        App.MealViewModel.Status.LoggedIn();
                    }
                if (value == 1 || value == 2) // 2 for Resending queued request after login where cnt wasnt decrement yet
                {
                    if (kindOfConnection == (int)KindsOfConnection.Upload)
                    {
                        App.MealViewModel.Status.SyncStarted();
                    }
                    else if (kindOfConnection == (int)KindsOfConnection.Download)
                    {
                        App.MealViewModel.Status.DownloadStarted();
                    }
                    else if (kindOfConnection == (int)KindsOfConnection.Login)
                    {
                        App.MealViewModel.Status.LoggingIn();
                    }
                }

                requestCnt = value;
            }
        }

        private int userId;

        /// <summary>
        /// SessionKey
        /// </summary>
        private String authKey;

        private enum KindsOfConnection { Login, Download, Upload };

        private int kindOfConnection;

        public Communication(MainLogic mainLogic)
        {
            this.mainLogic = mainLogic;
        }

        public void Register(Registration reg)
        {
            String para = "&username=" + reg.Username + "&password_1=" + reg.Password_1 + "&password_2=" + reg.Password_2
                 + "&email_1=" + reg.Email_1 + "&email_2=" + reg.Email_2 + "&gender=" + reg.Gender + "&unit=" + reg.Unit
                  + "&weight=" + reg.Weight + "&height=" + reg.Height + "&birthdate=" + reg.Birthdate;
            SendRequest(RequestType.REGISTER, para, false, (int)KindsOfConnection.Download);
        }

        public void Login(String user, String password)
        {
            String para = "&username=" + user + "&password=" + password;
            SendRequest(RequestType.LOGIN, para, false, (int)KindsOfConnection.Login);
            //userId = 7;
            //authKey = "e93999b9b833abe7a9709b472eb85c593795d99d";
            //App.MealViewModel.Status.LoggedIn();
            //resendQueuedRequests();
        }

        public void ReceiveUserData()
        {
            SendRequest(RequestType.USER_DATA, "", true, (int)KindsOfConnection.Download);
        }

        public void ReceiveFoodItems()
        {
            SendRequest(RequestType.LIST_NUTRITION, "", false, (int)KindsOfConnection.Download);
        }

        public void ReceiveExercises()
        {
            SendRequest(RequestType.LIST_FITNESS, "", false, (int)KindsOfConnection.Download);
        }

        public void UpdateDiet(List<FoodItemSync> fiSync)
        {
            foreach(FoodItemSync fi in fiSync)
            {
                String para = "&date=" + fi.Date + "&id=" + fi.Id + "&amount=" + fi.Amount + "&meal=" + fi.Meal;
                SendRequest(RequestType.UPDATE_DIET, para, true, (int)KindsOfConnection.Upload);
            }
        }

        public void UpdateFitness(List<ExerciseSync> exercises)
        {
            foreach (ExerciseSync e in exercises)
            {
                String para = "&date=" + e.Date + "&id=" + e.Id + "&timeframe=" + e.TimeFrame + "&amount=" + e.Amount + "&intensity=" + e.Intensity;
                SendRequest(RequestType.UPDATE_FITNESS, para, true, (int)KindsOfConnection.Upload);
            }
        }

        public void UpdateWeight(List<Weight> weights)
        {
            foreach (Weight w in weights)
            {
                String para = "&date=" + w.Date.ToString("yyyy-MM-dd") + "&weight=" + w.WeightValue;
                SendRequest(RequestType.UPDATE_WEIGHT, para, true, (int)KindsOfConnection.Upload);
            }
        }

        public void TrackDiet(DateTime startDate, DateTime endDate)
        {
            String para = "&startdate=" + startDate.ToString("yyyy-MM-dd")
                + "&enddate=" + endDate.ToString("yyyy-MM-dd");
            SendRequest(RequestType.TRACK_DIET, para, true, (int)KindsOfConnection.Download);
        }

        public void TrackFitness(DateTime startDate, DateTime endDate)
        {
            String para = "&startdate=" + startDate.ToString("yyyy-MM-dd")
                + "&enddate=" + endDate.ToString("yyyy-MM-dd");
            SendRequest(RequestType.TRACK_FITNESS, para, true, (int)KindsOfConnection.Download);
        }

        public void TrackWeight(DateTime startDate, DateTime endDate)
        {
            String para = "&startdate=" + startDate.ToString("yyyy-MM-dd")
                + "&enddate=" + endDate.ToString("yyyy-MM-dd");
            SendRequest(RequestType.TRACK_WEIGHT, para, true, (int)KindsOfConnection.Download);
        }

        private void SendRequest(RequestType rType, String para, Boolean userSpecific, int kindOfConnection)
        {
            if (!(rType == RequestType.LOGIN || rType == RequestType.REGISTER) && (userId <= 0 || authKey.Length <= 0))
            {
                //TODO Request die userid und authkey benötigen gesondert behandeln
                queuedRequests.Add(new QueuedRequest { Rtype = rType, PostParas = para, Retry = QueuedRequest.MAX_RETRY, UserSpecific = userSpecific
                    , KindOfConnection = kindOfConnection });
                return;
            }

            WebClient wc = new WebClient();
            wc.Headers["Content-type"] = "application/x-www-form-urlencoded";
            wc.UploadStringCompleted += new UploadStringCompletedEventHandler(UploadStringCompleted);
            //wc.UploadProgressChanged += new UploadProgressChangedEventHandler(UploadProgressChanged);
            String postRequest = "request=" + rType.ToString();
            if (userSpecific)
            {
                postRequest += "&userid=" + userId + "&auth=" + authKey;
            }
            postRequest += para;
            this.kindOfConnection = kindOfConnection;
            wc.UploadStringAsync(requestUri, "POST", postRequest, rType);
            RequestCnt++;
        }

        private void UploadStringCompleted(object sender, UploadStringCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                return;
            }
            else
            {
                String response = e.Result;
                Error err = CheckForError(response);

                RequestType rt = (RequestType)e.UserState;
                if (rt.Equals(RequestType.LOGIN))
                {
                    if (!err.Response.Equals("SUCCESS"))
                    {
                        App.MealViewModel.Status.ErrorOccured();
                    }
                    else
                    {
                        Login login = JsonConvert.DeserializeObject<Login>(response);
                        userId = login.UserId;
                        authKey = login.Auth;
                        App.MealViewModel.Status.LoggedIn();
                        resendQueuedRequests();
                    }
                }
                else if (rt.Equals(RequestType.REGISTER))
                {
                    if (!err.Response.Equals("SUCCESS"))
                    {
                        App.MealViewModel.Status.ErrorOccured();
                        App.MealViewModel.RegistrationFailedTxt = "Registration failed! " + err.Msg;
                    }
                    else
                    {
                        App.MealViewModel.RegistrationFailedTxt = "Registration done!";
                    }
                }
                else if (rt.Equals(RequestType.USER_DATA))
                {
                    if (!err.Response.Equals("SUCCESS"))
                    {
                        App.MealViewModel.Status.ErrorOccured();
                    }
                    else
                    {
                        User_Data ud = JsonConvert.DeserializeObject<User_Data>(response);
                        App.MealViewModel.LoadUserData(ud);
                    }
                }
                else if (rt.Equals(RequestType.LIST_NUTRITION))
                {
                    if (!err.Response.Equals("SUCCESS"))
                    {
                        App.MealViewModel.Status.ErrorOccured();
                    }
                    else
                    {
                        List_Nutrition ln = JsonConvert.DeserializeObject<List_Nutrition>(response);
                        mainLogic.Fis = ln.Items;
                    }
                }
                else if (rt.Equals(RequestType.LIST_FITNESS))
                {
                    if (!err.Response.Equals("SUCCESS"))
                    {
                        App.MealViewModel.Status.ErrorOccured();
                    }
                    else
                    {
                        List_Fitness lf = JsonConvert.DeserializeObject<List_Fitness>(response);
                        mainLogic.Exercises = lf.Items;
                    }
                }
                else if (rt.Equals(RequestType.TRACK_DIET))
                {
                    if (!err.Response.Equals("SUCCESS"))
                    {
                        App.MealViewModel.Status.ErrorOccured();
                        App.MealViewModel.CreateMealHistory(null);
                    }
                    else
                    {
                        Track_Diet td = JsonConvert.DeserializeObject<Track_Diet>(response);
                        App.MealViewModel.CreateMealHistory(td.Items);
                    }
                }
                else if (rt.Equals(RequestType.TRACK_FITNESS))
                {
                    if (!err.Response.Equals("SUCCESS"))
                    {
                        App.MealViewModel.Status.ErrorOccured();
                        App.MealViewModel.CreateFitnessHistory(null);
                    }
                    else
                    {
                        Track_Fitness tf = JsonConvert.DeserializeObject<Track_Fitness>(response);
                        App.MealViewModel.CreateFitnessHistory(tf.Items);
                    }
                }
                else if (rt.Equals(RequestType.TRACK_WEIGHT))
                {
                    if (!err.Response.Equals("SUCCESS"))
                    {
                        App.MealViewModel.Status.ErrorOccured();
                        App.MealViewModel.CreateWeightHistory(null);
                    }
                    else
                    {
                        Track_Weight tw = JsonConvert.DeserializeObject<Track_Weight>(response);
                        App.MealViewModel.CreateWeightHistory(tw.Items);
                    }
                }
                else if (rt.Equals(RequestType.UPDATE_DIET))
                {
                    if (!err.Response.Equals("SUCCESS"))
                    {
                        //TODO Queued Requests and Retry
                        App.MealViewModel.Status.ErrorOccured();
                    }
                    else
                    {

                    }
                    String r = e.Result;
                }
                else if (rt.Equals(RequestType.UPDATE_FITNESS))
                {
                    if (!err.Response.Equals("SUCCESS"))
                    {
                        //TODO Queued Requests and Retry
                        App.MealViewModel.Status.ErrorOccured();
                    }
                    else
                    {

                    }
                    String r = e.Result;
                }
                else if (rt.Equals(RequestType.UPDATE_WEIGHT))
                {
                    if (!err.Response.Equals("SUCCESS"))
                    {
                        //TODO Queued Requests and Retry
                        App.MealViewModel.Status.ErrorOccured();
                    }
                    else
                    {

                    }
                    String r = e.Result;
                }

                RequestCnt--;
            }
        }

        //TODO Use Retry and remove from list
        private void resendQueuedRequests()
        {
            foreach (QueuedRequest qr in queuedRequests)
            {
                SendRequest(qr.Rtype, qr.PostParas, qr.UserSpecific, qr.KindOfConnection);
            }
        }

        private Error CheckForError(String jsonResponse)
        {
            return JsonConvert.DeserializeObject<Error>(jsonResponse);
        }
    }
}

