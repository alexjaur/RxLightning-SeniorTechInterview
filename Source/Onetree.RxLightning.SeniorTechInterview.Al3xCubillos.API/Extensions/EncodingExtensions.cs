namespace Onetree.RxLightning.SeniorTechInterview.Al3xCubillos.API.Extensions
{
    public static class EncodingExtensions
    {
        public static string Encode(this string? plainText)
        {
            if (string.IsNullOrWhiteSpace(plainText))
            {
                return string.Empty;
            }

            var data = $"{Constants.EncodingSecretKey}{plainText}";
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(data);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string Decode(this string? encodedData)
        {
            string plainText = string.Empty;

            if (!string.IsNullOrWhiteSpace(encodedData))
            {
                try
                {
                    var base64EncodedBytes = Convert.FromBase64String(encodedData);
                    var data = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);

                    if (data.StartsWith(Constants.EncodingSecretKey))
                    {
                        plainText = data.Replace(Constants.EncodingSecretKey, string.Empty);
                    }
                }
                catch { }
            }

            return plainText;
        }
    }
}
