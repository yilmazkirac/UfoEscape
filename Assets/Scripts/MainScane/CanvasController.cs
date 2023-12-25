using UnityEngine;

public class CanvasController : MonoBehaviour
{

    public void LoadPanel(string islem)
    {
        switch (islem)
        {
            case "LoginPanel":
                UIManager.Instance.LoginScreen("Giris Yap","Kullanici Adi Veya Sifre Hatli", () => { 
                    NetworkManager.Instance.Login(); 
                });
                break;
            case "SingUp":
                UIManager.Instance.LoginScreen("Kayit Ol", "Gecerli Bir Kullanici Adi Veya Sifre Giriniz", () => {
                    NetworkManager.Instance.SignUp();
                });
                break;
            case "Back":
                UIManager.Instance.Back();
                break;
        }
    }
}
