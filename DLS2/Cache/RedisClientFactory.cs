namespace Cache;

public class RedisClientFactory
{
    public static RedisClient CreateRedisClient()
    {
        return new RedisClient("redis","");
    }
}