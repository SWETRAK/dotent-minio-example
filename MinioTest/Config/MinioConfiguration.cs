namespace MinioTest.Config;

public class MinioConfiguration
{
    public string Host { get; set; }
    public int Port { get; set; }

    public string ConnectionString => $"{Host}:{Port}";

    public string AccessKey { get; set; }
    public string SecureKey { get; set; }

    public string BucketName { get; set; }
}