namespace E_Commerce.Shared
{
    public class JwtOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SecurityKey { get; set; }
        public double DurationDays { get; set; }
    }
}
