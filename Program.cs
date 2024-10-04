using System.Collections;
using BloomFilter.Services;

class Program {
    static void Main(string[] args) {

        // Parameters
        int n = 100000;
        float p = 0.01f;

        FB fb = new FB(n, p);
        Console.WriteLine($"n = {fb.n}, p = {fb.p}, m = {fb.m}, k = {fb.k}");

        // Testes
        int fp = 0;
        int[] contained = new int[n];

        for (int i = 0; i < n; i++) {
            contained[i] = i;
            fb.Add(i.ToString());
        }

        foreach (int element in contained) {
            if (!fb.Contains(element.ToString())) {
                Console.WriteLine($"Element {element} inserted was not detected");
            }
            if (fb.Contains((-1*element).ToString())) {
                fp++;
            }
        }

        decimal fp_rate = fp / (decimal)contained.Length;
        Console.WriteLine($"False positives rate: {(float)Math.Round(fp_rate*100, 1)}%");
    }
}