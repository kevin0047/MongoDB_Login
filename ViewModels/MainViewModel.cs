using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace MongoDB_Login.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _username;
        private string _password;
        private string _message;

        private readonly IMongoCollection<BsonDocument> _usersCollection;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }

        public MainViewModel()
        {
            // MongoDB 연결 설정
            var client = new MongoClient("mongodb://localhost:27017"); // MongoDB 서버 주소
            var database = client.GetDatabase("UserDatabase"); // 사용할 데이터베이스
            _usersCollection = database.GetCollection<BsonDocument>("Users"); // 사용할 컬렉션

            LoginCommand = new RelayCommand(Login);
            RegisterCommand = new RelayCommand(Register);
        }

        private async void Login()
        {
            var filter = Builders<BsonDocument>.Filter.Eq("Username", Username) & Builders<BsonDocument>.Filter.Eq("Password", Password);
            var user = await _usersCollection.Find(filter).FirstOrDefaultAsync();

            if (user != null)
            {
                Message = "로그인 성공!";
            }
            else
            {
                Message = "로그인 실패. 사용자 이름 또는 비밀번호가 잘못되었습니다.";
            }
        }

        private async void Register()
        {
            var filter = Builders<BsonDocument>.Filter.Eq("Username", Username);
            var existingUser = await _usersCollection.Find(filter).FirstOrDefaultAsync();

            if (existingUser != null)
            {
                Message = "이미 존재하는 사용자 이름입니다.";
            }
            else
            {
                var newUser = new BsonDocument
                {
                    { "Username", Username },
                    { "Password", Password }
                };

                await _usersCollection.InsertOneAsync(newUser);
                Message = $"'{Username}' 사용자가 등록되었습니다.";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
