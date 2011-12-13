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
using System.Windows.Media.Imaging;

namespace USA_FitnessApp.Logic
{
    public class AppStatus
    {
        private static readonly ImageSource DOWNLOAD = new BitmapImage(new Uri(@"../Images/Download.png", UriKind.Relative));

        private static readonly ImageSource SYNC = new BitmapImage(new Uri(@"../Images/Sync.png", UriKind.Relative));

        private static readonly ImageSource OK = new BitmapImage(new Uri(@"../Images/Ok.png", UriKind.Relative));

        private static readonly ImageSource ERROR = new BitmapImage(new Uri(@"../Images/Error.png", UriKind.Relative));

        private static readonly ImageSource UPLOAD = new BitmapImage(new Uri(@"../Images/Upload.png", UriKind.Relative));

        private static readonly ImageSource LOGIN = new BitmapImage(new Uri(@"../Images/Login.png", UriKind.Relative));

        private static readonly ImageSource LOGIN_REQUIRED = new BitmapImage(new Uri(@"../Images/LoginRequired.png", UriKind.Relative));

        private ImageSource DEFAULT = OK;

        private Boolean readyToSync;
        public Boolean IsReadyToSync()
        {
            return readyToSync;
        }

        private Boolean readyToSyncTmp;

        private Boolean transferInProgress;

        private Boolean errorLock;

        private Boolean loggedIn;
        public Boolean IsLoggedIn()
        {
            return loggedIn;
        }

        public AppStatus()
        {
            updateStatusImage(DEFAULT);
        }

        private void updateStatusImage(ImageSource status)
        {
            if (!errorLock)
                App.MealViewModel.StatusImage = status;
        }

        public void NeedsToSync()
        {
            readyToSync = true;
            DEFAULT = SYNC;
            if(!transferInProgress)
                updateStatusImage(DEFAULT);
        }

        public void SyncStarted()
        {
            readyToSync = false;
            transferInProgress = true;
            updateStatusImage(UPLOAD);
        }

        public void SyncFinished()
        {
            DEFAULT = OK;
            transferInProgress = false;
            updateStatusImage(DEFAULT);
        }

        public void DownloadStarted()
        {
            transferInProgress = true;
            updateStatusImage(DOWNLOAD);
        }

        public void DownloadFinished()
        {
            transferInProgress = false;
            updateStatusImage(DEFAULT);
        }

        public void ErrorOccured()
        {
            readyToSyncTmp = readyToSync;
            readyToSync = false;
            updateStatusImage(ERROR);
            errorLock = true;
        }

        public void ErrorHandled()
        {
            //TODO Real ErrorHandling
            errorLock = false;
            readyToSync = readyToSyncTmp;
            updateStatusImage(DEFAULT);
        }

        public void LogInRequired()
        {
            loggedIn = false;
            updateStatusImage(LOGIN_REQUIRED);
        }

        public void LoggingIn()
        {
            updateStatusImage(LOGIN);
            errorLock = true;
        }

        public void LoggedIn()
        {
            loggedIn = true;
            errorLock = false;
            updateStatusImage(DEFAULT);
        }
    }
}
