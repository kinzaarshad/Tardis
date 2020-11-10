#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("iiCaHFp4N0l03UJkv9eHC9OQYe84igkqOAUOASKOQI7/BQkJCQ0IC3Hal83xfhrlC7KdOTrICSh+NFqgjFYr4UfxgSEBpjk/5+AVNEB55syKCQcIOIoJAgqKCQkIiHK8yb+nGlAX7v9y4RUl0ZF/FmZqu9iiiEAsUVaUJ1GNIMW8UuNQ1a7IcirST+f7/Y+jkYHjmt9BHpYzE+amRt10X+OBS2WuSnB9BcryLsX9aq1ho9o5ZQAVoQZgI99y3yz0nZaLiK0MXwZ9J6abi9bCovpvhBcTqXcQmk+Rlxa8jn23FE3xesEEQNs+Zi5Mz55lJcErl/1ziFnvAor04CTlv/TzBiFDIUGuCXxoUyOGREGtMjb207+mJwzrFzmt9yZNBQoLCQgJ");
        private static int[] order = new int[] { 8,8,3,4,8,7,7,9,8,13,13,12,13,13,14 };
        private static int key = 8;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
