using System.Collections;
using System.Security.Cryptography;
using System.Text;

namespace BloomFilter.Services {
    public class FB {
        public int n;
        public float p;
        public int m;
        public int k;
        private BitArray bitarr;

        public FB(int n, float p) {
            this.n = n;
            this.p = p;
            this.m = (int)Math.Ceiling((n* Math.Log(p)) / Math.Log(1 / Math.Pow(2, Math.Log(2))));
            this.k = Convert.ToInt16(Math.Round((double)m / n) * Math.Log(2));
            this.bitarr = new BitArray(this.m, false);

        }


        public void Add(string element) {
            int[] positions = this.GetPositions(element);
            foreach (int position in positions) {
                this.bitarr[position] = true;
            }
        }

        public bool Contains(string element) {
            int[] positions = this.GetPositions(element);
            foreach (int position in positions) {
                if (!this.bitarr[position]) return false;
            }
            return true;
        }
        private int[] GetPositions(string s) {
            int[] positions = new int[this.k];

            for (int i = 0; i < this.k; i++) {
                positions[i] = GetSHA256Integer(s + i) % this.m;
            }
            return positions;
        }
        private int GetSHA256Integer(string element) {
            using (SHA256 sha256 = SHA256.Create()) {
                byte[] digest = sha256.ComputeHash(Encoding.UTF8.GetBytes(element));
                int integer = Math.Abs(BitConverter.ToInt32(digest, 0));
                return integer;
            }
        }
    }
}