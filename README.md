# 💻 Terminal-Based Simulation

Unity kullanılarak geliştirilmiş, temel **C#** mantığı ve terminal komutlarını simüle eden interaktif bir projedir. Bu çalışma, siber güvenlik ve yazılım geliştirme alanındaki temel yetkinlikleri bir oyun mekaniği ile sunmayı amaçlar.

## 🛠️ Teknik Özellikler

* **Komut Sistemi:** `ls`, `whoami`, `cat`, `help` ve `clear` gibi temel komutları destekleyen dinamik yapı.
* **Yetki Yönetimi:** `sudo su` ve `admin` girişleri üzerinden yetki yükseltme (privilege escalation) simülasyonu.
* **Güvenlik Protokolü:** 3 hatalı şifre denemesinde sistemi kilitleyen ve Akbank Siber Güvenlik Birimi'ne (simüle) bildirim gönderen güvenlik mekanizması.
* **Dinamik UI:** **TextMeshPro** ile oluşturulmuş ve `ScrollRect` ile desteklenen akıcı terminal ekranı.

## 🎮 Nasıl Çalıştırılır?

1. Unity projesini editörde açın veya paylaşılan **WebGL** sürümüne gidin.
2. Kullanıcı adınızı girerek terminale erişim sağlayın.
3. `ls` komutu ile mevcut sistem dosyalarını görüntüleyin.
4. `cat sistem_notu.log` komutuyla sistemdeki ipuçlarını takip ederek admin yetkisine ulaşmaya çalışın.

## 👤 Geliştirici

**Gülse Döven**
* İzmir Bakırçay Üniversitesi - Bilgisayar Mühendisliği (2. Sınıf)

---
*Bu proje eğitim amaçlı geliştirilmiş bir simülasyondur.*
