using prs_rs.Net.Sys;
using System;

namespace HeroesONE_R.Utilities
{
    internal static class Prs
    {
        internal static unsafe Span<byte> DecompressData(byte[] compressedData)
        {
            // Calculate the decompressed size to allocate enough memory
            fixed (byte* srcPtr = compressedData)
            {
                // or get from file header etc.
                nuint decompressedSize = NativeMethods.prs_calculate_decompressed_size(srcPtr);
                byte[] dest = GC.AllocateUninitializedArray<byte>((int)decompressedSize);
                fixed (byte* destPtr = &dest[0])
                {
                    nuint actualDecompressedSize = NativeMethods.prs_decompress(srcPtr, destPtr);
                    return dest.AsSpan(0..(int)decompressedSize);
                }
            }
        }

        internal static unsafe Span<byte> CompressData(byte[] sourceData)
        {
            fixed (byte* srcPtr = sourceData)
            {
                // Get the maximum possible size of the compressed data
                nuint maxCompressedSize = NativeMethods.prs_calculate_max_compressed_size((nuint)sourceData.Length);
                byte[] dest = GC.AllocateUninitializedArray<byte>((int)maxCompressedSize);
                fixed (byte* destPtr = &dest[0])
                {
                    nuint compressedSize = NativeMethods.prs_compress(srcPtr, destPtr, (nuint)sourceData.Length);
                    return dest.AsSpan(0..(int)compressedSize);
                }
            }
        }

        internal static unsafe nuint GetDecompressedSize(byte[] compressedData)
        {
            // Calculate the decompressed size to allocate enough memory
            fixed (byte* srcPtr = compressedData)
            {
                // or get from file header etc.
                return NativeMethods.prs_calculate_decompressed_size(srcPtr);
            }
        }
    }
}
