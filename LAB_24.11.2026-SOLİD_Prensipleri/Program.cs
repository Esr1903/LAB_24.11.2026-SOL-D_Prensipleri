using System;
// Bildirim servislerinin ortak davranışını belirlemek için interface oluşturdum.
public interface IBildirimServisi
    {
        public void Gonder(string mesaj);
    }

    // Loglama işlemini ayrı bir interface içerisinde tanımladım.
    public interface ILoglanabilir
    {
        public void LogYaz(string mesaj);
    }

    // E-Mail Servisi
    public class EmailServisi : IBildirimServisi, ILoglanabilir
    {
        // Email gönderme ve loglama işlemlerini gerçekleştirdim.
        public void Gonder(string mesaj)
        {
            Console.WriteLine($"Email gönderildi : {mesaj}");
            LogYaz(mesaj);
        }

        public void LogYaz(string mesaj)
        {
            Console.WriteLine($"Email logu : {mesaj}");
        }
    }

    // SMS bildirim işlemini ayrı bir sınıfta gerçekleştirdim.
    public class SMSServisi : IBildirimServisi
    {
        public void Gonder(string mesaj)
        {
            Console.WriteLine($"SMS gönderildi : {mesaj}");
        }
    }

    // Mevcut yapıyı değiştirmeden yeni bir bildirim servisi ekledim.
    public class MobilBildirimServisi : IBildirimServisi
    {
        public void Gonder(string mesaj)
        {
            Console.WriteLine($"Mobil bildirim gönderildi : {mesaj}");
        }
    }

    // Sipariş oluşturma işlemini ayrı bir sınıfta tuttum.
    public class SiparisServisi
    {
        private IBildirimServisi bildirimServisi;

        // Bildirim servisini constructor aracılığıyla dışarıdan aldım.
        // Böylece Dependency Injection uyguladım.
        public SiparisServisi(IBildirimServisi bildirimServisi)
        {
            this.bildirimServisi = bildirimServisi;
        }

        // Sipariş oluşturulduktan sonra ilgili bildirim servisinin çalışmasını sağladım.
        public void SiparisOlustur(string urunAdi)
        {
            Console.WriteLine($"{urunAdi} : Siparişiniz Oluşturuldu");
            bildirimServisi.Gonder("Siparişiniz oluşturuldu");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // E-Mail servisini oluşturarak SiparisServisi sınıfına verdim.
            IBildirimServisi email = new EmailServisi();
            SiparisServisi siparis1 = new SiparisServisi(email);
            siparis1.SiparisOlustur("Laptop");
            Console.WriteLine();

            // SMS ile bildirim gönderme işlemini test ettim.
            IBildirimServisi sms = new SMSServisi();
            SiparisServisi siparis2 = new SiparisServisi(sms);
            siparis2.SiparisOlustur("Telefon");
            Console.WriteLine();

            // Mobil bildirim servisinin de aynı yapı ile çalışmasını sağladım.
            IBildirimServisi mobil = new MobilBildirimServisi();
            SiparisServisi siparis3 = new SiparisServisi(mobil);
            siparis3.SiparisOlustur("Tablet");
            Console.WriteLine();
        }
    }

