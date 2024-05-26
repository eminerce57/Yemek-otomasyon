namespace CrudProject.Common
{
    public static class ReturnMessages
    {
        public const string ImageUploaded = "Resim başarıyla yüklendi";
        public const string ImageUploadFailed = "Resim yükleme başarısız oldu";

        public const string RecordAdded = "Kayıt başarıyla eklendi";
        public const string ReecordUpdated = "Kayıt başarıyla güncellendi";
        public const string RecordDeleted = "Kayıt Başarıyla Silindi";

        public const string DataFetched = "Veriler başarıyla getirildi";
        public const string DataFetchingFailed = "Veri alınırken bir hata oluştu";

        public const string UnAuthorized = "Erişim izniniz yok";
        public const string Exception = "Beklenmeyen bir hata oluştu.Lütfen tekrar deneyiniz";
        public const string NotFound = "Aradığınız istek bulunamadı";

        public const string WrongFormat = "Yanlış format";
        public const string RecordWithSamePhone = "Aynı telefon numarasına sahip kayıt zaten mevcut";
        public const string RecordWithSameEmail = "Aynı e-posta numarasına sahip kayıt zaten mevcut";
        public const string RecordWithSameTC = "Aynı TC numarasına sahip kayıt zaten mevcut";
        public const string CourseDateRequired = "Kurs Tarihleri ​​doldurulmalıdır";
        public const string TraierInfoRequired = "Eğitmen Bilgileri doldurulmalıdır";
        public const string CoursePostponed = "Ders ertelenir ve yeni ders saatleri hakkında öğrencilere bilgi verilir.";
        public const string StudentsAlreadyAdded = "Bu derse öğrenci zaten eklenmiş";

        public const string UserCredentialsInvalidMessage = "Kullanıcı Adı Veya Şifre Yanlış";
        public const string UserTokenInvalidMessage = "Failed to update user token";


    }
}