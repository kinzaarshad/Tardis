#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class AppleTangle
    {
        private static byte[] data = System.Convert.FromBase64String("KDeGxAEPLAEGAgIABQU3hrEdhrR+J2Z0dHJqYnQnZmRkYndzZmlkYjohYCeNNG3wCoXI2eykKP5UbVxjQnkYS2xXkUaOw3NlDBeERoA0jYYBNwgBBFIaFAYG+AMCNwQGBvg3Gowejtn+TGvyAKwlNwXvHzn/Vw7Ur9t5JTLNItLeCNFs06UjJBbwpqsRNxMBBFIDBBQKRnd3a2InVWhoczeFA7w3hQSkpwQFBgUFBgU3CgEOI+Xs1rB32AhC5iDN9mp/6uCyEBB3a2InVWhocydERjcZEAo3MTczNU7fcZg0E2KmcJPOKgUEBgcGpIUGMjU2Mzc0MV0QCjQyNzU3PjU2MzeFBgcBDi2BT4HwZGMCBjeG9TctAbnzdJzp1WMIzH5IM9+lOf5/+GzPITcjAQRSAwwUGkZ3d2tiJ0RidXN9N4UGcTcJAQRSGggGBvgDAwQFBt4xeMaAUt6gnr41Rfzf0naZeaZVkpl9C6NAjFzTETA0zMMISskTbtZ3a2InRGJ1c25hbmRmc25oaSdGcnNvaHVuc342ETcTAQRSAwQUCkZ3KydkYnVzbmFuZGZzYid3aGtuZH51ZmRzbmRiJ3RzZnNiamJpc3QpN84edfJaCdJ4WJz1IgS9UohKWgr2hxMs125Ak3EO+fNsiilHofBASng0MV03ZTYMNw4BBFIDARQFUlQ2FAEEUhoJAxEDEyzXbkCTcQ7582yKc25hbmRmc2InZX4nZml+J3dmdXMCBwSFBggHN4UGDQWFBgYH45auDoh0hmfBHFwOKJW1/0NP92c/mRLyVWJrbmZpZGInaGknc29udCdkYnUKAQ4tgU+B8AoGBgICBwSFBgYHWxiChIIcnjpAMPWunEeJK9O2lxXfJ0RGN4UGJTcKAQ4tgU+B8AoGBgZrYidOaWQpNiE3IwEEUgMMFBpGd2VrYid0c2ZpY2Z1YydzYnVqdCdmNxYBBFIDDRQNRnd3a2InTmlkKTayParzCAkHlQy2JhEpc9I7CtxlEQMBFAVSVDYUNxYBBFIDDRQNRnd3CJo69CxOLx3P+cmyvgneWRvRzDpwcClmd3drYilkaGooZnd3a2JkZmMyJBJMEl4atJPw8ZuZyFe9xl9XJ2hhJ3NvYidzb2JpJ2Z3d2tuZGYxnksqf7Dqi5zb9HCc9XHVcDdIxqykdpVAVFLGqChGtP/85HfK4aRLx2Q0cPA9ACtR7N0IJgndvXQeSLIPLAEGAgIABQYRGW9zc3d0PSgocLAcupRFIxUtwAgasUqbWWTPTIcQeEavn/7WzWGbI2wW16S84xwtxBhpYydkaGljbnNuaGl0J2hhJ3J0YrY3X+tdAzWLb7SIGtlidPhgWWK7D1k3hQYWAQRSGicDhQYPN4UGAzduYW5kZnNuaGknRnJzb2h1bnN+NgDrej6EjFQn1D/DtridSA1s+Cz7YIgPsyfwzKsrJ2h3sTgGN4uwRMgtgU+B8AoGBgICBzdlNgw3DgEEUl6gAg57EEdRFhlz1LCMJDxApNJoKUeh8EBKeA9ZNxgBBFIaJAMfNxEYltwZQFfsAupZfoMq7DGlUEtS6ydmaWMnZGJ1c25hbmRmc25oaSd3V62N0t3j+9cOADC3cnIm");
        private static int[] order = new int[] { 37,40,56,25,29,16,54,12,37,53,54,27,12,34,50,54,28,58,18,29,28,44,46,29,32,37,30,39,50,55,38,47,49,34,37,42,53,55,56,46,53,50,50,48,49,57,50,59,58,58,56,55,56,55,58,55,56,59,58,59,60 };
        private static int key = 7;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
