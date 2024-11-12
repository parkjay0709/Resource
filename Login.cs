using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    public InputField usernameInput; // 아이디 입력 필드
    public InputField passwordInput; // 비밀번호 입력 필드
    public Button loginButton; // 로그인 버튼
    public Text messageText; // 메시지 출력 필드

    private string filePath;

    void Start()
    {
        filePath = Path.Combine(Application.persistentDataPath, "userData.txt");
        
        loginButton.onClick.AddListener(OnLoginButtonClicked);
    }

    public void OnLoginButtonClicked()
    {
        string username = usernameInput.text.Trim(); // 아이디 입력값
        string password = passwordInput.text.Trim(); // 비밀번호 입력값
        
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            messageText.text = "아이디와 비밀번호를 모두 입력해주세요.";
            return;
        }
        
        if (IsLoginSuccessful(username, password))
        {
            messageText.text = "로그인 성공!";

            LoadNextScene();
        }
        else
        {
            messageText.text = "아이디 또는 비밀번호가 틀렸습니다.";
        }
    }

    private bool IsLoginSuccessful(string username, string password)
    {
        if (!File.Exists(filePath))
        {
            return false;
        }
        
        string[] lines = File.ReadAllLines(filePath);
        foreach (string line in lines)
        {
            string[] userData = line.Split(',');
            if (userData[0] == username && userData[1] == password)
            {
                return true;
            }
        }
        return false;
    }
    
    private void LoadNextScene()
    {
        SceneManager.LoadScene("NextScene"); // 로그인 성공시 넘어갈 씬.
    }
}
