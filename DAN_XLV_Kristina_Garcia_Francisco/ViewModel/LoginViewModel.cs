using DAN_XLV_Kristina_Garcia_Francisco.Command;
using DAN_XLV_Kristina_Garcia_Francisco.View;
using System.Windows.Controls;
using System.Windows.Input;

namespace DAN_XLV_Kristina_Garcia_Francisco.ViewModel
{
    class LoginViewModel : BaseViewModel
    {
        Login view;

        #region Constructor
        public LoginViewModel(Login loginView)
        {
            view = loginView;
        }
        #endregion

        #region Property
        private string infoLabel;
        public string InfoLabel
        {
            get
            {
                return infoLabel;
            }
            set
            {
                infoLabel = value;
                OnPropertyChanged("InfoLabel");
            }
        }

        private string username;
        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
                OnPropertyChanged("Username");
            }
        }
        #endregion

        #region Commands
        /// <summary>
        /// Command used to log te user into the application
        /// </summary>
        private ICommand login;
        public ICommand Login
        {
            get
            {
                if (login == null)
                {
                    login = new RelayCommand(LoginExecute);
                }
                return login;
            }
        }

        /// <summary>
        /// Checks if its possible to login depending on the given username and password and saves the logged in user to a list
        /// </summary>
        /// <param name="obj"></param>
        private void LoginExecute(object obj)
        {
            string password = (obj as PasswordBox).Password;

            if (Username == "Mag2019" && password == "Mag2019")
            {
               // MainWindow mw = new MainWindow();
                InfoLabel = "Mag2019";
               // view.Close();
                //mw.Show();
            }
            else if (Username == "Man2019" && password == "Man2019")
            {
                InfoLabel = "Man2019";
            }
            else
            {
                InfoLabel = "Wrong Username or Password";
            }
        }
        #endregion
    }
}
