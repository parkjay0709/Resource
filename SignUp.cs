using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class SignUp : MonoBehaviour {
    
    public InputField usernameInput; // 아이디 입력 필드
    public InputField passwordInput; // 비밀번호 입력 필드
    public Button nextButton; // 넥스트 버튼
    public Text messageText; // 메시지 출력 필드

    private string filePath;

    void Start()
    {
        filePath = Path.Combine(Application.persistentDataPath, "userData.txt");
        
        nextButton.onClick.AddListener(OnSignUpButtonClicked);
    }

    public void OnSignUpButtonClicked()
    {
        string username = usernameInput.text.Trim(); // 아이디 입력값
        string password = passwordInput.text.Trim(); // 비밀번호 입력값
        
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            messageText.text = "아이디와 비밀번호를 모두 입력해주세요.";
            return;
        }
        
        if (IsUsernameTaken(username))
        {
            messageText.text = "이 아이디는 이미 사용 중입니다.";
            return;
        }
        
        SaveUserData(username, password);
        messageText.text = "회원가입 성공!";
        
        LoadNextScene();
    }

    private bool IsUsernameTaken(string username)
    {
        if (!File.Exists(filePath))
        {
            return false;
        }
        
        string[] lines = File.ReadAllLines(filePath);
        foreach (string line in lines)
        {
            if (line.Split(',')[0] == username)
            {
                return true;
            }
        }
        return false;
    }

    private void SaveUserData(string username, string password)
    {
        string userData = $"{username},{password}";
        File.AppendAllText(filePath, userData + "\n");
    }
    
    private void LoadNextScene()
    {
        SceneManager.LoadScene("NextScene"); // 회원가입 성공시 넘어갈 씬. 보통 로그인씬으로 이동.
    }
}
