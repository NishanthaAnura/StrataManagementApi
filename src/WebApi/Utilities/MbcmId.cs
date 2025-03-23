namespace WebApi.Utilities;
public static class MbcmId
{
    public static string NewId()
    {
        return Guid.NewGuid().ToString("D");
    }
}
