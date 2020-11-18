#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("jT+8n42wu7SXO/U7SrC8vLy4vb4/vLK9jT+8t78/vLy9PccJfAoSr/aU9Bu8yd3mljPx9BiHg0NmChOSowk7yAKh+ETPdLH1bovTm/l6K9A/lS+p782C/MFo99EKYjK+ZiXUWk5IOhYkNFYvavSrI4amUxPzaMHq0LWgFLPVlmrHaplBKCM+PRi56rPloltKx1SgkGQkyqPT3w5tFz31mZB0niJIxj3sWrc/QVWRUApBRrOUVjT+0Bv/xciwf0ebcEjfGNQWb4zIkhMuPmN3F0/aMaKmHMKlL/okIsRvInhEy69QvgcojI99vJ3Lge8VOeOeVPJENJS0E4yKUlWggfXMU3nk4yGS5DiVcAnnVuVgG33Hn2f6UrleoowYQpP4sL++vL28");
        private static int[] order = new int[] { 0,1,11,12,8,9,13,7,10,11,13,12,13,13,14 };
        private static int key = 189;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
