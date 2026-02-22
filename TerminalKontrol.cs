using UnityEngine;//Unity'nin temel kütüphanesi 
using TMPro;//TextMeshPro kütüphanesi, geliþmiþ metin iþleme için kullanýlýr
using UnityEngine.UI;//ScrollRect gibi UI bileþenleri için gerekli kütüphane

public class TerminalKontrol : MonoBehaviour
{
    [Header("UI Elemanlarý")]
    public TMP_InputField girisAlani;//Kullanýcýdan yazý girþi almak için kullanýlan TextMeshPro InputField
    public TMP_Text ekranLogu;//Ana terminal ekranýnda komutlarýn ve çýktýlarýnýn gösterileceði TextMeshPro Text bileþeni
    public ScrollRect scrollRect;//Terminal ekranýnýn kaydýrýlabilirliðini saðlayan ScrollRect bileþeni

    [Header("Sistem Ayarlarý")]
    public string adminSifre = "1234";
    private int denemeSayisi = 0;
    private const int MAX_DENEME = 3;

    [Header("Sistem Durumu")]
    private bool isimAlindi = false;
    private bool sifreBekleniyor = false;
    private bool sistemKilitli = false;
    private bool isAdmin = false;
    private string kullaniciAdi = "";

    void Start()//Oyun baþladýðýnda bir kez çalýþtýrýlacak olan baþlangýç fonksiyonu
    {
        if (scrollRect == null) scrollRect = GetComponentInChildren<ScrollRect>();//Otomatik olarak ScrollRect bileþenini bulmaya çalýþýr

        girisAlani.ActivateInputField();//Ýmleci giriþ alanýna odaklar
        ekranLogu.text = "GULSE-OS [Versiyon 1.0.2] YUKLENIYOR... \nLÜTFEN KULLANICI ADI GÝRÝNÝZ: ";
    }

    void Update()
    {
        if (sistemKilitli) return;

        if (Input.GetKeyDown(KeyCode.Return))//Enter tuþuna basýldýðýnda komut çözme fonksiyonunu çaðýrýr
        {
            KomutuCoz(girisAlani.text);
        }

        if (!girisAlani.isFocused && !sistemKilitli)//Giriþ alaný odakta deðilse ve sistem kilitli deðilse, tekrar odaklanýr
        {
            girisAlani.ActivateInputField();
        }
    }

    void KomutuCoz(string komut)//Ana komut çözme fonksiyonu, kullanýcýdan alýnan komutu iþler ve uygun yanýtlarý verir
    {
        if (string.IsNullOrEmpty(komut)) return;//Boþ komutlarý iþleme
        string temizKomut = komut.Trim().ToLower();//Komutu temizler ve küçük harfe çevirir

        if (temizKomut == "clear")
        {
            ekranLogu.text = "Sistem temizlendi.\n" + (isimAlindi ? (isAdmin ? "admin > " : kullaniciAdi + " > ") : "LÜTFEN KULLANICI ADI GÝRÝNÝZ: ");
            GirisSifirla();
            return;
        }

        
        if (!isimAlindi)
        {
            kullaniciAdi = komut.Trim();//Kullanýcý adýný alýr ve temizler
            isimAlindi = true;

            if (kullaniciAdi.ToLower() == "admin")//Eðer kullanýcý adý "admin" ise, þifre bekleme durumuna geçilir
            {
                sifreBekleniyor = true;
                ekranLogu.text += $"\n{kullaniciAdi}\n[UYARI] ADMÝN GÝRÝÞÝ TESPÝT EDÝLDÝ. ÞÝFRE GÝRÝNÝZ: ";
            }
            else
            {
                ekranLogu.text += $"\n{kullaniciAdi}\nHoþ geldin {kullaniciAdi}. Misafir modu aktif.\n{kullaniciAdi} > ";
            }
        }
        else if (sifreBekleniyor)
        {
            if (komut == adminSifre)
            {
                sifreBekleniyor = false;
                isAdmin = true;
                ekranLogu.text += $"\n****\n[BAÞARILI] Yetki Yükseltildi: ADMÝN MODU.\nadmin > ";
            }
            else
            {
                denemeSayisi++;
                if (denemeSayisi >= MAX_DENEME)
                {
                    sistemKilitli = true;
                    ekranLogu.text += "\n\n[KRÝTÝK HATA] ÇOK FAZLA HATALI DENEME! SÝSTEM KÝLÝTLENDÝ. Akbank Siber Güvenlik Birimi bilgilendirildi.";
                }
                else
                {
                    ekranLogu.text += $"\n[HATA] Geçersiz Þifre! (Kalan Hak: {MAX_DENEME - denemeSayisi}): ";
                }
            }
        }
        else
        {
            string aktifPrompt = isAdmin ? "admin > " : kullaniciAdi + " > ";

            if (temizKomut == "admin" || temizKomut == "sudo su")
            {
                sifreBekleniyor = true;
                ekranLogu.text += $"\n{temizKomut}\n[BÝLGÝ] Yetki yükseltme talebi. \nÞÝFRE GÝRÝNÝZ: ";
            }
            else if (temizKomut == "ls")
            {
                ekranLogu.text += $"\n{temizKomut}\nDizindeki Dosyalar:\n- sistem_notu.log\n- gorevler.txt\n- komutlar.txt\n{aktifPrompt}";
            }
            else if (temizKomut == "whoami")
            {
                string yetki = isAdmin ? "Sistem Yöneticisi (Root)" : "Standart Kullanýcý (Guest)";
                ekranLogu.text += $"\n{temizKomut}\nKULLANICI: {kullaniciAdi}\nYETKÝ: {yetki}\nOKUL: Ýzmir Bakýrçay Üniversitesi\nBÖLÜM: Bilgisayar Mühendisliði\n{aktifPrompt}";
            }
            else if (temizKomut == "help")
            {
                ekranLogu.text += $"\n{temizKomut}\nKOMUTLAR: ls, whoami, admin, clear, help, cat [dosya]\n{aktifPrompt}";
            }
            else if (temizKomut.StartsWith("cat ")) 
            {
                string dosyaAdi = temizKomut.Substring(4).Trim(); // cat kýsmýndan sonrasýný alýr ve temizler

                if (dosyaAdi == "sistem_notu.log") 
                {
                    ekranLogu.text += $"\n{temizKomut}\nDOSYA ÝÇERÝÐÝ: Admin geçici þifresi: 1234\n{aktifPrompt}";
                }
                else if (dosyaAdi == "gorevler.txt") // YENÝ EKLEME
                {
                    ekranLogu.text += $"\n{temizKomut}\nDOSYA ÝÇERÝÐÝ: \n1- Agdaki aciklari tara. \n2- Root yetkisi al.\n{aktifPrompt}";
                }
                else if (dosyaAdi == "komutlar.txt") // YENÝ EKLEME
                {
                    ekranLogu.text += $"\n{temizKomut}\nDOSYA ÝÇERÝÐÝ: ls, whoami, help, clear, cat, sudo su\n{aktifPrompt}";
                }
                else 
                {
                    ekranLogu.text += $"\n{temizKomut}\n[!] '{dosyaAdi}' isimli bir dosya bulunamadý.\n{aktifPrompt}";
                }
            }
            ekranLogu.text += $"\n{temizKomut}\n[!] '{temizKomut}' komutu bulunamadý.\n{aktifPrompt}";
            }
        }

        GirisSifirla();
    }

    void GirisSifirla()
    {
        girisAlani.text = "";
        girisAlani.ActivateInputField();

        Canvas.ForceUpdateCanvases();//Scrollu otomatik aþaðý kaydýrmak için
        if (scrollRect != null) scrollRect.verticalNormalizedPosition = 0f;
    }
}