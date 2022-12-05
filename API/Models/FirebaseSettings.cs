using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Models
{
    public class FirebaseSettings
    {
      [JsonPropertyName("Type")]
      public string Type => "service_account";

      [JsonPropertyName("project_id")]
      public string ProjectId => "adv-dev-dotnet";

      [JsonPropertyName("private_key_id")]
      public string PrivateKetId => "e52c99be22ce62e16324ae965e99ac10ba0e5559";

      [JsonPropertyName("private_key")]
      public string  PrivateKey => "-----BEGIN PRIVATE KEY-----\nMIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQDEENUqiHcoCdz9\nf/4gGRgLCZb4Dvh2hRNigXM4ma2M5om8Z93+WYMSbck7gkCvQi0UaXS9oNH7vTZx\n/OucBuhuSeV8LQ41XbYnVPUB4GCFAPFBQLQJ3G/sAZFhcADDDrgddTcJrxBRs0s1\n/+d26vBQGExPxtBytZAW87AK31Dq0uI67txX5c0WVYCuwzqk64frivR2CFaeY5Pf\nyrmoQD9inWuMJz/wB0oB8E6KzLT/IpzuaawAhSmzXjYxbOaQQoLc42PN3B0njptx\nINtomExwkQ9Zr4J0iWbjOYoNvt3g1YRZIrvZc8oF7ZFbL3ZMlsIlKKFJ5KZyPX0O\nVepz1UkRAgMBAAECggEAJzm04/sEV8rNXxwVw+cxU7f576vTEqciDzGV0yuPuKJA\nySRAmgvfeLblLRYsicOkEDLe67c6e3JJ67kti/wn0TTQiCzdbFjQwgKxt8vjk4hy\nO9tNibGD5MQViVhmlp+hvyb9uV7AVZAYyndln/l11Zhe4BEHEAP6DVV4kVxHLItl\nY/4s+DwkMmywum+4egJeCtSCcai9Zq5NeHWthoPEHGeqJy2bGYK739kJgvdjVhg4\n/0AtiJlIMPjlwxj4cZcyh0HJqbVPMBittmUpJn6TO+f7VLCnlsLGTlcDvg6lSU5L\nt4M6MwIBqX6g7eGXoxL5XHICr6VN9NKbVKE2eFyQhwKBgQDv2uS3Rd1E1R57pwEr\nWzX8DSk34iV5oiNgmMjshziWwo0OSrSFDpXkMUlATQqDHvbUiSqvCPK7j5+N9Fgv\nYQKOPJZ6obHokgQfgCmLDK7Azm8te2LzXlbWjRcUBi/KVSQlYlmO4M3AhVmNIQxp\nYDBBxbGJX5oxBeKcsz2iXjWQOwKBgQDRQ2A4h1if3mbhvxZrI1iMa9kYrs6u4QT3\n/qyiWyjRtkIfSXBDst6kNwDm5guQM1YA5CJ08RzlD3xqIVJiTJLJkyTvbijNOLFz\nH9aC4uSU6HJl69l+WwZ3NKwrDX8jKp+c14wUCxD2LDImU7lLNtYx0Pcz/6a2Kkgc\nKmOnwTejIwKBgBIM4xF3zIMVlsmvc8MThiiFxQhTmIZi0U6/sL88HeIamgrtTFCH\nHHijzONt6NCdT+4GtJOflMTQti00qj5Giq6xmJF6Aa2C75R/TKEOP0h7QJqnona2\nLsrOq2HyZT5Q0fuOPzcla9aFMowQZKK5fmg0vHueGjh+POOWCZlihZ1pAoGAMCug\nRKLzRam+aQzBrlvukDRrPwFOVnc525RmTOFLHiskQWt4Sj0sfwSiUoR1+PW4QHBD\ndkPicOAFtbCB9FrIF1qlz++9uK/qOSQFDxAHqjdvzgJiiHGPpXUchLSQpZm0MDh1\n6E870k8OJEB1kNjIqaL+pJue1qQZAVLIqd8SXZ0CgYEA7WeYo3VaJEami75rNCQA\nEq8dYWw+LfaI2iK1Vl0dBNjl5duUbsJdlYNMPbsdR5LC3r/pTEcVRJkBXe54f1/c\nZQenbu4q7DshmBhx+r+XxhejZgtmWqnaNagLKNvib5vI4NwaVIUWWS3oTM6LP/NW\na2TW0o7IkA9LEsfmVwjZr+A=\n-----END PRIVATE KEY-----\n";

      [JsonPropertyName("client_email")]
      public string ClientEmail => "firebase-adminsdk-cbsuh@adv-dev-dotnet.iam.gserviceaccount.com";

      [JsonPropertyName("client_id")]
      public string ClientId => "101795262472148367707";

      [JsonPropertyName("auth_uri")]
      public string AuthUri => "https://accounts.google.com/o/oauth2/auth";

      [JsonPropertyName("token_uri")]
      public string TokenUri => "https://oauth2.googleapis.com/token";

      [JsonPropertyName("auth_provider_x509_cert_url")]
      public string AuthProviderCertUrl => "https://www.googleapis.com/oauth2/v1/certs";

      [JsonPropertyName("client_x509_cert_url")]
      public string ClientCertUrl => "https://www.googleapis.com/robot/v1/metadata/x509/firebase-adminsdk-cbsuh%40adv-dev-dotnet.iam.gserviceaccount.com";



    }
}